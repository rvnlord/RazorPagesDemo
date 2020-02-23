using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesDemo.Services.Migrations
{
    public partial class spInsertEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string create_spInsertEmployee = @"
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
            ";
            migrationBuilder.Sql(create_spInsertEmployee);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            const string drop_spInsertEmployee = @"
                DROP PROCEDURE spInsertEmployee
            ";
            migrationBuilder.Sql(drop_spInsertEmployee);
        }
    }
}
