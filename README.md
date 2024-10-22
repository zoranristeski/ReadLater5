# Read Later - Solution Implementation

Thank you for the opportunity to work on the **Read Later** project. Below, I will outline the approach I took to complete the tasks as per the requirements.

## 1. Bookmark Management (CRUD)

I implemented the full Create, Read, Update, and Delete (CRUD) functionality for bookmarks. Here are the key highlights of my solution:

- **Create Bookmark**: Users can create bookmarks and at the same time can create a new category without a page refresh using AJAX.
- **Edit and Delete Bookmarks**: Users can easily update and delete their bookmarks, with all changes reflected immediately.
- **AJAX Integration**: The system uses AJAX for the "Create" operation to avoid page refreshes.

## 2. User Accounts with ASP.NET Core Identity

I implemented user authentication using **ASP.NET Core Identity**. The key aspects of this implementation include:

- **Private Bookmarks**: Each user's bookmarks are private, and users can only manage their own bookmarks.
- **Full User Authentication**: I enabled user registration, login, and logout using the ASP.NET Core Identity framework.

## 3. API Access

I chose the API access exercise to round off my solution. The API allows external systems to manage bookmarks with the following features:

- **CRUD via API**: I created an API so that external systems can create, retrieve, update, and delete bookmarks.
- **Authentication**: I implemented token-based authentication (JWT), ensuring that only authorized users can interact with the API. The token expires after 20 minutes.
- **API Secret Key**: The secret API key is placed in the `appsettings.json`.

## How to Start

1. Open the solution in Visual Studio.
2. Update the connection string in `appsettings.json` to point to a SQL Server.
3. In the Package Manager Console, ensure the Data project is selected and run the command `update-database` to run all the migrations.

## How to Test the API

1. Open Postman.
2. Import the configuration file `Readlater.postman_collection.json`.
3. Add the `accessToken` to the headers for every request (GET, POST, PUT, DELETE) as the value for the `Authorization` key.
