/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

exec Read_Employee
/*
insert into Employee(FirstName,LastName,Age,Position)
Values ('IRving','Sajor','18','Developer')
Update Employee SET Position  = 'Developer' , Age = '20'
Where Emp_ID = '1'*/
Truncate Table employee
