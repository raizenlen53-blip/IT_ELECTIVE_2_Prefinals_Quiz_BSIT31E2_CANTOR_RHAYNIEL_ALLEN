using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers;

/// <summary>
/// Everything below the login lives here. The controller-level [Authorize]
/// covers every action, so a new action cannot be left unprotected by accident.
/// </summary>
[Authorize]
public class ProjectsController : Controller
{
    private readonly IProjectRepository _projects;
    private readonly ICommentRepository _comments;

    public ProjectsController(IProjectRepository projects, ICommentRepository comments)
    {
        _projects = projects;
        _comments = comments;
    }

    /// <summary>Table of contents.</summary>
    [HttpGet]
    public IActionResult Index(string? q = null)
    {
        var model = new ContentsViewModel
        {
            Terms = _projects.GetGrouped(q),
            CommentCounts = _comments.GetCounts(),
            Query = q,
            TotalProjects = _projects.GetAll().Count
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Details(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return RedirectToAction(nameof(Index));

        var project = _projects.GetBySlug(id);
        if (project is null) return NotFound();

        var (previous, next) = _projects.GetNeighbours(project.Slug);

        return View(new ProjectDetailViewModel
        {
            Project = project,
            Comments = _comments.GetForProject(project.Slug),
            NewComment = new Comment { ProjectSlug = project.Slug },
            Previous = previous,
            Next = next
        });
    }

    [HttpPost]
    public IActionResult Comment(string id, [Bind(Prefix = "NewComment")] Comment newComment)
    {
        var project = _projects.GetBySlug(id);
        if (project is null) return NotFound();

        // Never trust the slug that came back with the form - the route value wins.
        ModelState.Remove("NewComment.ProjectSlug");

        if (!ModelState.IsValid)
        {
            return View("Details", new ProjectDetailViewModel
            {
                Project = project,
                Comments = _comments.GetForProject(project.Slug),
                NewComment = newComment,
                Previous = _projects.GetNeighbours(project.Slug).Previous,
                Next = _projects.GetNeighbours(project.Slug).Next
            });
        }

        _comments.Add(new Comment
        {
            ProjectSlug = project.Slug,
            Author = Clean(newComment.Author, 40),
            Body = Clean(newComment.Body, 600),
            PostedBy = User.FindFirstValue(ClaimTypes.Name) ?? "unknown"
        });

        TempData["CommentPosted"] = "Comment posted.";
        return RedirectToAction(nameof(Details), new { id = project.Slug });
    }

    [HttpPost]
    public IActionResult DeleteComment(string id, Guid commentId)
    {
        var project = _projects.GetBySlug(id);
        if (project is null) return NotFound();

        var user = User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        if (_comments.Delete(project.Slug, commentId, user))
        {
            TempData["CommentPosted"] = "Comment deleted.";
        }

        return RedirectToAction(nameof(Details), new { id = project.Slug });
    }

    /// <summary>
    /// Trims, caps the length and strips control characters. Razor already HTML-encodes
    /// on output, so this is about keeping the stored data tidy rather than escaping it.
    /// </summary>
    private static string Clean(string? input, int max)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var cleaned = new string(input.Where(c => !char.IsControl(c) || c == '\n').ToArray()).Trim();
        return cleaned.Length <= max ? cleaned : cleaned[..max];
    }
}
