using HelloWorldWebAPI.Context;
using HelloWorldWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApplication.Test.MockData
{
    public class MockDataSeed
    {
        public MockDataSeed()
        {
        }

        public void Seed(ProductContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Products.AddRange(
                new Product() { Name = "TestProduct1", Description = "Test Product1", Price = 1 },
                new Product() { Name = "TestProduct2", Description = "Test Product2", Price = 2 }


            );


            context.SaveChanges();

            foreach (var entity in context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }
    }
}
