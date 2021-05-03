# Documentation
This is an Web-Api project that is based on the swapi.

## [SpacePark-API project](https://github.com/PGBSNH20/spaceparkv2-buddygroup6-renegades/tree/main/Source/SpacePark-API):
This is the main project and contains code regarding:
- Data Models
- DbContext
- Controllers
- Logic

## [SpaceParkTests project](https://github.com/PGBSNH20/spaceparkv2-buddygroup6-renegades/tree/main/Source/SpaceParkTests):
This is an x-unit project that do all the testing of the application. We use an in-memory-sql database (dependent on nu-get EntityFrameworkCore.InMemory) to run tests of the db-context and we also use an test-unique web-host for testing the http-requests. See [MockWebHostFactory.cs](https://github.com/PGBSNH20/spaceparkv2-buddygroup6-renegades/blob/main/Source/SpaceParkTests/MockWebHostFactory.cs)
