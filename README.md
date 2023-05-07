# web_api

This task is done using Web Api and dapper

Download or clone the tutorial project code from
https://github.com/ashish1157/web_api

Update the database credentials in /appsettings.json to connect to your MS SQL
Server instance, and ensure MSSQL server is running.
Use following commond to create table


Create Table Products(
ProductId Int Identity(1,1) Primary Key,
Name Varchar(100) Not Null,
UnitPrice Decimal Not Null)

put some values in that table

Download packages of sqlclient and dapper using NuGet Package Manager

Start the api using green play button(https) and you will see swagger api opening from there 
copy the url and paste in the respective crud operation page according to react.js

