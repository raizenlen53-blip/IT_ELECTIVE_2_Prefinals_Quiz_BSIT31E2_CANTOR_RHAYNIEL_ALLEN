using Portfolio.Models;

namespace Portfolio.Services;

public interface IProjectRepository
{
    IReadOnlyList<Project> GetAll();
    IReadOnlyList<TermGroup> GetGrouped(string? query = null);
    Project? GetBySlug(string slug);
    (Project? Previous, Project? Next) GetNeighbours(string slug);
}
