# Main AltV Project
This includes the Server and Client resources.

# Local Machine Dependencies

- Latest [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- NodeJS [NodeJS](https://nodejs.org/en)
  - Use [NVM for Windows](https://github.com/coreybutler/nvm-windows) or [NVM linux](https://github.com/nvm-sh/nvm)

# Resources

- `alt:V` Documentation <https://docs.altv.mp/index.html>
- `alt:V` Download <https://altv.mp/#/downloads>
- `Server` EFCore <https://learn.microsoft.com/de-de/ef/core/>

## Getting your hands

Clone the Repository
````
    git clone git@github.com:DecisionV/AltV.git
````
- Download the latest Release/RC/Dev AltV Server Version (ask in team which branch) [here](https://altv.mp/#/downloads)  
Select everything on the download page, but only extract the chat example resource.  
- Extract the Server files into the root of this repository.

### Setup Configurations
- edit the server.toml file and enable debug by adding ````debug: true```` to your file for development servers
  - you can find a description on how to config it [here](https://docs.altv.mp/articles/configs/server.html)
- create a GameConfig.json and a DatabaseConfig.json in the Core project. You can copy the example files for an easy setup.
- edit your GameConfig.json and DatabaseConfig.json. These files will be exported on Building the Solution.

### Build the Solution
- Build Client see Client [README](js-client/README.md)
- Build the complete solution with your favorite tool, we recommend [Rider](https://www.jetbrains.com/de-de/rider/).
- After a successful build every resource is exported automatically to the resource folder.

### Start the Server
- Run the altv-server.exe (for windows) but keep in mind to Deploy or Host the [internal webviews](https://github.com/DecisionV/InternalUI)

### Development and Contribution
- Follow the development Guides and get a development partner for pair programming and code reviews.
- Allways work on a feature branch prefixed with feat/ fix/ chore/ depending on the story or bug you are working on.
- We Never ever push to the main branch. Use a pull request (PR) and get a PR review before merging into the main branch.