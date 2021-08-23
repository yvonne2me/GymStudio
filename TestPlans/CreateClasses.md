# Create Classes

## Summary ##

As a studio owner I want to create classes for my studio so that my members can attend classes.

## Test Strategy ##

### Unit Tests ###

Unit Tests will be added at a Controller, Service, and Repository Level.

These include:

- Controller Tests
    - POST Class - Request Valid - 200/OK
    - POST Class - Null Request - 400/Bad Request
    - POST Class - Exception Thrown - Error Returned
    - GET Class - Exists - Return Class
    - GET Class - Does Not Exist - Returns Not Found

- Service Tests
    - Class Name Not Provided
    - Start Date Is After End Date
    - Class with Class Name already exists
    - Class with ClassId already exists
    - Class is assigned ClassId
    - Created class is returned on a Get Class request

- Repository Tests
    - Class successfully saved
    - Class Sessions successfully saved
    - Class Session successfully saved with correct capacity

### End To End Tests ###

The End to End Testing will be achieved using the provided Postman collection.

Scenarios covered:

- POST Class - Valid Request - 200/OK
- POST Class - Missing expected data field - 400/Bad Request
- POST Class - Missing expected data field - Expected Error Message returned
- POST Class - StartDate Greater Than End Date - 400/Bad Request
- GET Class - Valid ClassId - Returns Class - 200/OK
- GET Class - Does Not Exist - Returns 404/NotFound