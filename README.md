
# Datum Blog

Projeto de cadastro de blog desenvolvido com as funcionalidades de cadastro de usuário, autenticação de usuário, CRUD (Criação, Consulta, Atualização e Exclusão) de uma publicação do blog e envio de notificação para os usuários sobre novas postagens em tempo real.


## Desenvolvimento

- .NET 8
- ORM Entity Framework
- Banco de dados PostgreSQL
- Biblioteca SignalR para comunicação WebSockets
- JWT (JSON Web Token)

## Documentação da API

#### Criar um novo usuário

```http
  POST /api/Usuario/Criar
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Nome` | `string` | **Obrigatório**. Nome do usuário a ser cadastrado |
| `Email` | `string` | **Obrigatório**. E-mail do usuário que será utilizado como credencial de autenticação. |
| `Senha` | `string` | **Obrigatório**. Senha do usuário que será utilizado como credencial de autenticação. |

#### Gerar token de autenticação
Retorna um token gerado após a autenticação das credenciais do usuário. Esse token deve ser enviado em todas as requisições que necessitem de autenticação. Esse token possui validade de 1 hora.

O token deve ser enviado junto ao header da requisição.
**Exemplo:** "Bearer 12345abcdef"

```http
  POST /api/Token/Gerar
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Email`      | `string` | **Obrigatório**. E-mail do usuário cadastrado no banco de dados. |
| `Senha`      | `string` | **Obrigatório**. Senha do usuário cadastrado no banco de dados. |

#### Criar uma publicação no blog
*Requerido o envio do token de autenticação.*

```http
  POST /api/Publicacao/Criar
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Titulo`      | `string` | **Obrigatório**. Título da publicação. |
| `Conteudo`      | `string` | **Obrigatório**. Conteúdo da publicação. |

#### Editar uma publicação do blog
*Requerido o envio do token de autenticação.*

```http
  PUT /api/Publicacao/Editar
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `PublicacaoId`      | `string` | **Obrigatório**. Identificador da publicação que será editado. |
| `Titulo`      | `string` | Novo título da publicação a ser alterado. |
| `Conteudo`      | `string` | Novo conteúdo da publicação a ser alterado. |

#### Excluir uma publicação do blog
*Requerido o envio do token de autenticação.*

```http
  DELETE /api/Publicacao/Excluir
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `PublicacaoId`      | `string` | **Obrigatório**. Identificador da publicação que será excluído. |

#### Consultar as publicações do blog

```http
  GET /api/Publicacao/Consultar
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Titulo`      | `string` | Parte ou totalidade do título que deseja considerar como filtro. |

Retorno
| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `Id`      | `string` | Identificador da publicação |
| `Titulo`      | `string` | Título da publicação |
| `Conteudo`      | `string` | Conteúdo da publicação |
| `DataPublicacao`      | `string` | Data de cadastro da publicação |
| `UsuarioPublicacao`      | `string` | Usuário que cadastrou a publicação |
| `EmailUsuarioPublicacao`      | `string` | E-mail do usuário que cadastrou a publicação |

## Screenshots

![App Screenshot](https://github.com/cleitonzummach/datumblog/blob/main/Datum.Blog.WebApi/PrintSwagger.PNG)


## Autor

- [Cleiton Zummach](https://www.github.com/cleitonzummach)

