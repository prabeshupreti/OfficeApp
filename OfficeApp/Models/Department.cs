using System.ComponentModel.DataAnnotations;

namespace OfficeApp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(75)]
        public string Name { get; set; }
    }
}
