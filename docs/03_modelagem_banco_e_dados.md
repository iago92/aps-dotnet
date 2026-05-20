# Modelagem de banco de dados

## 1. Objetivo da modelagem
Descrever a estrutura de dados necessária para persistir informações de usuários, seus diários, refeições catalogadas e metadados climáticos com integridade e segurança.

## 2. Entidades principais
* **usuarios:** Armazena as credenciais e dados básicos de perfil.
* **registros_diarios:** Entidade pivô associada a uma data específica de um usuário. Centraliza o texto do diário e o clima do dia.
* **refeicoes:** Armazena os itens alimentares catalogados, associados a um registro diário (relação N:1).

## 3. Relacionamentos
* Um **Usuário** possui de 0 a N **Registros Diários** (1:N).
* Um **Registro Diário** pertence obrigatoriamente a 1 **Usuário** (1:1).
* Um **Registro Diário** possui de 0 a N **Refeições** (1:N).
* Uma **Refeição** pertence obrigatoriamente a 1 **Registro Diário** (1:1).

## 4. Padrões obrigatórios
* **Chaves Primárias:** Uso obrigatório de UUID v4 para mitigar ataques de enumeração de IDs sequenciais nas URLs/APIs.
* **Chaves Estrangeiras:** Restrição `ON DELETE CASCADE` aplicada para garantir que se um usuário for deletado, todo o seu histórico seja removido para conformidade de privacidade.
* **Restrição de Unicidade:** Par composto `(usuario_id, data_registro)` na tabela pivô para impedir registros duplicados no mesmo dia.

## 5. Script SQL Inicial (PostgreSQL / Adaptável para SQLite)

```sql
-- Ativação da extensão para geração de UUID
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Tabela de Usuários
CREATE TABLE usuarios (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    senha_hash VARCHAR(255) NOT NULL,
    criado_em TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Tabela de Dias/Contexto (Agrupador para evitar repetição de Clima)
CREATE TABLE registros_diarios (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    usuario_id UUID NOT NULL,
    data_registro DATE NOT NULL,
    clima VARCHAR(50), -- Ex: Ensolarado, Chuvoso, Nublado, Frio
    texto_diario TEXT,
    atualizado_em TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    CONSTRAINT unique_usuario_data UNIQUE (usuario_id, data_registro)
);

-- Tabela de Refeições
CREATE TABLE refeicoes (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    registro_diario_id UUID NOT NULL,
    periodo VARCHAR(50) NOT NULL, -- Ex: Café da Manhã, Almoço, Jantar, Lanche
    descricao TEXT NOT NULL,
    criado_em TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (registro_diario_id) REFERENCES registros_diarios(id) ON DELETE CASCADE
);

-- Índices para otimização de consultas baseadas em busca histórica e performance
CREATE INDEX idx_registros_diarios_busca ON registros_diarios(usuario_id, data_registro);
CREATE INDEX idx_refeicoes_registro ON refeicoes(registro_diario_id);
