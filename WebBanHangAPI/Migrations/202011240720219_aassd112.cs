namespace WebBanHangAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aassd112 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NhapHoaDons", "IDSP", "dbo.SanPhams");
            DropForeignKey("dbo.TSDienThoais", "IDSP", "dbo.SanPhams");
            DropForeignKey("dbo.TSDongHoes", "IDSP", "dbo.SanPhams");
            DropIndex("dbo.NhapHoaDons", new[] { "IDSP" });
            DropIndex("dbo.TSDienThoais", new[] { "IDSP" });
            DropIndex("dbo.TSDongHoes", new[] { "IDSP" });
            DropPrimaryKey("dbo.TSDienThoais");
            DropPrimaryKey("dbo.TSDongHoes");
            AddColumn("dbo.TSDienThoais", "SanPham_IDSP", c => c.String(maxLength: 10));
            AddColumn("dbo.TSDongHoes", "SanPham_IDSP", c => c.String(maxLength: 10));
            AlterColumn("dbo.NhapHoaDons", "IDSP", c => c.String());
            AlterColumn("dbo.TSDienThoais", "IDSP", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.TSDongHoes", "IDSP", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.TSDienThoais", "IDSP");
            AddPrimaryKey("dbo.TSDongHoes", "IDSP");
            CreateIndex("dbo.TSDienThoais", "SanPham_IDSP");
            CreateIndex("dbo.TSDongHoes", "SanPham_IDSP");
            AddForeignKey("dbo.TSDienThoais", "SanPham_IDSP", "dbo.SanPhams", "IDSP");
            AddForeignKey("dbo.TSDongHoes", "SanPham_IDSP", "dbo.SanPhams", "IDSP");
            DropColumn("dbo.TSDienThoais", "IDDT");
            DropColumn("dbo.TSDienThoais", "MyProperty");
            DropColumn("dbo.TSDongHoes", "IDDH");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TSDongHoes", "IDDH", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TSDienThoais", "MyProperty", c => c.String());
            AddColumn("dbo.TSDienThoais", "IDDT", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TSDongHoes", "SanPham_IDSP", "dbo.SanPhams");
            DropForeignKey("dbo.TSDienThoais", "SanPham_IDSP", "dbo.SanPhams");
            DropIndex("dbo.TSDongHoes", new[] { "SanPham_IDSP" });
            DropIndex("dbo.TSDienThoais", new[] { "SanPham_IDSP" });
            DropPrimaryKey("dbo.TSDongHoes");
            DropPrimaryKey("dbo.TSDienThoais");
            AlterColumn("dbo.TSDongHoes", "IDSP", c => c.String(maxLength: 10));
            AlterColumn("dbo.TSDienThoais", "IDSP", c => c.String(maxLength: 10));
            AlterColumn("dbo.NhapHoaDons", "IDSP", c => c.String(maxLength: 10));
            DropColumn("dbo.TSDongHoes", "SanPham_IDSP");
            DropColumn("dbo.TSDienThoais", "SanPham_IDSP");
            AddPrimaryKey("dbo.TSDongHoes", "IDDH");
            AddPrimaryKey("dbo.TSDienThoais", "IDDT");
            CreateIndex("dbo.TSDongHoes", "IDSP");
            CreateIndex("dbo.TSDienThoais", "IDSP");
            CreateIndex("dbo.NhapHoaDons", "IDSP");
            AddForeignKey("dbo.TSDongHoes", "IDSP", "dbo.SanPhams", "IDSP");
            AddForeignKey("dbo.TSDienThoais", "IDSP", "dbo.SanPhams", "IDSP");
            AddForeignKey("dbo.NhapHoaDons", "IDSP", "dbo.SanPhams", "IDSP");
        }
    }
}
