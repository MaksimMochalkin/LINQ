using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
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

        public static IEnumerable<(Customer, IEnumerable<Supplier>)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result = customers.Select(c => (c, suppliers
                .Where(s => s.City == c.City && s.Country == c.Country).Select(s => s))).ToList();

            return result;
        }

        public static IEnumerable<(Customer, IEnumerable<Supplier>)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result = customers.Select(c => (c, suppliers
                .Where(s => s.City == c.City && s.Country == c.Country).Select(s => s))).ToList();


            return result;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            var listCustomers = (from customer in customers
                                 from order in customer.Orders
                                 where order.Total > limit
                                 select customer).Distinct().ToList();

            return listCustomers;

        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var filteredCustomers = (from customer in customers
                                    where customer.Orders.Count() != 0
                                    select customer).ToList();

            var customersWithDateOfEntry = (from customer in filteredCustomers
                      let dateOfEntry = customer.Orders.Select(d => d.OrderDate).Min()
                      select (customer, dateOfEntry)).Distinct().ToList();

            return customersWithDateOfEntry;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var filteredCustomers = (from customer in customers
                                     where customer.Orders.Count() != 0
                                     select customer).ToList();

            var customersWithDateOfEntry = (from customer in filteredCustomers
                                            let dateOfEntry = customer.Orders.Select(d => d.OrderDate)
                                            select (customer, dateOfEntry.Min())).Distinct().ToList();

            var sortedCustomers = (from customer in customersWithDateOfEntry
                                   orderby customer.Item2.Year, customer.Item2.Month, customer.Item2.Day ascending
                                   select (customer)).ToList();



            return sortedCustomers;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            var filteredCustomers = (from customer in customers
                                     where customer.PostalCode.Any(char.IsLetter) ||
                                     customer.Region ==  null || customer.Phone[0] != '('
                                     select customer).ToList();

            return filteredCustomers;
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

            var result = products.GroupBy(cat => cat.Category)
                .Select(c => new Linq7CategoryGroup
                {
                    Category = c.Key,
                    UnitsInStockGroup = c.GroupBy(uis => uis.UnitsInStock)
                        .Select(u => new Linq7UnitsInStockGroup
                        {
                            UnitsInStock = u.Key,
                            Prices = u.GroupBy(up => up.UnitPrice)
                                .Select(p => (p.Key)).ToList()
                        }).ToList()
                }).ToList();
           
            return result;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            var cheapList = products.Select(c => new
                {
                    Category = cheap,
                    Products = products.Where(p => p.UnitPrice == products.Select(p => p.UnitPrice).Min()).ToList()
            }).Select(c => (c.Category, c.Products)).ToList();

            var middleList = products.Select(c => new
            {
                Category = middle,
                Products = products.Where(p => p.UnitPrice != products.Select(p => p.UnitPrice).Min()
                && p.UnitPrice != products.Select(p => p.UnitPrice).Max()).ToList()
            }).Select(c => (c.Category, c.Products)).ToList();

            var expensiveList = products.Select(c => new
            {
                Category = expensive,
                Products = products.Where(p => p.UnitPrice == products.Select(p => p.UnitPrice).Max()).ToList()
            }).Select(c => (c.Category, c.Products)).ToList();

            var unionResult = cheapList.Concat(middleList).Concat(expensiveList).ToList();

            throw new NotImplementedException();
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            var result = customers.Where(co => co.Orders.Count() != 0)
                .Select(c => new
                {
                    City = c.City,
                    AverageIncome = (int)c.Orders.Average(o => o.Total),
                    AverageIntensity = (int)customers.Average(count => count.Orders.Count())
                }).Select(res => (res.City, res.AverageIncome, res.AverageIntensity)).ToList();

            return result;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }
    }
}