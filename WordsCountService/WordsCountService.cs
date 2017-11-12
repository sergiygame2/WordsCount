using System;
using System.Collections.Generic;
using AppModels;
using DbAdapter;

namespace WordsCount.Service
{
    internal class WordsCountService: IWordsCountService
    {
        public void AddUser(User entity)
        {
            GenericEntityWrapper.AddEntity(entity);
        }

        public void AddTextRequest(TextRequest entity)
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

        public void EditUser(User entity)
        {
            GenericEntityWrapper.EditEntity(entity);
        }

        public void EditTextRequest(TextRequest entity)
        {
            GenericEntityWrapper.EditEntity(entity);
        }
    }
}
