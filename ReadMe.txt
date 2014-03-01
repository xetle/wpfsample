This is a sample WPF application showing fictitious banks and their exposures written using the PRISM framework. It's a layered architecture using Entity Framework, the repository pattern, with services providing model data utilized by MVVM.

There a lot I'd still like to experiment with e.g. more testing using Moq, create a better looking front end using Blend, more business rule validation on the model, CRUD functionality.

But ... here are some features it does have  
Use of Prism modularity to add new views into regions. The menus are build dynamically depending on what views are registered.  
Dependency Injection - using Unity container.  
Mocking (using Moq) for testing  
Mega Bank no 5 has "real time" data that changes every 5 seconds.  
The Owl is in fact a re-templated checkbox. Click it to show the first exposure.  
