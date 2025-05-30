# FIAP Cloud Games (FCG)

Projeto FIAP Cloud Games (FCG) desenvolvido como parte do **Tech Challenge** do curso de **Pós-Graduação em Arquitetura de Sistemas .NET** da **FIAP**.

## 📚 Sobre o Projeto

Este repositório contém a implementação de um sistema de cadastro de jogos e controle de usuários baseado na arquitetura de sistemas .NET, com foco em boas práticas de desenvolvimento, como separação de responsabilidades, testes automatizados e uso de padrões de projeto.

## 📁 Estrutura do Projeto

O projeto está organizado nos seguintes diretórios:

- **FCG.Application**: Contém a lógica de aplicação, incluindo serviços e interfaces que definem os contratos da aplicação.
- **FCG.Domain**: Abriga as entidades de domínio e regras de negócio fundamentais do sistema.
- **FCG.Infrastructure**: Responsável pela implementação dos repositórios, acesso a dados e outras dependências externas.
- **FCG.Tests**: Inclui os testes automatizados para garantir a qualidade e a estabilidade do código.
- **FCG**: Camada principal da aplicação, possivelmente a API.
- **FGC.Tests**: Contém testes adicionais específicos de outros componentes do sistema.

## 🛠️ Tecnologias Utilizadas

- **.NET**
- **C#**
- **xUnit / Moq**
- **Entity Framework Core**
- **Swagger**
- **Autenticação JWT**

## 🚀 Como Executar o Projeto

### 1. Clone o repositório:

```bash
git clone https://github.com/raabelo11/TechChallenge_FIAP.git
````
### 2. Navegue até o diretório do projeto:

```bash
cd TechChallenge_FIAP
````
### 3. Configure a string de conexão localizada no appsettings.json
- No appsettings, configure um diretório válido para salvar os logs gerados no campo - "Directory": {"path":}
- Rode os migrations no seu banco de dados SQL Server via linha de comando no visual studio:
````bash
dotnet ef database update
````
### 4. Abra o projeto em sua IDE preferida (Visual Studio, VS Code, etc.)
### 5. Restaure os pacotes:
````bash
dotnet restore
````
### 6. Compile o projeto:
````bash
dotnet build
````
### 7. Execute o projeto:
````bash
dotnet run --project FCG
````
### 8. Crie um usuário válido via endpoint: **api/Usuarios/CriarUsuario**
### 9. Faça login com o usuário criado em: **api/Authorization/login**
### 10. Após login feito, copie o token enviado no data e autentique via authorize no swagger, seguindo o padrão:
**Bearer {Token gerado}**

✅ Como Executar os Testes
### 1. Navegue até o diretório de testes:
````bash
cd FCG.Tests
````
### 2. Execute os testes:
````bash
dotnet test
````
