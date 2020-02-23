SELECT * FROM Employees e


CREATE PROCEDURE spGetEmployeeById
  @Id INT
AS
BEGIN
  SELECT e.Id, e.Name, e.Email, e.PhotoPath, e.Department FROM Employees e
    WHERE e.Id = @Id
END

EXEC spGetEmployeeById 3

-- with Code First approach, the procedure should be created using migrations, not here
DROP PROC spGetEmployeeById


CREATE PROCEDURE spInsertEmployee
  @Name NVARCHAR(100),
  @Email NVARCHAR(100),
  @PhotoPath NVARCHAR(1000),
  @Dept INT
AS
BEGIN
  INSERT INTO Employees (Name, Email, PhotoPath, Department) 
    VALUES (@Name, @Email, @PhotoPath, @Dept)
END

EXEC spInsertEmployee 'Simon', 'simon@testsp.com', NULL, 1

DROP PROCEDURE spInsertEmployee