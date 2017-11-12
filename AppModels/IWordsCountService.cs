using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AppModels
{
    [ServiceContract]
    public interface IWordsCountService
    {
        [OperationContract]
        void AddEntity<T>(T entity) where T : class;

        [OperationContract]
        List<TextRequest> GetTextRequests(Guid userId);

        [OperationContract]
        bool IsExistingUsername(string username);

        [OperationContract]
        User GetUserByName(string userName);

        [OperationContract]
        void EditEntity<T>(T entity) where T : class;
    }
}
