namespace WebBanHangAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sasdggg111 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TSDienThoais", new[] { "SanPham_IDSP" });
            DropIndex("dbo.TSDongHoes", new[] { "SanPham_IDSP" });
            RenameColumn(table: "dbo.TSDienThoais", name: "SanPham_IDSP", newName: "IDDT");
            RenameColumn(table: "dbo.TSDongHoes", name: "SanPham_IDSP", newName: "IDDH");
            DropPrimaryKey("dbo.TSDienThoais");
            DropPrimaryKey("dbo.TSDongHoes");
            AlterColumn("dbo.TSDienThoais", "IDDT", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.TSDongHoes", "IDDH", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.TSDienThoais", "IDDT");
            AddPrimaryKey("dbo.TSDongHoes", "IDDH");
            CreateIndex("dbo.TSDienThoais", "IDDT");
            CreateIndex("dbo.TSDongHoes", "IDDH");
            DropColumn("dbo.TSDienThoais", "IDSP");
            DropColumn("dbo.TSDongHoes", "IDSP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TSDongHoes", "IDSP", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.TSDienThoais", "IDSP", c => c.String(nullable: false, maxLength: 10));
            DropIndex("dbo.TSDongHoes", new[] { "IDDH" });
            DropIndex("dbo.TSDienThoais", new[] { "IDDT" });
            DropPrimaryKey("dbo.TSDongHoes");
            DropPrimaryKey("dbo.TSDienThoais");
            AlterColumn("dbo.TSDongHoes", "IDDH", c => c.String(maxLength: 10));
            AlterColumn("dbo.TSDienThoais", "IDDT", c => c.String(maxLength: 10));
            AddPrimaryKey("dbo.TSDongHoes", "IDSP");
            AddPrimaryKey("dbo.TSDienThoais", "IDSP");
            RenameColumn(table: "dbo.TSDongHoes", name: "IDDH", newName: "SanPham_IDSP");
            RenameColumn(table: "dbo.TSDienThoais", name: "IDDT", newName: "SanPham_IDSP");
            CreateIndex("dbo.TSDongHoes", "SanPham_IDSP");
            CreateIndex("dbo.TSDienThoais", "SanPham_IDSP");
        }
    }
}
