# 💼 Leve Investimentos - Sistema de Gestão e Agendamento

<p align="center">
  <strong>Uma plataforma corporativa robusta para gestão de equipes, controle de acesso e agendamento de tarefas.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Linguagem-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="Linguagem C#">
  <img src="https://img.shields.io/badge/Framework-ASP.NET%20Core%20MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core MVC">
  <img src="https://img.shields.io/badge/Banco%20de%20Dados-SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="SQL Server">
  <img src="https://img.shields.io/badge/Interface-UIkit%20CSS-3A8DFD?style=for-the-badge&logo=uikit&logoColor=white" alt="Framework UIkit">
  <img src="https://img.shields.io/badge/Status-Completo-green?style=for-the-badge" alt="Status: Completo">
</p>

---

## 📖 Sobre o Projeto

Este sistema foi desenvolvido como a primeira demanda estratégica para a Leve Investimentos. A aplicação consiste em um ecossistema corporativo seguro focado na gestão de colaboradores e na distribuição inteligente de demandas operacionais.

A arquitetura do projeto segue o padrão Clean Architecture (N-Camadas), separando as responsabilidades em Core, Application, Infrastructure e Web para garantir um software altamente coeso, extensível e de fácil manutenção.

---

## ✨ Funcionalidades

O sistema foi projetado com perfis distintos para garantir a segurança e organização dos processos.

### Para Gestores:
- Controle Total de Usuários (CRUD): Cadastrar novos colaboradores, listar a equipe ativa, editar informações cadastrais e excluir perfis com segurança.
- Upload de Mídia: Botão nativo para anexar e salvar a foto real do funcionário no servidor.
- Gestão de Demandas: Agendar tarefas para qualquer subordinado direto, definindo instruções detalhadas e data/hora limite.
- Acompanhamento: Visualizar o andamento em tempo real de todas as atividades distribuídas no painel corporativo.

### Para Subordinados:
- Autenticação Segura: Login individual protegido por e-mail e senha em cookies de sessão.
- Visualização: Acesso exclusivo ao painel de tarefas sob sua responsabilidade, sem permissão para acessar áreas de cadastro da diretoria.

---

## 🛠️ Tecnologias Utilizadas

- Backend: C# com ASP.NET Core MVC
- Mecanismo de Renderização: Razor Pages (HTML5, JavaScript, jQuery)
- Design & UI: Framework CSS UIkit
- Persistência & Banco de Dados: SQL Server via Entity Framework Core

---

## 🚀 Como Executar o Projeto

Siga os passos abaixo para configurar, restaurar e rodar o projeto em seu ambiente local.

### Pré-requisitos
- .NET SDK: Versão 6.0 ou superior instalada.
- SQL Server: Instância ativa (LocalDB ou SQL Express).

### Instalação e Execução
1. Clone o Repositório
   git clone https://github.com/LuizHenriqueGon/LeveInvestimentos.git

2. Configure a String de Conexão
   Abra o arquivo LeveInvestimentos.Web/appsettings.json e verifique a conexão com o banco de dados.

3. Abra o Terminal na Raiz do Projeto
   cd LeveInvestimentos

4. Restaure as Dependências e compile o Sistema
   dotnet restore
   dotnet build

5. Execute a Aplicação
   cd LeveInvestimentos.Web
   dotnet run

6. Acesse o Sistema
   Abra seu navegador e acesse a URL gerada no terminal (geralmente http://localhost:5000).

---

## 🔐 Dados de Acesso Inicial

O banco de dados é criado e populado de forma automática no primeiro acesso através do componente DataSeeder. Use as credenciais abaixo para realizar o primeiro acesso administrativo:

| E-mail de Acesso | Senha Padrão | Nível de Permissão |
| :--- | :--- | :--- |
| ti@leveinvestimentos.com.br | teste123 | Gestor (Administrador Master) |

---

## 🤔 Solução de Problemas Comuns

- SqlException: Cannot insert duplicate key: O sistema possui uma trava de índice único no banco de dados para a coluna de e-mail.
- PendingModelChangesWarning: Se você realizar alterações nas entidades de domínio, gere uma nova migration antes de rodar o servidor.
- Erro 403 / Forbid na Tela de Cadastro: Você realizou o login com uma conta que possui perfil de subordinado. Apenas usuários marcados com o perfil de Gestor podem acessar as rotas de Cadastro de Usuário e Agendamento de Tarefas.

---

## 👥 Desenvolvedor Responsável

Este projeto foi projetado, arquitetado e refinado por:

- Luiz Henrique Gonçalves (https://github.com/LuizHenriqueGon)

---

<p align="center">
Sistema validado e pronto para homologação técnica! 🚀
</p>
