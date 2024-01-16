using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Domain.DTOs
{
    public class TodoTaskDTO
    {
        [Required(ErrorMessage = "Task ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Task ID must be a number greater than 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "Description cannot be longer than 250 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
