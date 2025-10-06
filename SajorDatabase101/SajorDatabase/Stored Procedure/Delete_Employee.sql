CREATE PROCEDURE [dbo].[Delete_Employee]
    @Emp_ID INT
AS
BEGIN
    DELETE FROM [dbo].[Employee]
    WHERE [Emp_ID] = @Emp_ID;
END
