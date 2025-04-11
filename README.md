# TarefaExpress

[Video de Explicação](https://www.youtube.com/watch?v=KgLNYpeX0JI)

### Banco de Dados
- No arquivo appsettings.json, colocar a connection string no valor da chave TaskDatabase com host, usuário e senha para conectar com o banco de dados Sql Server
- Para gerar o banco posicione a linha de comando na pasta do projeto TaskManager.Api e execute os comandos:
    - `dotnet new tool-manifest`
    - `dotnet tool install dotnet-ef --version 8.0.15`
    - `dotnet dotnet-ef migrations add M1 --project ../TaskManager.Infrastructure/TaskManager.Infrastructure.csproj`
    - `dotnet dotnet-ef database update --project ../TaskManager.Infrastructure/TaskManager.Infrastructure.csproj`

### API
- Abrir a pasta do projeto TaskManager.Api na linha de comando e executar `dotnet run` (Necessário a SDK do .net 8.0 instalada)

### FRONT-END
- Precisa executar os arquivos da pasta FrontEnd em algum servidor. Abra a pasta FrontEnd no Visual Studio Code, instala a extensão Live Server, depois irá aparecer no canto inferior direito um botão Go Live, clique nele e abrirá a página do gerenciador de tarefas