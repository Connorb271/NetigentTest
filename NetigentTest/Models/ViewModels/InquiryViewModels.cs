namespace NetigentTest.Models.ViewModels
{
    public class InquiryViewModel
    {
        public int Id { get; set; }
        public string? SendToPerson { get; set; }
        public string? SendToRole { get; set; }
        public int? SendToPersonId { get; set; }
        public string? Subject { get; set; }
        public string? InquiryText { get; set; }
        public string? Response { get; set; }
        public DateTime? AskedDt { get; set; }
        public DateTime? CompletedDt { get; set; }
    }
}
