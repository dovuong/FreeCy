//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNet.SignalR;
//using Models.Framework;
//using Models.DAO;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using AutoMapper;
//using System.Web.Script.Serialization;

//namespace FreeCy.Hubs
//{
//    [Authorize]
//    public class ChatHub : Hub
//    {
//        #region Properties
//        /// <summary>
//        /// List of online users
//        /// </summary>
//        public readonly static List<User> _Connections = new List<User>();

//        /// <summary>
//        /// List of all users
//        /// </summary>
//        public readonly static List<User> _Users = new List<User>();

//        /// <summary>
//        /// List of available chat rooms
//        /// </summary>
   

//        /// <summary>
//        /// Mapping SignalR connections to application users.
//        /// (We don't want to share connectionId)
//        /// </summary>
//        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
//        #endregion

//        public int Send( int fromUserId, int toUserId, string message)
//        {
//            var MesDao = new MessRoomsDAO();
//            var id = MesDao.GetIDRoom(fromUserId, toUserId);

//            return SendPrivate(message, fromUserId, toUserId,id);
           
//        }

//        public int SendPrivate(string message, int fromUserId, int toUserId,string idroom)
//        {
//            try
//            {
//                using (var db = new FreeCyDB())
//                {
//                    var userSender = db.Users.Where(u => u.ID_User == fromUserId).FirstOrDefault();
//                    var userReceiver = db.Users.Where(u => u.ID_User == toUserId).FirstOrDefault();

//                    // Create and save message in database
//                    Message msg = new Message()
//                    {
//                        Contents = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
//                        CreatedAt = DateTime.Now,
//                        ID_User = userSender.ID_User,
//                        ID_MessRoom = idroom,
//                    };
//                    db.Messages.Add(msg);
//                    db.SaveChanges();
//                    int idMess = msg.ID_Mess; 
                    
//                    try
//                    {
//                        string userId;

//                        if (_ConnectionsMap.TryGetValue(userReceiver.UserName, out userId))
//                        {
//                            // Who is the sender;
//                            var sender = _Connections.Where(u => u.UserName == IdentityName).First();

//                            // Send the message
//                            Clients.Client(userId).newMessage(msg);
//                            Clients.Caller.newMessage(msg);
//                        }
//                        else
//                        {
//                            Clients.Caller.newMessage(msg);
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        Clients.Caller.newMessage(msg);
//                    }

//                    return idMess;
//                }
//            }
//            catch (Exception)
//            {
//                Clients.Caller.onError("Message not send!");
//            }
//            return 0;
//        }


//        public IEnumerable<User> GetAllUsers()
//        {
//            List<User> _Users = new List<User>();
//            using (var db = new FreeCyDB())
//            {
//                //First run?
//                if (_Users.Count == 0)
//                {
//                    foreach (var user in db.Users)
//                    {
                        
//                        _Users.Add(user);
//                    }
//                }
//            }

//            return _Users.Where(u => u.UserName != IdentityName).ToList();
//        }

//        #region OnConnected/OnDisconnected
//        public override Task OnConnected()
//        {
//            using (var db = new FreeCyDB())
//            {
//                // First run?
//                if (_Users.Count == 0)
//                {
//                    foreach (ApplicationUser user in db.Users.ToList())
//                    {
//                        UserViewModel userViewModel = Mapper.Map<ApplicationUser, UserViewModel>(user);
//                        _Users.Add(userViewModel);
//                    }
//                }
//            }


//            using (var db = new ApplicationDbContext())
//            {
//                try
//                {
//                    var user = db.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();

//                    var userViewModel = Mapper.Map<ApplicationUser, UserViewModel>(user);
//                    userViewModel.Device = GetDevice();
//                    userViewModel.CurrentRoomId = 0;
//                    userViewModel.CurrentRoomName = "";

//                    var tempUser = _Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
//                    _Users.Remove(tempUser);

//                    _Users.Add(userViewModel);

//                    Clients.All.UpdateUser(userViewModel);
//                    _Connections.Add(userViewModel);
//                    _ConnectionsMap.Add(IdentityName, Context.ConnectionId);

//                    Clients.Caller.getProfileInfo(user.Id, user.UserName, user.DisplayName, user.Avatar);
//                }
//                catch (Exception ex)
//                {
//                    Clients.Caller.onError("OnConnected:" + ex.Message);
//                }
//            }

//            return base.OnConnected();
//        }

//        public override Task OnDisconnected(bool stopCalled)
//        {
            
//            try
//            {
//                var tempUser = _Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
//                _Users.Remove(tempUser);

//                tempUser.Device = "";
//                _Users.Add(tempUser);
//                Clients.All.UpdateUser(tempUser);
//                var user = _Connections.Where(u => u.UserName == IdentityName).FirstOrDefault();
//                _Connections.Remove(user);

//                // Tell other users to remove you from their list
//                Clients.OthersInGroup(user.CurrentRoomId.ToString()).removeUser(user);

//                // Remove mapping
//                _ConnectionsMap.Remove(user.UserName);
                
//            }
//            catch (Exception ex)
//            {
//                Clients.Caller.onError("OnDisconnected: " + ex.Message);
//            }

//            return base.OnDisconnected(stopCalled);
//        }

//        public override Task OnReconnected()
//        {
//            //var tempUser = _Users.Where(u => u.Username == IdentityName).FirstOrDefault();
//            //_Users.Remove(tempUser);

//            var user = _Connections.Where(u => u.UserName == IdentityName).FirstOrDefault();
//            Clients.Caller.getProfileInfo(user.Id, user.DisplayName, user.Avatar);


//            //_Users.Add(user);
//            return base.OnReconnected();
//        }
//        #endregion

//        private string IdentityName
//        {
//            get { return Context.User.Identity.Name; }
//        }

//        private string GetDevice()
//        {
//            string device = Context.Headers.Get("Device");

//            if (device != null && (device.Equals("Desktop") || device.Equals("Mobile")))
//                return device;

//            return "Web";
//        }
//    }
//}