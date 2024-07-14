using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Code_First_Approach.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Column("StudentName",TypeName= "varchar(100)")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string University { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int? Mobile { get; set; }




    }
}
