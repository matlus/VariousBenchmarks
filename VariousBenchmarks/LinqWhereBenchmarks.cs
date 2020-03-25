using BenchmarkDotNet.Attributes;
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
        private readonly StringCaseInsensitiveComparer _stringCaseInsensitiveComparer = new StringCaseInsensitiveComparer();

        [GlobalSetup]
        public void GlobalSetup()
        {
            _verifiedCustomerNames = new HashSet<string> { "AAA", "DDD", "EEE" };
            _certifiedCustomerNames = new HashSet<string> { "FFF", "GGG", "HHH" };
            _aListCustomerNames = new HashSet<string> { "III", "JJJ", "KKK" };

            _customers = new[]
            {
                new Customer("aaa", "Laaaa"),
                new Customer("bbb", "Lbbbb"),
                new Customer("ccc", "Lcccc"),
            };
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
                       _verifiedCustomerNames.Contains(c.FirstName, _stringCaseInsensitiveComparer)
                    || _certifiedCustomerNames.Contains(c.FirstName, _stringCaseInsensitiveComparer)
                    || _aListCustomerNames.Contains(c.FirstName, _stringCaseInsensitiveComparer)).ToList();
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
                 let uppercaseFirstName = customer.FirstName.ToUpper()
                 where _verifiedCustomerNames.Contains(uppercaseFirstName) || _certifiedCustomerNames.Contains(uppercaseFirstName) || _aListCustomerNames.Contains(uppercaseFirstName)
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
        public List<Customer> GetWithoutLINQ()
        {
            var matchingCustomers = new List<Customer>();

            foreach (var customer in _customers)
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
        public List<Customer> GetWithoutLINQNoToUpper()
        {
            var matchingCustomers = new List<Customer>();

            foreach (var customer in _customers)
            {
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

    public sealed class StringCaseInsensitiveComparer : IEqualityComparer<string>
    {
        private readonly StringComparer _stringComparer = StringComparer.OrdinalIgnoreCase;

        public bool Equals(string x, string y)
        {
            return _stringComparer.Compare(x, y) == 0;
        }

        public int GetHashCode(string obj)
        {
            var hashCode = 1938039292;
            return hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(obj);
        }
    }
}
