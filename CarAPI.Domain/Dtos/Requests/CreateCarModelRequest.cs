namespace CarAPI.Domain.Dtos.Requests
{
    public class CreateCarModelRequest
    {
        public string? CarID { get; set; }
        public string? Model { get; set; }
        public string? CategoryId { get; set; }
    }
}
