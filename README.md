# 📝 Taskify
Taskify é uma API desenvolvida com o framework ASP.NET Web API utilizando .NET 7. O objetivo deste projeto é permitir que os usuários gerenciem suas tarefas diárias, oferecendo funcionalidades como criar, editar, deletar e listar tarefas. Além disso, é possível filtrar as tarefas por status, data de vencimento e prioridade.

## ⚙️ Funcionalidades
- Criar, editar e deletar tarefas
- Listar tarefas
- Filtrar por status, data de vencimento e prioridade
- Autenticação JWT

## 🗂️ Modelo de Dados
### A entidade Tasks tem os seguintes campos:

- Id: int (Identificador único)
- Titulo: string (Título da tarefa)
- Descricao: string (Descrição detalhada da tarefa)
- Data de Vencimento: DateTime (Data limite para conclusão)
- Concluido: bool (Indica se a tarefa foi concluída)
- Prioridade: string (Prioridade da tarefa, ex: Alta, Média, Baixa)
  
## 🛠️ Tecnologias Utilizadas
- Linguagem: C#
- Framework: ASP.NET Web API
- Banco de Dados: MySQL
- ORM: Entity Framework Core
- Testes: XUnit
- Documentação: Swagger
- Padrão de Projeto: Repository
- Autenticação: JWT
- Rate Limiter: Controle de requisições por usuário
  
## 🚀 Como Rodar o Projeto
- Clone o repositório:
```` git
https://github.com/joaoveasey/taskify.git
````

- Abra o projeto no Visual Studio.
  
- Configure a string de conexão no arquivo appsettings.json com os dados do seu banco de dados:
````json
"ConnectionStrings": {
  "Default": "Server=;Port=;User ID=;Password=;Database="
}
````

- Execute o comando update-database no Console do Gerenciador de Pacotes para criar as tabelas no banco de dados.
- Inicie o projeto.
- Acesse o Swagger em https://localhost:5001/swagger/index.html para testar a API.
- Autentique-se para usar os endpoints de tarefas:
- Crie uma conta usando o endpoint /api/usuarios/register.
- Faça login com o endpoint /api/usuarios/login para obter o token de autenticação.
- No Swagger ou na sua ferramenta de testes de API, clique em "Authorize".
- ![image](https://github.com/user-attachments/assets/105e6b59-b67d-4788-ab01-693a630365ba)

- Insira o token no formato:
````
Bearer seu_token_gerado
````
- Após autorizar, você poderá usar os endpoints de Tasks.

## 🧪 Testes Unitários
Os testes foram implementados utilizando xUnit para garantir a qualidade e o funcionamento adequado do sistema. Para executar os testes, siga os passos abaixo:

- No arquivo TasksUnitTestController, ajuste a connectionString com os parâmetros do seu banco de dados:
````csharp
public static string connectionString =
    "Server=;Port=;User ID=;Password=;Database=";
````
### Executando os Testes:
- Utilize o seguinte comando no terminal para rodar todos os testes unitários:
````bash
dotnet test
````
### 📋 Cenários Testados

#### 🔖 Tasks
- ✅ Criar uma tarefa
- ✅ Editar uma tarefa
- ✅ Deletar uma tarefa
- ✅ Listar tarefas
- ✅ Filtrar tarefas por status
- ✅ Filtrar tarefas por data de vencimento
- ✅ Filtrar tarefas por prioridade
- ✅ Filtrar tarefas por status e data de vencimento
