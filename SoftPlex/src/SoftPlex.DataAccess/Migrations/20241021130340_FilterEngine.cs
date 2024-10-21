using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftPlex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FilterEngine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql("""
					CREATE OR REPLACE FUNCTION filter_engine(
					product_name_in varchar
					, product_version_name_in varchar
					, min_size numeric
					, max_size numeric
					)
				RETURNS TABLE (
				    id uuid,
					product_name varchar,
					product_version_name varchar,
					width numeric,
					height numeric,
					length numeric
				)
				LANGUAGE plpgsql
				AS $$
				BEGIN
				    IF max_size < 0 THEN
				        RETURN QUERY
				            select pv.id, p."name",pv."name",pv.width,pv.height,pv.length
								from product as p join product_version as pv on p.id = pv.product_id
								where p."name" like concat('%', product_name_in, '%') 
					        			and pv."name" like concat('%', product_version_name_in, '%') 
					        			and (pv.width  * pv.height * pv.length) >= min_size 
					        			and (pv.width  * pv.height * pv.length) <= max_size
				           ;
				    ELSE
				        RETURN QUERY
				            select pv.id, p."name",pv."name",pv.width,pv.height,pv.length
								from product as p join product_version as pv on p.id = pv.product_id
								where p."name" like concat('%', product_name_in, '%') 
					        			and pv."name" like concat('%', product_version_name_in, '%') 
					        			and (pv.width  * pv.height * pv.length) >= min_size 
				           ;
				    END IF;
				END;
				$$;
				""");
}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql("DROP FUNCTION public.search_engine(varchar, varchar, numeric, numeric);");
		}
    }
}
