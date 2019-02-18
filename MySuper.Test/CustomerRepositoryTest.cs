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
                var customer = new Customer { FirstName = "Isuru" };
                customerRepository.InsertCustomer(customer);

                Assert.True(customer.Id > 0);
            }
        }

        [Fact]
        public void Should_Display_Customer_List_Count_As_Two()
        {
            optionsBuilder.UseInMemoryDatabase("MySuperTestDb");

            // Run the test against one instance of the context
            using (var context = new MySuperContext(optionsBuilder.Options))
            {
                var customerRepository = new CutomerRepository(context);
                var customer1 = new Customer { FirstName = "Isuru" };
                var customer2 = new Customer { FirstName = "Mahesh" };

                customerRepository.InsertCustomer(customer1);
                customerRepository.InsertCustomer(customer2);

                var count = context.Customers.Count();
                Assert.Equal(3, count);
            }
        }
    }
}
