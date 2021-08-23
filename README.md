# Gym Studio API

This API was created to support the creation of classes and bookings.

- It allows Studio Owners to create Classes for their members.
- It allow Members create Bookings on those Classes.

## Pre-requisites ##

[.NET 5 SDK](https://dotnet.microsoft.com/download) is required to run this project.

### How to use ###

To run this project, navigate to the GymStudioApi folder and run:

    dotnet run


To run all the unit tests for this project, navigate to GymStudioUnitTests folder and run:

    dotnet test

## API endpoints ##

The API endpoints available:

    Classes:    https://localhost:5001/classes
    Bookings:   https://localhost:5001/bookings
    
## Swagger ##

Swagger is included with this project. 

Once the application is running, this can be accessed at:

    https://localhost:5001/swagger/
        
## Postman Collection ##

A Postman Collection is included with this project.

***How to run the Gym Studio Postman Collection***

- Ensure GymStudioApi project is running locally on https://localhost:5001
- Download the Postman Collection & Import the collection into Postman
- Once the 'Gym Studio Collection' has been imported, you can choose the 'Run Collection' option
    - This will present you with a preview screen & you can choose 'Run Gym Studio Collection' to start the test run.
- Each test can also be run individually - You can see the outcome in the 'Test Results' of the response pane

## Test plans ##

Test Plans can be found in the TestPlans Folder provided. It contains an outline of the test strategy taken, and includes a high-level overview of the unit test project (GymStudioUnitTests) and the end-to-end tests defined in the included GymStudio Postman Collection.

Story test strategies available:

    CreateClasses.md
    CreateClassBookings.md