using SaleManagementLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementLibrary.Repository
{
    public interface IProductRepository
    {
        // Create
        void Add(Product product);

        // Read
        Product GetById(int id);
        IEnumerable<Product> GetAll();

        // Update
        void Update(Product product);

        // Delete
        void Delete(int id);
    }

}
