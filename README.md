# Desafio Conductor

## PROJETOS
Conductor.Desafio.Api : Projeto da API
Conductor.Desafio.Core : Classes compartilhadas entre os projetos
Conductor.Desafio.Database : Estruturação da Database
Conductor.Desafio.Uwp : Demo de utilização da API com aplicações
Conductor.Desafio.Web : Demo de utilização da API com projetos WEB

## CRIAR O BANCO DE DADOS
Digitar o comando abaixo no Package Manager Console
```sh
Update-Database
```
Caso não exista nenhuma Migration, a mesma deve ser adicionada antes de atualizar o bando de dados através do seguinte comando:
```sh
Add-Migration NomeDaMigration
```

## Consumindo a API
```sh
Base URL: http://localhost:60072
```
```sh
Documentação Swagger: http://localhost:60072/swagger
```

### Testar API
Para testar a API pode-se utilizar os projetos Conductor.Desafio.Web, onde é possível testar todas as suas funcionalidades, e o projeto Conductor.Desafio.Uwp para exemplificar como consumir a API através de aplicações
### Path Pessoas

```sh
GET: pegar todas as pessoas.
path: /api/pessoas
Parametros: ?PorNome=&PorCpf=&NascimentoMinimo=&PorGenero=&PorEmail=
```
```sh
GET: pegar pessoas por Id.
path: /api/pessoas/{id}
```

```sh
POST: inserir pessoa.
path: /api/pessoas
model: PessoaDTO
```

```sh
POST: checar credenciais
path: /api/pessoas/check
Model: PessoaDTO
```

```sh
PUT: Inserir Pessoa
Path: /api/pessoas
Model: PessoaDTO
```
```sh
DELETE: Deletar pessoa
Path: /api/pessoas/{ID}
```



**Model: PessoaDTO**
| Propriedade | Tipo       |
| ------------|------------|
| Nome        | string     |
| Sobrenome   | string     |
| Cpf         | string     |
| Email       | string     |
| Senha       | string     |
| ConfSenha   | string     |
| Nascimento  | DateTime   |
| Genero      | GeneroEnum |
**Model: PessoaDTOLogin**
| Propriedade | Tipo       |
| ------------|------------|
| Nome        | string     |
| Senha       | string     |

### Path Contas:
```sh
GET: pegar todas as contas
path: /api/contas
Parametros: ?ativo={true ou false}
```
```sh
GET: pegar conta por ID
path: /api/contas/{id}
```
```sh
GET: pegar conta por ID da Pessoa
path: /api/contas/pessoa/{id}
```
```sh
GET: pegar saldo da conta
path: /api/contas/saldo/{id}
```
```sh
POST: Inserir Conta
path: /api/contas
Model: ContaDTO
```
```sh
PUT: Editar Conta
path: /api/contas/{id}
Model: ContaDTO
```
```sh
PUT: Desativar Conta
path: /api/contas/desativar/{id}
```
```sh
DELETE: Deletar Conta
path: /api/contas/{id}
```
**Model: ContaDTO**
| Propriedade       | Tipo    |
| ----------------- | ------  |
| Saldo             | decimal |
| LimiteSaqueDiario | decimal |
| FlagAtivo         | bool    |
| TipoContaEnum     | Tipo    |
| Descricao         | string  |
| IdPessoa          | int     |

### Path Transações
```sh
GET: pegar todas as transações
path: /api/transacoes
Parametros: ?PorConta=&PorTipo=&DataMinima=&DataMaxima=";
(As datas devem ser enviadas no padrão en-us aaaa/mm/dd)
```
```sh
GET: pegar transação por ID 
path: /api/transacoes/{id}
```
```sh
GET: pegar transação por ID da conta
path: /api/transacoes/conta/{id}
```
```sh
POST: inserir transação
path: /api/transacoes
Model: TransacaoDTO
```
```sh
DELETE: deletar transação
path: /api/transacoes/{id}
```
**Model: TransacaoDTO**
| Propriedade   | Tipo              |
| ------------- | ----------------- |
| TipoTransacao | TipoTransacaoEnum |
| Valor         | decimal           |
| Descricao     | string            |
| ContaId       | int               |
