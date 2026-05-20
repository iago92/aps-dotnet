# Contratos de API REST

Versão: v1
Data: 2026-05-20
Autor: Agente Designer de API
Prompt: versão 1

## 1. Objetivo
Definir contratos REST inequívocos para os requisitos do sistema de diário com autenticação, histórico de registros, clima e catálogo de refeições.

## 2. Convenções gerais
- Base da API: `/api/v1`
- Formato de data: `YYYY-MM-DD` (ISO 8601) para todos os recursos de data.
- Identificadores: UUID v4 para todos os IDs expostos nas APIs.
- Autenticação: `Authorization: Bearer <token>` em todos os endpoints protegidos.
- Cabeçalhos obrigatórios:
  - `Content-Type: application/json`
  - `Accept: application/json`
- Todos os endpoints retornam JSON.
- Erros retornam objeto padrão com `code`, `message` e, quando aplicável, `details`.

## 3. Autenticação
### 3.1 `POST /api/v1/auth/register`
Registra um novo usuário.

Request:
```json
{
  "nome": "Mariana Silva",
  "email": "mariana.silva@example.com",
  "senha": "SenhaForte2026!"
}
```

Response 201 Created:
```json
{
  "usuarioId": "5b2f9a67-8d49-4c3a-9f6e-1d9f2d0f8b2c",
  "nome": "Mariana Silva",
  "email": "mariana.silva@example.com",
  "criadoEm": "2026-05-20T14:22:33Z"
}
```

Erros possíveis:
- 400 Bad Request
  - `AUTH_INVALID_REQUEST`: Campos obrigatórios ausentes ou formato inválido.
- 409 Conflict
  - `AUTH_EMAIL_IN_USE`: E-mail já cadastrado.

### 3.2 `POST /api/v1/auth/login`
Realiza login e retorna token de acesso.

Request:
```json
{
  "email": "mariana.silva@example.com",
  "senha": "SenhaForte2026!"
}
```

Response 200 OK:
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "tokenType": "Bearer",
  "expiresIn": 3600
}
```

Erros possíveis:
- 400 Bad Request
  - `AUTH_INVALID_REQUEST`: Formato inválido no corpo da requisição.
- 401 Unauthorized
  - `AUTH_INVALID_CREDENTIALS`: Credenciais inválidas.

## 4. Recursos de Diário
### 4.1 `GET /api/v1/diarios?startDate={startDate}&endDate={endDate}`
Lista registros diários do usuário dentro de um intervalo de datas. Este endpoint atende o histórico de calendário.

Request de exemplo:
```
GET /api/v1/diarios?startDate=2026-05-01&endDate=2026-05-15
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Response 200 OK:
```json
{
  "intervalo": {
    "startDate": "2026-05-01",
    "endDate": "2026-05-15"
  },
  "registros": [
    {
      "data": "2026-05-03",
      "clima": "Ensolarado",
      "textoResumo": "Dia de reflexões sobre metas semanais.",
      "refeicoesCount": 2
    },
    {
      "data": "2026-05-06",
      "clima": "Nublado",
      "textoResumo": "Sessão de meditação e planejamento.",
      "refeicoesCount": 3
    }
  ]
}
```

Erros possíveis:
- 400 Bad Request
  - `DIARY_DATE_RANGE_INVALID`: `startDate` ou `endDate` ausente ou em formato inválido.
- 401 Unauthorized
  - `AUTH_MISSING_TOKEN`: Token não enviado ou expirado.

### 4.2 `GET /api/v1/diarios/{date}`
Retorna o registro diário completo de uma data específica, incluindo refeições.

Request de exemplo:
```
GET /api/v1/diarios/2026-05-06
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Response 200 OK:
```json
{
  "registroId": "17f4f6d5-3e5a-41a4-b0f0-7653c9921a54",
  "data": "2026-05-06",
  "clima": "Nublado",
  "textoDiario": "Hoje foi um bom dia para organizar a semana.",
  "atualizadoEm": "2026-05-06T20:12:10Z",
  "refeicoes": [
    {
      "refeicaoId": "bda7c4a1-4d39-4c12-9f8a-fb2e3c5dd356",
      "periodo": "Café da Manhã",
      "descricao": "Iogurte natural com granola e frutas"
    },
    {
      "refeicaoId": "c8d9a5f0-1d10-4e51-98b3-a8390b8d3b8d",
      "periodo": "Almoço",
      "descricao": "Salada de quinoa com legumes grelhados"
    }
  ]
}
```

Erros possíveis:
- 400 Bad Request
  - `DIARY_DATE_INVALID`: Data fora do padrão `YYYY-MM-DD`.
- 401 Unauthorized
  - `AUTH_MISSING_TOKEN`: Token ausente ou inválido.
- 404 Not Found
  - `DIARY_NOT_FOUND`: Registro diário não encontrado para a data solicitada.

### 4.3 `PUT /api/v1/diarios/{date}`
Cria ou atualiza o registro diário de uma data específica. Esta operação garante o `Registro Único Diário` por usuário e data.

Request:
```json
{
  "clima": "Chuvoso",
  "textoDiario": "Passei o dia refletindo sobre hábitos e autocuidado."
}
```

Response 200 OK:
```json
{
  "registroId": "17f4f6d5-3e5a-41a4-b0f0-7653c9921a54",
  "data": "2026-05-06",
  "clima": "Chuvoso",
  "textoDiario": "Passei o dia refletindo sobre hábitos e autocuidado.",
  "atualizadoEm": "2026-05-06T21:05:44Z"
}
```

Erros possíveis:
- 400 Bad Request
  - `DIARY_DATE_INVALID`: Data inválida.
  - `DIARY_INVALID_PAYLOAD`: `clima` ou `textoDiario` com formato inválido.
- 401 Unauthorized
  - `AUTH_MISSING_TOKEN`: Token inválido ou expirado.
- 403 Forbidden
  - `RESOURCE_FORBIDDEN`: Tentativa de acessar ou modificar registro de outro usuário.

### 4.4 `DELETE /api/v1/diarios/{date}`
Remove o registro diário e todas as refeições vinculadas a essa data para o usuário autenticado.

Request de exemplo:
```
DELETE /api/v1/diarios/2026-05-06
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Response 204 No Content

Erros possíveis:
- 400 Bad Request
  - `DIARY_DATE_INVALID`: Data incorreta.
- 401 Unauthorized
  - `AUTH_MISSING_TOKEN`: Token ausente ou inválido.
- 404 Not Found
  - `DIARY_NOT_FOUND`: Registro diário não existe.
- 403 Forbidden
  - `RESOURCE_FORBIDDEN`: Recurso pertence a outro usuário.

## 5. Recursos de Refeição
### 5.1 `POST /api/v1/diarios/{date}/refeicoes`
Adiciona uma nova refeição ao registro diário de uma data específica.

Request:
```json
{
  "periodo": "Almoço",
  "descricao": "Sanduíche natural com peito de peru e salada"
}
```

Response 201 Created:
```json
{
  "refeicaoId": "3c71f6a9-9f76-4a97-b8c8-190983bfe70d",
  "registroId": "17f4f6d5-3e5a-41a4-b0f0-7653c9921a54",
  "data": "2026-05-06",
  "periodo": "Almoço",
  "descricao": "Sanduíche natural com peito de peru e salada",
  "criadoEm": "2026-05-06T21:14:12Z"
}
```

Erros possíveis:
- 400 Bad Request
  - `DIARY_DATE_INVALID`: Data em formato inválido.
  - `MEAL_INVALID_PAYLOAD`: `periodo` ou `descricao` inválidos ou ausentes.
- 401 Unauthorized
  - `AUTH_MISSING_TOKEN`: Token ausente ou inválido.
- 404 Not Found
  - `DIARY_NOT_FOUND`: Não existe registro para a data informada.
- 403 Forbidden
  - `RESOURCE_FORBIDDEN`: Registro não pertence ao usuário autenticado.

### 5.2 `GET /api/v1/diarios/{date}/refeicoes`
Lista todas as refeições do registro diário da data especificada.

Request de exemplo:
```
GET /api/v1/diarios/2026-05-06/refeicoes
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Response 200 OK:
```json
{
  "data": "2026-05-06",
  "refeicoes": [
    {
      "refeicaoId": "3c71f6a9-9f76-4a97-b8c8-190983bfe70d",
      "periodo": "Almoço",
      "descricao": "Sanduíche natural com peito de peru e salada"
    },
    {
      "refeicaoId": "d7ea5f91-a6a4-4f0f-8a1f-2e4a4d9a6f4b",
      "periodo": "Lanche",
      "descricao": "Maçã e castanhas"
    }
  ]
}
```

Erros possíveis:
- 400 Bad Request
  - `DIARY_DATE_INVALID`: Data inválida.
- 401 Unauthorized
  - `AUTH_MISSING_TOKEN`: Token ausente ou expirado.
- 404 Not Found
  - `DIARY_NOT_FOUND`: Registro diário não encontrado.
- 403 Forbidden
  - `RESOURCE_FORBIDDEN`: Recurso fora do escopo do usuário.

### 5.3 `PUT /api/v1/diarios/{date}/refeicoes/{refeicaoId}`
Atualiza uma refeição existente.

Request:
```json
{
  "periodo": "Jantar",
  "descricao": "Risoto de cogumelos com salada verde"
}
```

Response 200 OK:
```json
{
  "refeicaoId": "3c71f6a9-9f76-4a97-b8c8-190983bfe70d",
  "registroId": "17f4f6d5-3e5a-41a4-b0f0-7653c9921a54",
  "data": "2026-05-06",
  "periodo": "Jantar",
  "descricao": "Risoto de cogumelos com salada verde",
  "atualizadoEm": "2026-05-06T21:37:08Z"
}
```

Erros possíveis:
- 400 Bad Request
  - `MEAL_INVALID_PAYLOAD`: Campos inválidos.
- 401 Unauthorized
  - `AUTH_MISSING_TOKEN`: Token inválido ou expirado.
- 404 Not Found
  - `DIARY_NOT_FOUND`: Registro diário não existe.
  - `MEAL_NOT_FOUND`: Refeição não encontrada para o `refeicaoId` informado.
- 403 Forbidden
  - `RESOURCE_FORBIDDEN`: Refeição pertence a outro usuário.

### 5.4 `DELETE /api/v1/diarios/{date}/refeicoes/{refeicaoId}`
Remove uma refeição do registro diário.

Request de exemplo:
```
DELETE /api/v1/diarios/2026-05-06/refeicoes/3c71f6a9-9f76-4a97-b8c8-190983bfe70d
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Response 204 No Content

Erros possíveis:
- 400 Bad Request
  - `DIARY_DATE_INVALID`: Data inválida.
- 401 Unauthorized
  - `AUTH_MISSING_TOKEN`: Token ausente ou inválido.
- 404 Not Found
  - `DIARY_NOT_FOUND`: Registro diário não existe.
  - `MEAL_NOT_FOUND`: Refeição não encontrada.
- 403 Forbidden
  - `RESOURCE_FORBIDDEN`: Recurso não pertence ao usuário.

## 6. Modelo de erro padrão
Todos os erros retornam status HTTP apropriado e JSON com:
- `code`: código interno exclusivo do erro.
- `message`: mensagem legível para o cliente.
- `details`: campo opcional para contexto adicional.

Exemplo de erro:
```json
{
  "code": "AUTH_INVALID_CREDENTIALS",
  "message": "Credenciais inválidas. Verifique e tente novamente.",
  "details": "Email ou senha incorretos."
}
```

## 7. Dados sensíveis e segurança
- Senhas nunca são enviadas de volta em respostas.
- O token de acesso é exigido em todos os endpoints que retornam ou alteram dados do usuário.
- O usuário só pode acessar seus próprios registros e refeições.

## 8. Glossário de API proposto
- `Endpoint`: URL que expõe um recurso ou ação da API.
- `Access Token`: token JWT usado para autenticação de chamadas protegidas.
- `Registro Diário`: recurso único por data e usuário que agrega texto e clima.
- `Refeição`: item atrelado ao registro diário, com período e descrição.

## 9. Pontos que precisam de validação antes do desenvolvimento
1. Confirmação do padrão de rota: `/api/v1/diarios` versus `/api/v1/registros-diarios`.
2. Se o endpoint de listagem de histórico deve suportar filtros adicionais além de `startDate` e `endDate`.
3. Se é necessário incluir endpoint de refresh de token ou logout no contrato inicial.
4. Se o `PUT /api/v1/diarios/{date}` deve ser tratado como criação exclusiva (`201`) quando o registro não existir.
5. Se a exclusão de `diario` deve remover automaticamente as refeições ou retornar erro quando houver refeições existentes.

---

Agente Designer de API | 2026-05-20 | versão do prompt 1
