﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DataAccess
    {
        public static List<Customer> GetCustomers() => OrderBaseBozyaEntities.GetContext().Customers.ToList();

        public static Customer LoginCustomer(string login, string password) => GetCustomers().FirstOrDefault(x => x.User.Login == login && x.User.Password == password);

        public static bool IsLoginBusy(string login) => GetCustomers().Any(x => x.User.Login == login);

        public static void SaveCustomer(Customer customer)
        {
            if (customer.Id == 0)
                OrderBaseBozyaEntities.GetContext().Customers.Add(customer);

            OrderBaseBozyaEntities.GetContext().SaveChanges();
        }
    }
}