# AGENTS.md

Este arquivo define diretrizes para agentes de IA (ex: Codex) que atuarem neste repositório.
O objetivo é manter consistência arquitetural, organização de pastas e decisões técnicas.

---

## 📦 Visão Geral do Projeto

Sistema de **controle de estoque e controle de caixa para brechó**.

Arquitetura baseada em:

- Frontend desacoplado (Next.js)
- Backend em .NET seguindo padrões de Clean Architecture e CQRS

---

## 🧱 Stack Tecnológica

### Frontend

- Next.js
- TypeScript
- Comunicação com backend via HTTP (REST)

### Backend

- .NET
- ASP.NET Web API
- PostgreSQL
- MediatR (IMediatr)
- Padrão CQRS (Command / Query)

---

## 🎨 Estrutura do Frontend (Next.js)

O frontend deve seguir a seguinte organização:

/front
├── app ou pages
│ ├── <pagina>
│ │ ├── components # Componentes específicos da página
│ │ └── page.tsx
│
├── components # Componentes genéricos e reutilizáveis
│ ├── Button
│ ├── Input
│ └── Modal
│
├── services # Comunicação com o backend
│ ├── estoque.service.ts
│ ├── caixa.service.ts
│ └── httpClient.ts
│
├── utils # Funções utilitárias
│ ├── formatters.ts
│ ├── validators.ts
│ └── dates.ts
│
└── types # Tipagens globais (DTOs do front, enums, etc)

### Regras Importantes (Frontend)

- Componentes genéricos **não** devem conter lógica específica de negócio
- Componentes de página podem conter lógica específica da tela
- Serviços devem concentrar toda comunicação com o backend
- Utilitários devem ser funções puras e reutilizáveis

---

## ⚙️ Estrutura do Backend (.NET)

O backend será dividido em múltiplos projetos:
/back
├── Api
│ ├── Controllers
│ └── Program.cs
│
├── Domain
│ ├── Models
│ ├── DTOs
│ ├── Enums
│ └── Interfaces
│
├── Persistence
│ ├── Context
│ ├── Mappings
│ └── Repositories
│
├── Service
│ ├── Commands
│ │ └── <Feature>
│ │ ├── CreateXCommand.cs
│ │ └── UpdateXCommand.cs
│ │
│ ├── Queries
│ │ └── <Feature>
│ │ └── GetXQuery.cs
│ │
│ └── Handlers
│ └── <Feature>
│ ├── CreateXHandler.cs
│ └── GetXHandler.cs

---

## 🧠 Padrões Arquiteturais (Backend)

- Controllers devem ser **finos**
- Toda regra de negócio deve estar nos **Handlers**
- Controllers apenas:
  - Recebem requisições
  - Disparam Commands ou Queries via IMediatr
  - Retornam respostas HTTP

### CQRS

- **Commands**: operações que alteram estado (Create, Update, Delete)
- **Queries**: operações de leitura
- **Handlers**: contêm a lógica de execução

---

## 🤖 Diretrizes para Agentes de IA

- Respeitar rigorosamente a estrutura de pastas definida
- Não misturar responsabilidades entre camadas
- Não adicionar lógica de negócio no frontend
- Não adicionar acesso direto ao banco fora da camada Persistence
- Sempre sugerir código alinhado à stack definida neste documento

---

## 📌 Observação

Este arquivo pode evoluir para incluir:

- Padrões de nomenclatura
- Convenções de commit
- Regras de testes
- Guidelines de performance e segurança
