using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return (from customer in customers 
                let total = customer.Orders.Sum(order => order.Total) 
                where total > limit 
                select customer);
        }

        public static Dictionary<Customer, List<Supplier>> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var tt = customers.Where((customer) => suppliers
                .Contains(supplier => customer.City == supplier.City && customer.Country == supplier.Country))


            var dictionary = (from customer in customers
                from supplier in suppliers
                where customer.City == supplier.City && customer.Country == supplier.Country
                select new
                {
                   Customer = new Customer
                   {
                       CustomerID = customer.CustomerID,
                       City = customer.City,
                       Country = customer.Country
                   },
                   Supliers = new List<Supplier>
                   {
                       new Supplier
                       {
                           City = supplier.City,
                           Country = supplier.Country
                       }
                   }

                }).ToDictionary(item => item.Customer, item => item.Supliers);

            return dictionary;
        }

        public static Dictionary<Customer, List<Supplier>> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            // need to talk
            var dictionary = (from customer in customers
                from supplier in suppliers
                where customer.City == supplier.City && customer.Country == supplier.Country
                select new
                {
                    Customer = new Customer
                    {
                        CustomerID = customer.CustomerID,
                        City = customer.City,
                        Country = customer.Country
                    },
                    Supliers = new List<Supplier>
                    {
                        new Supplier
                        {
                            City = supplier.City,
                            Country = supplier.Country
                        }
                    }

                }).ToDictionary(item => item.Customer, item => item.Supliers);

            return dictionary;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return (from customer in customers
                let total = customer.Orders.Sum(order => order.Total)
                where total > limit
                select customer);
            //var listCustomers = (from customer in customers 
            //    from order in customer.Orders 
            //    where order.Total > limit 
            //    select customer).ToList();

            //return listCustomers;

        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var tt = (from customer in customers
                from order in customer.Orders
                where order.OrderDate.Year > DateTime.Parse("1995-01-01T00:00:00").Year
                      select customer).Distinct().ToList();

                //select new
                //{
                //    Customer = new Customer
                //    {
                //        CustomerID = customer.CustomerID,
                //        CompanyName = customer.CompanyName
                //    },
                //    DateOfEntry = order.OrderDate
                //}).ToList();

                 throw new NotImplementedException();
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            throw new NotImplementedException();
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            throw new NotImplementedException();
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }
    }
}