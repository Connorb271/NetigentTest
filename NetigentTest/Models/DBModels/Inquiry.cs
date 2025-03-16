using System.ComponentModel.DataAnnotations.Schema;

namespace NetigentTest.Models.DBModels
{
    [Table("Inquries", Schema = "gov")]
    public class Inquiry
    {
        public int Id { get; set; }
        [Column("ApplicationId")]
        public int? AppProjectId { get; set; }
        public AppProject? AppProject { get; set; }
        public string? SendToPerson { get; set; }
        public string? SendToRole { get; set; }
        public int? SendToPersonId { get; set; }
        public string? Subject { get; set; }
        [Column("Inquiry")]
        public string? InquiryText { get; set; }
        public string? Response { get; set; }
        public DateTime? AskedDt { get; set; }
        public DateTime? CompletedDt { get; set; }
    }
}
