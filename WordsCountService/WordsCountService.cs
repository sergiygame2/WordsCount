using System;
using System.Collections.Generic;
using AppModels;
using DbAdapter;

namespace WordsCount.Service
{
    internal class WordsCountService: IWordsCountService
    {
        public void AddEntity<T>(T entity) where T : class
        {
            GenericEntityWrapper.AddEntity(entity);
        }

        public List<TextRequest> GetTextRequests(Guid userId)
        {
            return GenericEntityWrapper.GetTextRequests(userId);
        }

        public bool IsExistingUsername(string username)
        {
            return GenericEntityWrapper.IsExistingUsername(username);
        }

        public User GetUserByName(string userName)
        {
            return GenericEntityWrapper.GetUserByName(userName);
        }

        public void EditEntity<T>(T entity) where T : class
        {
            GenericEntityWrapper.EditEntity(entity);
        }
    }
}
