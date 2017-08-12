
1. Escolher o caminho Físico para publicação do Site com as Api's:

Exemplo : D:\EnquetePublished

2. Criar no caminho físico escolhido a subpasta App_Data, nesta pasta serão gravados os arquivos 
no formato .txt, que armazenarão os dados da aplicação. 
Portanto não será necessário qualquer banco de dados especifico.

Exemplo : D:\EnquetePublished\App_Data

arquivos de armazenamento que a aplicação criará :

Polls:

Poll-1.txt
Poll-2.txt
etc...

Views :

view-1-11082017 154339.txt
view-1-11082017 154411.txt


3. Abaixo, as configurações para o IIS, para publicação do WebApp com as APis do sistema :

a. No IIS opção Sites, clicar com botão direito --> Adicionar site

b. Na Janela Adicionar Site, preencher os seguintes campos :

	Nome do site : EnqueteApi
	Pool de aplicativos : botão Selecionar, escolher .NET v4.5
	Caminho físico : D:\EnquetePublished
	Porta : 8080
	Iniciar site automaticamente : Marcar checkbox
	Clicar em OK
	
c. Entrar em qualquer Browse de internet : digitar localhost:8080

d. O site com as Api's estara rodando.


4. Para rodar a Aplicação cliente 

a. Descompactar o arquivo ConsoleApp.rar
b. Dentro da pasta ConsoleApp, executar o arquivo ConsoleApp.exe
c . A aplicação apresentará o menu para escolha de opções :

	1. Adicionar enquete
	2. Consultar enquete
	3. Registar voto
	4. Consultar estatística
	5. Fechar

d. Toda as opções acima retornarão o Json referente a operação realizada.

e. Os dados armazenados poderão ser visualizados nos arquivos textos gravados na pasta App_Data,
criada anteriormente, com qualquer editor de texto.


f. A aplicação cliente console e o site das Apis foram criados com Visual Studio 2017, mas pode ser 
aberta e compilada também nas versoes 2013 e 2015.

g. Segue anexado tambem o arquivo contendo os fontes do projeto, em formato .rar.



