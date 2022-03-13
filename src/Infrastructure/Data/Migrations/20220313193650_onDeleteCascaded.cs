using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class onDeleteCascaded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListMembers_ShoppingLists_ShoppingListId",
                table: "ShoppingListMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListMembers_ShoppingLists_ShoppingListId",
                table: "ShoppingListMembers",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListMembers_ShoppingLists_ShoppingListId",
                table: "ShoppingListMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListMembers_ShoppingLists_ShoppingListId",
                table: "ShoppingListMembers",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
