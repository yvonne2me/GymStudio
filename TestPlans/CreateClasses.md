# Create Classes

## Summary ##

As a studio owner I want to create classes for my studio so that my members can attend classes.

## Test Strategy ##

### Unit Tests ###

Unit Tests will be added at a Controller, Service, and Repository Level.

These include:

- Controller Tests
    - Expect 200/OK for valid POST Class request
    - Expect 400/Bad Request for Null Class request
    - Expect Error Message when exception thrown during POST Class
    - Expect Get Class Request to return Class when it exists
    - Expect Get Class returns NotFound when Class does not exist

- Service Tests
    - Expect Validation Errors for the following scenarios:
        - Class Name Not Provided
        - Start Date Is After End Date
        - Start Date Is Historical
        - Total Class Days greater than 30 days (Limitation added to prevent user sending in Classes that could cause large volumes of ClassSessions on the DB)
        - Class with Class Name already exists
        - Class with ClassId already exists
    - Expect new Class Request is assigned a ClassId
    - Expect Get Class Returns Correct class

- Repository Tests
    - Expect Valid Class to be successfully saved
    - Expect Class Sessions to be created for a new Class and successfully saved
    - Expect Class Sessions to be successfully saved with correct Capacity

### End To End Tests ###

The End to End Testing will be achieved using the provided Postman collection.

Scenarios covered:

- POST Class - Valid Request - 200/OK
- POST Class - Missing expected data field - 400/Bad Request
- POST Class - Missing expected data field - Expected Error Message returned
- POST Class - StartDate Greater Than End Date - 400/Bad Request
- POST Class - More than 30 days of Classes - 400/Bad Request
- POST Class - Historical StartDate - 400/Bad Request
- GET Class - Valid ClassId - Returns Class - 200/OK
- GET Class - Does Not Exist - Returns 404/NotFound