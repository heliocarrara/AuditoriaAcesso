# Desafio de Seleção Técnica: Desenvolvedor Full Stack

Este documento descreve o desafio prático para a vaga de Desenvolvedor. O objetivo é avaliar sua capacidade técnica na construção de sistemas robustos, aplicando boas práticas de arquitetura, segurança e qualidade de código no ecossistema Microsoft.

---

## 1. Escopo do Projeto: Sistema de Auditoria de Acesso

Você deve desenvolver uma aplicação *full-stack* que gerencie usuários e registre logs de acesso. O foco principal não é apenas a funcionalidade, mas a estrutura e a manutenibilidade do código.

### Requisitos Funcionais:

1. **Gerenciamento de Usuários:**
* **Cadastro:** Criar um endpoint `POST` para cadastrar usuários (Nome, E-mail, Senha). A senha deve ser armazenada com hash (BCrypt).
* **Listagem:** Criar um endpoint `GET` para listar usuários.
* **Exclusão:** Criar um endpoint `DELETE` para remover um usuário. **Regra de Negócio:** Não permitir a exclusão de usuários que possuam logs de acesso vinculados.


2. **Auditoria:**
* Toda vez que um usuário for cadastrado, um registro deve ser criado automaticamente na tabela `LogsAcesso` (DataAcesso, IpAddress).


3. **Front-end:**
* Interface em React ou Vue.js.
* Formulário de cadastro com validação de campos.
* Tabela com listagem de usuários e botão para exclusão.
* Integração via API (Fetch/Axios).



---

## 2. Requisitos Técnicos

* **Back-end:** .NET 10 (Web API).
* **Banco de Dados:** Microsoft SQL Server (utilizar EF Core).
* **Front-end:** React ou Vue.js (utilizar componentes).
* **Segurança:** Uso obrigatório de DTOs (não exponha entidades do banco) e hashing de senhas.
* **Testes:** Implementar no mínimo 2 testes unitários (xUnit ou NUnit) validando regras de negócio críticas.

---

## 3. Arquitetura e Estrutura (DDD-Lite)

Para garantir a qualidade, o projeto **deve** ser organizado seguindo a separação de camadas. Sua estrutura deve refletir claramente a divisão abaixo:

| Camada | Responsabilidade |
| --- | --- |
| **Domain** | Contém as **Entidades** de negócio, as regras de validação (ex: regra de exclusão) e os *Value Objects*. Esta camada não deve possuir dependências externas de framework. |
| **Application** | Contém a orquestração (Services/Use Cases) e os **DTOs**. É a camada responsável por receber a requisição, validar, chamar o domínio e retornar a resposta. |
| **Infrastructure** | Contém a implementação do acesso a dados (**EF Core**, Migrations, Repositórios), configurações de banco e integrações externas. |

*Nota: O uso de **Injeção de Dependência** é obrigatório para conectar essas camadas.*

---

## 4. Critérios de Avaliação

Seu projeto será avaliado com base nos seguintes pilares:

* **Arquitetura:** Aplicação correta dos princípios de camadas (DDD-Lite) e uso de injeção de dependência.
* **Qualidade de Código:** Legibilidade, convenções de nomenclatura, métodos concisos e ausência de lógica de negócio no Controller.
* **Segurança:** Implementação de hashing, tratamento correto de erros (HTTP Status Codes) e prevenção de exposição de dados internos.
* **Git:** Histórico de commits organizados, atômicos e descritivos.
* **Documentação:** Arquivo `README.md` contendo:
* Instruções de como rodar o banco de dados e aplicar migrations.
* Como executar o back-end e o front-end.
* Breve explicação das decisões de design tomadas.



---

## 5. Instruções de Entrega

1. Suba o código para um repositório público no **GitHub**.
2. Certifique-se de que o arquivo `README.md` está claro e completo.

Boa sorte!