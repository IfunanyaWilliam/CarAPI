namespace CarAPI.Domain.Entities
{
    public class CarModel
    {
        public Guid Id { get; set; }
        public string? CarID { get; set; }
        public string? Model { get; set; }
        public string? CategoryId { get; set; }
    }
}
