using InfraEstrutura.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace TodoMvc.Models
{
    public class ToDoItemModel
    {
       
        public Guid Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public String AssignedFor { get; set; }
        [Required]
        public State Status { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Description { get; set; }
    }
}
