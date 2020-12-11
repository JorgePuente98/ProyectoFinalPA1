namespace TiendaVideojuegos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seañadeelclientesolamente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25, unicode: false),
                        Apellidos = c.String(nullable: false, maxLength: 25, unicode: false),
                        Edad = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 40, unicode: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clientes");
        }
    }
}
