using Microsoft.EntityFrameworkCore;
using NetigentTest.Models.BindingModels;
using NetigentTest.Models.DBModels;

namespace NetigentTest.Services;
public interface IInquiryService
{
    Task<Inquiry> CreateAsync(CreateInquiryBindingModel model);
    Task<Inquiry> EditAsync(EditInquiryBindingModel model);
    Task<bool> DeleteAsync(int id);
    Task<Inquiry> GetAsync(int id);
    Task<List<Inquiry>> GetAsync();
}

public class InquiryService : APIService, IInquiryService
{
    public InquiryService(AppDbContext dbContext, ILogger<APIService> logger) : base(dbContext, logger) { }

    public async Task<Inquiry> CreateAsync(CreateInquiryBindingModel model)
    {
        try
        {
            var inquiry = new Inquiry
            {
                AppProjectId = model.AppProjectId,
                SendToPerson = "Person",
                SendToRole = "Role",
                SendToPersonId = 0,
                Subject = model.Subject,
                InquiryText = model.InquiryText,
                Response = "",
                AskedDt = DateTime.UtcNow,
                CompletedDt = null
            };

            await _dbContext.Inquiries.AddAsync(inquiry);
            await _dbContext.SaveChangesAsync();
            return inquiry;
        }
        catch (Exception ex)
        {
            Log($"Error creating Inquiry: {ex.Message}", nameof(CreateAsync), nameof(InquiryService));
            throw;
        }
    }

    public async Task<Inquiry> EditAsync(EditInquiryBindingModel model)
    {
        try
        {
            var existingInquiry = await _dbContext.Inquiries.FindAsync(model.Id);
            if (existingInquiry == null) return null;

            existingInquiry.AppProjectId = model.AppProjectId;
            existingInquiry.SendToPerson = model.SendToPerson;
            existingInquiry.SendToRole = model.SendToRole;
            existingInquiry.SendToPersonId = model.SendToPersonId;
            existingInquiry.Subject = model.Subject;
            existingInquiry.InquiryText = model.InquiryText;
            existingInquiry.Response = model.Response;
            existingInquiry.AskedDt = model.AskedDt;
            existingInquiry.CompletedDt = model.CompletedDt;

            await _dbContext.SaveChangesAsync();
            return existingInquiry;
        }
        catch (Exception ex)
        {
            Log($"Error editing Inquiry with Id {model.Id}: {ex.Message}", nameof(EditAsync), nameof(InquiryService));
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var inquiry = await _dbContext.Inquiries.FindAsync(id);
            if (inquiry == null) return false;

            _dbContext.Inquiries.Remove(inquiry);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Log($"Error deleting Inquiry with Id {id}: {ex.Message}", nameof(DeleteAsync), nameof(InquiryService));
            throw;
        }
    }

    public async Task<Inquiry> GetAsync(int id)
    {
        try
        {
            var inquiry = await _dbContext.Inquiries
                .Include(i => i.AppProject)
                .FirstOrDefaultAsync(i => i.Id == id);
            return inquiry;
        }
        catch (Exception ex)
        {
            Log($"Error fetching Inquiry with Id {id}: {ex.Message}", nameof(GetAsync), nameof(InquiryService));
            throw;
        }
    }

    public async Task<List<Inquiry>> GetAsync()
    {
        try
        {
            var inquiries = await _dbContext.Inquiries
                .Include(i => i.AppProject)
                .ToListAsync();
            return inquiries;
        }
        catch (Exception ex)
        {
            Log($"Error fetching all Inquiries: {ex.Message}", nameof(GetAsync), nameof(InquiryService));
            throw;
        }
    }
}
