using Microsoft.EntityFrameworkCore;

namespace CRUD_Code_First_Approach.Models
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions options) : base(options) { 
        
        }
        public DbSet<Student> Students { get; set; }
    }
}
