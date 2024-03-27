namespace Todo.Api.Domain.Models
{
    public class AuthenticationReqModel
    {
        public string Code { get; set; }
        public string ApiId { get; set; }
        public string ApiSecret { get; set; }
    }

    public class AuthenticationResModel
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Currentuser CurrentUser { get; set; }
    }

    public class Currentuser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePicturePath { get; set; }
    }

}
