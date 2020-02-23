using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesDemo.Services.Migrations
{
    public partial class spGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string create_spGetEmployeeById = @"
                CREATE PROCEDURE spGetEmployeeById
                  @Id INT
                AS
                BEGIN
                  SELECT e.Id, e.Name, e.Email, e.PhotoPath, e.Department FROM Employees e
                    WHERE e.Id = @Id
                END
            ";
            migrationBuilder.Sql(create_spGetEmployeeById);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            const string drop_spGetEmployeeById = @"
                DROP PROCEDURE spGetEmployeeById
            ";
            migrationBuilder.Sql(drop_spGetEmployeeById);
        }
    }
}
