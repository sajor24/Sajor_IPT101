CREATE TABLE [dbo].[Employee]
(

    Emp_ID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NULL,
    LastName NVARCHAR(50) NULL,
    Age INT NULL,
    Position NVARCHAR(50) NULL

)
