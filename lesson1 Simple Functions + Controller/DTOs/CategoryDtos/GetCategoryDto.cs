namespace lesson1_Simple_Functions___Controller.DTOs.CategoryDtos
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int ParentId { get; set; }
    }
}
