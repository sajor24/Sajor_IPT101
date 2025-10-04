CREATE PROCEDURE [dbo].[DeleteEmployee]
    @Emp_ID INT
AS
BEGIN
    DELETE FROM [dbo].[Employee]
    WHERE [Emp_ID] = @Emp_ID;
END
