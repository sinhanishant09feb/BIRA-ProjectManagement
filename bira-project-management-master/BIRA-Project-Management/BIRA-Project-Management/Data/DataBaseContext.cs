using BIRA_Project_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BIRA_Project_Management.Data {
    public class DataBaseContext:DbContext {
        public DbSet<Project> Projects{ get; set; }
        public DbSet<Issue> Issue { get; set; }

        public DataBaseContext() { }
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options) {

        }
    }
}
