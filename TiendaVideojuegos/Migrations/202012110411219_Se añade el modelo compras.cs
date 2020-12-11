namespace TiendaVideojuegos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seañadeelmodelocompras : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        CompraID = c.Int(nullable: false),
                        ProductoID = c.Int(nullable: false),
                        ClienteID = c.Int(nullable: false),
                        SubTotal = c.Decimal(nullable: false, storeType: "money"),
                        FechaCompra = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompraID, t.ProductoID, t.ClienteID })
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ProductoID, cascadeDelete: true)
                .Index(t => t.ProductoID)
                .Index(t => t.ClienteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compras", "ProductoID", "dbo.Productoes");
            DropForeignKey("dbo.Compras", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.Compras", new[] { "ClienteID" });
            DropIndex("dbo.Compras", new[] { "ProductoID" });
            DropTable("dbo.Compras");
        }
    }
}
