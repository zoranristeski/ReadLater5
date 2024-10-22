    <h1>Read Later - Solution Implementation</h1>
    <p>Thank you for the opportunity to work on the Read Later project. Below, I will outline the approach I took to complete the tasks as per the requirements.</p>

    <h2>1. Bookmark Management (CRUD)</h2>
    <p> I implemented the full Create, Read, Update, and Delete (CRUD) functionality for bookmarks. Here are the key highlights of my solution:</p>
    <ul>
        <li>Create Bookmark: Users can create bookmarks and at the same time can create a new category without a page refresh using AJAX</li>
        <li>Edit and Delete Bookmarks: Users can easily update and delete their bookmarks, with all changes reflected immediately.</li>
        <li>AJAX Integration: The system uses AJAX for the "Create" </li>
    </ul>

    <h2>2. User Accounts with ASP.NET Core Identity</h2>
    <p>I implemented the user authentication using ASP.NET Core Identity. The key aspects of this implementation include:</p>
    <ul>
        <li>User's bookmarks are private, and users can only manage their own bookmarks.</li>
        <li>Full User Authentication: I enabled user registration, login and logout using the ASP.NET Core Identity framework</li>       
    </ul>

    <h2>3. API Access </h2>
    <p>I chose the API access exercise to round off my solution. The API allows external systems to manage bookmarks with the following features:</p>
    <ul>
        <li>CRUD via API: I created API so that external systems can create, get, update, and delete bookmarks.</li>
        <li>Authentication: I implemented token-based authentication (JWT), ensuring that only authorized users can interact with the API. Token expires for 20 min</li>     
        <li>The Secret Api Key is placed in the appsettings.json </li>
    </ul>

    <h2>How to Start</h2>
    <ul>
        <li>Open solution in Visual Studio</li>
        <li>Update the connection string in appsettings.json to point to a Sql Server </li>
        <li>In Package Manager Console, ensure the Data project is selected and run the command 'update-database' in order to run all the migrations</li>       
    </ul>
    <h2>How to test the API</h2>
    <ul>
        <li>Open Postman</li>
        <li>Import the configuration file Readlater.postman_collection.json</li>
        <li>Put the accessToken for every request GET,POST,PUT,DELETE in Headers as Value for Authorization Key</li>       
    </ul>
    