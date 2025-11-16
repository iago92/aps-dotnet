...

# Projeto: APS3 - Clean Architecture e DDD

## Visão Geral

Sistema web desenvolvido em ASP.NET Core 8 com Clean Architecture e Domain-Driven Design (DDD). O projeto implementa um relacionamento 1-para-muitos entre **Categorias** e **Produtos**, com funcionalidades completas de CRUD, validações personalizadas e busca dinâmica com AJAX.

## Estrutura do Projeto

O projeto segue os princípios de Clean Architecture com separação clara em camadas:

### 1. **University.Domain** (Camada de Domínio)
- **Entidades**: `Category`, `Product`, `Aluno`, `Student`
- **Interfaces de Repositório**: `ICategoryRepository`, `IProductRepository`, etc.
- **Validadores Personalizados**: 
  - `ValidPriceAttribute`: Valida preços positivos com até 2 casas decimais
  - `ValidProductNameAttribute`: Valida nomes sem caracteres especiais inválidos

### 2. **University.Application** (Camada de Aplicação)
- **ViewModels**: `CategoryViewModel`, `ProductViewModel`
- **Interfaces de Serviço**: `ICategoryService`, `IProductService`
- **Serviços de Aplicação**: Implementam lógica de negócio com Mapster para mapeamento
- **Mapeamento**: Configuração centralizada com Mapster

### 3. **University.Infrastructure** (Camada de Infraestrutura)
- **DbContext**: `UniversityDbContext` configurado com SQL Server
- **Repositórios**: Implementações concretas de `CategoryRepository`, `ProductRepository`
- **Migrations**: Controle de versão do banco de dados
- **Factory**: `DesignTimeDbContextFactory` para design-time operations

### 4. **University.WebUI** (Camada de Apresentação)
- **Controllers**: `CategoriesController`, `ProductsController`
- **Views (Razor)**: Interfaces para CRUD com Bootstrap
- **AJAX**: Busca dinâmica sem recarregar página

## Dependências Principais

- ASP.NET Core 8
- Entity Framework Core 8
- Microsoft SQL Server
- Mapster 7.4.0
- Bootstrap 5
- jQuery

## Relacionamento 1:N

**Categoria → Produtos** (1 para Muitos)

```
┌─────────────────┐
│   Category      │
├─────────────────┤
│ Id (PK)         │
│ Name            │
│ Description     │
│ CreatedAt       │
└────────┬────────┘
         │ 1
         │
         │ n
    ┌────▼─────────────┐
    │   Product       │
    ├─────────────────┤
    │ Id (PK)         │
    │ Name            │
    │ Price           │
    │ Stock           │
    │ CategoryId (FK) │
    │ CreatedAt       │
    └─────────────────┘
```

## Funcionalidades Implementadas

✅ **CRUD Completo**
- Criar, ler, atualizar e deletar categorias
- Criar, ler, atualizar e deletar produtos
- Atualizar estoque de produtos

✅ **Validações**
- Data Annotations para validação básica
- Validadores personalizados para preço e nome de produto
- Mensagens de erro em português

✅ **Busca Dinâmica com AJAX**
- Busca em tempo real sem recarregar página
- Debounce para otimizar requisições
- Suporte para buscar por nome ou descrição

✅ **Injeção de Dependências**
- Configuração centralizada em `Program.cs`
- Padrão Repository implementado
- Services com responsabilidade única

✅ **Mapeamento com Mapster**
- Automático entre Entidades e ViewModels
- Configuração centralizada em `MappingConfig`
- Evita acoplamento entre camadas

## Como Executar

### Pré-requisitos
- .NET 8 SDK
- SQL Server (local ou remoto)
- Visual Studio 2022 ou VS Code

### Passos

1. **Clonar o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd aps-dotnet
   ```

2. **Restaurar dependências**
   ```bash
   dotnet restore
   ```

3. **Configurar conexão com banco de dados**
   
   Edite `appsettings.json` em `University.WebUI`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=seu-servidor;Database=UniversityDb;User Id=sa;Password=sua-senha;"
     }
   }
   ```

4. **Aplicar migrations**
   ```bash
   cd University.WebUI
   dotnet ef database update -p ../University.Infrastructure
   ```

5. **Executar a aplicação**
   ```bash
   dotnet run
   ```

   A aplicação estará disponível em `https://localhost:5001`

## Navegação

- **Início**: Página principal da aplicação
- **Categorias**: Gerenciar categorias de produtos
  - Listar categorias com busca dinâmica
  - Criar nova categoria
  - Editar categoria existente
  - Deletar categoria
  
- **Produtos**: Gerenciar produtos
  - Listar produtos com busca dinâmica
  - Criar novo produto (selecionar categoria)
  - Editar produto existente
  - Deletar produto
  - Filtrar por categoria

## Exemplos de Uso

### Criar Categoria

1. Acesse "Categorias" no menu
2. Clique em "Nova Categoria"
3. Preencha nome e descrição
4. Clique em "Salvar"

### Criar Produto

1. Acesse "Produtos" no menu
2. Clique em "Novo Produto"
3. Preencha nome, descrição, preço e estoque
4. Selecione a categoria
5. Clique em "Salvar"

### Buscar Produtos

1. Acesse "Produtos"
2. Digite o nome ou descrição no campo de busca
3. A lista é atualizada automaticamente

## Padrões e Boas Práticas

✅ **Repository Pattern**: Abstração de acesso a dados
✅ **Service Layer**: Lógica de negócio centralizada
✅ **Dependency Injection**: IoC container do ASP.NET Core
✅ **Data Annotations**: Validações declarativas
✅ **SOLID Principles**: Aplicados em toda arquitetura
✅ **Clean Code**: Nomenclatura clara e responsabilidade única
✅ **Entity Framework Core**: ORM moderno com LINQ

## Notas Importantes

- Todos os IDs são `Guid` (UUID) para melhor distribuição
- Datas são armazenadas em UTC
- Preços têm precisão de 2 casas decimais
- Campos obrigatórios são validados no cliente e servidor
- O banco de dados é atualizado automaticamente com migrations

## Próximos Passos (Sugestões)

- Implementar autenticação e autorização
- Adicionar testes unitários
- Implementar relatórios de vendas
- Adicionar carrinho de compras
- Melhorar performance com cache
- Implementar soft delete

---

**Desenvolvido com Clean Architecture e DDD** 🏛️
