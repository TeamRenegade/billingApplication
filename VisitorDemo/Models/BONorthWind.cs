using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisitorDemo.DataLayer;

namespace VisitorDemo.Models
{
    public class BONorthWind
    {
        private NORTHWNDEntities _dbContext = new NORTHWNDEntities();


        public List<Customer> GetCustomers()
        {
            var customers = from c in _dbContext.Customers
                            orderby c.CustomerID
                            select c;

            return customers.ToList<Customer>();
        }

        public List<Order> GetOrders(string customerID) {

            return _dbContext.Orders.Where(o => o.CustomerID.Equals(customerID, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(o => o.OrderDate).ToList<Order>();

        }
    }
}