﻿// <auto-generated />
using System;
using CadastroDeClientesBackEnd.DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CadastroDeClientesBackEnd.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240830103601_estrutura_inicial")]
    partial class estrutura_inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CadastroDeClientesBackEnd.DatabaseLayer.Entity+Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Logotipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("CadastroDeClientesBackEnd.DatabaseLayer.Entity+ClienteEndereco", b =>
                {
                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EnderecoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClienteId", "EnderecoId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("ClienteId", "EnderecoId")
                        .IsUnique();

                    b.ToTable("ClienteEndereco");
                });

            modelBuilder.Entity("CadastroDeClientesBackEnd.DatabaseLayer.Entity+Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Logradouro")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("CadastroDeClientesBackEnd.DatabaseLayer.Entity+ClienteEndereco", b =>
                {
                    b.HasOne("CadastroDeClientesBackEnd.DatabaseLayer.Entity+Cliente", "Cliente")
                        .WithMany("Enderecos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CadastroDeClientesBackEnd.DatabaseLayer.Entity+Endereco", "Endereco")
                        .WithMany("Enderecos")
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("CadastroDeClientesBackEnd.DatabaseLayer.Entity+Cliente", b =>
                {
                    b.Navigation("Enderecos");
                });

            modelBuilder.Entity("CadastroDeClientesBackEnd.DatabaseLayer.Entity+Endereco", b =>
                {
                    b.Navigation("Enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
