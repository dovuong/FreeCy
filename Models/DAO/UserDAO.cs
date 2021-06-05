using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models.DAO
{
    public class UserDAO
    {
        FreeCyDB db = null;
        static long _Id;


        private static UserDAO _Instance;
        public static UserDAO Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UserDAO();
                }
                return _Instance;
            }
            set
            {
                ;
            }
        }

        public UserDAO()
        {
            db = new FreeCyDB();
        }

        public int SaveChanges()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckUserEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
        public int Insert(User entity)
        {

            db.Users.Add(entity);
            SaveChanges();
            return entity.ID_User;
        }
        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.Users.Add(entity);
                SaveChanges();
                return entity.ID_User;
            }
            else
            {
                return user.ID_User;
            }

        }
        public void ValidateOnSaveEnabled()
        {
            db.Configuration.ValidateOnSaveEnabled = false;
        }

        public bool CheckId(int idd)
        {
            return db.Users.Count(x => x.ID_User == idd) > 0;
        }
        public int Login(string UserName, string Password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == UserName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == Password)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public String GetNameById(int id)
        {
            return (db.Users.SingleOrDefault(x => x.ID_User == id)).Name;
        }
        public void SaveUserId(long id)
        {
            _Id = id;
        }
        public long GetUserId()
        {
            return _Id;
        }


        public User ViewDetails(long id)
        {
            return db.Users.Find(id);
        }
        public User GetUserByEmail(string email)
        {
            return db.Users.Where(x => x.Email == email).FirstOrDefault();
        }
        public User GetUserByResetCode(string code)
        {
            User user = new User();
            List<User> list = db.Users.ToList();
            foreach (User item in list)
            {
                if (item.ResetPasswordCode == code)
                {
                    user = item;
                }
            }
            return user;
        }

        public List<User> ListUsers(int top, int soTrang,User user)
        {
            return db.Users.Where(x => x.Status == true).OrderByDescending(x => x.ID_User ).Skip((soTrang - 1) * top).Take(top).ToList();
        }


        public int GetTotalRecords()
        {
            return db.Users.OrderByDescending(x => x.ID_User).Count();
        }


    }
}

