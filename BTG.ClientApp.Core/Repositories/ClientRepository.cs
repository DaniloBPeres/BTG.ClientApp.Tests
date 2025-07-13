using BTG.ClientApp.Core.Models;
using BTG.ClientApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BTG.ClientApp.Core.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IFileService _fileService;
        private readonly string _filePath;
        private readonly ObservableCollection<Client> _clients;


        public ClientRepository(IFileService fileService)
        {
            _fileService = fileService;
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _filePath = Path.Combine(folder, "clients.json");

            _clients = new ObservableCollection<Client>();
            Load();
        }

        public ObservableCollection<Client> GetAll() => _clients;

        public Client? GetById(Guid id) =>
            _clients.FirstOrDefault(c => c.Id == id);

        public void Add(Client client)
        {
            _clients.Add(client);
            Save();
        }

        public void Update(Client client)
        {
            var existing = GetById(client.Id);

            if (existing != null)
            {
                existing.Name = client.Name;
                existing.Lastname = client.Lastname;
                existing.Age = client.Age;
                existing.Address = client.Address;
                Save();
            }
        }

        public void Delete(Guid id) 
        {
            var client = GetById(id);

            if(client != null)
            {
                _clients.Remove(client);
                Save();
            }
        }

        public void Load()
        {
            if (_fileService.Exists(_filePath))
            {
                try
                {
                    string jsonString = _fileService.ReadAllText(_filePath);
                    var loadedClients = JsonSerializer.Deserialize<List<Client>>(jsonString);
                    if (loadedClients != null)
                    {
                        _clients.Clear();
                        foreach (var client in loadedClients)
                            _clients.Add(client);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar clientes do arquivo JSON: {ex.Message}");
                }
            }
        }

        public void Save()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_clients, options);
                _fileService.WriteAllText(_filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar clientes no arquivo JSON: {ex.Message}");
            }
        }
    }
}
