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

        public string getUser(string username)
        {
            var user = Database.Employees.Where(e => e.Username == username).ToList();           
           if(user.Count() ==1)
            {
                return user[0].Password + " " + user[0].Entropy;
            }
            return "";
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
    }
}
