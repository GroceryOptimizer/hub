﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Core.Entities.StockItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StockListId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StockListId");

                    b.ToTable("StockItem");
                });

            modelBuilder.Entity("Core.Entities.StockList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("StockList");
                });

            modelBuilder.Entity("Core.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GrpcAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Location", "Core.Entities.Store.Location#Coordinates", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<double>("Latitude")
                                .HasColumnType("REAL");

                            b1.Property<double>("Longitude")
                                .HasColumnType("REAL");
                        });

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Core.Entities.StockItem", b =>
                {
                    b.HasOne("Core.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.StockList", null)
                        .WithMany("StockItems")
                        .HasForeignKey("StockListId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Core.Entities.StockList", b =>
                {
                    b.Navigation("StockItems");
                });
#pragma warning restore 612, 618
        }
    }
}
