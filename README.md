# Music Library

Music Library is a web application that allows users to create and manage playlists, add tracks to playlists, and engage in chat with other users. The project is built using the .NET and Angular frameworks.

Feel free to check it out!

https://musiclibraryweb.p2122.app.fit.ba

## Features

- Create your own unique profile
- Create, delete and share playlists with other users
- Fill up those playlists with tracks
- Chat with other users

## Setup Instructions

### Prerequisites

Make sure you have the following installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- [Angular CLI](https://cli.angular.io/)

### Local Development:

1. Clone the repository:

    ```bash
    git clone https://github.com/your-username/music-library.git
    cd music-library
    ```

2. **Check Database Configuration:**
    - Open `ApplicationServiceExtension.cs` in the `backend` directory.
    - Confirm the default connection to set up the database. Switch between SQL Server and SQLite by modifying the `UseSqlServer` or `UseSqlite` keyword accordingly.

3. **Run Database Migration:**
    - Open the Package Manager Console.
    - Run the following command to apply the pre-made migration with seed data:
        ```bash
        update-database
        ```

    (Note: After running the update-database command, it's necessary to run the following command to download all dependencies, especially for `kolkov/ngx-gallery`. Use the --force flag due to compatibility issues):
    ```bash
    npm install --force
    ```

4. **Check SSL Configuration:**
    - Default SSL port for IIS Express is 44360, but the app is configured to run on `https://localhost:5001`. Confirm this in the `environments.ts` file in the `frontend` directory.

5. **Run the Application:**
    - Open a terminal and navigate to the `backend` directory.
    - Run the following command to start the API:
        ```bash
        dotnet run --launch-profile https
        ```
        
    (Note: If you are using JetBrains WebStorm and your API and Hub URL are set to `https://localhost:44360`, you can use the following command instead for the frontend):
    ```bash
    ng serve 
    ```

    Access the application at `https://localhost:5001/` in your web browser.
  
6. **Registration:**
    - Registration is the first step before using the application.

### Additional Configuration:

These steps ensure a smooth setup for the Music Library application. Feel free to reach out if you encounter any issues during the setup process.

