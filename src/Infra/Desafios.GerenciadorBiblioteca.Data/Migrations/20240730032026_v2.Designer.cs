﻿// <auto-generated />
using System;
using Desafios.GerenciadorBiblioteca.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Desafios.GerenciadorBiblioteca.Data.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20240730032026_v2")]
    partial class v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("Year")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("LibraryId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Library", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("Id");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LoanValidity")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Returned")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.VerificationCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("VerificationCodes");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Inventory", b =>
                {
                    b.HasOne("Desafios.GerenciadorBiblioteca.Domain.Entities.Book", "Book")
                        .WithMany("Inventories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Desafios.GerenciadorBiblioteca.Domain.Entities.Library", "Library")
                        .WithMany("Inventories")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Library");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Loan", b =>
                {
                    b.HasOne("Desafios.GerenciadorBiblioteca.Domain.Entities.Inventory", "Inventory")
                        .WithMany("Loans")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Desafios.GerenciadorBiblioteca.Domain.Entities.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.VerificationCode", b =>
                {
                    b.HasOne("Desafios.GerenciadorBiblioteca.Domain.Entities.User", "User")
                        .WithMany("VerificationCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Book", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Inventory", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.Library", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("Desafios.GerenciadorBiblioteca.Domain.Entities.User", b =>
                {
                    b.Navigation("Loans");

                    b.Navigation("VerificationCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
