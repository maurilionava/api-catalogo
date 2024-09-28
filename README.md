
# My ASP.NET Core Web API Learning Journey

## About This Repository

Welcome to my personal project repository! Here, I am documenting my learning journey as I implement the concepts and techniques from an **ASP.NET Core Web API** course taught by  **José Carlos Macoratti** . This repository serves as both a portfolio of my progress and a space for experimenting with various features and best practices in Web API development using the .NET platform.

## What I'm Learning

This project is a hands-on application of the following topics covered in the course:

* **Building RESTful APIs** : Creating APIs using ASP.NET Core, understanding REST principles, and working with JSON and HTTP protocols.
* **Entity Framework Core (EF Core)** : Using Code-First approach with EF Core, applying Migrations, and working with databases through a Web API.
* **Routing and Filters** : Setting up custom routes, implementing filters, and managing different response types.
* **Asynchronous Programming** : Using `async/await` for better performance and scalability in API methods.
* **Error Handling and Logging** : Implementing structured error handling and logging to track and debug issues effectively.
* **Security** : Adding authentication and authorization with JWT tokens, and securing the API endpoints.
* **Data Pagination** : Implementing data pagination to efficiently handle large data sets.
* **Design Patterns** : Applying the Repository and Unit of Work patterns for better code organization and maintainability.
* **AutoMapper** : Using AutoMapper to simplify object-to-object mapping.
* **API Versioning and Documentation** : Implementing API versioning and using OpenAPI (Swagger) for clear and comprehensive API documentation.
* **Cross-Origin Resource Sharing (CORS)** : Configuring CORS to allow or restrict access to the API from different origins.
* **API Consumption** : Creating client applications using Angular and Windows Forms to consume and interact with the API.

## Current Project

As part of my learning, I am building a sample Web API project that evolves with each new concept learned. The project covers:

1. **API Setup** : Initial setup of the ASP.NET Core Web API with basic configurations and middleware.
2. **CRUD Operations** : Implementing Create, Read, Update, and Delete operations using EF Core.
3. **Authentication and Authorization** : Securing the API with JWT and role-based access control.
4. **Advanced Features** : Adding features like pagination, filtering, and async methods to optimize the API.
5. **API Clients** : Creating sample client applications in Angular and Windows Forms to demonstrate API consumption.

## How to Use This Repository

Feel free to explore the code and see how different concepts are implemented. If you're also learning ASP.NET Core Web API, you can use this repository as a reference or for your own learning purposes.

### Running the Project

1. Clone the repository:
   <pre class="!overflow-visible"><div class="dark bg-gray-950 contain-inline-size rounded-md border-[0.5px] border-token-border-medium relative"><div class="flex items-center text-token-text-secondary bg-token-main-surface-secondary px-4 py-2 text-xs font-sans justify-between rounded-t-md h-9">bash</div><div class="sticky top-9 md:top-[5.75rem]"><div class="absolute bottom-0 right-2 flex h-9 items-center"><div class="flex items-center rounded bg-token-main-surface-secondary px-2 font-sans text-xs text-token-text-secondary"><span class="" data-state="closed"><button class="flex gap-1 items-center py-1"><svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" class="icon-sm"><path fill-rule="evenodd" clip-rule="evenodd" d="M7 5C7 3.34315 8.34315 2 10 2H19C20.6569 2 22 3.34315 22 5V14C22 15.6569 20.6569 17 19 17H17V19C17 20.6569 15.6569 22 14 22H5C3.34315 22 2 20.6569 2 19V10C2 8.34315 3.34315 7 5 7H7V5ZM9 7H14C15.6569 7 17 8.34315 17 10V15H19C19.5523 15 20 14.5523 20 14V5C20 4.44772 19.5523 4 19 4H10C9.44772 4 9 4.44772 9 5V7ZM5 9C4.44772 9 4 9.44772 4 10V19C4 19.5523 4.44772 20 5 20H14C14.5523 20 15 19.5523 15 19V10C15 9.44772 14.5523 9 14 9H5Z" fill="currentColor"></path></svg>Copy code</button></span></div></div></div><div class="overflow-y-auto p-4" dir="ltr"><code class="!whitespace-pre hljs language-bash">git clone https://github.com/yourusername/your-repo-name.git
   </code></div></div></pre>
2. Open the solution in Visual Studio.
3. Restore the NuGet packages and build the project.
4. Update the database using EF Core Migrations:
   <pre class="!overflow-visible"><div class="dark bg-gray-950 contain-inline-size rounded-md border-[0.5px] border-token-border-medium relative"><div class="flex items-center text-token-text-secondary bg-token-main-surface-secondary px-4 py-2 text-xs font-sans justify-between rounded-t-md h-9">bash</div><div class="sticky top-9 md:top-[5.75rem]"><div class="absolute bottom-0 right-2 flex h-9 items-center"><div class="flex items-center rounded bg-token-main-surface-secondary px-2 font-sans text-xs text-token-text-secondary"><span class="" data-state="closed"><button class="flex gap-1 items-center py-1"><svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" class="icon-sm"><path fill-rule="evenodd" clip-rule="evenodd" d="M7 5C7 3.34315 8.34315 2 10 2H19C20.6569 2 22 3.34315 22 5V14C22 15.6569 20.6569 17 19 17H17V19C17 20.6569 15.6569 22 14 22H5C3.34315 22 2 20.6569 2 19V10C2 8.34315 3.34315 7 5 7H7V5ZM9 7H14C15.6569 7 17 8.34315 17 10V15H19C19.5523 15 20 14.5523 20 14V5C20 4.44772 19.5523 4 19 4H10C9.44772 4 9 4.44772 9 5V7ZM5 9C4.44772 9 4 9.44772 4 10V19C4 19.5523 4.44772 20 5 20H14C14.5523 20 15 19.5523 15 19V10C15 9.44772 14.5523 9 14 9H5Z" fill="currentColor"></path></svg>Copy code</button></span></div></div></div><div class="overflow-y-auto p-4" dir="ltr"><code class="!whitespace-pre hljs language-bash">dotnet ef database update
   </code></div></div></pre>
5. Run the API project and test the endpoints using tools like Postman or Swagger.

## Future Plans

As I continue to progress in the course, I plan to:

* Implement more advanced features like caching and rate limiting.
* Explore the integration of external services and third-party libraries.
* Create more detailed documentation and testing for the API.
* Build more sophisticated client applications to consume the API.

## Contributing

I am open to suggestions, feedback, and collaboration! If you have ideas on how to improve this project or want to share your own experiences, feel free to open an issue or a pull request.

## Acknowledgments

Special thanks to **José Carlos Macoratti** for providing such a comprehensive course and to the community for the support in my learning journey.
