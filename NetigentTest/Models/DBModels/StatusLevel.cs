namespace NetigentTest.Models.DBModels
{
    public class StatusLevel
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public ICollection<AppProject> AppProjects { get; set; } = new List<AppProject>();
    }
}
