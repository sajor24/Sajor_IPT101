CREATE PROCEDURE [dbo].[Create_Employee]
    @Emp_ID INT,
    @FirstName NVARCHAR(50) = NULL,
    @LastName  NVARCHAR(50) NULL,
    @Age  NVARCHAR(50) NULL,
    @Position NVARCHAR(50) = NULL
AS
BEGIN
    INSERT INTO [dbo].[Employee] ([Emp_ID], [FirstName], [LastName], [Age], [Position])
    VALUES (@Emp_ID, @FirstName, @LastName, @Age, @Position);
END
