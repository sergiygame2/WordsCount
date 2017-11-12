using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppServices.Services;

namespace AppModels
{
    [ServiceContract]
    public interface IWordsCountService
    {
        [OperationContract]
        void AddUser(User entity);

        [OperationContract]
        void AddTextRequest(TextRequest entity);

        [OperationContract]
        List<TextRequest> GetTextRequests(Guid userId);

        [OperationContract]
        bool IsExistingUsername(string username);

        [OperationContract]
        User GetUserByName(string userName);

        [OperationContract]
        void EditUser(User entity);

        [OperationContract]
        void EditTextRequest(TextRequest entity);
    }
}
