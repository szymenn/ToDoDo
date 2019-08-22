[![Build Status](https://dev.azure.com/zalewsks7/ToDo/_apis/build/status/szymenn.ToDoDo?branchName=master)](https://dev.azure.com/zalewsks7/ToDo/_build/latest?definitionId=3&branchName=master)
[![Coverage](http://mysonas.eastus.azurecontainer.io:9000/api/project_badges/measure?project=ToDoDo&metric=coverage)](http://mysonas.eastus.azurecontainer.io:9000/dashboard?id=ToDoDo)
[![Bugs](http://mysonas.eastus.azurecontainer.io:9000/api/project_badges/measure?project=ToDoDo&metric=bugs)](http://mysonas.eastus.azurecontainer.io:9000/dashboard?id=ToDoDo)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
# ToDoDo
Todo list app implementation I made to learn basics of frontend web development using React.js and Redux as well as to practice my backend coding skills 
## Live
Live version of the app is accessible [here](https://tododoapp.azurewebsites.net/). Note that the app is using SendGrid free account plan, which limits account registration to 100 per day. 
## Used technologies
### CI/CD, deployment tools 
- Azure DevOps
- Azure app services
- SonarQube
### ToDoDo backend
- C# 7.3
- ASP.NET Core 2.2 
- Entity Framework Core
- Automapper
- SqlServer
- xUnit
- Moq
### ToDoDo frontend
- JavaScript
- React.js 
- Redux
- React Router
- Reactstrap
## Features
- Authentication and Authorization using Json Web Token
- Creating, editing and deleting tasks
- Storing tasks in database
## Screenshots
<img src="https://github.com/szymenn/ToDoDo/blob/master/screenshots/ToDoDoRegister.png" width="200" height="400" />
![Alt Text](https://github.com/szymenn/ToDoDo/blob/master/screenshots/ToDoDoRegister.png)
![Alt Text](https://github.com/szymenn/ToDoDo/blob/master/screenshots/ToDoDoToDos.png)
## Todo
- Write more unit tests and add some integration tests
