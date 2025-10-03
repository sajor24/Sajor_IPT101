CREATE PROCEDURE [dbo].[Update_Employee]
    @Emp_ID INT,
    @FirstName NCHAR(50) = NULL,
    @LastName NCHAR(50) = NULL,
    @Age NCHAR(50) = NULL,
    @Position NCHAR(50) = NULL
AS
BEGIN
    UPDATE [dbo].[Employee]
    SET 
        [FirstName] = @FirstName,
        [LastName] = @LastName,
        [Age] = @Age,
        [Position] = @Position
    WHERE [Emp_ID] = @Emp_ID;
END
