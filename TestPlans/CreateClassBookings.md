# Create Class Bookings

## Summary ##

As a member of a studio, I can book for a class, so that I can attend a class.

## Test Strategy ##

### Unit Tests ###

Unit Tests will be added at a Controller, Service, and Repository Level.

These include:

- Controller Tests
    - Expect 200/OK for valid POST Booking request
    - Expect 400/Bad Request for Null Booking request
    - Expect Error Message when exception thrown during POST Booking
    - Expect Get Booking Request to return Bookings when they exists
    - Expect Get Booking returns NotFound when Booking does not exist

- Service Tests
    - Expect Errors for the following scenarios:
        - Booking Not Available on a Date
        - Class Does Not Exist
    - Expect new Booking is successfully created
    - Expect Get Bookings Returns Bookings

- Repository Tests
    - Expect valid Booking to be successfully saved
    - Expect Get Bookings to return all Bookings for a Class

### End To End Tests ###

The End to End Testing will be achieved using the provided Postman collection.

Scenarios covered:

- POST Booking - Valid Request - 200/OK
- POST Booking - Missing expected data field - 400/Bad Request
- POST Booking - Missing expected data field - Expected Error Message returned
- POST Booking - Class Does Not Exist - 404/NotFound
- POST Booking - No Class Session Available on Date - Returns 400/Bad Request
- GET Bookings - Returns all bookings for class