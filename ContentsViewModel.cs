namespace Portfolio.Models;

/// <summary>Table of contents: projects grouped by term, plus the running comment counts.</summary>
public class ContentsViewModel
{
    public IReadOnlyList<TermGroup> Terms { get; init; } = Array.Empty<TermGroup>();
    public IReadOnlyDictionary<string, int> CommentCounts { get; init; } = new Dictionary<string, int>();
    public string? Query { get; init; }
    public int TotalProjects { get; init; }
}

public class TermGroup
{
    public string Term { get; init; } = string.Empty;
    public string Blurb { get; init; } = string.Empty;
    public IReadOnlyList<Project> Projects { get; init; } = Array.Empty<Project>();
}
