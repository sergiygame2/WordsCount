using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AppModels
{
    public class WordsCountServiceWrapper
    {
        public static void AddEntity<T>(T entity) where T : class
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();
                client.AddEntity(entity);
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

        public static void EditEntity<T>(T entity) where T : class
        {
            using (var myChannelFactory = new ChannelFactory<IWordsCountService>("Server"))
            {
                var client = myChannelFactory.CreateChannel();
                client.EditEntity(entity);
            }
        }
    }
}
