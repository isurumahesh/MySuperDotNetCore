using Microsoft.EntityFrameworkCore;
using MySuper.Api.DatabaseContext;
using MySuper.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySuper.Api.Repositories
{
    public class CutomerRepository 
    {
        private readonly MySuperContext _context;
        public CutomerRepository(MySuperContext context)
        {
            _context = context;
        }

        public Customer GetCustomerByID(int CustomerId)
        {
            return _context.Customers.Find(CustomerId);
        }

        public List<Customer> GetCustomers()
        {
            //query using shadow properties
           // _context.Customers.Where(a => EF.Property<DateTime>(a, "CreatedDate") >= DateTime.UtcNow.AddDays(-5));

            //retrive shadow properties
           // _context.Customers.Select(a => new { Name = a.FirstName, Created = EF.Property<DateTime>(a, "CreatedDate") });

            return _context.Customers.ToList();
        }

        public void InsertCustomer(Customer customer)
        {

            _context.Customers.Add(customer);
            _context.SaveChanges();

            //data binding shadow properties
            //_context.Entry(customer).Property("CreatedDate").CurrentValue = DateTime.UtcNow;
            //_context.Entry(customer).Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
        }

        public void UpdateCustomer(Customer Customer)
        {
            _context.Entry(Customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {          
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public void Save()
        {
           
        }
    }
}
