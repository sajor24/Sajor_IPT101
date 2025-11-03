CREATE PROCEDURE Create_Employee
    @FirstName NVARCHAR(50) = NULL,
    @LastName  NVARCHAR(50) = NULL,
    @Age NVARCHAR(50) = NULL,
    @Position NVARCHAR(50) = NULL
AS
BEGIN
    INSERT INTO [dbo].[Employee] ([FirstName], [LastName], [Age], [Position])
    VALUES (@FirstName, @LastName, @Age, @Position);
END
