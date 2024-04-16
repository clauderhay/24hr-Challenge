# 24hr-Challenge

### This is my own implementation and understanding of how to handle huge amounts of data.

## For this project, I used RDBMS because it has been my roots in my whole Software Development career and I feel much more comfortable and confident using it as for data manipulation and handling data.

How to run the project. Step by step guide.

1. Open your most comfortable IDE. As for me, I used Visual Studio Code for this since i'm using a Mac and Visual Studio Mac has stopped updates and will be retiring it this coming August 2024. In order for me to use .NET 8, I used Visual Studio Code.

2. Clone this repository.

3. Make sure you install the necessary packages needed for this project. These are the core packages that I personally used for this project:

   - Microsoft.AspNetCore.Mvc
   - Microsoft.EntityFrameworkCore
   - Microsoft.OpenApi.Models

4. Create the database needed for this project. I used MySQL Workbench in manually creating the database and importing the CSV file for the data given. Alter necessary relationships and foreign keys so it would work properly when accessing your tables.
   P.S. I could've used entity migration for this project but it already took me too much time in altering the tables and columns from the CSV file export.

5. Run `dotnet build`

6. If build is Successful, run `dotnet run`

7. To open Swagger, I personally used the root endpoint which is `http://localhost:{port}/` to access the Swagger UI (this has been called in my Startup.cs file)

8. Explore all the API's i've added and test the necessary data needed for each endpoint.

9. Enjoy!
