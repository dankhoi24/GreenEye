using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class EmployeeDAO
    {

        private BookStoreContext Database = new BookStoreContext();

        public int getId(string username)
        {

            return Database.Employees.SingleOrDefault(e => e.Username == username).EmployeeId;
        }
        public string getName(string username)
        {
            return Database.Employees.SingleOrDefault(e => e.Username == username).Name;
        }
        public string getUser(string username)
        {
            var user = Database.Employees.Where(e => e.Username == username).ToList();           
           if(user.Count() ==1)
            {
                return user[0].Password + " " + user[0].Entropy;
            }
            return "";
        }

        public Employee getByID (int id)
        {
            return Database.Employees.Find(id);
        }

        public ObservableCollection<string> getSuggestUsername()
        {
            var user = Database.Employees.Where(e => e.Remember == true).Select(u => u.Username).ToList();
            return new ObservableCollection<string>(user);
        }

        public void updateUserRemember(string username)
        {
            var user = Database.Employees.SingleOrDefault(e => e.Username == username);
            if(user != null)
            {
                user.Remember = true;
                Database.SaveChanges();
            }
            
        }

              internal ObservableCollection<Employee> getAll()
        {
            var employee = Database.Employees.ToList();

            return new ObservableCollection<Employee>(employee);
        }

        internal void deleteOne(Employee employee)
        {
            employee = Database.Employees.Find(employee.EmployeeId);


            var orders = Database.Orders.Where(x => x.EmployeeId == employee.EmployeeId).ToList();

            foreach (Order order in orders)
            {
                var refunds = Database.Refunds.Where(x => x.OrderId == order.OrderId).ToList();
                Database.Refunds.RemoveRange(refunds);
            }

            Database.Orders.RemoveRange(orders);

            Database.Employees.Remove(employee);
            Database.SaveChanges();
        }

        internal void insertOne(Employee employee)
        {
            Database.Employees.Add(employee);
            Database.SaveChanges();
        }

        internal void updateOne(Employee employee)
        {
            var entity = Database.Employees.Find(employee.EmployeeId);
            if (entity == null)
            {
                return;
            }

            Database.Entry(entity).CurrentValues.SetValues(employee);
            Database.SaveChanges();
        }

        internal int countLikedUsername(string username)
        {
            int r = Database.Employees.Where(x=>x.Username.StartsWith(username)).Count();

            return r;
        }

        internal Employee getByUsername(string username)
        {
            return Database.Employees.SingleOrDefault(x => x.Username == username);
        }
    }
}
