# Glossário de domínio

## 1. Termos do negócio
* **Usuário:** Entidade individual autenticada que possui a propriedade e acesso exclusivo aos registros de diário e refeições.
* **Entrada de Diário (Journal Entry):** Registro textual livre contendo as reflexões, sentimentos ou anotações gerais do usuário, associado a uma data específica.
* **Refeição (Meal):** Item alimentar individual catalogado pelo usuário (contendo descrição textual), categorizado por um período do dia.
* **Clima (Weather Status):** Estado meteorológico simples escolhido ou associado para contextualizar o ambiente do usuário em uma determinada data.

## 2. Termos técnicos
* **UUID:** *Universally Unique Identifier*. Identificador de 128 bits usado como chave primária para garantir unicidade global e segurança por obscuridade.
* **Tabela Pivô (ou Registro Diário Base):** Entidade do banco de dados que serve de ponto de união entre o usuário, uma data do calendário, o texto do diário e o clima, servindo também como pai para as refeições daquele dia.
* **BCrypt:** Função de hash criptográfica projetada para segurança e proteção de senhas contra ataques de força bruta.

## 3. Convenções de nomenclatura
* Tabelas de banco de dados sempre em letras minúsculas e no plural (`usuarios`, `refeicoes`).
* Chaves estrangeiras utilizando o sufixo `_id` (ex: `usuario_id`).
* Datas armazenadas estritamente no padrão ISO (`YYYY-MM-DD`).