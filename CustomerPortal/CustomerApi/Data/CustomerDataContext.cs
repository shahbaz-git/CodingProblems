using CustomerApi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApi.Data
{
    public static class CustomerDataContext
    {
        public static List<Customer> Customers { get; set; } = Enumerable.Empty<Customer>().ToList();
    }
}
