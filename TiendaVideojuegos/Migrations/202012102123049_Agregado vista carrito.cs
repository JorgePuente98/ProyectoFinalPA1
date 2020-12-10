namespace TiendaVideojuegos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregadovistacarrito : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarritoItems",
                c => new
                    {
                        CarritoID = c.Int(nullable: false, identity: true),
                        ProductoID = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarritoID)
                .ForeignKey("dbo.Productoes", t => t.ProductoID, cascadeDelete: true)
                .Index(t => t.ProductoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarritoItems", "ProductoID", "dbo.Productoes");
            DropIndex("dbo.CarritoItems", new[] { "ProductoID" });
            DropTable("dbo.CarritoItems");
        }
    }
}
