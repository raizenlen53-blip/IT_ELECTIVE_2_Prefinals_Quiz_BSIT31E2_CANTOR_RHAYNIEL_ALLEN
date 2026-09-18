# IT Elective 2 - Modern Portfolio (ASP.NET Core MVC)

Prefinals quiz submission for **BSIT 31E2 - IT Elective 2**
Rhayniel Allen Cantor

A portfolio application built with ASP.NET Core MVC. It lists every project from the
course with its GitHub link, a short description and a thumbnail, organised into a
table of contents, with a detail page and a comment section for each project.

---

## Login details

The assignment asks for a hardcoded login, and these are the credentials to use.
| Field | Value |
| --- | --- |
| Username | `admin` |
| Password | `admin123` |

The credentials live in `appsettings.json` under the `PortfolioLogin` section, so they
can be changed without touching any code. They are also shown on the sign-in page so a
marker never has to go looking for them.

---

## Running it

```bash
dotnet restore
dotnet run
```

Then open <https://localhost:7148>. Requires the .NET 8 SDK. It also opens straight from
Visual Studio 2022 with **Run** (IIS Express or the Portfolio profile).

No database is needed. Projects and comments are held in memory, so comments reset when
the application restarts.

---

## What is in the application

| Requirement | Where it is |
| --- | --- |
| MVC structure | `Controllers/`, `Models/`, `Views/` |
| Hardcoded login | `Services/HardcodedAuthenticator.cs`, `Controllers/AccountController.cs` |
| Login recorded in README | this file, the table above |
| GitHub links, descriptions, thumbnails | `Services/ProjectRepository.cs`, `wwwroot/img/` |
| Table of contents | `Views/Projects/Index.cshtml` - grouped by term, with a sticky jump rail and search |
| Detail page per project | `Views/Projects/Details.cshtml` |
| Comment section per project | `Views/Shared/_Comments.cshtml`, `ProjectsController.Comment` |

### Pages

- `/` - sign-in landing page (the only page open to visitors)
- `/Account/Login` - the hardcoded login form
- `/Projects` - table of contents, grouped Prelim / Midterm / Prefinals, with search
- `/Projects/Details/{slug}` - full write-up, repository link, highlights and comments

---

## Projects included

| # | Project | Term | Repository |
| --- | --- | --- | --- |
| 1 | Prelim Activity 1 | Prelim | https://github.com/NEILTUMALA/BSIT31E2_PRELIM_A2_TUMALA_NEIL.git |
| 2 | Prelim Activity 2 | Prelim | https://github.com/Lycevm-3Alabang/IT_ELECTIVE_SSO_BSIT_31A2.git |
| 3 | Prelim Activity 3 | Prelim | https://github.com/raizenlen53-blip/CANTOR_RHAYNIEL-ALLEN-D.-BSIT31E2.git |
| 4 | Prelim Hands-On 1 | Prelim | https://github.com/raizenlen53-blip/CANTOR_RHAYNIEL-ALLEN-D.-BSIT31E2.git |
| 5 | Prelim Hands-On 1 (pair) | Prelim | https://github.com/raizenlen53-blip/ExamMvc.git |
| 6 | IT Elective 2 working repository | Prelim | https://github.com/raizenlen53-blip/HtpServer.git |
| 7 | WebApplication1 | Prelim | https://github.com/raizenlen53-blip/it-elective-two-prelim-quiz-one.git |
| 8 | Midterm Quiz 1 | Midterm | https://github.com/raizenlen53-blip/MIDTERM_Q2_BSIT_Cantor_RhaynielAllen.git |
| 9 | Midterm Quiz 3 | Midterm | https://github.com/raizenlen53-blip/IT_ELECTIVE_2_MIDTERM_Q3_CANTOR.git |
| 10 | Midterm Quiz 3 (team) | Midterm https://github.com/raizenlen53-blip/IT_ELECTIVE_2_PREFINALS_A1_CANTOR_RHAYNIEL_ALLEN.git |
| 11 | Midterm Hands-On 1 to 3 | Midterm | https://github.com/raizenlen53-blip/IT_ELECTIVE_2_MIDTERM_H1_H2_H3.git |
| 12 | Midterm Exam - Item 9 | Midterm | https://github.com/raizenlen53-blip/IT_ELECTIVE_PREFINALS_PROJECT_CantorRhayniel_LeanoJoyce_GonzalesKrister-main.git |
| 13 | Single Sign-On integration | Prefinals | https://github.com/Lycevm-3Alabang/IT_ELECTIVE_SSO_BSIT_31A3 |
| 14 | Prefinal Exam | Prefinals | https://github.com/raizenlen53-blip/IT_ELECTIVE_2_BSIT31E2_PREFINAL_EXAM_CANTOR_RHAYNIEL-ALLEN-D.-main.git |
| 15 | Prefinals Group Project | Prefinals | https://github.com/raizenlen53-blip/IT_ELECTIVE_2_PREFINALS_A1_CANTOR_RHAYNIEL_ALLEN.git |

---

## Security notes

The login is hardcoded because the brief asks for it, but the rest of the application is
written the way a real one would be:

- **The password is never held as plain text after startup.** It is hashed with PBKDF2
  (SHA-256, 210,000 iterations, random salt) and every attempt is compared in fixed time,
  so the comparison cannot be timed to guess characters.
- **Brute force is throttled.** Five failed attempts from the same client lock the form
  for five minutes.
- **The error message never says which field was wrong**, so the form does not confirm
  whether a username exists.
- **Cookie authentication** with an HttpOnly, SameSite=Strict cookie that expires after
  two hours (24 with "keep me signed in"). It cannot be read by JavaScript.
- **Every project page is behind `[Authorize]`** at controller level, so a new action
  cannot be left unprotected by accident.
- **Anti-forgery tokens are validated on every POST** via a global
  `AutoValidateAntiforgeryToken` filter, not only where someone remembered to add it.
- **Open redirects are blocked** - the return URL after sign-in is checked with
  `Url.IsLocalUrl`.
- **Comment input is validated and length-capped** on the server, control characters are
  stripped, and Razor HTML-encodes on output, so comments cannot inject script.
- **Response headers**: Content-Security-Policy, X-Content-Type-Options,
  X-Frame-Options and Referrer-Policy are set for every response.

### What would change in production

A shared hardcoded account is fine for a graded exercise and nothing else. A real version
would use ASP.NET Core Identity with per-user accounts, store comments in a database, and
keep secrets outside `appsettings.json`.

---

## Before you submit

Two things worth doing so the portfolio is yours rather than mine:

1. **Replace the thumbnails.** `wwwroot/img/*.svg` are generated placeholders. Take a
   screenshot of each application running, save it as `wwwroot/img/{slug}.png`, and update
   the `Thumbnail` value for that project in `Services/ProjectRepository.cs`.
2. **Check the descriptions.** The write-ups in `ProjectRepository.cs` describe what each
   task generally covered. Edit any that do not match what you actually built - that file
   is the only place project text lives.

---

## Project structure

```
Portfolio/
├── Controllers/
│   ├── AccountController.cs      login and logout
│   ├── HomeController.cs         public landing page
│   └── ProjectsController.cs     contents, details, comments  [Authorize]
├── Models/
│   ├── Comment.cs                validation rules for comments
│   ├── ContentsViewModel.cs      table of contents view model
│   ├── LoginViewModel.cs
│   ├── Project.cs
│   └── ProjectDetailViewModel.cs
├── Services/
│   ├── HardcodedAuthenticator.cs the hardcoded login, hashed and throttled
│   ├── InMemoryCommentRepository.cs
│   └── ProjectRepository.cs      all 15 projects live here
├── Views/
│   ├── Account/Login.cshtml
│   ├── Home/Index.cshtml
│   ├── Projects/Index.cshtml     table of contents
│   ├── Projects/Details.cshtml   detail page
│   └── Shared/_Comments.cshtml   comment section
├── wwwroot/
│   ├── css/site.css
│   └── img/                      thumbnails
├── Program.cs                    services, auth, security headers
└── README.md
```
