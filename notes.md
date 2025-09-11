# day 1
//i am starting this project like a bootcamp to apply everything i know and learn new things to apply along the way.
//the first thing to try is onion architecture , i've always run from it because i feel like it is complicated and sets me away from 
 learning the concept as it is but no escape , i have to know it so...
//let's create a repo for this project, very good.
//now i'll start installing packages needed for every part in the project and that made my rethink, i need to figure out a way to have a 
 template-like project will usually used packages for me to be ready for coding , for offline days
 
 // so in infrastructure, we need our ORM packages, in this case ef core
 //in PayWise.Api i have a problem installing this package: Microsoft.AspNetCore.Authentication.JwtBearer
 //goodbye for now

 # day 2 
 //Hi, new day and new opportunities, despite all hopelessness in the air!!!!!!!
 

## fluent validation
//as you saw i am reading about fluent validation because i had a question in my mind, why am i using that package, i can validate inputs by using 
  data annotations or by writing in my DbContext Class. the attribute [ApiController] at the beginning of every controller already sees if the input
  model violates any validation rule and if that's the case it throws exception i think(i don't know what is the reaction exactly but i'll search)

If the incoming request is invalid, ModelState.IsValid is false, and with [ApiController], ASP.NET Core returns a 400 Bad Request automatically.

  but why to use fluent validation??????????? we will know together now
  //keep in mind these videoes can be seriously boring but i want to remember how i am learning and learning is boring!

  //i will make another file for packages i installed in the whole solution and another file for articles i read along the way 

  //filters, what!!!!!!!!

so i think chatgpt made it easier, now i like it because it is really easy, powerful, and i want to reuse it and test it so i will use
 it in this project

 remember they keep dtos clean also and makes validation customizable and code-based instead of attribute-based data annotations approach

 -------
 now back to models
 i have 4 models: User, Merchant, Wallet, Transaction 
 not sure , i can refactor of course but those are what i need now
 i will start creating the classes now

 can user or merchant exist without having any wallet, or is it a must?

 now relationships, ughhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh
 -> user has one to one with wallet
 -> user has many transactions , no wallet has transactions not user

 @@@@@ i need a breaaaaaaaaaaaaaaaaaaak @@@@@
 @@@@@ i will write linkedin post on fluentValidation today ان شاء الله @@@@@

 i added relations , now i will build the ApplicationDbContext and the Services class that i will inject services in it but a break is needed
  first because i expect errors from migration and also i still need to learn where to put the new files so see you in another video

welocme again, i made the ApplicationDbCOntext.cs class  omg!!!, where is the ai agent???????
now i will try to do a migration i didn't use onModelCreating method before so let's see what happens
//where should i put the connection string????? in infrastructure????? or in api?????
i put it in infrastructure because it is related to dbcontext class
let's be sure, this is intersting
the autocomplete was right, you little boy!!!
Rule of thumb: migrations should sit next to the DbContext, not where controllers or entities are.

ugh programming!!
now the expected errors started to show up , the first was that the dll file wasn't found as you saw and 
 it is my fault because i thought that build dependencies is adding a project reference, now i need to recap and add the project 
 references all over again
so one error solved and other showed up, if i will have to install ef core packages in api project, why am i using a layer like infrastructure????

the idea is that the api project doesn't need the ef core packages for persistence logic but because ef core itself needs a startup
 project to run from it, so we have to install the packages in the api project (only the design package becuase it gives the info the ef
  core needs to build the applicationDbContext)

another error, finallyyyyyyyyyyyyyyyyyyyyyyyyyyyyy

i need to start reading about design time dbcontext creation
also on methods used in OnModelCreating method
Done. saw that or what, sorry i am just happy something went right
 
i have to go now, i will continue later

# day 3
i am not ready for starting the record because i feel alittle lost, onion architecture, repository pattern,serilog, global error handling , then 
 starting with services in a vertical way like user -> wallet -> depositAsync, and of course learning async
 then refactoring this use case with using  repository pattern and see the difference , my day is gonna be heavy but one problem at a time

 first i will read about serilog and apply it now...
 so seq is managememnt tool for serilog that visualized logs to analyze them easily
 the article says i must choose the serilog package that matches the target framework of my project, but i am not doing so( i installed 9 while i am using .net 8)
 , we will see if it works
 the first article is complicated
 //let's structure a little bit so that i have classes i can apply serilog in
 what i understood is that serilog implements the ILogger interface which asp.net core provides but it has its special features also 
 //i will go for 5 minutes break
 //we are going into a loop, i must use repository pattern now 

 ////////////
 Application only talks to Core (interfaces).

Infrastructure provides the actual EF implementation.

Api wires everything up with Dependency Injection.
///////////////////
//im tired now, i will try to be back

//i want to master logging and serilog today so i found a good playlist and let's see
//okay i am good with serilog, i will see the seq video and then move on to the next step( i don't know yet )


In ServiceResult<t> class, i used private constructor this time for immutability because chatgpt reminded me thaty anyone can alter the errormessage which is not consistent 
 actually my old version will work fine but i want to get used to writing not good code, but better code