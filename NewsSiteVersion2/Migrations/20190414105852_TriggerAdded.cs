using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsSiteVersion2.Migrations
{
    public partial class TriggerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" USE NewsSiteUsers
                                     SET ANSI_NULLS ON
                                     GO
                                     SET QUOTED_IDENTIFIER ON
                                     GO
                                     CREATE  TRIGGER DeleteNews
                                         ON NewsSiteUsers.dbo.AspNetUsers
                                         FOR DELETE
                                     AS
                                         DELETE FROM NewsSite.dbo.News
                                     WHERE  News.AuthorId IN(SELECT deleted.id FROM deleted)");
           }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"USE NewsSiteUsers
                                   DROP TRIGGER IF EXISTS DeleteNews");
        }
    }
}
