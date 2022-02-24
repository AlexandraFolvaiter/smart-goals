![1](https://user-images.githubusercontent.com/17809789/155537976-960b0b3a-ebb5-41ab-ac70-d691b768c945.png)

# Table of Contents

1. [Motivation](#motivation)
2. [Project description](#project-description)
4. [Infrastructure Architecture & Technologies](#Infrastructure-Architecture-&-Technologies)
5. [Demo](#demo)
6. [License](#license)

# Motivation

SmartGoals project was created to participate in the [Microsoft Azure Trial Hackathon](https://dev.to/devteam/hack-the-microsoft-azure-trial-on-dev-2ne5) and to show the process of integrating an application in the Azure ecosystem.

# Project description

The main idea on which this project is created can be found in [this file](https://github.com/AlexandraFolvaiter/smart-goals/blob/main/CONCEPTS.md).
 
The project aims to do the management of the goals and objectives that a person has, giving the opportunity to be accessible anywhere by any device. The user can update them as often as needed, keeping the progress and having an overview of the next steps that he has to make in order to complete the goal.

# Infrastructure Architecture & Technologies
The application is based on 3 components:
- **A Client** - Is an Angular application deployed on a **Azure App Service**. It allows users to see and manipulate the goals and objectives using a friendly UI.
- **An API** - Is an .Net 6.0 Azure Function deployes on a **Azure Function App**. It exposes an API with CRUD operations to manipulate the goals and the objectives.
- **A Database** - Is a **Azure SQL Database** which stores the data.

![SmartGoals_structure_3](https://user-images.githubusercontent.com/17809789/155595336-af7c269e-9bed-4377-b27e-efb9e835a575.jpg)

# Demo

# License
[MIT License](https://github.com/AlexandraFolvaiter/smart-goals/blob/main/LICENSE)
