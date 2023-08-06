namespace WebAppEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_brand",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discounted = c.Boolean(nullable: false),
                        DiscountRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Detail = c.String(),
                        FeaturedImage = c.String(),
                        CategoryId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_brand", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.tbl_category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_product_image",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        ProductId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_product_image", "ProductId", "dbo.tbl_product");
            DropForeignKey("dbo.tbl_product", "CategoryId", "dbo.tbl_category");
            DropForeignKey("dbo.tbl_product", "BrandId", "dbo.tbl_brand");
            DropIndex("dbo.tbl_product_image", new[] { "ProductId" });
            DropIndex("dbo.tbl_product", new[] { "BrandId" });
            DropIndex("dbo.tbl_product", new[] { "CategoryId" });
            DropTable("dbo.tbl_product_image");
            DropTable("dbo.tbl_category");
            DropTable("dbo.tbl_product");
            DropTable("dbo.tbl_brand");
        }
    }
}
