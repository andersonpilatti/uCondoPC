## Hands On - uCondo
Como se trata de um teste básico não foi implementada autenticação, 
sendo assim todos os métodos são públicos


## Aplicar Migration
No projet src/API altere o arquivo:   
	- appsettings.json

A propriedade DefaultConnection da seção ConnectionStrings para refletir a conexão com 
o banco no qual irá executar a aplicação


No console acesse a pasta da solução e execute o comando	
	dotnet ef database update -p src\Data -s src\API


## Executar testes unitários
Os testes são executados numa versao do contexto inMemory 
No console acesse a pasta da solução e execute o comando
	dotnet test


## Metodos da API
Toda a documentação para a API encontra-se online no enderço da api/swagger
Em todos os metodos são passados os números das contas ao invés do Id

** Para adicionar uma conta
	Deve ser enviado os dados em formato json de acordo com documentação
		POST ​/api​/v1​/PlanoConta​/Inclusao

** Para remover uma conta
	Informar apenas o código da conta, a conta não pode ter contas filhas
		DELETE ​/api​/v1​/PlanoConta​/Exclusao

** Listagem de conta
	Este metodo recupera todas as contas que podem ter filhos criados ou seja, apenas as 
	contas que não permitem receber lançamentos
		GET ​/api​/v1​/PlanoConta​/ListarContasPaiElegiveis

** Listagem geral
	Criada no estilo infinito deve ser chamada ao chegar no final da lista solicitando os 
	proximos registros caso existam, os filtros são realizados pelo nome e pelo código
		POST ​/api​/v1​/PlanoConta​/ListarContas

	Os parmetros sao:
	  "length": qtde de registros a ser retornado
	  "start": posicao do ultimo registro visualizado
	  "search": termo para busca nos campos nome e codigo

	Paremtros adicionais no retorno
	  "recordsTotal": total de registros existentes na tabela
	  "recordsFiltered": total de registro recuperados para a requisicao em questao

** Sugestao de codigo de conta
	Utilizado para gerenciar o melhor codigo de conta a ser atribuido a uma nova conta
	Caso não seja inforado uma conta pai, será sugerido o proximo codigo de uma conta raiz
		GET ​/api​/v1​/PlanoConta​/SugestaoCodigoConta


## Feedback
Anderson Caetano Pilatti
<a href="mailto:anderson@tyle.com.br">anderson@tyle.com.br</a>
(41) 9 9925-7281
