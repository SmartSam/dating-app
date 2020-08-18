# DatingApp

Work-in-progress dating app with ASP.NET Core WebAPI (v3.1) and Angular (v9) with VSCode

## WebAPI Development server - Kestrel

Open a terminal window then cd to DatingApp.API and run: `dotnet watch run`. Navigate to `http://localhost:5000/`. The app will automatically reload if you change any of the source files. 

## Angular Development server - Kestrel

Open another terminal window then cd to DatingApp-SPA and run: `ng serve`. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Database

Currently using SQLite. Database (datingapp.db) was setup with EF Core migrations.  FYI: "DB Browser for SQLite" is an excellent tool for SQLite DBs. 

## Integration Tests

Using NUnit for integration tests against an in-memory SQLite db.  Data for this db is seeded from a json file on SetUp.  
