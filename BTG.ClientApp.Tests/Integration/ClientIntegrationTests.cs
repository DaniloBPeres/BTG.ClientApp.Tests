using BTG.ClientApp.Core.Models;
using BTG.ClientApp.Core.Repositories;
using BTG.ClientApp.Core.Services;
using FluentAssertions;
using Moq;

namespace BTG.ClientApp.Tests.Integration;

public class ClientIntegrationTests
{
    private readonly IClientService _service;

    public ClientIntegrationTests()
    {
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
        mockFileService.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("[]");
        IClientRepository repository = new ClientRepository(mockFileService.Object);
        _service = new ClientService(repository);
    }

    [Fact]
    public void Should_Execute_Full_Client_Lifecycle()
    {
        // Adiciona
        var client = new Client
        {
            Name = "Danilo",
            Lastname = "Teste",
            Age = 33,
            Address = "Rua A"
        };

        _service.Add(client);

        // Consulta
        var saved = _service.GetById(client.Id);
        saved.Should().NotBeNull();
        saved!.FullName.Should().Be("Danilo Teste");

        // Atualiza
        saved.Address = "Rua B";
        _service.Update(saved);

        var updated = _service.GetById(client.Id);
        updated!.Address.Should().Be("Rua B");

        // Exclui
        _service.Delete(client.Id);
        var removed = _service.GetById(client.Id);
        removed.Should().BeNull();
    }
}