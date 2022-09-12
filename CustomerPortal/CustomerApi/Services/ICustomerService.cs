using CustomerApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Services
{
    /// <summary>
    /// The ICustomerService interface.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Gets the list of customers.
        /// </summary>
        /// <returns>Returns the list of customers.</returns>
        Task<int> AddCustomers(Customer[] customers);

        /// <summary>
        /// Adds customers.
        /// </summary>
        /// <param name="customers">The list of customers to be added.</param>
        /// <returns>The insert response.</returns>
        Task<List<Customer>> GetCustomers();
    }
}