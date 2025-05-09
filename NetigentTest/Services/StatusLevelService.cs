﻿using Microsoft.EntityFrameworkCore;
using NetigentTest.Models.BindingModels;
using NetigentTest.Models.DBModels;

namespace NetigentTest.Services;
public interface IStatusLevelService
{
    Task<StatusLevel> CreateAsync(CreateStatusLevelBindingModel model);
    Task<StatusLevel> EditAsync(EditStatusLevelBindingModel model);
    Task<bool> DeleteAsync(int id);
    Task<StatusLevel> GetAsync(int id);
    Task<List<StatusLevel>> GetAsync();
}
public class StatusLevelService : APIService, IStatusLevelService
{
    public StatusLevelService(AppDbContext dbContext, ILogger<APIService> logger) : base(dbContext, logger) { }

    public async Task<StatusLevel> CreateAsync(CreateStatusLevelBindingModel model)
    {
        try
        {
            var statusLevel = new StatusLevel
            {
                StatusName = model.StatusName
            };

            await _dbContext.StatusLevels.AddAsync(statusLevel);
            await _dbContext.SaveChangesAsync();
            return statusLevel;
        }
        catch (Exception ex)
        {
            Log($"Error creating StatusLevel: {ex.Message}", nameof(CreateAsync), nameof(StatusLevelService));
            throw;
        }
    }

    public async Task<StatusLevel> EditAsync(EditStatusLevelBindingModel model)
    {
        try
        {
            var existingStatusLevel = await _dbContext.StatusLevels.FindAsync(model.Id);
            if (existingStatusLevel == null) return null;

            existingStatusLevel.StatusName = model.StatusName;
            await _dbContext.SaveChangesAsync();
            return existingStatusLevel;
        }
        catch (Exception ex)
        {
            Log($"Error editing StatusLevel with Id {model.Id}: {ex.Message}", nameof(EditAsync), nameof(StatusLevelService));
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var statusLevel = await _dbContext.StatusLevels.FindAsync(id);
            if (statusLevel == null) return false;

            _dbContext.StatusLevels.Remove(statusLevel);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Log($"Error deleting StatusLevel with Id {id}: {ex.Message}", nameof(DeleteAsync), nameof(StatusLevelService));
            throw;
        }
    }

    public async Task<StatusLevel> GetAsync(int id)
    {
        try
        {
            var statusLevel = await _dbContext.StatusLevels.FindAsync(id);
            if (statusLevel == null) return null;

            return statusLevel;
        }
        catch (Exception ex)
        {
            Log($"Error fetching StatusLevel with Id {id}: {ex.Message}", nameof(GetAsync), nameof(StatusLevelService));
            throw;
        }
    }

    public async Task<List<StatusLevel>> GetAsync()
    {
        try
        {
            var statusLevels = await _dbContext.StatusLevels.ToListAsync();
            return statusLevels;
        }
        catch (Exception ex)
        {
            Log($"Error fetching all StatusLevels: {ex.Message}", nameof(GetAsync), nameof(StatusLevelService));
            throw;
        }
    }
}
