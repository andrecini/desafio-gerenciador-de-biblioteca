![image](https://github.com/andrecini/desafio-gerenciador-de-biblioteca/assets/79148213/16c27d61-7e75-445e-87a2-a5da8c221aed)

# Mentoria .NET - Desafio: Gerenciador de Bibliotecas 📚

## 💡 Descrição

Este projeto foi criado com intuito de aplicar os conceitos aprendidos durante os cursos e mentorias da NextWave. Aqui, foi implementado um Sistema de Gerenciamento de Bibliotecas utilizando princípios de Clean Architecture para garantir a separação de responsabilidades, facilitando a manutenção e evolução do sistema. Além disso, o projeto foi desenvolvido com os Padrões Repository e UnitOfWork e com os conceitos básicos de Programação Orientada a Objetos.

## 🏗️ Estrutura do Projeto

O projeto está organizado em camadas seguindo os princípios de Clean Architecture:

- **Domain**: Contém as entidades de negócio e interfaces.
- **Application**: Contém a lógica de aplicação e casos de uso.
- **Infrastructure**: Implementa repositórios e serviços.
- **Presentation**: Contém os controladores de API e interfaces de usuário.
- **Tests**: Contém os Testes Unitários da Aplicação;

## 🔎 Tecnologias Utilizadas

<details>
  <summary>.NET 8</summary>
  Utilizado como framework principal para o desenvolvimento da aplicação, proporcionando uma plataforma robusta e escalável para construir APIs e serviços web.
</details>

<details>
  <summary>Blazor Server</summary>
  Desenvolvimento do FrontEnd com Blazor Server para criar interfaces de usuário interativas e dinâmicas, aproveitando a execução no lado do servidor.
</details>

<details>
  <summary>XUnit ⌛</summary>
  (Carregando...)
  Planejado para ser utilizado para a escrita e execução de testes automatizados, garantindo a qualidade e a confiabilidade do código.
</details>

<details>
  <summary>Entity Framework (EF)</summary>
  Utilizado como ORM (Object-Relational Mapper) para facilitar a interação com o banco de dados SQL Server, simplificando operações CRUD (Create, Read, Update, Delete).
</details>

<details>
  <summary>SQL Server</summary>
  Banco de dados relacional utilizado para armazenar e gerenciar os dados da aplicação de forma eficiente e segura.
</details>

<details>
  <summary>Records para DTOs</summary>
  Utilizados para definir DTOs (Data Transfer Objects) de forma concisa e imutável, melhorando a clareza e a segurança do código ao transferir dados entre camadas.
</details>

<details>
  <summary>AutoMapper</summary>
  Utilizado para mapear automaticamente objetos de um tipo para outro, reduzindo a necessidade de código repetitivo e facilitando a transformação de dados entre diferentes camadas da aplicação.
</details>

<details>
  <summary>FluentValidator</summary>
  Utilizado para validação de dados de entrada de forma declarativa e fluente, garantindo que as regras de negócio sejam aplicadas de maneira consistente e centralizada.
</details>

## 🗂️ Modelagem de Dados

![ModelagemDeDados drawio](https://github.com/andrecini/desafio-gerenciador-de-biblioteca/assets/79148213/c2217ec9-0481-4097-9170-fb7d458f55a1)

## 🤝 Relacionamentos

<details>
  <summary>Usuário (User) ↔ Empréstimo (Loan)</summary>
  
  - **Descrição:** Um usuário pode ter múltiplos empréstimos. Um empréstimo é feito por um único usuário.
  - **Entidades:**
  
    ```csharp
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class Loan
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime LoanValidity { get; set; }
        public Inventory Inventory { get; set; }
        public User User { get; set; }
    }
    ```
</details>

<details>
  <summary>Livro (Book) ↔ Inventário (Inventory)</summary>
  
  - **Descrição:** Um livro pode estar em múltiplos inventários. Um inventário refere-se a um único livro.
  - **Entidades:**
  
    ```csharp
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
    }

    public class Inventory
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public Library Library { get; set; }
        public Book Book { get; set; }
    }
    ```
</details>

<details>
  <summary>Livraria (Library) ↔ Inventário (Inventory)</summary>
  
  - **Descrição:** Uma livraria pode ter múltiplos inventários. Um inventário pertence a uma única livraria.
  - **Entidades:**
  
    ```csharp
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Phone { get; set; }
    }

    public class Inventory
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public Library Library { get; set; }
        public Book Book { get; set; }
    }
    ```
</details>

<details>
  <summary>Inventário (Inventory) ↔ Empréstimo (Loan)</summary>
  
  - **Descrição:** Um inventário pode ter múltiplos empréstimos. Um empréstimo refere-se a um único inventário.
  - **Entidades:**
    
    ```csharp
    public class Inventory
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public Library Library { get; set; }
        public Book Book { get; set; }
    }

    public class Loan
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime LoanValidity { get; set; }
        public Inventory Inventory { get; set; }
        public User User { get; set; }
    }
    ```
</details>

## 😁 Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests para melhorar o projeto.





