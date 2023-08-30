namespace lesson1_Simple_Functions___Controller.DTOs.UsersDtos
{
    public class UpdateUserDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
    }
}
