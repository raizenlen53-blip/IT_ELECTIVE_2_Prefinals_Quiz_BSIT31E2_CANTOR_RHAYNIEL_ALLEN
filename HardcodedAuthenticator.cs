using System.Security.Cryptography;
using System.Text;
using System.Collections.Concurrent;

namespace Portfolio.Services;

/// <summary>
/// The hardcoded login required by the assignment brief.
///
/// The credentials themselves are never stored as plain text in the compiled app:
/// at startup the configured password is turned into a PBKDF2 hash with a random
/// salt, and every sign-in attempt is hashed and compared in fixed time. That does
/// not make a shared credential secret - the README publishes it on purpose - but it
/// does mean the password is not sitting in memory or in a stack trace, and the
/// comparison cannot be timed to guess characters.
///
/// Repeated failures from the same client are throttled to blunt brute-force attempts.
/// </summary>
public class HardcodedAuthenticator : IPortfolioAuthenticator
{
    private const int Iterations = 210_000;
    private const int MaxAttempts = 5;
    private static readonly TimeSpan LockoutWindow = TimeSpan.FromMinutes(5);

    private readonly string _username;
    private readonly string _displayName;
    private readonly byte[] _salt;
    private readonly byte[] _hash;

    private readonly ConcurrentDictionary<string, (int Count, DateTimeOffset First)> _attempts = new();

    public HardcodedAuthenticator(IConfiguration configuration)
    {
        var section = configuration.GetSection("PortfolioLogin");
        _username = section["Username"] ?? "admin";
        _displayName = section["DisplayName"] ?? _username;
        var password = section["Password"] ?? "Portfolio@2026";

        _salt = RandomNumberGenerator.GetBytes(16);
        _hash = Hash(password, _salt);
    }

    public LoginResult Validate(string username, string password, string clientKey)
    {
        var now = DateTimeOffset.UtcNow;

        if (_attempts.TryGetValue(clientKey, out var state))
        {
            if (now - state.First > LockoutWindow)
            {
                _attempts.TryRemove(clientKey, out _);
            }
            else if (state.Count >= MaxAttempts)
            {
                var wait = LockoutWindow - (now - state.First);
                return new LoginResult(false,
                    Error: $"Too many attempts. Try again in {Math.Ceiling(wait.TotalMinutes)} minute(s).",
                    RetryAfter: wait);
            }
        }

        var userMatches = CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(username.Trim().ToLowerInvariant()),
            Encoding.UTF8.GetBytes(_username.ToLowerInvariant()));

        var passwordMatches = CryptographicOperations.FixedTimeEquals(Hash(password, _salt), _hash);

        if (userMatches && passwordMatches)
        {
            _attempts.TryRemove(clientKey, out _);
            return new LoginResult(true, _displayName);
        }

        _attempts.AddOrUpdate(clientKey,
            _ => (1, now),
            (_, existing) => (existing.Count + 1, existing.First));

        // One message for both cases so the form never reveals which field was wrong.
        return new LoginResult(false, Error: "That username and password do not match.");
    }

    private static byte[] Hash(string password, byte[] salt) =>
        Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            32);
}
