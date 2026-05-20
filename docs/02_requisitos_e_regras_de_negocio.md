# Requisitos e regras de negócio

## 1. Requisitos funcionais
* **RF01 (Autenticação):** O sistema deve permitir que novos usuários se cadastrem e usuários existentes realizem login via e-mail e senha.
* **RF02 (Calendário Histórico):** O sistema deve permitir que o usuário selecione qualquer data (presente ou passada) para criar, visualizar ou editar um registro diário.
* **RF03 (Entrada de Diário):** O sistema deve fornecer um campo de texto livre para o usuário escrever suas reflexões diárias associadas ao dia selecionado.
* **RF04 (Catálogo de Refeições):** O usuário deve conseguir adicionar múltiplas refeições no dia selecionado, informando o período (ex: Café da Manhã, Almoço, Jantar, Lanche) e a descrição do que comeu.
* **RF05 (Registro de Clima):** O usuário deve conseguir associar um status climático (ex: Ensolarado, Chuvoso, Nublado, Frio) ao dia selecionado.

## 2. Requisitos não funcionais
* **RNF01 (Segurança):** O sistema deve criptografar as senhas dos usuários antes de persistir no banco de dados (usando algoritmos como BCrypt).
* **RNF02 (Usabilidade/Responsividade):** A interface do aplicativo deve ser desenhada com foco em dispositivos móveis (Mobile-First) para facilitar o preenchimento rápido em qualquer lugar.
* **RNF03 (Performance):** A listagem histórica do calendário e o carregamento dos dias devem responder em menos de 1 segundo para evitar atrito de uso.

## 3. Regras de negócio
* **RN01 (Registro Único Diário):** Cada usuário só pode ter um único registro base (pivô) por data, que conterá o texto do diário e o clima daquele dia.
* **RN02 (Múltiplas Refeições):** Um usuário pode registrar quantas refeições desejar dentro do mesmo dia, desde que vinculadas ao registro base daquela data.
* **RN03 (Privacidade Estrita):** Um usuário nunca poderá visualizar, editar ou listar os registros de diário ou refeições de outro usuário.

## 4. Casos de uso prioritários
1. Fluxo de Criação de Conta e Login.
2. Fluxo de Seleção de Data e gravação combinada de Entrada de Diário + Clima.
3. Fluxo de Adição de itens no Catálogo de Refeições do dia.

## 5. Critérios de aceite
* O usuário só acessa a tela do Diário se estiver autenticado via token válido.
* Ao salvar uma refeição, ela deve aparecer imediatamente listada sob a categoria correta do dia selecionado.
* Caso o usuário mude de dia no calendário, as informações da tela devem ser atualizadas para refletir o conteúdo da nova data selecionada.