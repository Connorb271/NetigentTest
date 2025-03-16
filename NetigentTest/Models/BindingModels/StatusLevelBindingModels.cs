namespace NetigentTest.Models.BindingModels
{
    public class EditStatusLevelBindingModel : CreateStatusLevelBindingModel
    {
        public int Id { get; set; }
    }

    public class CreateStatusLevelBindingModel 
    {
        public string StatusName { get; set; } = string.Empty;
    }
}
