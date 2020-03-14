namespace DailyPlanner.Api.ViewDtos
{
    public class LoginResponseDto
    {
        public LoginResponseDto(
            string token,
            string email,
            string firstName,
            string lastName,
            string id)
        {
            Token = token;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        public string Id { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
