# Fluxograma de usabilidade do PhysioCore

Este documento mostra como navegar pelo aplicativo e concluir os principais processos: acesso, cadastro e consulta de pacientes, agendamento e registro de sessões, e gestão de exercícios.

## 1. Visão geral da navegação

```mermaid
%%{init: {"themeVariables": {"fontSize": "24px"}, "flowchart": {"padding": 24}}}%%
flowchart LR
    Abrir([Abrir aplicativo]) --> Login[Login]
    Login --> Acesso{Possui usuário?}
    Acesso -- Não --> Cadastro[Cadastro de usuário]
    Cadastro --> Login
    Acesso -- Sim --> Inicio[Início]

    Inicio --> Navegacao{Navegação principal}
    Navegacao --> Home[Início: agenda e calendário]
    Navegacao --> Dashboard[Dashboard: cadastros]
    Navegacao --> Sessoes[Sessão: lista e filtro]
    Navegacao --> Pacientes[Pacientes: lista]

    Home --> Dia[Detalhes do dia]
    Dia --> DetalheSessao[Detalhes da sessão]
    Home --> Calendario[Calendário mensal]
    Calendario --> DetalheSessao
    Sessoes --> DetalheSessao

    Dashboard --> Cadastros[Paciente, exercício ou sessão]
    Pacientes --> Perfil[Perfil do paciente]
    Perfil --> Editar[Editar paciente]
    Editar --> Pacientes
    Perfil --> Cadastros
    Dia --> Cadastros
    DetalheSessao --> Sessoes
```

As quatro opções da barra inferior ficam disponíveis nas telas principais: **Início**, **Dashboard**, **Sessão** e **Pacientes**. O calendário mensal é aberto pelo ícone de calendário na tela Início. Para voltar de telas de detalhe ou formulário, use o botão de voltar do próprio aplicativo.

## 2. Cadastrar paciente

O cadastro começa pelos dados pessoais e segue para a avaliação de goniometria e força. É necessário preencher nome, data de nascimento, peso e altura na primeira etapa e registrar ao menos uma articulação na segunda.

```mermaid
flowchart TD
    Entrada([Início]) --> AbrirLista[Abrir a aba Pacientes]
    Entrada --> AbrirDashboard[Abrir Dashboard]
    AbrirLista --> NovoPaciente[Tocar em adicionar paciente]
    AbrirDashboard --> NovoPaciente
    NovoPaciente --> Dados[Informar sexo, nome, nascimento, peso, altura e diagnóstico]
    Dados --> Obrigatorios{Nome, nascimento, peso e altura preenchidos?}
    Obrigatorios -- Não --> Dados
    Obrigatorios -- Sim --> Continuar[Tocar em Continuar]
    Continuar --> Avaliacao[Selecionar articulação e preencher medidas de goniometria e força]
    Avaliacao --> TemMedida{Ao menos uma articulação preenchida?}
    TemMedida -- Não --> Avaliacao
    TemMedida -- Sim --> SalvarPaciente[Salvar dados do paciente]
    SalvarPaciente --> PacienteSalvo{Paciente salvo?}
    PacienteSalvo -- Não --> CorrigirDados[Verificar os dados e tentar novamente]
    CorrigirDados --> Dados
    PacienteSalvo -- Sim --> SalvarAvaliacao[Salvar as avaliações articulares]
    SalvarAvaliacao --> AvaliacaoSalva{Avaliações salvas?}
    AvaliacaoSalva -- Não --> FalhaParcial[Aviso: paciente pode já estar cadastrado; confira a lista antes de tentar novamente]
    FalhaParcial --> Lista[Voltar à lista de pacientes]
    AvaliacaoSalva -- Sim --> Lista
    Lista --> AbrirPerfil[Selecionar o paciente para abrir o perfil]
```

### Consultar ou editar paciente

```mermaid
flowchart LR
    Pacientes[Aba Pacientes] --> Pesquisar[Pesquisar por nome e, se necessário, filtrar por sexo ou faixa etária]
    Pesquisar --> Selecionar[Selecionar paciente]
    Selecionar --> Perfil[Perfil do paciente]
    Perfil --> Revisar[Consultar dados pessoais, diagnóstico, mobilidade, dor, exercícios e sessões]
    Perfil --> Editar[Tocar em editar]
    Editar --> Alterar[Atualizar dados pessoais]
    Alterar --> Validar{Nome, nascimento, peso e altura preenchidos?}
    Validar -- Não --> Alterar
    Validar -- Sim --> Salvar[Salvar alterações]
    Salvar --> Lista[Voltar à lista de pacientes]
```

No perfil, as sessões do paciente também podem ser abertas para consultar seus detalhes. A evolução da dor (EVA) e dos exercícios é apresentada quando há registros disponíveis.

## 3. Agendar e acompanhar sessão

Uma sessão pode ser criada pelo Dashboard, pelo botão de adicionar nos detalhes de um dia, ou pela tela Sessão. Na criação, escolha paciente, data, horários e, se necessário, repetição semanal.

```mermaid
flowchart TD
    Entrada([Dashboard, detalhes de um dia ou aba Sessão]) --> NovaSessao[Abrir cadastro de sessão]
    NovaSessao --> Dados[Selecionar paciente, data, início, término e descrição opcional]
    Dados --> Recorrencia{Repetir semanalmente?}
    Recorrencia -- Sim --> Ativar[Ativar repetição semanal]
    Recorrencia -- Não --> Validar[Validar paciente e horários]
    Ativar --> Validar
    Validar --> DadosValidos{Paciente selecionado e término após início?}
    DadosValidos -- Não --> Dados
    DadosValidos -- Sim --> Salvar[Salvar sessão]
    Salvar --> Agenda[A sessão aparece na aba Sessão, no Início e no calendário]

    Agenda --> Abrir[Selecionar a sessão]
    Abrir --> Detalhes[Detalhes da sessão]
    Detalhes --> Status{Qual foi o resultado?}
    Status -- Executada --> Registro[Selecionar Executada e registrar EVA, queixa principal e exame físico/evolução]
    Registro --> SalvarExec[Salvar alterações]
    SalvarExec --> Exercicios[Abrir exercícios da sessão]
    Exercicios --> SelecionarEx[Selecionar exercícios e informar repetições ou tempo]
    SelecionarEx --> TemExercicio{Ao menos um exercício selecionado?}
    TemExercicio -- Não --> SelecionarEx
    TemExercicio -- Sim --> SalvarEx[Salvar exercícios]
    SalvarEx --> SessaoConcluida([Sessão e exercícios registrados])

    Status -- Cancelada pelo paciente --> Motivo[Selecionar status e informar o motivo]
    Status -- Não executada --> Motivo
    Motivo --> SalvarMotivo[Salvar alterações]
    SalvarMotivo --> Voltar([Voltar à lista ou agenda])

    Detalhes --> Encerrar{É uma sessão recorrente que deve parar?}
    Encerrar -- Sim --> Desativar[Encerrar recorrência]
    Desativar --> Voltar
```

Para marcar uma sessão como **Executada**, os três registros clínicos são obrigatórios: EVA, queixa principal e exame físico/evolução. Depois, vincule pelo menos um exercício e informe sua medida (repetições ou tempo). Para **Cancelada pelo paciente** ou **Não executada**, informe o motivo; não é necessário preencher os campos clínicos. Sessões recorrentes podem ser encerradas pelos detalhes da sessão, sem apagar as ocorrências anteriores.

## 4. Cadastrar e vincular exercícios

```mermaid
flowchart TD
    Entrada([Dashboard ou exercícios de uma sessão]) --> Novo[Toque em cadastrar exercício]
    Novo --> Formulario[Informar nome, categoria e forma de medição]
    Formulario --> Obrigatorios{Nome, categoria e medição preenchidos?}
    Obrigatorios -- Não --> Formulario
    Obrigatorios -- Sim --> Descricao[Adicionar descrição, se desejar]
    Descricao --> Salvar[Salvar exercício]
    Salvar --> Retorno[Voltar à tela anterior]

    Retorno --> Vincular{Cadastro começou nos exercícios da sessão?}
    Vincular -- Não --> Fim([Exercício disponível na biblioteca])
    Vincular -- Sim --> Selecionar[Selecionar exercício na lista da sessão]
    Selecionar --> Medida[Informar repetições ou tempo]
    Medida --> Mais{Vincular outro exercício?}
    Mais -- Sim --> Selecionar
    Mais -- Não --> SalvarSessao[Salvar exercícios da sessão]
    SalvarSessao --> FimSessao([Voltar aos detalhes ou à aba Sessão])
```

## 5. Consultar agenda e sessões

- **Início:** percorra os dias e selecione um para ver as sessões daquele dia. Nos detalhes do dia, toque em uma sessão para abrir seu registro ou use **Cadastrar nova sessão**.
- **Calendário:** abra pelo ícone de calendário no Início, navegue entre meses e toque em uma sessão para ver seus detalhes.
- **Sessão:** pesquise pelo nome do paciente e/ou filtre por data; selecione um resultado para consultar ou atualizar o status.
- **Pacientes:** pesquise e filtre a lista, abra o perfil e consulte dados clínicos, avaliações, evolução e sessões vinculadas.

## 6. Resumo dos principais caminhos

| Objetivo | Caminho principal |
| --- | --- |
| Criar usuário | Login → Cadastro → Login |
| Cadastrar paciente | Pacientes ou Dashboard → Novo paciente → Dados pessoais → Goniometria e força → Salvar |
| Editar paciente | Pacientes → Perfil → Editar → Salvar |
| Agendar sessão | Dashboard, Início ou Sessão → Cadastrar sessão → Salvar |
| Registrar atendimento | Início, Calendário ou Sessão → Detalhes da sessão → Executada → Registro clínico → Exercícios → Salvar |
| Marcar ausência/cancelamento | Detalhes da sessão → Cancelada pelo paciente ou Não executada → Informar motivo → Salvar |
| Cadastrar exercício | Dashboard ou exercícios da sessão → Cadastrar exercício → Salvar |
| Encerrar sessão semanal | Detalhes da ocorrência recorrente → Encerrar recorrência |
