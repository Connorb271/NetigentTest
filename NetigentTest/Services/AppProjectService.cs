using Microsoft.EntityFrameworkCore;
using NetigentTest.Models.BindingModels;
using NetigentTest.Models.DBModels;
using NetigentTest.Models.ViewModels;

namespace NetigentTest.Services;
public interface IAppProjectService
{
    Task<AppProject> CreateAsync(CreateEditAppProjectBindingModel model);
    Task<AppProject> EditAsync(CreateEditAppProjectBindingModel model);
    Task<bool> DeleteAsync(int id);
    Task<AppProjectIndividualViewModel> GetAsync(int id);
    Task<List<AppProjectSearchViewModel>> GetAsync();
}
public class AppProjectService : APIService, IAppProjectService
{
    public AppProjectService(AppDbContext dbContext, ILogger<APIService> logger) : base(dbContext, logger) { }

    public async Task<AppProject> CreateAsync(CreateEditAppProjectBindingModel model)
    {
        try
        {
            var openDt = new DateTime();
            var startDt = new DateTime();
            var completedDt = new DateTime();
            if (!DateTime.TryParse(model.OpenDt, out openDt))
                throw new ArgumentException($"Invalid open date format: {model.OpenDt}");

            if (!DateTime.TryParse(model.StartDt, out startDt))
                throw new ArgumentException($"Invalid start date format: {model.StartDt}");

            if (!DateTime.TryParse(model.CompletedDt, out completedDt))
                throw new ArgumentException($"Invalid completed date format: {model.CompletedDt}");



            var appProject = new AppProject
            {
                AppStatus = model.AppStatus,
                ProjectRef = model.ProjectRef,
                ProjectName = model.ProjectName,
                ProjectLocation = model.ProjectLocation,
                OpenDt = openDt,
                StartDt = startDt,
                CompletedDt = completedDt,
                ProjectValue = model.ProjectValue,
                StatusId = model.StatusId,
                Notes = model.Notes,
                Modified = DateTime.UtcNow,
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



    public async Task<AppProject> EditAsync(CreateEditAppProjectBindingModel model)
    {
        try
        {
            var existingAppProject = await _dbContext.AppProjects.FindAsync(model.Id);
            if (existingAppProject == null) return null;

            var openDt = new DateTime();
            var startDt = new DateTime();
            var completedDt = new DateTime();

            if (!DateTime.TryParse(model.OpenDt, out openDt))
                throw new ArgumentException($"Invalid open date format: {model.OpenDt}");

            if (!DateTime.TryParse(model.StartDt, out startDt))
                throw new ArgumentException($"Invalid start date format: {model.StartDt}");

            if (!DateTime.TryParse(model.CompletedDt, out completedDt))
                throw new ArgumentException($"Invalid completed date format: {model.CompletedDt}");

            existingAppProject.AppStatus = model.AppStatus;
            existingAppProject.ProjectRef = model.ProjectRef;
            existingAppProject.ProjectName = model.ProjectName;
            existingAppProject.ProjectLocation = model.ProjectLocation;
            existingAppProject.OpenDt = openDt;
            existingAppProject.StartDt = startDt;
            existingAppProject.CompletedDt = completedDt;
            existingAppProject.ProjectValue = model.ProjectValue;
            existingAppProject.StatusId = model.StatusId;
            existingAppProject.Notes = model.Notes;
            existingAppProject.Modified = DateTime.UtcNow;
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

            appProject.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Log($"Error deleting AppProject with Id {id}: {ex.Message}", nameof(DeleteAsync), nameof(AppProjectService));
            throw;
        }
    }

    public async Task<AppProjectIndividualViewModel> GetAsync(int id)
    {
        try
        {
            var appProject = await _dbContext.AppProjects
                .Include(a => a.StatusLevel)
                .Include(a => a.Inquiries)
                .Where(a => a.Id == id)
                .Select(e => new AppProjectIndividualViewModel()
                {
                    Id = e.Id,
                    AppStatus = e.AppStatus,
                    ProjectRef = e.ProjectRef,
                    ProjectName = e.ProjectName,
                    ProjectLocation = e.ProjectLocation,
                    OpenDt = e.OpenDt,
                    StartDt = e.StartDt,
                    CompletedDt = e.CompletedDt,
                    ProjectValue = e.ProjectValue ?? 0,
                    StatusId = e.StatusId ?? 0,
                    Notes = e.Notes,
                    Inquiries = e.Inquiries != null
                    ? e.Inquiries.Select(i => new InquiryViewModel
                    {
                        Id = i.Id,
                        SendToPerson = i.SendToPerson,
                        SendToRole = i.SendToRole,
                        SendToPersonId = i.SendToPersonId,
                        Subject = i.Subject,
                        InquiryText = i.InquiryText,
                        Response = i.Response,
                        AskedDt = i.AskedDt,
                        CompletedDt = i.CompletedDt
                    }).ToList()
                    : new List<InquiryViewModel>()
                })
                .FirstOrDefaultAsync();

            return appProject;
        }
        catch (Exception ex)
        {
            Log($"Error fetching AppProject with Id {id}: {ex.Message}", nameof(GetAsync), nameof(AppProjectService));
            throw;
        }
    }

    public async Task<List<AppProjectSearchViewModel>> GetAsync()
    {
        try
        {
            var appProjects = await _dbContext.AppProjects
                .Include(a => a.StatusLevel)
                .Where(a => !a.IsDeleted)
                .Select(e => new AppProjectSearchViewModel()
                {
                    Id = e.Id,
                    Location = e.ProjectLocation,
                    Name = e.ProjectName,
                    Reference = e.ProjectRef,
                    StatusLevel = e.StatusLevel.StatusName
                })
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