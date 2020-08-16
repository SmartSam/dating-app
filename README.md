# DatingApp

Work-in-progress dating app with ASP.NET Core WebAPI (v3.1) and Angular (v9) with VSCode

## WebAPI Development server

Open a terminal window then cd to DatingApp.API and run: `dotnet watch run`. Navigate to `http://localhost:5000/`. The app will automatically reload if you change any of the source files. 

## Angular Development server

Open another terminal window then cd to DatingApp-SPA and run: `ng serve`. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Database

Currently using SQLite. FYI: "DB Browser for SQLite" is an excellent tool for SQLite DBs. 

## Integration Tests

Using NUnit for integration tests against an in-memory SQLite db.  The data is seeded from a json file on SetUp.  
