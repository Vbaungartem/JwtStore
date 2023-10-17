# Implementando Segurança e Autenticação de API’s [ASP.NET](http://ASP.NET) com JWT e Bearer authentication JwtStore🚀

### Não esqueça de deixar sua ⭐!!!

Meu Linkedin: [https://www.linkedin.com/in/vitório-baungartem-221041192/](https://www.linkedin.com/in/vit%C3%B3rio-baungartem-221041192/)

Meu github: [https://github.com/Vbaungartem](https://github.com/Vbaungartem)

Repositório do Projeto: https://github.com/Vbaungartem/JwtStore

---

## **OBSERVAÇÕES IMPORTANTES:**

O projeto construído teve como objetivo principal acompanhar, construir e finalizar o curso de **Segurança em APIs ASP.NET com JWT e Bearer Authentication** ministrada pelo Professor André Baltieri, proprietário da plataforma Balta.io. 

Tanto os código encontrados aqui, quanto as anotações dispostas no Readme e no docs.zip do projeto tem o objetivo de implementar, explicar e validar meus conhecimentos na área de segurança e desenvolvimento com C# e .Net Core.

Todo o código que objetivava o acompanhamento e aprendizado do curso foi implementado.

Á medida que os códigos desse projeto forem evoluindo se tornando mais sólidos seguindo os Patterns de construção ensinados no curso, eles poderão ser conferidos a partir dos novos commits e branchs que forem sendo implementadas. 

---

Comandos iniciais utilizados:

1. Primeiramente é inicializado um projeto com o padrão Minimal API que é um padrão totalmente reduzido e otimizado do AspNet Core. 

```sql
dotnet new classlib JwtStore.Core
dotnet new classlib JwtStore.Infra

dotnet new web -o JwtStore.Api

cd nomeProjeto
```

1. Outros packages importantes foram adicionados aos seus devidos classlibs de acordo com a necessidade do projeto: 

```sql
donet add package Microsoft.AspNetCore.Authentication.JwtBearer // package de autenticação
donet add package Flunt // Validação e Notifications
dotnet add package MediatR // Mediator de Requisições
```

---

## Patterns:

O projeto conta com um poderoso Pattern também citado e ensinado de forma enxuta no curso, em que se observa um crescimento horizontal do projeto, que basicamente é separado em 3 camadas, sendo elas:

- A Api → A própria aplicação do projeto
- O Core → Central de processamento e concentração das regras específicas de negócio do projeto
- A Infra → Onde se concentram as configurações e Implementations que irão descer ao banco e fazer as devidas movimentações.

Vale dizer também que o projeto está escopado em função de contextos, sendo que cada contexto sintetizado possui sua própria atomicidade derivando seus:

- Entidaes
- UseCases (regras de negócio e suas Interfaces [Contracts])
- ValueObjects
- Mappings (Database Configuration [ORM])

Além disso, o projeto conta com pattern de Extension Methods que através de classes estáticas organizam melhor a fluidez do código e permitem uma implementação limpa e reduzida em arquivos importantes como o Program.cs (arquivo que inicializa e coordena o funcionamento do projeto). 

Contexts sintetizados no Projeto:

- Create (user)
- Authenticate (user)

Endpoints da API:

```json
//api/v1/auth -> autenticar usuário existente:
{
	"Email": "userEmail@test.com"
	"Password": "userP4$$w0rd"
}

Response:
{
	"data": {
	        "token": "",
	        "id": "",
	        "name": "",
	        "email": "",
	        "roles": [
	            "",
	            "",
	        ]
	    },
	    "message": "Autenticação realizada com sucesso!",
	    "status": 200,
	    "isSuccess": true,
	    "notifications": null
	}
}
```

```json
//api/v1/users -> inserir novo usuario:
{
	"Email": "userEmail@test.com",
	"Name" : "userName",
	"Password": "userP4$$w0rd"
}

Response:
{
    "data": {
        "id": "...",
        "name": "...",
        "email": "..."
    },
    "message": "Cadastro realizado com sucesso. Obrigado!",
    "status": 201,
    "isSuccess": true,
    "notifications": null
}
```

---

### Instalação do código em um Ambiente de Desenvolvimento:

[coming soon…]
