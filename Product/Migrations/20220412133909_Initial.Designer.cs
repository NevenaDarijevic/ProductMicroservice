﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Product.Data;

namespace Product.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20220412133909_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Product.Models.Dobavljac", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Napomena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PIB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dobavljac");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Napomena = "Napomena....",
                            Naziv = "Dobavljac 1",
                            PIB = "1221223344"
                        },
                        new
                        {
                            Id = 2L,
                            Naziv = "Dobavljac 2",
                            PIB = "9874512"
                        },
                        new
                        {
                            Id = 3L,
                            Naziv = "Dobavljac 3",
                            PIB = "23974524123"
                        });
                });

            modelBuilder.Entity("Product.Models.JedinicaMere", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JedinicaMere");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Naziv = "Kilogram"
                        },
                        new
                        {
                            Id = 2L,
                            Naziv = "Kilogram"
                        },
                        new
                        {
                            Id = 3L,
                            Naziv = "Gram"
                        },
                        new
                        {
                            Id = 4L,
                            Naziv = "Miligram"
                        });
                });

            modelBuilder.Entity("Product.Models.Proizvod", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cena")
                        .HasColumnType("float");

                    b.Property<long>("JedinicaMereId")
                        .HasColumnType("bigint");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Pdv")
                        .HasColumnType("float");

                    b.Property<long>("TipProizvodaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("JedinicaMereId");

                    b.HasIndex("TipProizvodaId");

                    b.ToTable("Proizvod");
                });

            modelBuilder.Entity("Product.Models.ProizvodDobavljac", b =>
                {
                    b.Property<long>("DobavljacId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProizvodId")
                        .HasColumnType("bigint");

                    b.HasKey("DobavljacId", "ProizvodId");

                    b.HasIndex("ProizvodId");

                    b.ToTable("ProizvodDobavljac");
                });

            modelBuilder.Entity("Product.Models.TipProizvoda", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipProizvoda");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Naziv = "Tip 1"
                        },
                        new
                        {
                            Id = 2L,
                            Naziv = "Tip 2"
                        },
                        new
                        {
                            Id = 3L,
                            Naziv = "Tip 3"
                        },
                        new
                        {
                            Id = 4L,
                            Naziv = "Tip 4"
                        });
                });

            modelBuilder.Entity("Product.Models.Proizvod", b =>
                {
                    b.HasOne("Product.Models.JedinicaMere", "JedinicaMere")
                        .WithMany()
                        .HasForeignKey("JedinicaMereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Product.Models.TipProizvoda", "TipProizvoda")
                        .WithMany()
                        .HasForeignKey("TipProizvodaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JedinicaMere");

                    b.Navigation("TipProizvoda");
                });

            modelBuilder.Entity("Product.Models.ProizvodDobavljac", b =>
                {
                    b.HasOne("Product.Models.Dobavljac", "Dobavljac")
                        .WithMany("Proizvodi")
                        .HasForeignKey("DobavljacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Product.Models.Proizvod", "Proizvod")
                        .WithMany("Dobavljaci")
                        .HasForeignKey("ProizvodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dobavljac");

                    b.Navigation("Proizvod");
                });

            modelBuilder.Entity("Product.Models.Dobavljac", b =>
                {
                    b.Navigation("Proizvodi");
                });

            modelBuilder.Entity("Product.Models.Proizvod", b =>
                {
                    b.Navigation("Dobavljaci");
                });
#pragma warning restore 612, 618
        }
    }
}
