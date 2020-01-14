[![CircleCI](https://circleci.com/gh/szymenn/ToDoDo-backend.svg?style=svg)](https://circleci.com/gh/szymenn/ToDoDo-backend)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ToDoDo&metric=coverage)](https://sonarcloud.io/dashboard?id=ToDoDo)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=ToDoDo&metric=bugs)](https://sonarcloud.io/dashboard?id=ToDoDo)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
# ToDoDo-backend
ASP.NET Core 2.2 Web API for todo list app implementation I made, frontend code is available [here](https://github.com/szymenn/ToDoDo-frontend)
## Live
Live version of the app is accessible [here](https://szymenn.github.io/ToDoDo-frontend/). Note that the app is using [SendGrid](https://sendgrid.com/pricing/) free account plan, which limits account registration to 100 per day. 
## Used technologies
### CI/CD, deployment tools 
- CircleCI
- Heroku 
- Docker
- SonarCloud
### API
- C# 7.3
- ASP.NET Core 2.2 
- Entity Framework Core
- Automapper
- SqlServer
- xUnit
- Moq
## Features
- Authentication and Authorization using Json Web Token
- Creating, editing and deleting tasks
- Storing tasks in database
## Todo
- Write more unit tests and add some integration tests
## Screenshots
<img src="https://github.com/szymenn/ToDoDo/blob/master/screenshots/ToDoDoHome.png" />
<img src="https://github.com/szymenn/ToDoDo/blob/master/screenshots/ToDoDoRegister.png" />
<img src="https://github.com/szymenn/ToDoDo/blob/master/screenshots/ToDoDoToDos.png" />

