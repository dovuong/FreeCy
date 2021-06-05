using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DAO;
using Models.Framework;
namespace Models.DAO
{
    public class ProductCategoryDAO
    {
        FreeCyDB db = null;
        public ProductCategoryDAO()
        {
            db = new FreeCyDB();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name).ToList();
        }
        public Category ViewDetail(int productID)
        {
            return db.Categories.Find(productID);
        }

    }
}
