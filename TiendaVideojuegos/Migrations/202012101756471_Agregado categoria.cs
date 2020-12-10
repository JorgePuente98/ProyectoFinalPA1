namespace TiendaVideojuegos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregadocategoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "CategoriaID", c => c.Int(nullable: false));
            CreateIndex("dbo.Productoes", "CategoriaID");
            AddForeignKey("dbo.Productoes", "CategoriaID", "dbo.Categorias", "CategoriaID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productoes", "CategoriaID", "dbo.Categorias");
            DropIndex("dbo.Productoes", new[] { "CategoriaID" });
            DropColumn("dbo.Productoes", "CategoriaID");
        }
    }
}
