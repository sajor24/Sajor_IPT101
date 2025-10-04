CREATE PROCEDURE [dbo].[Get_Employee_ID]
    @Emp_ID INT
AS
BEGIN
    SELECT * FROM [dbo].[Employee] AS a WHERE a.[Emp_ID] = @Emp_ID;
END
