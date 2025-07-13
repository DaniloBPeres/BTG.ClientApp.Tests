using BTG.ClientApp.Core.Models;
using BTG.ClientApp.Core.Repositories;
using BTG.ClientApp.Core.Services;
using FluentAssertions;
using Moq;

namespace BTG.ClientApp.Tests.Repositories;

public class ClientRepositoryTests
{
    private readonly ClientRepository _repository;

    public ClientRepositoryTests()
    {
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
        mockFileService.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("[]");

        _repository = new ClientRepository(mockFileService.Object);
    }

    [Fact]
    public void Should_Add_And_Get_Client_By_Id()
    {
        var client = new Client
        {
            Name = "Lucas",
            Lastname = "Fernandes",
            Age = 28,
            Address = "Rua Teste"
        };

        _repository.Add(client);

        var result = _repository.GetById(client.Id);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Lucas");
    }

    [Fact]
    public void Should_Update_Client()
    {
        var client = new Client
        {
            Name = "Ana",
            Lastname = "Silva",
            Age = 30,
            Address = "Rua X"
        };

        _repository.Add(client);

        client.Address = "Rua Y";
        _repository.Update(client);

        var updated = _repository.GetById(client.Id);
        updated!.Address.Should().Be("Rua Y");
    }

    [Fact]
    public void Should_Delete_Client()
    {
        var client = new Client
        {
            Name = "Carlos",
            Lastname = "Oliveira",
            Age = 42,
            Address = "Rua Z"
        };

        _repository.Add(client);
        _repository.Delete(client.Id);

        var result = _repository.GetById(client.Id);
        result.Should().BeNull();
    }

    [Fact]
    public void Should_List_All_Clients()
    {
        var client1 = new Client { Name = "A", Lastname = "B", Age = 20, Address = "1" };
        var client2 = new Client { Name = "C", Lastname = "D", Age = 25, Address = "2" };

        _repository.Add(client1);
        _repository.Add(client2);

        var all = _repository.GetAll();
        all.Should().Contain(client1).And.Contain(client2);
    }
}
