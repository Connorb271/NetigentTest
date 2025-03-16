namespace NetigentTest.Models.BindingModels
{
    public class CreateInquiryBindingModel
    {
        public int AppProjectId { get; set; }
        public string SendToPerson { get; set; } = string.Empty;
        public string SendToRole { get; set; } = string.Empty;
        public int SendToPersonId { get; set; } = 0;
        public string Subject { get; set; } = string.Empty;
        public string InquiryText { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime AskedDt { get; set; } = DateTime.MinValue;
        public DateTime CompletedDt { get; set; } = DateTime.MinValue;
    }

    public class EditInquiryBindingModel : CreateInquiryBindingModel
    {
        public int Id { get; set; }
    }
}
