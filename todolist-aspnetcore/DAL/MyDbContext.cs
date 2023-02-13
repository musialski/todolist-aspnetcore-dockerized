using Microsoft.EntityFrameworkCore;
using todolistaspnetcore.Models;

namespace todolistaspnetcore.DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ToDoPosition> Positions { get; set; }
    }
}