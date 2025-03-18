using NetigentTest.Models.DBModels;

namespace NetigentTest.Models.ViewModels
{
    public class AppProjectSearchViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Reference { get; set; }
        public string? Location { get; set; }
        public string? StatusLevel { get; set; }  
    }

    public class AppProjectIndividualViewModel
    {
        public int Id { get; set; }
        public string? AppStatus { get; set; }
        public string? ProjectRef { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectLocation { get; set; }
        public DateTime? OpenDt { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? CompletedDt { get; set; }
        public decimal ProjectValue { get; set; } = 0;
        public int StatusId { get; set; } = 0;
        public string? Notes { get; set; }
        public List<InquiryViewModel> Inquiries { get; set; }
    }
}
