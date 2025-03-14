﻿using Microsoft.EntityFrameworkCore;
using Register.Models.Entities;

namespace Register.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Signup> Signups { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
    }
}

