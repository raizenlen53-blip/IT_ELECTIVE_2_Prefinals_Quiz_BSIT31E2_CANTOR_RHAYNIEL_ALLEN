using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models;

/// <summary>A visitor comment attached to a single project.</summary>
public class Comment
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string ProjectSlug { get; init; } = string.Empty;

    [Required(ErrorMessage = "Add a name so people know who wrote this.")]
    [StringLength(40, MinimumLength = 2, ErrorMessage = "Names run from 2 to 40 characters.")]
    [Display(Name = "Your name")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Write a comment before posting.")]
    [StringLength(600, MinimumLength = 2, ErrorMessage = "Comments run from 2 to 600 characters.")]
    [Display(Name = "Comment")]
    public string Body { get; set; } = string.Empty;

    public DateTimeOffset PostedAt { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>Username of the signed-in account that posted the comment.</summary>
    public string PostedBy { get; init; } = string.Empty;
}
