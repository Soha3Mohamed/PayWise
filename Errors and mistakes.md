##################################
---------------------
File 'C:\Users\soha mohamed\source\repos\PayWise\PayWise.Api\bin\Debug\net8.0\PayWise.Infrastructure.dll' not found.
--------------------
when: when i tried to add migration for the first time
solution: i added the project reference from api to infrastructure and from infrastructure to domain

##################################
---------------------
Your startup project 'PayWise.Api' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for 
 the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again.
--------------------
when: after the previous error was solved and i tried to add migration again
solution : i installed the package in api project

##################################
---------------------
Unable to create a 'DbContext' of type 'RuntimeType'. The exception 'Unable to resolve service for type
 'Microsoft.EntityFrameworkCore.DbContextOptions`1[PayWise.Infrastructure.Contexts.ApplicationDbContext]' while attempting to activate 
 'PayWise.Infrastructure.Contexts.ApplicationDbContext'.' was thrown while attempting to create an instance. For the different patterns 
 supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
--------------------
when: after the previous error was solved and i tried to add migration again
solution : i don't know yet
i added the DbContext class in program.cs:
builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


##################################
---------------------
Unable to create a 'DbContext' of type 'ApplicationDbContext'. The exception 'Unable to determine the relationship represented 
by navigation 'Transaction.DestinationWallet' of type 'Wallet'. Either manually configure the relationship, or ignore this 
property using the '[NotMapped]' attribute or by using 'EntityTypeBuilder.Ignore' in 'OnModelCreating'.' was thrown while 
attempting to create an instance. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
--------------------
when: after the previous error was solved and i tried to add migration again
solution : i need to configure the relationships in onModelCreating method in ApplicationDbContext class
