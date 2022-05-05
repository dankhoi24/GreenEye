using GreenEye.DataAccess.Domain;
using GreenEye.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    class DebitBookDAO
    {

         BookStoreContext Database = new BookStoreContext();



         public void init()
        {
            // init customers
            var customers = Database.Customers.Select(x => new { x.CustomerId }).ToList();
           


            // check date
            DateTime date = DateTime.Now;
            foreach( var entity in customers)
            {
               var debit = Database.DebitBooks.SingleOrDefault(x => ( (x.CustomerId == entity.CustomerId) && ( x.Date.Month == date.Month) &&( x.Date.Year == date.Year)));
                if(debit == null)
                {
                    // add debit
                    Database.DebitBooks.Add(new Domain.DebitBook()
                    {




                    });
                    Database.SaveChanges();
                }
                else
                {
                    // Do nothing
                }
            }
        }




        public List<ReportBill> getDate(DateTime date)
        {
            var data = Database.DebitBooks.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year);
            var customer = data.Join(Database.Customers, d => d.CustomerId, c => c.CustomerId, 
                (d, c) => new ReportBill { Name = c.Name, Init=d.BeginDebit, Incurred = d.CurrentDebit - d.BeginDebit, Final =  d.CurrentDebit});

            return customer.ToList();
        }

        internal DebitBook getCurrentDebitBook(int customerId)
        {
           
           var debitBook = Database.DebitBooks.Where(x => x.CustomerId == customerId).OrderByDescending(x=> x.Date).FirstOrDefault();

            if (debitBook == null)
                return new DebitBook()
                {
                    DebitBookId = 0,
                    Date = DateTime.Now,
                    BeginDebit = 0,
                    CurrentDebit = 0,
                    CustomerId = customerId
                };
            else if (debitBook.Date.Year != DateTime.Now.Year || debitBook.Date.Month != DateTime.Now.Month)
            {
                return new DebitBook() 
                { 
                    DebitBookId = 0, 
                    Date=DateTime.Now, 
                    BeginDebit=debitBook.CurrentDebit, 
                    CurrentDebit=debitBook.CurrentDebit, 
                    CustomerId=customerId 
                };
            }

           return debitBook;
        }

        internal void increaseCurrentDebit(Customer selectedSearchCustomer, DebitBook currentDebitBook)
        {
            if (currentDebitBook.DebitBookId == 0)
            {
                Database.DebitBooks.Add(currentDebitBook);
            }
            else
            {
                var _curr = Database.DebitBooks.Find(currentDebitBook.DebitBookId);

                Database.Entry(_curr).CurrentValues.SetValues(currentDebitBook);
            }
            Database.SaveChanges();
        }



        internal void updateOrInsertDebit(DebitBook currentDebitBook)
        {
            if (currentDebitBook.DebitBookId == 0)
            {
                Database.DebitBooks.Add(currentDebitBook);
            }
            else
            {
                var _curr = Database.DebitBooks.Find(currentDebitBook.DebitBookId);

                Database.Entry(_curr).CurrentValues.SetValues(currentDebitBook);
            }
            Database.SaveChanges();
        }











        internal void decreaseCurrentDebit(Customer oldCustomer, decimal total)
        {
            var _curr = Database.DebitBooks.Find();
        }





    }
}
