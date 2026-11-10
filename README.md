# API de Estoque — Produtos

API REST de estudo para consultar e gerenciar produtos de estoque. O projeto usa ASP.NET Core, Entity Framework Core e MySQL e inclui uma página web simples para listar e filtrar produtos.

## Funcionalidades

- Listagem de produtos com filtro opcional pelo nome.
- Consulta de produto por identificador.
- Criação, atualização e exclusão de produtos pela API.
- Página inicial estática para visualizar e pesquisar produtos.
- Swagger/OpenAPI habilitado no ambiente de desenvolvimento.

## Tecnologias

- C# e ASP.NET Core (alvo .NET 8)
- Entity Framework Core e Pomelo para MySQL
- MySQL
- Swagger / Swashbuckle
- HTML, CSS e JavaScript para a página de consulta

## Modelo de produto

Cada produto contém:

| Campo | Tipo | Regra |
|---|---|---|
| Id | inteiro | Chave primária |
| Nome | texto | Obrigatório, até 100 caracteres |
| Quantidade | inteiro | Obrigatório |
| Preco | decimal | Obrigatório |

## Rotas da API

A rota base é /api/produtos.

| Método | Rota | Descrição |
|---|---|---|
| GET | /api/produtos | Lista produtos; aceita o parâmetro opcional nome |
| GET | /api/produtos/{id} | Busca um produto pelo identificador |
| POST | /api/produtos | Cadastra um produto |
| PUT | /api/produtos/{id} | Atualiza um produto; o id da rota deve corresponder ao corpo |
| DELETE | /api/produtos/{id} | Exclui um produto |

Exemplo de filtro:

~~~http
GET /api/produtos?nome=cafe
~~~

Exemplo de corpo para criar um produto:

~~~json
{
  "nome": "Produto de exemplo",
  "quantidade": 10,
  "preco": 19.90
}
~~~

## Requisitos

- .NET 8 SDK
- MySQL em execução
- Banco e tabela compatíveis com o modelo Produto

O repositório não contém migrations do Entity Framework. Crie o banco e a tabela Produtos antes da primeira execução. Exemplo de tabela compatível:

~~~sql
CREATE TABLE Produtos (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Quantidade INT NOT NULL,
    Preco DECIMAL(18, 2) NOT NULL
);
~~~

## Configurar a conexão local

A aplicação lê a chave ConnectionStrings:DefaultConnection. Para manter credenciais fora do Git, configure-a com User Secrets (na raiz do repositório):

~~~powershell
dotnet user-secrets init --project .\Trabalhoestoque\TrabalhoApi.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=SEU_BANCO;User ID=SEU_USUARIO;Password=SUA_SENHA;" --project .\Trabalhoestoque\TrabalhoApi.csproj
~~~

Substitua os valores de exemplo localmente. Nunca envie senhas reais para o repositório.

## Executar

Na raiz do repositório:

~~~powershell
dotnet restore .\Trabalhoestoque.sln
dotnet run --project .\Trabalhoestoque\TrabalhoApi.csproj
~~~

Com o perfil HTTP de desenvolvimento, o Swagger fica disponível em http://localhost:5034/swagger e a página de consulta em http://localhost:5034. A porta pode mudar conforme o perfil de execução.

## Limitações

- A aplicação depende de um banco MySQL local configurado e com o esquema criado.
- A página web fornecida lista e filtra; as operações de criação, atualização e exclusão estão disponíveis nas rotas da API.
- Este é um projeto de estudo. Revise autenticação, autorização, validação, configuração de segredos e controles de acesso antes de qualquer implantação pública.
