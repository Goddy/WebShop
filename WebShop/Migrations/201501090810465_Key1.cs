namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Key1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderProducts", "Product_Id", "dbo.Products");
            DropIndex("dbo.OrderProducts", new[] { "Product_Id" });
            RenameColumn(table: "dbo.OrderProducts", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.OrderProducts", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderProducts", "ProductId");
            AddForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            AlterColumn("dbo.OrderProducts", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.OrderProducts", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.OrderProducts", "Product_Id");
            AddForeignKey("dbo.OrderProducts", "Product_Id", "dbo.Products", "Id");
        }
    }
}
