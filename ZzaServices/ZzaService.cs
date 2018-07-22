using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using Zza.Entities;

namespace ZzaServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class ZzaService : IZaaService, IDisposable
    {
        readonly ZzaDbContext _Context = new ZzaDbContext();

        public void Dispose()
        {
            _Context.Dispose();
        }

        public List<Customer> GetCustomers()
        {
            return _Context.Customers.ToList();
        }

        public List<Product> GetProducts()
        {
            return _Context.Products.ToList();
        }

        [OperationBehavior(TransactionScopeRequired =true)]
        public void SubmitOrder(Order order)
        {
            _Context.Orders.Add(order);
            order.OrderItems.ForEach(x => _Context.OrderItems.Add(x));
            _Context.SaveChanges();
        }
    }
}
