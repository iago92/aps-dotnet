-- Script para inserir dados de exemplo no banco de dados
-- Execute este script após criar as migrations

-- Inserir categorias de exemplo
INSERT INTO Categories (Id, Name, Description, CreatedAt)
VALUES 
    (NEWID(), 'Eletrônicos', 'Produtos eletrônicos em geral', GETUTCDATE()),
    (NEWID(), 'Livros', 'Livros e publicações', GETUTCDATE()),
    (NEWID(), 'Alimentos', 'Alimentos e bebidas', GETUTCDATE());

-- Inserir produtos de exemplo (após obter os IDs das categorias)
-- Obs: Substitua os CategoryIds pelos IDs gerados acima

-- Exemplo de como executar:
-- 1. Execute o INSERT das categorias
-- 2. Copie os IDs gerados
-- 3. Substitua nos inserts abaixo

-- INSERT INTO Products (Id, Name, Description, Price, Stock, CategoryId, CreatedAt)
-- VALUES 
--     (NEWID(), 'Notebook Dell', 'Notebook Dell Inspiron 15 com processador Intel Core i5', 3500.00, 10, '{CategoryId1}', GETUTCDATE()),
--     (NEWID(), 'Mouse Logitech', 'Mouse wireless Logitech MX Master 3', 350.00, 25, '{CategoryId1}', GETUTCDATE()),
--     (NEWID(), 'Clean Code', 'Livro sobre desenvolvimento de software limpo', 89.90, 15, '{CategoryId2}', GETUTCDATE());
