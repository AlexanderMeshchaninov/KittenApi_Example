using System.Collections.Generic;

namespace AuthServiceAbstraction.AuthServiceInterfaces
{
    public interface IClientDictionaryInterface<T> 
        where T : class
    {
        public Dictionary<string, T> ClientDictionary(string email, string password);
    }
}