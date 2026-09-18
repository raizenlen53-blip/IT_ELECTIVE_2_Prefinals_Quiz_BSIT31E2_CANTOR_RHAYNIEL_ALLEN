namespace Portfolio.Models;

/// <summary>
/// One GitHub project shown in the portfolio. The data is seeded in
/// <see cref="Services.ProjectRepository"/> so the app runs with no database.
/// </summary>
public class Project
{
    /// <summary>URL-friendly id used in /Projects/Details/{slug}.</summary>
    public string Slug { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    /// <summary>Short line shown in the table of contents.</summary>
    public string Summary { get; init; } = string.Empty;

    /// <summary>Longer write-up shown on the detail page.</summary>
    public string Description { get; init; } = string.Empty;

    public string RepositoryUrl { get; init; } = string.Empty;

    /// <summary>GitHub account that hosts the repository.</summary>
    public string RepositoryOwner { get; init; } = string.Empty;

    /// <summary>Prelim, Midterm or Prefinals - used to group the table of contents.</summary>
    public string Term { get; init; } = string.Empty;

    /// <summary>Solo, Pair or Group.</summary>
    public string Role { get; init; } = "Solo";

    public string Course { get; init; } = string.Empty;

    /// <summary>Path of the thumbnail under wwwroot.</summary>
    public string Thumbnail { get; init; } = string.Empty;

    public IReadOnlyList<string> Tech { get; init; } = Array.Empty<string>();

    public IReadOnlyList<string> Highlights { get; init; } = Array.Empty<string>();
}
