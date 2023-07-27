# Drones Exercise

## Application created using .NET Technologies with EntityFramework Core, besides SQL Server as Database.

Introduction to the Exercise:

There is a major new technology that is destined to be a disruptive force in the field of transportation: the drone. Just as the mobile phone allowed developing countries to leapfrog older technologies for personal communication, the drone has the potential to leapfrog traditional transportation infrastructure. 

Useful drone functions include delivery of small items that are (urgently) needed in locations with difficult access.

## Task Description

We have a fleet of 10 drones. A drone is capable of carrying devices, other than cameras, and capable of delivering small loads. For our use case the load is medications.

Develop a service via REST API that allows clients to communicate with the drones (i.e. dispatch controller).

The specific communicaiton with the drone is outside the scope of this task.

The service should allow:
 * Registering a drone;
 * Loading a drone with medication items;
 * Checking loaded medication items for a given drone;
 * Checking available drones for loading;
 * Check drone battery level for a given drone;

Functional requirements
 * There is no need for UI;
 * Prevent the drone from being loaded with more weight that it can carry;
 * Prevent the drone from being in LOADING state if the battery level is below 25%;
 * Introduce a periodic task to check drones battery levels and create history/audit event log for this.

## Used Technologies

* .NET Framework 7.0
* EntityFramework Core: As ORM to the Persistence and Data Access
* Class Libraries in each Layer in the Program.
* Asp.Net Core Web Api: As the Interface Layer

## Integrated Development Enviroment

* Visual Studio.NET 2022

## Build/Run/Test instructions

The Project have in the appsettings.json the SQL Connection, 
The Database is in a folder named Database
No Test could be implemented because of the Time

## Tarea 1 - Creating the Blank Solution and each Project that compound it

* Drone.Exercise Solution created.
* Four Projects (Class Libraries) added to the Solution. 
  1. Drones.Infrastructure
  2. Drones.Domain
  3. Drones.Application
  4. Drones.Utilities
* And a Project (ASP.NET Core Web App) added too.
  5. Drones.WebAPI
* For the Test, it was added a MSTest Test Project.
* Project References to the Projects wer added.

## Tarea 2 - Implementing the Class Library Projects
* Entity Classes added, Configuration Classes, Injections in the Infrastructure and the Application Layers.
* Generated the Entity Classes using the Scaffolding command. 
* Implemented Repository Pattern and Unit Of Work pattern.
* Implemented Application and Infrastructure Layer

## Tarea 3 - Implementing the Web API
* Added the Dispatch Controller, which will have the implementing of the 5 Tasks.
* Added the Drone and Medication Controller, to manage the two entities.
