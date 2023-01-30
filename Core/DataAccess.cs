using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DataAccess
    {
        public delegate void AddNewItem();
        public static event AddNewItem AddNewItemEvent;

        public static List<Customer> GetCustomers() => OrderBaseBozyaEntities.GetContext().Customers.ToList().FindAll(x => !x.IsDeleted);
        public static List<Order> GetOrders() => OrderBaseBozyaEntities.GetContext().Orders.ToList();
        public static List<Order> GetOrders(Customer customer) => GetOrders().FindAll(x => x.Customer == customer);
        public static List<Pen> GetPens() => OrderBaseBozyaEntities.GetContext().Pens.ToList().FindAll(x => !x.IsDeleted);
        public static List<PenType> GetPenTypes() => OrderBaseBozyaEntities.GetContext().PenTypes.ToList();
        public static List<Company> GetCompanies() => OrderBaseBozyaEntities.GetContext().Companies.ToList();

        public static Customer LoginCustomer(string login, string password) => GetCustomers().FirstOrDefault(x => x.User.Login == login && x.User.Password == password);

        public static bool IsLoginBusy(string login) => GetCustomers().Any(x => x.User.Login == login);

        public static void SaveCustomer(Customer customer)
        {
            if (customer.Id == 0)
                OrderBaseBozyaEntities.GetContext().Customers.Add(customer);

            OrderBaseBozyaEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }

        public static void DeleteCustomer(Customer customer)
        {
            customer.IsDeleted = true;
            SaveCustomer(customer);
        }

        public static void SavePen(Pen pen)
        {
            if (pen.Id == 0)
                OrderBaseBozyaEntities.GetContext().Pens.Add(pen);

            OrderBaseBozyaEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }

        public static void DeletePen(Pen pen)
        {
            pen.IsDeleted = true;
            SavePen(pen);
        }

        public static void SaveOrder(Order order)
        {
            if (order.Id == 0)
                OrderBaseBozyaEntities.GetContext().Orders.Add(order);

            OrderBaseBozyaEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }
    }
}
