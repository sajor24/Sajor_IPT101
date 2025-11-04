CREATE PROCEDURE Get_Employee_ID
    @Emp_ID INT
AS
BEGIN
    SELECT * FROM Employee WHERE Emp_ID = @Emp_ID;
END
