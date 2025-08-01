﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quoter.Framework.Data;

#nullable disable

namespace Quoter.Framework.Migrations
{
    [DbContext(typeof(QuoterContext))]
    [Migration("20230302092719_AddIsFavourite")]
    partial class AddIsFavourite
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("Quoter.Framework.Entities.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CollectionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsFavourite")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BookId");

                    b.HasIndex("CollectionId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Chapter", b =>
                {
                    b.Property<int>("ChapterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChapterIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsFavourite")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ChapterId");

                    b.HasIndex("BookId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Collection", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsFavourite")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Language")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CollectionId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Quote", b =>
                {
                    b.Property<long>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ChapterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CollectionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuoteIndex")
                        .HasColumnType("INTEGER");

                    b.HasKey("QuoteId");

                    b.HasIndex("BookId");

                    b.HasIndex("ChapterId");

                    b.HasIndex("CollectionId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Book", b =>
                {
                    b.HasOne("Quoter.Framework.Entities.Collection", "Collection")
                        .WithMany("LstBooks")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Chapter", b =>
                {
                    b.HasOne("Quoter.Framework.Entities.Book", "Book")
                        .WithMany("LstChapters")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Quote", b =>
                {
                    b.HasOne("Quoter.Framework.Entities.Book", "Book")
                        .WithMany("LstQuotes")
                        .HasForeignKey("BookId");

                    b.HasOne("Quoter.Framework.Entities.Chapter", "Chapter")
                        .WithMany("LstQuotes")
                        .HasForeignKey("ChapterId");

                    b.HasOne("Quoter.Framework.Entities.Collection", "Collection")
                        .WithMany("LstQuotes")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Chapter");

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Book", b =>
                {
                    b.Navigation("LstChapters");

                    b.Navigation("LstQuotes");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Chapter", b =>
                {
                    b.Navigation("LstQuotes");
                });

            modelBuilder.Entity("Quoter.Framework.Entities.Collection", b =>
                {
                    b.Navigation("LstBooks");

                    b.Navigation("LstQuotes");
                });
#pragma warning restore 612, 618
        }
    }
}
