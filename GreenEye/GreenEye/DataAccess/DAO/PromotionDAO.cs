using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class PromotionDAO
    {
        private BookStoreContext Database = new BookStoreContext();

        public ObservableCollection<Promotion> getAll()
        {
            List<Promotion> list = Database.Promotions.ToList();
            return new ObservableCollection<Promotion>(list);
        }

        internal void deleteOne(Promotion selectedPromotion)
        {
            throw new NotImplementedException();
        }

        internal void insertOne(Promotion promotion)
        {
            Database.Promotions.Add(promotion);
            Database.SaveChanges();
        }

        internal void updateOne(Promotion promotion)
        {
            var entity = Database.Promotions.Find(promotion.PromotionId);
            if (entity == null)
            {
                return;
            }

            Database.Entry(entity).CurrentValues.SetValues(promotion);
            Database.SaveChanges();
        }

        internal Promotion getBestDiscount(decimal subtotal)
        {
            List<Promotion> list = Database.Promotions.Where(promotion => promotion.StartDate <= DateTime.Now && promotion.EndDate>=DateTime.Now).ToList();

            Promotion _discount = new Promotion() { PercentDiscount = 0, Name="None discount program" };

            foreach (Promotion discount in list)
            {
                if (_discount.PercentDiscount < discount.PercentDiscount && subtotal > discount.MinPrice)
                    _discount = discount;
            }

            return _discount;
        }
    }
}
