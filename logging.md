This file is intended to be my draft from studying and applying logging in my project
i read about two articles and now i am watching logging playlist( alittle bit old) but the developer is really talented so i am in for the basics
now i will list all i am learning

- Logging is a cross-cutting concern
- ASP.NET Core has built-in logging capabilities that can be extended with third-party providers like Serilog, NLog, and log4net.
- Logging levels include Trace, Debug, Information, Warning, Error, and Critical.
- Every level is lower that the one above it so Debug is lower than Information and so if my default level is Information, i will log Information, Warning, Error, and Critical but not Debug or Trace
- i think you can configure logging in program.cs file or appsettings.json file but i am not sure
- appsettings.json file is better because it is more flexible and i can add some settings that i can't add in program.cs file
- i can create different log configurations for different environments like development, staging, production
- ef core fortunately has built-in logging support that can be configured to log sql queries and other db operations
- if end-user entered object that couldn't be serialized because of validation errors, if request and response weren't logged, i won't be able to debug the issue
  because i couldn't see the returned object as it didn't enter the endpoint in the controller so that my logging would work
- 