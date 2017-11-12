using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AppModels
{
    public class WordsCountServiceWrapper
    {
        public static void AddEntity(User entity)
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();
                
                client.AddUser(entity);
            }
        }

        public static void AddEntity(TextRequest entity)
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();

                client.AddTextRequest(entity);
            }
        }

        public static List<TextRequest> GetTextRequests(Guid userId)
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();
                return client.GetTextRequests(userId);
            }
        }

        public static bool IsExistingUsername(string username)
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();
                return client.IsExistingUsername(username);
            }
        }

        public static User GetUserByName(string userName)
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();
                return client.GetUserByName(userName);
            }
        }

        public static void EditEntity(User entity)
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();
                client.EditUser(entity);
            }
        }

        public static void EditEntity(TextRequest entity)
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();
                client.EditTextRequest(entity);
            }
        }
    }
}
