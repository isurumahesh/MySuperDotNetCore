using Microsoft.EntityFrameworkCore;
using MySuper.Api.DatabaseContext;
using MySuper.Api.Entities;
using MySuper.Api.Repositories;
using System;
using System.Linq;
using Xunit;

namespace MySuper.Test
{
    public class CustomerRepositoryTest
    {
        private DbContextOptionsBuilder<MySuperContext> optionsBuilder = new DbContextOptionsBuilder<MySuperContext>();

        [Fact]
        public void Should_Add_New_Customer()
        {
            optionsBuilder.UseInMemoryDatabase("MySuperTestDb");

            // Run the test against one instance of the context
            using (var context = new MySuperContext(optionsBuilder.Options))
            {
                var customerRepository = new CutomerRepository(context);
                customerRepository.InsertCustomer(new Customer { FirstName = "Isuru" });
                var result=context.SaveChanges();            
                Assert.Equal(1, result);
            }
        }
    }
}
