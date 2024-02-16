# CRUD Demo - the Backend API #

This is the backend of a tandem app demonstration - an API built in .NET 4.7 handling the JWT and SQL needed by
an HTML5 frontend built in VueJs 3, Vite, Pinia.

Working demonstration of API https://demo.northberry.net/crud/api-crud/swagger

The frontend: https://demo.northberry.net/

About the project : https://demo.northberry.net/crud/about

## Backend Notes ##

I added class libraries to separate out the various functionality - jwt, crypto, users, data.  
It was an exercise in "let's build everything from first principles - ground up" - so I won't
try to suggest that my approach was superior.  (My approach certainly was not superior when it came to my data model - more on that later.)

### A non-apology ###

Over the years I've found that frameworks often usurp common names and trying to use common english words such as object, list, token, 
crypto, data, database etc.  has regularly put me afoul of microsoft & team's frameworks - so I INTENTIONALLY MISSPELL classes and functions.  
They don't conflict with common frameworks and a lot easier to find when doing a search!

### Data ###

I wanted to make a quick little demo - and for that reason alone - this is a failure. But like a lot of projects, this 
evolved into working through a few little features that I wanted to get beyond simple.   But for the data - yes 
I should have just created a simple table, made an EF model of it - and just coded the darn thing - but I decided to 
make something that would use a common table - and then "magically" transform it into a Json object (or list/array of
Json objects) and well... you can look at the code for yourself to see how complicated that got.   After doing it, I
wanted to do it levering the c# JToken - and I intend to report on that - it wasn't super clean eather - mainly because
I was a cross purposes with my desire to build a data handling library that allowed me to "code-once-deploy-many" - with
only the configuration files to specify the data models.  

### Permissions ###

This was another feature that ended up within the "code-once-deploy-many" goal.  Because I hadn't settled on a user 
infrastructure - and I really didn't want to add another database structure to this demo - I made the users dynamically
from a string in configuration.  Pros-and-cons for sure - and those users had to hold the permissions for data elements
and because I was determined to not add extra tables (ie normalized permissions that a fully functioning database
would provide), they ended up being config strings inside of config strings.  Not something I'd recommend for a 
real database oriented backend. Kludgey. 

### JWT & Crypto ###

This was demonstrated in my earlier Json Web Token demo: https://demo.northberry.net/jwt - what is different was that I moved them into 
two separate projects to keep code "clean".  In hind-sight - that was probably overkill - a simple folder of classes probably would have
sufficed.  It made the testing full of references and made the project heavier and more complicated than necessary. 

### SQL ###

I love data - and so the database part was a welcome break into the fun - creating random data sets to be merged in with the real list
of real songs with odd or funny names so that the data had proper publishers, creators, created & edited dates etc..  
