using NetigentTest.Models.DBModels;

namespace NetigentTest.Models.BindingModels
{
    public class CreateAppProjectBindingModel
    {
        public string AppStatus { get; set; } = string.Empty;
        public string ProjectRef { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectLocation { get; set; } = string.Empty;
        public DateTime OpenDt { get; set; } = DateTime.MinValue;
        public DateTime StartDt { get; set; } = DateTime.MinValue;
        public DateTime CompletedDt { get; set; } = DateTime.MinValue;
        public decimal ProjectValue { get; set; } = 0;
        public int StatusId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime Modified { get; set; } = DateTime.MinValue;
        public bool IsDeleted { get; set; } = false;
    }

    public class EditAppProjectBindingModel : CreateAppProjectBindingModel
    {
        public int Id { get; set; }
    }
}
