CREATE PROCEDURE Update_Employee
    @Emp_ID NVARCHAR(50),
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @Age NVARCHAR(50) = NULL,
    @Position NVARCHAR(50) = NULL
AS
BEGIN
    UPDATE Employee
    SET 
        [FirstName] = @FirstName,
        [LastName] = @LastName,
        [Age] = @Age,
        [Position] = @Position
    WHERE [Emp_ID] = @Emp_ID;
END
