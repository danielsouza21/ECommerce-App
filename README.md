# ECommerce-App
Development of an e-commerce store using Angular, .Net Core and other technologies

### Description of the architecture standards used and details of the project

DDD (Domain-Driven Development) methodology applied with Clean Architecture, always using the concepts of Clean Code and SOLID to the maximum. 

Repository pattern (Data Access Object Classes) used to decouple business code from data access, separate interests, minimize duplicate query logic and improve testability. Applied the concept of Generic Repository and Specifications Pattern with IQueryable<T> types for centralization of responsibilities, standardize the code and among other advantages.

Using static files in the wwwroot folder, setting the startup to UseStaticFiles(). [Initial data found publicly was used to fill the files and the database.]

### Technologies

Main: .NET Core, Entity Framework Core, Angular

Secondary: AutoMapper

### Next implementations to be made:

- One way to insert and remove products, including their images (upload)

