namespace TiendaVideojuegos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.CategoriaID);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ProductoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25, unicode: false),
                        Precio = c.Decimal(nullable: false, storeType: "money"),
                        Descripcion = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ProductoID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Productoes");
            DropTable("dbo.Categorias");
        }
    }
}
