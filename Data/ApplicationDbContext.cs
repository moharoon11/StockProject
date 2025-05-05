using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data {


    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) {

        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments {get; set; }

        public DbSet<User> Users {get; set; }
    }
}