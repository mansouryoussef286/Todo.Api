namespace Todo.Api.Domain.Models
{
    public class AuthServerAuthenticationReqModel
    {
        public string Code { get; set; }
        public string ApiId { get; set; }
        public string ApiSecret { get; set; }
    }

    public class AuthServerAuthenticationResModel
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

    public class RefreshTokenReqModel
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class AuthServerRefreshTokenReqModel
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ApiId { get; set; }
        public string ApiSecret { get; set; }
    }

    public class AuthServerRefreshTokenResModel
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
