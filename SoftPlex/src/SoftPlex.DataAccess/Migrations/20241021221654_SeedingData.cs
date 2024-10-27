using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftPlex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("""
					insert into product (id, name, description) 
					VALUES ('9382c4ec-4642-40c8-950d-c302ecfcd51c', 'Деревообрабатывающие станки', 'Деревообрабатывающие станки и оборудование ') 
					ON CONFLICT (id) DO update SET name = excluded.name, description = excluded.description;
				""");
			migrationBuilder.Sql("""
				insert into product_version (id, product_id, name, description, width, height, length) 
				VALUES ('d09e0847-49a2-4893-825c-d786cec086c9','9382c4ec-4642-40c8-950d-c302ecfcd51c', 'Фрезерный', 'Фрезерный станк по дереву', 100,200,100) 
				ON CONFLICT (id) DO update SET product_id = excluded.product_id , name = excluded.name  , description = excluded.description , width = excluded.width , height = excluded.height , length = excluded.length ;
				""");
			migrationBuilder.Sql("""
				insert into product_version (id, product_id, name, description, width, height, length) 
				VALUES ('011416e0-375d-4392-a7a5-2787d02138a5','9382c4ec-4642-40c8-950d-c302ecfcd51c', 'Токарно-фрезерный станок', 'Токарно-фрезерный станок по дереву', 110,110,110) 
				ON CONFLICT (id) DO update SET product_id = excluded.product_id , name = excluded.name  , description = excluded.description , width = excluded.width , height = excluded.height , length = excluded.length ;
				""");
			migrationBuilder.Sql("""
				insert into product_version (id, product_id, name, description, width, height, length) 
				VALUES ('d28a85de-88a7-40f8-898f-53784a91b036','9382c4ec-4642-40c8-950d-c302ecfcd51c', 'Циркулярный станок', 'Циркулярный станок (распиловочные) по дереву', 90,100,100) 
				ON CONFLICT (id) DO update SET product_id = excluded.product_id , name = excluded.name  , description = excluded.description , width = excluded.width , height = excluded.height , length = excluded.length ;
				""");

			migrationBuilder.Sql("""
					insert into product (id, name, description) 
					VALUES ('74e36111-3871-49b0-bc37-5ae965ccd9c2', 'Шлифовальные станки', 'Шлифовальные станки и оборудование ') 
					ON CONFLICT (id) DO update SET name = excluded.name, description = excluded.description;
				""");
			migrationBuilder.Sql("""
				insert into product_version (id, product_id, name, description, width, height, length) 
				VALUES ('d46a99df-edd1-4a67-84ef-1c6c233fafac','74e36111-3871-49b0-bc37-5ae965ccd9c2', 'Дисковый', 'Дисковый станк по дереву', 50,200,100) 
				ON CONFLICT (id) DO update SET product_id = excluded.product_id , name = excluded.name  , description = excluded.description , width = excluded.width , height = excluded.height , length = excluded.length ;
				""");
			migrationBuilder.Sql("""
				insert into product_version (id, product_id, name, description, width, height, length) 
				VALUES ('3d0dcefa-b1c1-4a92-97b0-e5c54e5f4abc','74e36111-3871-49b0-bc37-5ae965ccd9c2', 'Ленточный станок', 'Ленточный станок по дереву', 90,80,70) 
				ON CONFLICT (id) DO update SET product_id = excluded.product_id , name = excluded.name  , description = excluded.description , width = excluded.width , height = excluded.height , length = excluded.length ;
				""");
			migrationBuilder.Sql("""
				insert into product_version (id, product_id, name, description, width, height, length) 
				VALUES ('4f7138b3-dd7f-4e9f-8f91-580b58bce86b','74e36111-3871-49b0-bc37-5ae965ccd9c2', 'Барабанный станок', 'Барабанный и шлифовальны станок по дереву', 90,140,100) 
				ON CONFLICT (id) DO update SET product_id = excluded.product_id , name = excluded.name  , description = excluded.description , width = excluded.width , height = excluded.height , length = excluded.length ;
				""");
			migrationBuilder.Sql("""
				insert into product_version (id, product_id, name, description, width, height, length) 
				VALUES ('4f7138b3-dd7f-4e9f-8f91-580b58bce86b','74e36111-3871-49b0-bc37-5ae965ccd9c2', 'Точильный станок', 'Барабанный Точильный станок по металу', 90,140,100) 
				ON CONFLICT (id) DO update SET product_id = excluded.product_id , name = excluded.name  , description = excluded.description , width = excluded.width , height = excluded.height , length = excluded.length ;
				""");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
