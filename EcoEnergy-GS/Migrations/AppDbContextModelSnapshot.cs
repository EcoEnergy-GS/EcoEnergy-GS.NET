﻿// <auto-generated />
using System;
using EcoEnergy_GS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoEnergy_GS.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EcoEnergy_GS.Models.ConsumoEnergiaModel", b =>
                {
                    b.Property<int>("id_consumo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_CONSUMO");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_consumo"));

                    b.Property<int>("consumo")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("CONSUMO");

                    b.Property<DateTime>("data_consumo")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("DATA_CONSUMO");

                    b.Property<int>("id_residencia")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_RESIDENCIA");

                    b.HasKey("id_consumo");

                    b.HasIndex("id_residencia");

                    b.ToTable("CONSUMO_ENERGIA");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.EnderecoModel", b =>
                {
                    b.Property<int>("id_endereco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_ENDERECO");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_endereco"));

                    b.Property<string>("cep")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CEP");

                    b.Property<string>("complemento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasColumnName("COMPLEMENTO");

                    b.Property<int>("numero")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("NUMERO");

                    b.Property<string>("rua")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasColumnName("RUA");

                    b.HasKey("id_endereco");

                    b.ToTable("ENDERECO");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.HistoricoPontosModel", b =>
                {
                    b.Property<int>("id_historico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_HISTORICO");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_historico"));

                    b.Property<DateTime>("data_historico")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("DATA_HISTORICO");

                    b.Property<int>("id_usuarios")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIOS");

                    b.Property<int>("quantidade")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("QUANTIDADE");

                    b.HasKey("id_historico");

                    b.HasIndex("id_usuarios");

                    b.ToTable("HISTORICO_PONTOS");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.RecompensasModel", b =>
                {
                    b.Property<int>("id_recompensas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_RECOMPENSAS");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_recompensas"));

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("DESCRICAO");

                    b.Property<int>("pontos_necessarios")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PONTOS_NECESSARIOS");

                    b.HasKey("id_recompensas");

                    b.ToTable("RECOMPENSAS");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.ResidenciaModel", b =>
                {
                    b.Property<int>("id_residencia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_RESIDENCIA");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_residencia"));

                    b.Property<string>("dispotivico_monitoramento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasColumnName("DISPOSITIVO_MONITORAMENTO");

                    b.Property<int>("id_eletrodomestico")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_ELETRODOMESTICO");

                    b.Property<int>("id_endereco")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_ENDERECO");

                    b.Property<int>("id_usuarios")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIOS");

                    b.Property<double>("media_consumo")
                        .HasColumnType("BINARY_DOUBLE")
                        .HasColumnName("MEDIA_CONSUMO");

                    b.Property<int>("quantidade_pessoas")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("QUANTIDADE_PESSOAS");

                    b.HasKey("id_residencia");

                    b.HasIndex("id_eletrodomestico");

                    b.HasIndex("id_endereco");

                    b.HasIndex("id_usuarios");

                    b.ToTable("RESIDENCIA");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.TipoEletrodomesticoModel", b =>
                {
                    b.Property<int>("id_eletrodomestico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_ELETRODOMESTICO");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_eletrodomestico"));

                    b.Property<string>("nome_eletrodomestico")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasColumnName("NOME_ELETRODOMESTICO");

                    b.Property<int>("quantidade")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("QUANTIDADE");

                    b.HasKey("id_eletrodomestico");

                    b.ToTable("TIPO_ELETRODOMESTICO");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.TrocasRecompensasModel", b =>
                {
                    b.Property<int>("id_trocas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_TROCAS");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_trocas"));

                    b.Property<DateTime>("data_troca")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("DATA_TROCA");

                    b.Property<int>("id_recompensas")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_RECOMPENSAS");

                    b.Property<int>("id_usuarios")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIOS");

                    b.Property<int>("pontos_utilizados")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PONTOS_UTILIZADOS");

                    b.HasKey("id_trocas");

                    b.HasIndex("id_recompensas");

                    b.HasIndex("id_usuarios");

                    b.ToTable("TROCAS_RECOMPENSAS");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.UsuarioModel", b =>
                {
                    b.Property<int>("id_usuarios")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIOS");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_usuarios"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasColumnName("NOME");

                    b.Property<int>("pontos")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PONTOS");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("SENHA");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("TELEFONE");

                    b.HasKey("id_usuarios");

                    b.ToTable("USUARIOS");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.ConsumoEnergiaModel", b =>
                {
                    b.HasOne("EcoEnergy_GS.Models.ResidenciaModel", "Residencia")
                        .WithMany("ConsumoEnergia")
                        .HasForeignKey("id_residencia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Residencia");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.HistoricoPontosModel", b =>
                {
                    b.HasOne("EcoEnergy_GS.Models.UsuarioModel", "Usuario")
                        .WithMany("HistoricoPontos")
                        .HasForeignKey("id_usuarios")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.ResidenciaModel", b =>
                {
                    b.HasOne("EcoEnergy_GS.Models.TipoEletrodomesticoModel", "TipoEletrodomestico")
                        .WithMany("Residencia")
                        .HasForeignKey("id_eletrodomestico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoEnergy_GS.Models.EnderecoModel", "Endereco")
                        .WithMany("Residencia")
                        .HasForeignKey("id_endereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoEnergy_GS.Models.UsuarioModel", "Usuario")
                        .WithMany("Residencia")
                        .HasForeignKey("id_usuarios")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("TipoEletrodomestico");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.TrocasRecompensasModel", b =>
                {
                    b.HasOne("EcoEnergy_GS.Models.RecompensasModel", "Recompensas")
                        .WithMany("Trocas")
                        .HasForeignKey("id_recompensas")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoEnergy_GS.Models.UsuarioModel", "Usuario")
                        .WithMany("Trocas")
                        .HasForeignKey("id_usuarios")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recompensas");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.EnderecoModel", b =>
                {
                    b.Navigation("Residencia");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.RecompensasModel", b =>
                {
                    b.Navigation("Trocas");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.ResidenciaModel", b =>
                {
                    b.Navigation("ConsumoEnergia");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.TipoEletrodomesticoModel", b =>
                {
                    b.Navigation("Residencia");
                });

            modelBuilder.Entity("EcoEnergy_GS.Models.UsuarioModel", b =>
                {
                    b.Navigation("HistoricoPontos");

                    b.Navigation("Residencia");

                    b.Navigation("Trocas");
                });
#pragma warning restore 612, 618
        }
    }
}
