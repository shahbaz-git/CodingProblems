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

                int insertPostion = GetInsertPosition(_customers, customer) + 1;
                _customers.Insert(insertPostion, customer);
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Gets the index position after which a new entry can be added.
        /// </summary>
        /// <param name="_customers">The list of exisitng customers.</param>
        /// <param name="customer">The customer to be added.</param>
        /// <returns>The index position after which a new entry can be added.</returns>
        private static int GetInsertPosition(List<Customer> _customers, Customer customer)
        {
            var customerLastNameIndex = (int)customer.LastName.First();
            var lastRecordAsPerLastName = _customers.Where(k => (int)k.LastName.FirstOrDefault() <= customerLastNameIndex).LastOrDefault();
            var indexAsPerLastName = _customers.IndexOf(lastRecordAsPerLastName);

            var customerFirstNameIndex = (int)customer.FirstName.First();
            var lastRecordAsPerFirstName = _customers.Where(k =>
           (int)k.LastName.FirstOrDefault() <= customerFirstNameIndex &&
            (int)k.FirstName.FirstOrDefault() <= customerLastNameIndex).LastOrDefault();

            return _customers.IndexOf(lastRecordAsPerFirstName);
        }
    }
}
