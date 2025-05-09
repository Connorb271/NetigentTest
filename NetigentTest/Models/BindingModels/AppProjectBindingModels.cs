﻿using NetigentTest.Models.DBModels;

namespace NetigentTest.Models.BindingModels
{
    public class CreateEditAppProjectBindingModel
    {
        public int Id { get; set; }
        public string AppStatus { get; set; } = string.Empty;
        public string ProjectRef { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectLocation { get; set; } = string.Empty;
        public string OpenDt { get; set; } = string.Empty;
        public string StartDt { get; set; } = string.Empty;
        public string CompletedDt { get; set; } = string.Empty;
        public decimal ProjectValue { get; set; } = 0;
        public int StatusId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
