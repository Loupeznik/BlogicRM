﻿// <auto-generated />
using System;
using BlogicRM_.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogicRM_.Migrations
{
    [DbContext(typeof(BlogicRM))]
    partial class BlogicRMModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvisorContract", b =>
                {
                    b.Property<Guid>("AdvisorsAdvisorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContractsContractID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdvisorsAdvisorID", "ContractsContractID");

                    b.HasIndex("ContractsContractID");

                    b.ToTable("AdvisorContract");
                });

            modelBuilder.Entity("BlogicRM_.Models.Advisor", b =>
                {
                    b.Property<Guid>("AdvisorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("BirthNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdvisorID");

                    b.ToTable("Advisor");
                });

            modelBuilder.Entity("BlogicRM_.Models.Client", b =>
                {
                    b.Property<Guid>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("BirthNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientID");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("BlogicRM_.Models.Contract", b =>
                {
                    b.Property<Guid>("ContractID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdministratorAdvisorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ConclusionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EvidenceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstitutionID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidityDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ContractID");

                    b.HasIndex("AdministratorAdvisorID");

                    b.HasIndex("ClientID");

                    b.HasIndex("InstitutionID");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("BlogicRM_.Models.Institution", b =>
                {
                    b.Property<int>("InstitutionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstitutionID");

                    b.ToTable("Institution");
                });

            modelBuilder.Entity("AdvisorContract", b =>
                {
                    b.HasOne("BlogicRM_.Models.Advisor", null)
                        .WithMany()
                        .HasForeignKey("AdvisorsAdvisorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogicRM_.Models.Contract", null)
                        .WithMany()
                        .HasForeignKey("ContractsContractID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlogicRM_.Models.Contract", b =>
                {
                    b.HasOne("BlogicRM_.Models.Advisor", "Administrator")
                        .WithMany("Administering")
                        .HasForeignKey("AdministratorAdvisorID");

                    b.HasOne("BlogicRM_.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID");

                    b.HasOne("BlogicRM_.Models.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionID");

                    b.Navigation("Administrator");

                    b.Navigation("Client");

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("BlogicRM_.Models.Advisor", b =>
                {
                    b.Navigation("Administering");
                });
#pragma warning restore 612, 618
        }
    }
}
