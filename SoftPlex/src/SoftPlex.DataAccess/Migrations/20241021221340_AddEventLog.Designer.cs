﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SoftPlex.DataAccess;

#nullable disable

namespace SoftPlex.DataAccess.Migrations
{
    [DbContext(typeof(SoftPlexDbContext))]
    [Migration("20241021221340_AddEventLog")]
    partial class AddEventLog
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SoftPlex.DataAccess.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_product");

                    b.HasIndex("Name")
                        .HasDatabaseName("ix_product_name");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("SoftPlex.DataAccess.Entities.ProductVersionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatingDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creating_date")
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal>("Height")
                        .HasColumnType("numeric")
                        .HasColumnName("height");

                    b.Property<decimal>("Length")
                        .HasColumnType("numeric")
                        .HasColumnName("length");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<decimal>("Width")
                        .HasColumnType("numeric")
                        .HasColumnName("width");

                    b.HasKey("Id")
                        .HasName("pk_product_version");

                    b.HasIndex("CreatingDate")
                        .HasDatabaseName("ix_product_version_creating_date");

                    b.HasIndex("Height")
                        .HasDatabaseName("ix_product_version_height");

                    b.HasIndex("Length")
                        .HasDatabaseName("ix_product_version_length");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_product_version_product_id");

                    b.HasIndex("Width")
                        .HasDatabaseName("ix_product_version_width");

                    b.ToTable("product_version", (string)null);
                });

            modelBuilder.Entity("SoftPlex.DataAccess.Entities.ProductVersionEntity", b =>
                {
                    b.HasOne("SoftPlex.DataAccess.Entities.ProductEntity", null)
                        .WithMany("ProductVersionEntities")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_version_product_product_id");
                });

            modelBuilder.Entity("SoftPlex.DataAccess.Entities.ProductEntity", b =>
                {
                    b.Navigation("ProductVersionEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
