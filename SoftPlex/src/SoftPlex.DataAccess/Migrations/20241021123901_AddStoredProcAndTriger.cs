using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftPlex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcAndTriger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql("""
				CREATE OR REPLACE FUNCTION trigger_function_for_product() RETURNS TRIGGER AS $$
				BEGIN
				    IF (TG_OP = 'INSERT') THEN
				        INSERT INTO event_log (id, event_date, description) VALUES (gen_random_uuid(), now(), concat('product INSERT: ID = ',NEW.ID));
				        RETURN NEW;
				    ELSIF (TG_OP = 'UPDATE') THEN
				        INSERT INTO event_log (id, event_date, description) VALUES (gen_random_uuid(), now(), concat('product UPDATE: ID = ',NEW.ID));
				        RETURN NEW;
				    ELSIF (TG_OP = 'DELETE') THEN
						INSERT INTO event_log (id, event_date, description) VALUES (gen_random_uuid(), now(), concat('product DELETE: ID = ',OLD.ID));

				        RETURN OLD;
				    END IF;
				END;
				$$ LANGUAGE plpgsql;
				""");

	        migrationBuilder.Sql("CREATE TRIGGER track_changes AFTER INSERT OR UPDATE OR DELETE ON product FOR EACH ROW EXECUTE PROCEDURE trigger_function_for_product();");

	        migrationBuilder.Sql("""
				CREATE OR REPLACE FUNCTION trigger_function_for_product_version() RETURNS TRIGGER AS $$
				BEGIN
				    IF (TG_OP = 'INSERT') THEN
				        INSERT INTO event_log (id, event_date, description) VALUES (gen_random_uuid(), now(), concat('product_version INSERT: ID = ',NEW.ID));
				        RETURN NEW;
				    ELSIF (TG_OP = 'UPDATE') THEN
				        INSERT INTO event_log (id, event_date, description) VALUES (gen_random_uuid(), now(), concat('product_version UPDATE: ID = ',NEW.ID));
				        RETURN NEW;
				    ELSIF (TG_OP = 'DELETE') THEN
						INSERT INTO event_log (id, event_date, description) VALUES (gen_random_uuid(), now(), concat('product_version DELETE: ID = ',OLD.ID));

				        RETURN OLD;
				    END IF;
				END;
				$$ LANGUAGE plpgsql;
				""");

	        migrationBuilder.Sql("CREATE TRIGGER track_changes AFTER INSERT OR UPDATE OR DELETE ON product_version FOR EACH ROW EXECUTE PROCEDURE trigger_function_for_product_version();");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql("DROP TRIGGER track_changes ON public.product_version;");
	        migrationBuilder.Sql("DROP TRIGGER track_changes ON public.product;");

	        migrationBuilder.Sql("DROP FUNCTION public.trigger_function_for_product_version();");
	        migrationBuilder.Sql("DROP FUNCTION public.trigger_function_for_product();");
}
    }
}
