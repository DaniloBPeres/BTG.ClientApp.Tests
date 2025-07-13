using BTG.ClientApp.Core.Models;
using BTG.ClientApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.ClientApp.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public ObservableCollection<Client> GetAll()
        {
            return _repository.GetAll();
        }

        public Client? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Add(Client client)
        {
            if (client.Age <= 0)
                throw new ArgumentException("Idade inválida");

            _repository.Add(client);
        }

        public void Update(Client client)
        {
            _repository.Update(client);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}
