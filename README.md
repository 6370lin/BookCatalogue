Github url: https://github.com/6370lin/BookCatalogue

Sample ASP.NET Core reference application, demonstrating a clean-architecture using IRepository and Specification pattern. 

1a. By default, the project uses a real database. If you want an in-memory database, you can add in `appsettings.json`

   json { "UseOnlyInMemoryDatabase": true }

1b. Ensure your connection strings in `appsettings.json` point to a local SQL Server instance.

2. Ensure the tool EF was already installed. You can find some help [here](https://docs.microsoft.com/ef/core/miscellaneous/cli/dotnet)

    ```
    dotnet tool install --global dotnet-ef
    ```

3. Open a command prompt in the src/Web folder and execute the following commands:

    ```
    dotnet restore
    dotnet tool restore
    dotnet ef database update -c BookCatalogDbContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
    dotnet ef database update -c AppIdentityDbContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
    ```

    These commands will create two separate databases, one for the store's catalog data and shopping cart information, and one for the app's user credentials and identity data.

4. Run the application.

    The first time you run the application, it will seed both databases with data such that you should see products in the store, and you should be able to log in using the demouser@microsoft.com account.
    Username: demouser@microsoft.com Password:Pass@word1

    Note: If you need to create migrations, you can use these commands:

    ```
    -- create migration (from Web folder CLI)
    dotnet ef migrations add InitialModel --context BookCatalogDbContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations

    dotnet ef migrations add InitialIdentityModel --context AppIdentityDbContext-p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Identity/Migrations