using Microsoft.EntityFrameworkCore;
using NetigentTest.Models.BindingModels;
using NetigentTest.Models.DBModels;

namespace NetigentTest.Services;
public interface IAppProjectService
{
    Task<AppProject> CreateAsync(CreateAppProjectBindingModel model);
    Task<AppProject> EditAsync(EditAppProjectBindingModel model);
    Task<bool> DeleteAsync(int id);
    Task<AppProject> GetAsync(int id);
    Task<List<AppProject>> GetAsync();
}
public class AppProjectService : APIService, IAppProjectService
{
    public AppProjectService(AppDbContext dbContext, ILogger<APIService> logger) : base(dbContext, logger) { }

    public async Task<AppProject> CreateAsync(CreateAppProjectBindingModel model)
    {
        try
        {
            var appProject = new AppProject
            {
                AppStatus = model.AppStatus,
                ProjectRef = model.ProjectRef,
                ProjectName = model.ProjectName,
                ProjectLocation = model.ProjectLocation,
                OpenDt = model.OpenDt,
                StartDt = model.StartDt,
                CompletedDt = model.CompletedDt,
                ProjectValue = model.ProjectValue,
                StatusId = model.StatusId,
                Notes = model.Notes,
                Modified = model.Modified,
                IsDeleted = model.IsDeleted
            };

            await _dbContext.AppProjects.AddAsync(appProject);
            await _dbContext.SaveChangesAsync();
            return appProject;
        }
        catch (Exception ex)
        {
            Log($"Error creating AppProject: {ex.Message}", nameof(CreateAsync), nameof(AppProjectService));
            throw;
        }
    }

    public async Task<AppProject> EditAsync(EditAppProjectBindingModel model)
    {
        try
        {
            var existingAppProject = await _dbContext.AppProjects.FindAsync(model.Id);
            if (existingAppProject == null) return null;

            existingAppProject.AppStatus = model.AppStatus;
            existingAppProject.ProjectRef = model.ProjectRef;
            existingAppProject.ProjectName = model.ProjectName;
            existingAppProject.ProjectLocation = model.ProjectLocation;
            existingAppProject.OpenDt = model.OpenDt;
            existingAppProject.StartDt = model.StartDt;
            existingAppProject.CompletedDt = model.CompletedDt;
            existingAppProject.ProjectValue = model.ProjectValue;
            existingAppProject.StatusId = model.StatusId;
            existingAppProject.Notes = model.Notes;
            existingAppProject.Modified = model.Modified;
            existingAppProject.IsDeleted = model.IsDeleted;

            await _dbContext.SaveChangesAsync();
            return existingAppProject;
        }
        catch (Exception ex)
        {
            Log($"Error editing AppProject with Id {model.Id}: {ex.Message}", nameof(EditAsync), nameof(AppProjectService));
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var appProject = await _dbContext.AppProjects.FindAsync(id);
            if (appProject == null) return false;

            appProject.IsDeleted = true; // Soft delete
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Log($"Error deleting AppProject with Id {id}: {ex.Message}", nameof(DeleteAsync), nameof(AppProjectService));
            throw;
        }
    }

    public async Task<AppProject> GetAsync(int id)
    {
        try
        {
            var appProject = await _dbContext.AppProjects
                .Include(a => a.StatusLevel)
                .FirstOrDefaultAsync(a => a.Id == id);
            return appProject;
        }
        catch (Exception ex)
        {
            Log($"Error fetching AppProject with Id {id}: {ex.Message}", nameof(GetAsync), nameof(AppProjectService));
            throw;
        }
    }

    public async Task<List<AppProject>> GetAsync()
    {
        try
        {
            var appProjects = await _dbContext.AppProjects
                .Include(a => a.StatusLevel)
                .Where(a => !a.IsDeleted)
                .ToListAsync();
            return appProjects;
        }
        catch (Exception ex)
        {
            Log($"Error fetching all AppProjects: {ex.Message}", nameof(GetAsync), nameof(AppProjectService));
            throw;
        }
    }
}