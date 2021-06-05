using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DAO;
using Models.Framework;
using PagedList;

namespace Models.DAO
{
    public class ProductDAO
    {
        FreeCyDB db = null;
        public ProductDAO()
        {
            db = new FreeCyDB();
        }
        public List<Product> ListProduct(int top , int soTrang ,User user,string sort)
        {
            switch (sort)
            {
                case "recent":
                    return db.Products.OrderByDescending(x => x.CreatedAt).Skip((soTrang - 1) * top).Take(top).ToList();
                   
                case "name":
                    return db.Products.OrderByDescending(x => x.Name).Skip((soTrang - 1) * top).Take(top).ToList();
                    
                case "price":
                    return db.Products.OrderByDescending(x => x.Price).Skip((soTrang - 1) * top).Take(top).ToList();
                    
            }
            return db.Products.OrderByDescending(x => x.ID_Category).Skip((soTrang-1)*top).Take(top).ToList();
        }

        public int GetTotalRecords()
        {
             return db.Products.OrderByDescending(x => x.ID_Category).Count() ;
        }

        public List<User> ListUser(int top)
        {
            return db.Users.OrderByDescending(x => x.CreateAt).Take(top).ToList();
        }
        public Product ViewDetail(int productID)
        {
            return db.Products.Find(productID);
        }
        public IEnumerable<Product> ListAllPaging(int page ,int pageSize )
        {
            return db.Products.OrderByDescending(x=>x.CreatedAt).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns

    }
}
