using System.Collections.Concurrent;
using Portfolio.Models;

namespace Portfolio.Services;

/// <summary>
/// Comments are held in memory for the lifetime of the process, which keeps the
/// app database-free. Registered as a singleton so every request sees the same list.
/// </summary>
public class InMemoryCommentRepository : ICommentRepository
{
    private readonly ConcurrentDictionary<string, List<Comment>> _comments = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _writeLock = new();

    public InMemoryCommentRepository()
    {
        Seed("prefinals-project", "Prof. Reyes", "Good split of work across the three of you. The commit history tells the story clearly.");
        Seed("prelim-h1", "Jeremiah R.", "Clean take on this one. Mine ended up much longer for the same output.");
        Seed("sso", "Andrei Y.", "Worth reading if you have only ever done hardcoded logins before.");
    }

    private void Seed(string slug, string author, string body) =>
        Add(new Comment
        {
            ProjectSlug = slug,
            Author = author,
            Body = body,
            PostedAt = DateTimeOffset.UtcNow.AddDays(-Random.Shared.Next(2, 20)),
            PostedBy = "seed"
        });

    public IReadOnlyList<Comment> GetForProject(string slug)
    {
        if (!_comments.TryGetValue(slug, out var list)) return Array.Empty<Comment>();
        lock (_writeLock)
        {
            return list.OrderByDescending(c => c.PostedAt).ToList();
        }
    }

    public IReadOnlyDictionary<string, int> GetCounts()
    {
        lock (_writeLock)
        {
            return _comments.ToDictionary(kv => kv.Key, kv => kv.Value.Count, StringComparer.OrdinalIgnoreCase);
        }
    }

    public void Add(Comment comment)
    {
        lock (_writeLock)
        {
            var list = _comments.GetOrAdd(comment.ProjectSlug, _ => new List<Comment>());
            list.Add(comment);
        }
    }

    public bool Delete(string slug, Guid id, string requestedBy)
    {
        lock (_writeLock)
        {
            if (!_comments.TryGetValue(slug, out var list)) return false;
            var comment = list.FirstOrDefault(c => c.Id == id);
            if (comment is null) return false;
            list.Remove(comment);
            return true;
        }
    }
}
