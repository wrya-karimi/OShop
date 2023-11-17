using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
