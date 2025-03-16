using System.ComponentModel.DataAnnotations.Schema;

namespace NetigentTest.Models.DBModels
{
    [Table("Application", Schema = "gov")]
    public class AppProject
    {
        public int Id { get; set; }
        public string AppStatus { get; set; } = string.Empty;
        public string ProjectRef { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectLocation { get; set; } = string.Empty;
        public DateTime OpenDt { get; set; } = DateTime.MinValue;
        public DateTime StartDt { get; set; } = DateTime.MinValue;
        public DateTime CompletedDt { get; set; } = DateTime.MinValue;
        public decimal ProjectValue { get; set; } = 0;
        public int StatusId { get; set; }
        public StatusLevel? StatusLevel { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime Modified { get; set; } = DateTime.MinValue;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
    }
}
