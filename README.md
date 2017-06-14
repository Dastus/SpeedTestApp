# SpeedTestApp
Application which checks sitemap and measures response time of all pages

Functionality:
- Test results are displayed graphically and in table (slowest requests on top)
- Table view contains min and max responses for each page
- Table view contains links to history of responses for according page
- Autorefresh of results is set to 10 seconds

Features:
- Ajax partial view used to display results
- StructureMap used as IoC/DI container
- Project contains 2 repositories: with memory cache(setted up now) and with DB. 

Short notes about application:

Application checks sitemap.xml file, if it's not found, simple web crawling logic used.
This solution suits only for small sites!

Info about responses is stored in memory cache ,but can be stored in DB, SQLExpress has been used.
Script for DataBase creation:
https://github.com/Dastus/SpeedTestApp/blob/master/SpeedTestApp/dbscript.sql
Following file should be updated to use EF repository:
https://github.com/Dastus/SpeedTestApp/blob/master/SpeedTestApp/IoC/ConfigurationHelper.cs 
