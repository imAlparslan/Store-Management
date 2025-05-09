﻿// <auto-generated />
using System;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagement.Infrastructure.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20250420124750_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InventoryManagement.Domain.StockAggregateRoot.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StockId")
                        .HasColumnOrder(0);

                    b.PrimitiveCollection<string>("GroupIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("GroupIds");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Stocks", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
