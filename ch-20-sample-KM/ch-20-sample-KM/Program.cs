using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ch_20_sample_KM
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer jim = new Customer("Jim Carrey", 200);
            Customer matt = new Customer("Matt Thiessen", 150);

            Console.WriteLine(jim.CompareTo(matt).ToString());
            Console.WriteLine(jim < matt);
            Console.WriteLine(jim <= matt);
            Console.WriteLine(jim > matt);
            Console.WriteLine(jim >= matt);

            var customers = new List<Customer>
            {
                new Customer("Jim Carrey", 200),
                new Customer("Matt Thiessen", 150),
                new Customer("Takeshi Hosomi", 300),
                new Customer("Oliver Sykes", 100),
            };

            Action<List<Customer>> action = (list) =>
            {
                Console.WriteLine("---");
                foreach (var item in list)
                {
                    Console.WriteLine($"{item.name,-20}{item.revenue}");
                }
                Console.WriteLine("---");
            };

            action(customers);

            customers.Sort(Customer.CompareByRevenue);
            action(customers);
        }
    }

    public struct Customer : IComparable<Customer>, IComparable
    {
        public readonly string name;
        public double revenue;

        public Customer(string name, double revenue)
        {
            this.name = name;
            this.revenue = revenue;
        }

        public int CompareTo([AllowNull] Customer other)
        {
            // retur 1  : name > other.name
            // retur 0  : name = other.name
            // retur -1 : name < other.name

            return name.CompareTo(other.name);
        }

        public int CompareTo(object obj)
        {
            if(!(obj is Customer))
            {
                throw new ArgumentException("引数はCustomer型ではありません", "obj");
            }
            Customer otherCustomer = (Customer)obj;
            return this.CompareTo(otherCustomer);
        }

        public static bool operator <(Customer left, Customer right) => left.CompareTo(right) < 0;
        public static bool operator <=(Customer left, Customer right) => left.CompareTo(right) <= 0;
        public static bool operator >(Customer left, Customer right) => left.CompareTo(right) > 0;
        public static bool operator >=(Customer left, Customer right) => left.CompareTo(right) >= 0;


        private static Lazy<RevenueComparer> revComp = new Lazy<RevenueComparer>(() => new RevenueComparer());
        public static IComparer<Customer> RevenueCompare => revComp.Value;
        public static Comparison<Customer> CompareByRevenue => (left, right) =>
        {
            return left.revenue.CompareTo(right.revenue);
        };

        private class RevenueComparer : IComparer<Customer>
        {
            public int Compare([AllowNull] Customer left, [AllowNull] Customer right)
            {
                return left.revenue.CompareTo(right.revenue);
            }
        }
    }
}
