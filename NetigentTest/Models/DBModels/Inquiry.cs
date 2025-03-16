using System.ComponentModel.DataAnnotations.Schema;

namespace NetigentTest.Models.DBModels
{
    [Table("Inquries", Schema = "gov")]
    public class Inquiry
    {
        public int Id { get; set; }
        public int AppProjectId { get; set; }
        public AppProject? AppProject { get; set; }
        public string SendToPerson { get; set; } = string.Empty;
        public string SendToRole { get; set; } = string.Empty;
        public int SendToPersonId { get; set; } = 0;
        public string Subject { get; set; } = string.Empty;
        public string InquiryText { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime AskedDt { get; set; } = DateTime.MinValue;
        public DateTime CompletedDt { get; set; } = DateTime.MinValue;
    }
}
