﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SimpleCashFlow.Domain.ValueObjects;
using SimpleCashFlow.Infrastructure.Data.Context;

#nullable disable

namespace SimpleCashFlow.Infrastructure.Data.Migrations
{
    [DbContext(typeof(SimpleCashFlowContext))]
    [Migration("20230618140845_AddedDeletedOnMovementAndAddedSummary")]
    partial class AddedDeletedOnMovementAndAddedSummary
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SimpleCashFlow.Domain.DomainEvents.Base.DomainEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("MovementId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MovementId");

                    b.ToTable("DomainEvent");
                });

            modelBuilder.Entity("SimpleCashFlow.Domain.Entities.Movement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Classification")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.ToTable("Movements");
                });

            modelBuilder.Entity("SimpleCashFlow.Domain.ValueObjects.CashFlowDailySummary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<List<CashFlowDailySummary.MovementItem>>("MovementItems")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalCreditAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalDebitAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("Date")
                        .IsUnique();

                    b.ToTable("Summaries");
                });

            modelBuilder.Entity("SimpleCashFlow.Domain.DomainEvents.Base.DomainEvent", b =>
                {
                    b.HasOne("SimpleCashFlow.Domain.Entities.Movement", null)
                        .WithMany("DomainEvent")
                        .HasForeignKey("MovementId");
                });

            modelBuilder.Entity("SimpleCashFlow.Domain.Entities.Movement", b =>
                {
                    b.Navigation("DomainEvent");
                });
#pragma warning restore 612, 618
        }
    }
}
