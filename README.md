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

Short notes about application:

google.com used for all tests because it has sitemap.xml and matches pattern 
http://www.{SiteURL}/sitemap.xml

Info about responses is stored in DB, SQLExpress has been used.
Script for DataBase creation:
https://github.com/Dastus/SpeedTestApp/blob/master/SpeedTestApp/dbscript.sql
