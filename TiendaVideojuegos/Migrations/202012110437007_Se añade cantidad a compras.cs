namespace TiendaVideojuegos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seañadecantidadacompras : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "Cantidad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "Cantidad");
        }
    }
}
