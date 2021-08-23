# Create Class Bookings

## Summary ##

As a member of a studio, I can book for a class, so that I can attend a class.

## Test Strategy ##

### Unit Tests ###

Unit Tests will be added at a Controller, Service, and Repository Level.

These include:

- Controller Tests
    - POST Booking - Request Valid - 200/OK
    - POST Booking - Null Request - 400/Bad Request
    - POST Booking - Exception Thrown - Returns Error
    - GET Booking - Booking Exists - Return Booking
    - GET Booking - Booking Does Not Exist - Returns Not Found

- Service Tests
    - Booking Created
    - Booking Not Available on a Date
    - Booking Not Created when Class does not exist

- Repository Tests
    - Booking successfully saved
    - Get Bookings returns Bookings for Class

### End To End Tests ###

The End to End Testing will be achieved using the provided Postman collection.

Scenarios covered:

- POST Booking - Valid Request - 200/OK
- POST Booking - Missing expected data field - 400/Bad Request
- POST Booking - Missing expected data field - Expected Error Message returned
- POST Booking - Class Does Not Exist - 404/NotFound
- POST Booking - No Class Session Available on Date - Returns 400/Bad Request
- GET Bookings - Returns all bookings for class