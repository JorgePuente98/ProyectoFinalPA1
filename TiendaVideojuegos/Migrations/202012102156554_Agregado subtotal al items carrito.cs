namespace TiendaVideojuegos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregadosubtotalalitemscarrito : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarritoItems", "Subtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarritoItems", "Subtotal");
        }
    }
}
