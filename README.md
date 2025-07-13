# BTG.ClientApp.Tests

Este repositório contém os **testes unitários e de integração** do projeto `BTG.ClientApp`, desenvolvidos separadamente do projeto principal `.NET MAUI`.

---

## 🚧 Por que este repositório é separado?

O projeto principal (`BTG.ClientApp`) é baseado em **.NET MAUI**, que é **multi-target** (Android, iOS, Windows, etc.) e utiliza recursos específicos como `FileSystem.AppDataDirectory` e `Resizetizer`.

Essas características tornam os testes mais difíceis de isolar, pois:

- Qualquer teste tentando referenciar diretamente o projeto MAUI pode **falhar no build** (ex: conflitos com `MauiIcon`, imagens duplicadas, etc.)
- A camada de UI não deve ser acoplada aos testes de lógica

> ✅ Por isso, **extraímos a lógica de negócio** (`Models`, `Repositories`, `Services`) para um projeto separado (`BTG.ClientApp.Core`) e criamos os testes aqui.

---

## 🧱 Estrutura

---

## ✅ O que está sendo testado

| Categoria           | Descrição                                                           |
|---------------------|----------------------------------------------------------------------|
| `ClientRepository`  | Testes unitários para Add, Update, Delete, GetById, GetAll          |
| `ClientService`     | Testes de integração com validações e repositório em memória        |
| `ClientIntegration` | Teste completo de ciclo de vida: Adicionar → Atualizar → Deletar    |

---

## 🧪 Tecnologias usadas

- [`xUnit`](https://xunit.net/) — framework de testes moderno
- [`FluentAssertions`](https://fluentassertions.com/) — sintaxe fluente para asserções
- [.NET 9.0](https://dotnet.microsoft.com/) — compatível com o projeto principal

---

## ▶️ Como executar os testes

Certifique-se de ter o .NET SDK 9 instalado.

```bash
git clone https://github.com/seu-usuario/BTG.ClientApp.Tests.git
cd BTG.ClientApp.Tests
dotnet restore
dotnet test
