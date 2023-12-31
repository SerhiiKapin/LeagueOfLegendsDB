﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeagueOfLegendsDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Character> Characters { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
        public ApplicationContext() 
        {
            Database.EnsureCreated();
        } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=LOLChampions.db");
        }
    }
}
