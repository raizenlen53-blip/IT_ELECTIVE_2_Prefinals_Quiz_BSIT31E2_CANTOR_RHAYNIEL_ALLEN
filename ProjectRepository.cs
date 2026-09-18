using Portfolio.Models;

namespace Portfolio.Services;

/// <summary>
/// In-memory catalogue of the portfolio projects. Swap this class for an
/// EF Core repository later and nothing else in the app has to change.
/// </summary>
public class ProjectRepository : IProjectRepository
{
    private static readonly (string Term, string Blurb)[] TermOrder =
    {
        ("Prelim",    "First term. Getting comfortable with controllers, views and model binding."),
        ("Midterm",   "Quizzes and hands-on exams built under time pressure."),
        ("Prefinals", "Larger builds that pull the whole term together.")
    };

    private readonly List<Project> _projects = new()
    {
        new Project
        {
            Slug = "prelim-a1",
            Title = "Prelim Activity 1",
            Term = "Prelim",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "First MVC build: routing, a controller and a strongly typed view.",
            Description = "The starting point of the course. This activity sets up an ASP.NET MVC project from scratch and walks through the request pipeline: a route reaches a controller action, the action passes a model to a view, and the view renders it with Razor. It is deliberately small so the moving parts stay visible.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/BSIT31E3_PRELIM_A1_ESGUERRA_MA.-COROLLA",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/prelim-a1.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Razor" },
            Highlights = new[]
            {
                "Controller and action set up by hand rather than scaffolded",
                "Strongly typed view with a model passed from the controller",
                "Default route mapped in the startup configuration"
            }
        },
        new Project
        {
            Slug = "prelim-a2",
            Title = "Prelim Activity 2",
            Term = "Prelim",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Forms, model binding and server-side validation.",
            Description = "Builds on the first activity by adding input. A form posts back to the controller, model binding fills the view model, and data annotations reject anything invalid before it reaches the action body. Validation messages are rendered next to the fields that caused them.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/BSIT31E3_PRELIM_A2_ESGUERRA_MA.COROLLA",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/prelim-a2.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Data annotations" },
            Highlights = new[]
            {
                "GET and POST actions for the same form",
                "Data annotation validation with inline error messages",
                "ModelState checked before any work is done"
            }
        },
        new Project
        {
            Slug = "prelim-a3",
            Title = "Prelim Activity 3",
            Term = "Prelim",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Layouts, partial views and shared page structure.",
            Description = "A pass over the view layer. Shared markup moves into a layout, repeated blocks become partial views, and the pages stop duplicating each other. The result is the same output with much less markup to maintain.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/BSIT31E3_PRELIM_A3_ESGUERRA_MACOROLLA",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/prelim-a3.svg",
            Tech = new[] { "ASP.NET MVC", "Razor", "Bootstrap" },
            Highlights = new[]
            {
                "Shared layout with named sections",
                "Partial views for repeated blocks",
                "Consistent navigation across every page"
            }
        },
        new Project
        {
            Slug = "prelim-h1",
            Title = "Prelim Hands-On 1",
            Term = "Prelim",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Timed hands-on exam covering the full prelim MVC toolkit.",
            Description = "The graded hands-on for the prelim term. Everything from the three activities had to come together in one sitting: routing, a model, form handling with validation, and a layout that holds the pages together.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/BSIT31E3_PRELIM_H1_ESGUERRA_MA.COROLLA",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/prelim-h1.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Razor" },
            Highlights = new[]
            {
                "Built inside the exam time limit",
                "CRUD-style actions over an in-memory list",
                "Validation kept on the server side"
            }
        },
        new Project
        {
            Slug = "prelim-h1-pair",
            Title = "Prelim Hands-On 1 (pair repository)",
            Term = "Prelim",
            Role = "Pair",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "The same hands-on worked through with a classmate on his repository.",
            Description = "A pair version of the prelim hands-on hosted on Jeremiah Romulo's account. Working from someone else's repository meant reading code written by another person before adding to it, which is closer to how real projects run than starting from an empty folder.",
            RepositoryUrl = "https://github.com/Softjeeem/BSIT31E3_PRELIM_H1_ROMULO_JEREMIAH",
            RepositoryOwner = "Softjeeem",
            Thumbnail = "/img/prelim-h1-pair.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Git" },
            Highlights = new[]
            {
                "Shared repository with a classmate",
                "Changes pushed to a branch instead of straight to main",
                "Code read before it was edited"
            }
        },
        new Project
        {
            Slug = "it-elective-main",
            Title = "IT Elective 2 - Working Repository",
            Term = "Prelim",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Running repository for in-class exercises and lecture follow-alongs.",
            Description = "The catch-all repository for class work: small exercises, things tried during lectures, and experiments that did not need a repository of their own. It is the most honest record of the term because it shows the attempts that did not make it into the graded builds.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/IT_ELECTIVE_BSIT_31E3_ESGUERRA_MA.COROLLA",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/it-elective-main.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Git" },
            Highlights = new[]
            {
                "Several exercises kept side by side",
                "Commit history that follows the lecture schedule",
                "Scratch space for ideas before they became projects"
            }
        },
        new Project
        {
            Slug = "webapplication1",
            Title = "WebApplication1",
            Term = "Prelim",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Sandbox project for testing scaffolding and NuGet packages.",
            Description = "The default project name, kept on purpose. This is where new packages, scaffolding options and template settings get tried before they are used in something graded. Small, disposable, and useful exactly because nothing depends on it.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/WebApplication1",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/webapplication1.svg",
            Tech = new[] { "ASP.NET", "C#", "NuGet" },
            Highlights = new[]
            {
                "Scaffolded controllers and views compared against hand-written ones",
                "Package setup tested here first",
                "Kept deliberately throwaway"
            }
        },
        new Project
        {
            Slug = "midterm-q1",
            Title = "Midterm Quiz 1",
            Term = "Midterm",
            Role = "Group",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Team quiz build on the section repository.",
            Description = "A quiz answered as a team on the section's shared account. The interesting part was coordination: agreeing who owned which file so the work merged without stepping on each other, and keeping commits small enough to review.",
            RepositoryUrl = "https://github.com/Lycevm-3Alabang/IT_ELECTIVE_2_MIDTERM_Q1",
            RepositoryOwner = "Lycevm-3Alabang",
            Thumbnail = "/img/midterm-q1.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Git" },
            Highlights = new[]
            {
                "Work split across the team by file",
                "Merge conflicts resolved rather than avoided",
                "Shared section repository"
            }
        },
        new Project
        {
            Slug = "midterm-q3",
            Title = "Midterm Quiz 3",
            Term = "Midterm",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Individual quiz build on controller logic and view rendering.",
            Description = "A solo quiz covering how a controller decides what to hand a view, and how the view renders a collection without putting logic where it does not belong. Short, focused, and graded on whether the separation holds.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/IT_ELECTIVE_2_MIDTERM_Q3",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/midterm-q3.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Razor" },
            Highlights = new[]
            {
                "Collection rendered from a view model",
                "Logic kept out of the view",
                "Finished inside the quiz window"
            }
        },
        new Project
        {
            Slug = "midterm-q3-team",
            Title = "Midterm Quiz 3 (team repository)",
            Term = "Midterm",
            Role = "Group",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "The team's version of Quiz 3, compared against the solo build.",
            Description = "The same quiz submitted through the section repository. Putting it next to the solo version is useful: two groups solved the same problem differently, and the differences in structure are easier to judge when the requirements are identical.",
            RepositoryUrl = "https://github.com/Lycevm-3Alabang/IT_ELECTIVE_2_MIDTERM_Q3",
            RepositoryOwner = "Lycevm-3Alabang",
            Thumbnail = "/img/midterm-q3-team.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Git" },
            Highlights = new[]
            {
                "Same brief solved as a team",
                "Structure compared against the solo attempt",
                "Reviewed before pushing"
            }
        },
        new Project
        {
            Slug = "midterm-h1-h2-h3",
            Title = "Midterm Hands-On 1 to 3",
            Term = "Midterm",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Three hands-on exercises kept in one repository.",
            Description = "The three midterm hands-on tasks live together so the progression is easy to follow. Each one adds something the previous did not have, and keeping them in one place makes it obvious which parts were reused and which were rewritten.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/IT_ELECTIVE_2_MIDTERM_H1_H2_H3",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/midterm-h1-h2-h3.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Razor" },
            Highlights = new[]
            {
                "Three exercises in one solution",
                "Shared helpers pulled out instead of copied",
                "Progression visible in the commit history"
            }
        },
        new Project
        {
            Slug = "midterm-exam-9",
            Title = "Midterm Exam - Item 9",
            Term = "Midterm",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Graded midterm exam build, submitted under exam conditions.",
            Description = "The midterm exam answer. One problem, one sitting, no reference material. The constraint changes how you write: you reach for the pattern you already know works instead of trying something new halfway through.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/IT_ELECTIVE_2_MIDTERM_EXAM_9_ESGUERRA",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/midterm-exam-9.svg",
            Tech = new[] { "ASP.NET MVC", "C#" },
            Highlights = new[]
            {
                "Written under exam conditions",
                "Requirements met before anything was polished",
                "Committed as the work progressed"
            }
        },
        new Project
        {
            Slug = "sso",
            Title = "Single Sign-On Integration",
            Term = "Prefinals",
            Role = "Group",
            Course = "BSIT 31A3 - IT Elective",
            Summary = "Authentication flow using an external identity provider.",
            Description = "A step past hardcoded credentials: signing users in through an external identity provider instead of checking a username and password in the application itself. It forces you to understand what a login actually is - a claims principal issued by something you trust, carried in a cookie - rather than treating it as a single if statement.",
            RepositoryUrl = "https://github.com/Lycevm-3Alabang/IT_ELECTIVE_SSO_BSIT_31A3",
            RepositoryOwner = "Lycevm-3Alabang",
            Thumbnail = "/img/sso.svg",
            Tech = new[] { "ASP.NET", "Authentication", "OAuth" },
            Highlights = new[]
            {
                "Sign-in handled by an external provider",
                "Claims read from the returned identity",
                "Protected routes behind an authorisation check"
            }
        },
        new Project
        {
            Slug = "prefinal-exam",
            Title = "Prefinal Exam",
            Term = "Prefinals",
            Role = "Solo",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Individual prefinal exam covering the full term.",
            Description = "The prefinal exam build. Everything from the term was fair game, so the work was as much about deciding what to build first as about writing it. The structure had to hold up without any refactoring time at the end.",
            RepositoryUrl = "https://github.com/MaCorollaEsguerra/-IT_ELECTIVE_2_BSIT31E3_PREFINAL_EXAM_ESGUERRA_MA.-COROLLA",
            RepositoryOwner = "MaCorollaEsguerra",
            Thumbnail = "/img/prefinal-exam.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Razor" },
            Highlights = new[]
            {
                "Covers material from the whole term",
                "Planned before any code was written",
                "Submitted within the exam window"
            }
        },
        new Project
        {
            Slug = "prefinals-project",
            Title = "Prefinals Group Project",
            Term = "Prefinals",
            Role = "Group",
            Course = "BSIT 31E3 - IT Elective 2",
            Summary = "Three-person project with Esguerra, Galang and Gregorio.",
            Description = "The largest build of the term and the only one with three people in it. Scope had to be agreed before anyone started, the work was split so two people were never editing the same file, and the repository history shows how the pieces came together at the end.",
            RepositoryUrl = "https://github.com/yuwanandrei/IT_ELECTIVE_PREFINALS_PROJECT_Esguerra_GalangJM_Gregorio",
            RepositoryOwner = "yuwanandrei",
            Thumbnail = "/img/prefinals-project.svg",
            Tech = new[] { "ASP.NET MVC", "C#", "Git", "Bootstrap" },
            Highlights = new[]
            {
                "Three contributors on one codebase",
                "Scope agreed up front and split by feature",
                "Integrated and tested before submission"
            }
        }
    };

    public IReadOnlyList<Project> GetAll() => _projects;

    public Project? GetBySlug(string slug) =>
        _projects.FirstOrDefault(p => string.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<TermGroup> GetGrouped(string? query = null)
    {
        IEnumerable<Project> source = _projects;

        if (!string.IsNullOrWhiteSpace(query))
        {
            var q = query.Trim();
            source = source.Where(p =>
                p.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                p.Summary.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                p.Role.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                p.Tech.Any(t => t.Contains(q, StringComparison.OrdinalIgnoreCase)));
        }

        var filtered = source.ToList();

        return TermOrder
            .Select(t => new TermGroup
            {
                Term = t.Term,
                Blurb = t.Blurb,
                Projects = filtered.Where(p => p.Term == t.Term).ToList()
            })
            .Where(g => g.Projects.Count > 0)
            .ToList();
    }

    public (Project? Previous, Project? Next) GetNeighbours(string slug)
    {
        var ordered = GetGrouped().SelectMany(g => g.Projects).ToList();
        var index = ordered.FindIndex(p => string.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase));
        if (index < 0) return (null, null);

        return (
            index > 0 ? ordered[index - 1] : null,
            index < ordered.Count - 1 ? ordered[index + 1] : null);
    }
}
