using BTG.ClientApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.ClientApp.Core.Services
{
    public interface IClientService
    {
        ObservableCollection<Client> GetAll();
        Client? GetById(Guid id);
        void Add(Client client);
        void Update(Client client);
        void Delete(Guid id);
    }
}
