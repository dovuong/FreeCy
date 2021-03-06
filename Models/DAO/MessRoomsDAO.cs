using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class MessRoomsDAO
    {
        FreeCyDB db = null;
        public MessRoomsDAO()
        {
            db = new FreeCyDB();
        }

        public void Update(string idroom)
        {
            var a=db.MessRooms.Find(idroom);
            a.UpdatedAt = DateTime.Now;
            db.SaveChanges();
        }

        public void AddMess(Message a)
        {
            db.Messages.Add(a);
            db.SaveChanges();
        }


        public List<Message> ListAllMess(string id_Room)
        {

                return db.Messages.Where(x => x.ID_MessRoom == id_Room).ToList();

        }
        public List<MessList> ListMess(int idus)
        {
            List<MessList> lime = new List<MessList>();
            List<int> listi = new List<int>();
            foreach(var i in db.Conversations.OrderByDescending(x => x.ID_Convers).Where(x => x.ID_User == idus).ToList())
            {
                lime.Add(new MessList { 
                    ID_User=  GetIdByRoom(i.ID_MessRoom, idus),
                    UserName=UserDAO.Instance.GetUserNameById(GetIdByRoom(i.ID_MessRoom, idus)),
                    UpdateTime= GetTimeUpdate(i.ID_MessRoom)
                });
            }
            //return db.Messages.Where(x => x.ID_MessRoom == id_Room).ToList();
            return lime;
        }

        public DateTime GetTimeUpdate(string idroom)
        {
            var r = db.MessRooms.Where(x => x.ID_MessRoom == idroom).FirstOrDefault();
            DateTime a = r.UpdatedAt;
            return a;
        }
        public int TotalMess(string id_Room)
        {

            return db.Messages.Count(x => x.ID_MessRoom == id_Room);

        }

        public int GetIdByRoom(string id_Room,int id)
        {
            var a = db.Conversations.Where(x => x.ID_MessRoom == id_Room && x.ID_User != id).FirstOrDefault();

            return a.ID_User;

        }

        public string GetIDRoom(int id1, int id2)
        {
            var a = db.Conversations.Where(x => x.ID_User == id1).ToList();
            var b = db.Conversations.Where(x => x.ID_User == id2).ToList();
            //if (a != null && b != null)
            //{
                foreach (var i in a)
                    { foreach (var j in b)
                        {
                            if (i.ID_MessRoom == j.ID_MessRoom)
                                 return i.ID_MessRoom;
                        }
                    }
            MessRoom ms = new MessRoom();
            string sss = Guid.NewGuid().ToString();
            ms.ID_MessRoom = sss;
            ms.CreatedAt = DateTime.Now;
            ms.UpdatedAt = DateTime.Now;

            db.MessRooms.Add(ms);
            db.SaveChanges();
            
            Conversation con1 = new Conversation();            
            con1.ID_User = id1;
            con1.ID_MessRoom = sss;
            con1.CreatedAt = DateTime.Now;            
            db.Conversations.Add(con1);
            db.SaveChanges();

            Conversation con2 = new Conversation();
            con2.ID_User = id2;
            con2.ID_MessRoom = sss;
            con2.CreatedAt = DateTime.Now;
            db.Conversations.Add(con2);
            db.SaveChanges();


            return sss;

           

        }
    }
}
