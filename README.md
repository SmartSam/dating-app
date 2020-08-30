# DatingApp

Work-in-progress dating app with ASP.NET Core WebAPI (v3.1) and Angular (v9) with vsCode

## WebAPI Development server - Kestrel

Open a terminal window then cd to DatingApp.API and run: `dotnet watch run`. Navigate to `http://localhost:5000/`. The app will automatically reload if you change any of the source files. 

## Angular Development server - Kestrel

Open another terminal window then cd to DatingApp-SPA and run: `ng serve`. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Database

The development configuration pionts to SQLite, and the production configuration points to SQLServer. Database (datingapp.db) was setup with EF Core migrations. When changing data providers (SQLite <=> SQLServer), a new migration has to be setup.  FYI: "DB Browser for SQLite" is an excellent tool for SQLite DBs. 

## Integration Tests

Using NUnit for integration tests against the SQLite db.  Data for this db is seeded from a json file.  
