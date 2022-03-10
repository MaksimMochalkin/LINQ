using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit);
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
            return customers.Where(customer => customer.Orders.Any(order => order.Total > limit)).ToList();
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
            var result = new List<(decimal category, IEnumerable<Product> products)>();
            var cheapCategory = products.Select(product =>
                (
                   cheap,
                   products.Select(p => p).Where(p => p.UnitPrice == products.Min(p => p.UnitPrice)).AsEnumerable()
            )).FirstOrDefault();

            var middleCategory = products.Select(c =>
            (
               middle,
               products.Where(p => p.UnitPrice != products.Min(p => p.UnitPrice)
                && p.UnitPrice != products.Max(p => p.UnitPrice)).AsEnumerable()
            )).FirstOrDefault();

            var expensiveCategory = products.Select(c =>
            (
                expensive,
                products.Where(p => p.UnitPrice == products.Max(p => p.UnitPrice)).AsEnumerable()
            )).FirstOrDefault();

            result.Add(cheapCategory);
            result.Add(middleCategory);
            result.Add(expensiveCategory);

            return result;
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            return customers.GroupBy(c => c.City)
                .Select(g =>
                (
                    g.Key,
                    Convert.ToInt32(g.Average(c => c.Orders.Sum(o => o.Total))),
                    Convert.ToInt32(g.Average(c => c.Orders.Length))
                ));
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var sortedList = suppliers.Select(s => s.Country).OrderBy(s => s.Length).ThenBy(s => s).Distinct().ToList();

            var sb = new StringBuilder();
            sortedList.ForEach(s => sb.Append(s));
            
            return sb.ToString();
        }
    }
}