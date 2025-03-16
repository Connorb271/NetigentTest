using System.ComponentModel.DataAnnotations.Schema;

namespace NetigentTest.Models.DBModels
{
    [Table("StatusLevel", Schema = "dbo")]
    public class StatusLevel
    {
        public int? Id { get; set; }
        public string? StatusName { get; set; }
        public ICollection<AppProject>? AppProjects { get; set; }
    }
}
