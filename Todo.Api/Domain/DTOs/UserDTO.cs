using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Todo.Api.Infrastructure.Data.Models;

namespace Todo.Api.Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicPath { get; set; }
        public string Email { get; set; }
    }

}
