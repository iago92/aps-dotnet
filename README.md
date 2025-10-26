# UniversityApp (Clean Architecture + DDD) - Português

Projeto de exemplo estruturado com princípios de Clean Architecture e DDD.

Estrutura de projetos:
- `University.Domain` - Entidades (ex.: `Aluno`)
- `University.Application` - Serviços de aplicação e ViewModels (ex.: `AlunoViewModel`, `IAlunoServico`, `AlunoServico`)
- `University.Infrastructure` - EF Core DbContext, repositórios e migrations
- `University.WebUI` - ASP.NET Core MVC (controllers e views)

Como rodar (requisições mínimas):

1. Tenha um SQL Server acessível. Por exemplo, no Docker:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

2. Ajuste a connection string em `University.WebUI/appsettings.json` se necessário.

3. Aplicar migrations (a partir da raiz do repositório):

```bash
dotnet tool install --global dotnet-ef # se ainda não instalado
dotnet ef database update --project University.Infrastructure --startup-project University.WebUI
```

4. Rodar a aplicação:

```bash
dotnet run --project University.WebUI
```

5. Acesse `https://localhost:5001` (ou a porta exibida) e navegue para `/Alunos`.

Notas:
- Os nomes de domínio e arquivos foram traduzidos para português: `Aluno`, `IAlunoRepositorio`, `AlunoServico`, `AlunosController`, views em `Views/Alunos`.
- Algumas versões antigas em inglês permanecem como stubs no projeto para compatibilidade, mas a implementação ativa está em português.
# teste11