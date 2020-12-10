namespace TiendaVideojuegos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregadalaimgenalproducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "RutaImagen", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "RutaImagen");
        }
    }
}
