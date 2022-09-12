using CustomerApi.Data;
using CustomerApi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Services
{
    /// <summary>
    /// The CustomerService class.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Gets the list of customers.
        /// </summary>
        /// <returns>Returns the list of customers.</returns>
        public async Task<List<Customer>> GetCustomers()
        {
            return await Task.FromResult(CustomerDataContext.Customers);
        }

        /// <summary>
        /// Adds customers.
        /// </summary>
        /// <param name="customers">The list of customers to be added.</param>
        /// <returns>The insert response.</returns>
        public async Task<int> AddCustomers(Customer[] customers)
        {
            int result = 1;
            var _customers = CustomerDataContext.Customers;

            foreach (var customer in customers)
            {
                if (_customers.Any(k => k.Id == customer.Id))
                {
                    result = 0;
                    continue;
                }

                var custLnameIndex = (int)customer.LastName.First();
                var customerWLN = _customers.Where(k => (int)k.LastName.FirstOrDefault() <= custLnameIndex).LastOrDefault();
                var lastNameIndex = _customers.IndexOf(customerWLN);

                var custFnameIndex = (int)customer.FirstName.First();
                var customerWFN = _customers.Where(k =>
               (int)k.LastName.FirstOrDefault() <= custLnameIndex &&
                (int)k.FirstName.FirstOrDefault() <= custFnameIndex).LastOrDefault();
                var firstNameIndex = _customers.IndexOf(customerWFN);
                _customers.Insert(firstNameIndex + 1, customer);
            }

            return await Task.FromResult(result);
        }
    }
}
