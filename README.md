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

-----

## 📖 Sobre o Projeto

Este sistema foi desenvolvido como a primeira demanda estratégica para a **Leve Investimentos**. A aplicação consiste em um ecossistema corporativo seguro focado na gestão de colaboradores e na distribuição inteligente de demandas operacionais.

A arquitetura do projeto segue o padrão **Clean Architecture (N-Camadas)**, separando as responsabilidades em `Core`, `Application`, `Infrastructure` e `Web` para garantir um software coeso, extensível e alinhado às melhores práticas de desenvolvimento.

-----

## ✨ Funcionalidades

O sistema possui regras de controle de acesso baseadas em perfis administrativos:

### Perfil Gestor:
  - 👤 **Controle Total de Usuários (CRUD):** Cadastrar novos colaboradores, listar a equipe ativa, editar informações cadastrais e excluir perfis com segurança.
  - 🖼️ **Upload de Mídia:** Botão nativo para anexar e salvar a foto real do funcionário no servidor.
  - 📝 **Gestão de Demandas:** Agendar tarefas para qualquer subordinado direto, definindo instruções detalhadas e data/hora limite.
  - 🔍 **Acompanhamento:** Visualizar o andamento em tempo real de todas as atividades distribuídas no painel corporativo.

### Perfil Subordinado:
  - 🔐 **Autenticação Segura:** Login individual protegido por e-mail e senha em cookies de sessão.
  - 📊 **Visualização:** Acesso exclusivo ao painel de tarefas sob sua responsabilidade, sem permissão para acessar áreas de cadastro da diretoria.

### 📧 Funcionalidades Desejáveis:
  - Disparo automático de notificações via e-mail corporativo estruturado na camada de serviços (`EmailService`) para alertar subordinados sobre novas tarefas ou notificar gestores sobre finalizações.

-----

## 🛠️ Tecnologias Utilizadas

  - **Camada Web (Apresentação):** C# com ASP.NET Core MVC
  - **Mecanismo de Renderização:** Razor Pages (HTML5, JavaScript, jQuery)
  - **Design & UI:** Framework CSS UIkit (Padrão Visual)
  - **Persistência & Banco de Dados:** SQL Server via Entity Framework Core

-----

## 🚀 Começando

Siga os passos abaixo para configurar, restaurar e rodar o projeto em seu ambiente local.

### Pré-requisitos

  - **.NET SDK:** Versão 6.0 ou superior instalada.
  - **SQL Server:** Instância ativa (LocalDB ou SQL Express).

### Instalação e Execução

1. **Clone o Repositório**
   ```sh
   git clone [https://github.com/LuizHenriqueGon/LeveInvestimentos.git](https://github.com/LuizHenriqueGon/LeveInvestimentos.git)
   ```

2. **Configure a String de Conexão**
Abra o arquivo `LeveInvestimentos.Web/appsettings.json` e verifique a sua conexão com o banco de dados. O padrão já vem configurado para o LocalDB do Visual Studio:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LeveInvestimentosDb;Trusted_Connection=True;"
}

```


3. **Abra o Terminal na Raiz do Projeto**
```sh
cd LeveInvestimentos

```


4. **Restaure as Dependências e compile o Sistema**
```sh
dotnet restore
dotnet build

```


5. **Execute a Aplicação**
Navegue até a pasta do projeto Web e inicie o servidor:
```sh
cd LeveInvestimentos.Web
dotnet run

```


6. **Acesse o Sistema**
Abra seu navegador e acesse a URL gerada no terminal (geralmente `https://localhost:7000` ou `http://localhost:5000`).

---

## 🔐 Dados de Acesso Inicial (Seed do Banco)

O banco de dados é criado e populado de forma automática no primeiro acesso através do componente `DataSeeder`. Use as credenciais abaixo para realizar o primeiro acesso administrativo:

| E-mail de Acesso | Senha Padrão | Nível de Permissão |
| --- | --- | --- |
| `ti@leveinvestimentos.com.br` | `teste123` | **Gestor (Administrador Master)** |

---

## 🤔 Solução de Problemas Comuns

* **`SqlException: Cannot insert duplicate key`**: O sistema possui uma trava de índice único no banco de dados para a coluna de e-mail. Você está tentando cadastrar um e-mail que já existe. O sistema interceptará isso e exibirá um alerta em vermelho na tela.
* **`PendingModelChangesWarning`**: Se você realizar alterações nas entidades de domínio, o Entity Framework exigirá que você gere uma nova migration antes de rodar o servidor. Execute os comandos de `migrations add` e `database update` para corrigir.
* **Erro 403 / Forbid na Tela de Cadastro**: Você realizou o login com uma conta que possui perfil de subordinado. Apenas usuários marcados com o perfil de Gestor podem cadastrar novos funcionários ou agendar tarefas.

---

## 👥 Desenvolvedor Responsável

Este projeto foi projetado, arquitetado e refinado por:

* [Luiz Henrique Gonçalves](https://github.com/LuizHenriqueGon)

---
