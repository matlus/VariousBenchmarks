using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser, RankColumn]
    public class LinqWhereBenchMarks
    {
        private HashSet<string> _verifiedCustomerNames;
        private HashSet<string> _certifiedCustomerNames;
        private HashSet<string> _aListCustomerNames;
        private Customer[] _customers;
        private IEnumerable<Customer> _customersEnumerable;
        private readonly StringComparer _stringComparerOrdinalIgnoreCase = StringComparer.OrdinalIgnoreCase;        

        [GlobalSetup]
        public void GlobalSetup()
        {
            _verifiedCustomerNames = new HashSet<string> { "AAA", "DDD", "EEE" };
            _certifiedCustomerNames = new HashSet<string> { "BBB", "GGG", "HHH", "III", "LLL", "MMM", "NNN" };
            _aListCustomerNames = new HashSet<string> { "CCC", "JJJ", "KKK" };

            _customers = new[]
            {
                new Customer("aaa", "Laaaa"),
                new Customer("bbb", "Lbbbb"),
                new Customer("ccc", "Lcccc"),
                new Customer("ooo", "Loooo"),
                new Customer("ppp", "Lpppp"),
                new Customer("qqq", "Lqqqq"),
            };

            _customersEnumerable = _customers.ToList();
            var customersList = new List<Customer>();
            customersList.AddRange(_customers);
            _customersEnumerable = customersList;
        }

        [Benchmark(Baseline = true)]
        public List<Customer> GetUsingMultipleToUpper()
        {
            return
                _customers
                    .Where(c =>
                       _verifiedCustomerNames.Contains(c.FirstName.ToUpper())
                    || _certifiedCustomerNames.Contains(c.FirstName.ToUpper())
                    || _aListCustomerNames.Contains(c.FirstName.ToUpper())).ToList();
        }

        [Benchmark]
        public List<Customer> GetUsingMultipleEqualityComparer()
        {
            return
                _customers
                    .Where(c =>
                       _verifiedCustomerNames.Contains(c.FirstName, _stringComparerOrdinalIgnoreCase)
                    || _certifiedCustomerNames.Contains(c.FirstName, _stringComparerOrdinalIgnoreCase)
                    || _aListCustomerNames.Contains(c.FirstName, _stringComparerOrdinalIgnoreCase)).ToList();
        }

        [Benchmark]
        public List<Customer> GetUsingMergeSetsAndSingleToUpper()
        {
            _verifiedCustomerNames.UnionWith(_certifiedCustomerNames);
            _verifiedCustomerNames.UnionWith(_aListCustomerNames);

            return
                _customers
                    .Where(c => _verifiedCustomerNames.Contains(c.FirstName.ToUpper())).ToList();
        }

        [Benchmark]
        public List<Customer> GetUsingConcatAndMultipleToUpper()
        {
            return
                _customers.Where(c => _verifiedCustomerNames
                    .Concat(_certifiedCustomerNames)
                    .Concat(_aListCustomerNames)
                    .Contains(c.FirstName.ToUpper())).ToList();
        }

        [Benchmark]
        public List<Customer> GetUsingQueryExpLetAndOneToUpper()
        {
            return
                (from customer in _customers
                 let firstNameUppered = customer.FirstName.ToUpper()
                 where _verifiedCustomerNames.Contains(firstNameUppered) || _certifiedCustomerNames.Contains(firstNameUppered) || _aListCustomerNames.Contains(firstNameUppered)
                 select customer).ToList();
        }

        [Benchmark]
        public List<Customer> GetUsingLocalvariableAssignmentForToUpper()
        {
            return (_customers
                .Where(c =>
                {
                    var firstNameUppered = c.FirstName.ToUpper();
                    return _verifiedCustomerNames.Contains(firstNameUppered)
                    || _certifiedCustomerNames.Contains(firstNameUppered)
                    || _aListCustomerNames.Contains(firstNameUppered);
                })).ToList();

        }

        [Benchmark]
        public List<Customer> GetWithoutLINQForEach()
        {
            var matchingCustomers = new List<Customer>();

            foreach (var customer in _customersEnumerable)
            {
                var firstNameUppered = customer.FirstName.ToUpper();
                if (_verifiedCustomerNames.Contains(firstNameUppered)
                    || _certifiedCustomerNames.Contains(firstNameUppered)
                    || _aListCustomerNames.Contains(firstNameUppered))
                    matchingCustomers.Add(customer);
            }

            return matchingCustomers;
        }

        [Benchmark]
        public List<Customer> GetWithoutLINQForLoop()
        {
            var matchingCustomers = new List<Customer>();

            for (int i = 0; i < _customers.Length; i++)
            {
                var customer = _customers[i];
                var firstNameUppered = customer.FirstName.ToUpper();
                if (_verifiedCustomerNames.Contains(firstNameUppered)
                    || _certifiedCustomerNames.Contains(firstNameUppered)
                    || _aListCustomerNames.Contains(firstNameUppered))
                    matchingCustomers.Add(customer);
            }

            return matchingCustomers;
        }

        /// <summary>
        /// This method exists only to get  a sense of the 
        /// overhead/cost of doing the ToUpper()
        /// </summary>
        /// <returns></returns>
        [Benchmark]
        public List<Customer> GetWithoutLINQNoToUpperForEach()
        {
            var matchingCustomers = new List<Customer>();

            foreach (var customer in _customersEnumerable)
            {
                if (_verifiedCustomerNames.Contains(customer.FirstName)
                    || _certifiedCustomerNames.Contains(customer.FirstName)
                    || _aListCustomerNames.Contains(customer.FirstName))
                    matchingCustomers.Add(customer);
            }

            return matchingCustomers;
        }

        /// <summary>
        /// This method exists only to get  a sense of the 
        /// overhead/cost of doing the ToUpper()
        /// </summary>
        /// <returns></returns>
        [Benchmark]        
        public List<Customer> GetWithoutLINQNoToUpperForLoop()
        {
            var matchingCustomers = new List<Customer>();

            for (int i = 0; i < _customers.Length; i++)
            {
                var customer = _customers[i];
                if (_verifiedCustomerNames.Contains(customer.FirstName)
                    || _certifiedCustomerNames.Contains(customer.FirstName)
                    || _aListCustomerNames.Contains(customer.FirstName))
                    matchingCustomers.Add(customer);
            }

            return matchingCustomers;
        }
    }

    public sealed class Customer
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
