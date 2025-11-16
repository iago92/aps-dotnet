# ⚡ QUICKSTART - Começar Rápido

## 1️⃣ Configurar Banco de Dados (2 minutos)

Edite o arquivo `University.WebUI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=UniversityDb;User Id=sa;Password=sua_senha_aqui;"
  }
}
```

## 2️⃣ Aplicar Migrations (1 minuto)

```bash
cd /workspaces/aps-dotnet/University.WebUI
dotnet ef database update -p ../University.Infrastructure
```

## 3️⃣ Executar Aplicação (1 minuto)

```bash
cd /workspaces/aps-dotnet/University.WebUI
dotnet run
```

Acesse: **https://localhost:5001**

---

## 📱 Menu Principal

| Menu | O que fazer |
|------|-----------|
| **Categorias** | Gerenciar categorias de produtos |
| **Produtos** | Gerenciar produtos (vinculados a categorias) |

---

## 🧪 Testar a Aplicação

### 1. Criar Categoria
- Clique em "Categorias" → "Nova Categoria"
- Preencha: Nome = "Eletrônicos", Descrição = "Produtos eletrônicos"
- Clique "Salvar"

### 2. Criar Produto
- Clique em "Produtos" → "Novo Produto"
- Preencha: Nome = "Notebook", Preço = 3500.00, Estoque = 10
- Selecione a categoria "Eletrônicos"
- Clique "Salvar"

### 3. Testar Busca AJAX
- Em "Produtos" ou "Categorias"
- Digite na barra de busca
- A lista atualiza em tempo real!

---

## 📋 Requisitos Implementados

✅ Clean Architecture (Domain, Application, Infrastructure, Presentation)  
✅ Relacionamento 1:N (Categoria → Produtos)  
✅ CRUD Completo  
✅ Validações Personalizadas  
✅ Busca com AJAX  
✅ Mapster para mapeamento  
✅ Entity Framework Core  
✅ Injeção de Dependências  
✅ Tudo em Português  

---

## 🔍 Arquivos Importantes

- **README.md** - Documentação completa
- **RESUMO_EXECUTIVO.md** - Resumo técnico
- **IMPLEMENTACAO.md** - Detalhes de implementação
- **DADOS_EXEMPLO.sql** - Scripts SQL de exemplo

---

## 💡 Dicas

- As buscas usam AJAX com debounce (espera 300ms ao digitar)
- Deletar categoria deleta todos os produtos vinculados (Cascade)
- Preços aceitos: até 2 casas decimais (ex: 99.99)
- Todos os IDs usam Guid (universalmente únicos)
- Datas armazenadas em UTC

---

**Pronto! Seu projeto Clean Architecture + DDD está funcionando! 🎉**
