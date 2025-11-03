CREATE PROCEDURE Delete_Employee
    @Emp_ID INT
AS
BEGIN
    DELETE FROM Employee
    WHERE [Emp_ID] = @Emp_ID;
END
