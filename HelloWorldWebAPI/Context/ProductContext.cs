using HelloWorldWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebAPI.Context
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
