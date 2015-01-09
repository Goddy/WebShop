namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Key2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderProducts", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderProducts", new[] { "Order_Id" });
            RenameColumn(table: "dbo.OrderProducts", name: "Order_Id", newName: "OrderId");
            AlterColumn("dbo.OrderProducts", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderProducts", "OrderId");
            AddForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            AlterColumn("dbo.OrderProducts", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.OrderProducts", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.OrderProducts", "Order_Id");
            AddForeignKey("dbo.OrderProducts", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
