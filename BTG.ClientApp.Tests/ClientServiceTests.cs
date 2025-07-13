using BTG.ClientApp.Core.Models;
using BTG.ClientApp.Core.Repositories;
using BTG.ClientApp.Core.Services;
using FluentAssertions;
using Moq;

namespace BTG.ClientApp.Tests.Services;

public class ClientServiceTests
{
    private readonly IClientService _service;

    public ClientServiceTests()
    {
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
        mockFileService.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("[]");

        IClientRepository repository = new ClientRepository(mockFileService.Object); // In-memory
        _service = new ClientService(repository);
    }

    [Fact]
    public void Should_Add_Client()
    {
        var client = new Client
        {
            Name = "Carlos",
            Lastname = "Silva",
            Age = 35,
            Address = "Rua Teste"
        };

        _service.Add(client);

        var result = _service.GetById(client.Id);
        result.Should().NotBeNull();
        result!.FullName.Should().Be("Carlos Silva");
    }

    [Fact]
    public void Should_Update_Client()
    {
        var client = new Client
        {
            Name = "Ana",
            Lastname = "Maria",
            Age = 29,
            Address = "Rua A"
        };
        _service.Add(client);

        client.Address = "Rua B";
        _service.Update(client);

        var updated = _service.GetById(client.Id);
        updated.Should().NotBeNull();
        updated!.Address.Should().Be("Rua B");
    }

    [Fact]
    public void Should_Delete_Client()
    {
        var client = new Client
        {
            Name = "João",
            Lastname = "Santos",
            Age = 40,
            Address = "Rua C"
        };
        _service.Add(client);

        _service.Delete(client.Id);

        var result = _service.GetById(client.Id);
        result.Should().BeNull();
    }

    [Fact]
    public void Should_Throw_When_Adding_Client_With_Invalid_Age()
    {
        var invalidClient = new Client
        {
            Name = "João",
            Lastname = "Errado",
            Age = -5,
            Address = "Algum lugar"
        };

        Action act = () => _service.Add(invalidClient);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Idade inválida");
    }
}
