﻿// <auto-generated />
using System;
using Karami.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Karami.Persistence.Migrations.C
{
    [DbContext(typeof(SQLContext))]
    [Migration("20230723151022_CreateTablesVersion_1_0_0")]
    partial class CreateTablesVersion_1_0_0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Karami.Core.Domain.Entities.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedAt_PersianDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Payload")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Table")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt_EnglishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedAt_PersianDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.ArticleComment.Entities.ArticleComment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("IsActive")
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsDeleted")
                        .HasColumnType("tinyint");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id", "IsDeleted");

                    b.ToTable("ArticleComments", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.ArticleCommentAnswer.Entities.ArticleCommentAnswer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte>("IsActive")
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsDeleted")
                        .HasColumnType("tinyint");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("Id", "IsDeleted");

                    b.ToTable("ArticleCommentAnswers", (string)null);
                });

            modelBuilder.Entity("Karami.Domain.ArticleComment.Entities.ArticleComment", b =>
                {
                    b.OwnsOne("Karami.Core.Domain.ValueObjects.CreatedAt", "CreatedAt", b1 =>
                        {
                            b1.Property<string>("ArticleCommentId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime?>("EnglishDate")
                                .IsRequired()
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedAt_EnglishDate");

                            b1.Property<string>("PersianDate")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CreatedAt_PersianDate");

                            b1.HasKey("ArticleCommentId");

                            b1.ToTable("ArticleComments");

                            b1.WithOwner()
                                .HasForeignKey("ArticleCommentId");
                        });

                    b.OwnsOne("Karami.Core.Domain.ValueObjects.UpdatedAt", "UpdatedAt", b1 =>
                        {
                            b1.Property<string>("ArticleCommentId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime?>("EnglishDate")
                                .IsRequired()
                                .HasColumnType("datetime2")
                                .HasColumnName("UpdatedAt_EnglishDate");

                            b1.Property<string>("PersianDate")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("UpdatedAt_PersianDate");

                            b1.HasKey("ArticleCommentId");

                            b1.ToTable("ArticleComments");

                            b1.WithOwner()
                                .HasForeignKey("ArticleCommentId");
                        });

                    b.OwnsOne("Karami.Domain.ArticleComment.ValueObjects.Comment", "Comment", b1 =>
                        {
                            b1.Property<string>("ArticleCommentId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(800)
                                .HasColumnType("nvarchar(800)")
                                .HasColumnName("Comment");

                            b1.HasKey("ArticleCommentId");

                            b1.ToTable("ArticleComments");

                            b1.WithOwner()
                                .HasForeignKey("ArticleCommentId");
                        });

                    b.Navigation("Comment");

                    b.Navigation("CreatedAt");

                    b.Navigation("UpdatedAt");
                });

            modelBuilder.Entity("Karami.Domain.ArticleCommentAnswer.Entities.ArticleCommentAnswer", b =>
                {
                    b.HasOne("Karami.Domain.ArticleComment.Entities.ArticleComment", "Comment")
                        .WithMany("Answers")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Karami.Core.Domain.ValueObjects.CreatedAt", "CreatedAt", b1 =>
                        {
                            b1.Property<string>("ArticleCommentAnswerId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime?>("EnglishDate")
                                .IsRequired()
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedAt_EnglishDate");

                            b1.Property<string>("PersianDate")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CreatedAt_PersianDate");

                            b1.HasKey("ArticleCommentAnswerId");

                            b1.ToTable("ArticleCommentAnswers");

                            b1.WithOwner()
                                .HasForeignKey("ArticleCommentAnswerId");
                        });

                    b.OwnsOne("Karami.Core.Domain.ValueObjects.UpdatedAt", "UpdatedAt", b1 =>
                        {
                            b1.Property<string>("ArticleCommentAnswerId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime?>("EnglishDate")
                                .IsRequired()
                                .HasColumnType("datetime2")
                                .HasColumnName("UpdatedAt_EnglishDate");

                            b1.Property<string>("PersianDate")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("UpdatedAt_PersianDate");

                            b1.HasKey("ArticleCommentAnswerId");

                            b1.ToTable("ArticleCommentAnswers");

                            b1.WithOwner()
                                .HasForeignKey("ArticleCommentAnswerId");
                        });

                    b.OwnsOne("Karami.Domain.ArticleCommentAnswer.ValueObjects.Answer", "Answer", b1 =>
                        {
                            b1.Property<string>("ArticleCommentAnswerId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(800)
                                .HasColumnType("nvarchar(800)")
                                .HasColumnName("Answer");

                            b1.HasKey("ArticleCommentAnswerId");

                            b1.ToTable("ArticleCommentAnswers");

                            b1.WithOwner()
                                .HasForeignKey("ArticleCommentAnswerId");
                        });

                    b.Navigation("Answer");

                    b.Navigation("Comment");

                    b.Navigation("CreatedAt");

                    b.Navigation("UpdatedAt");
                });

            modelBuilder.Entity("Karami.Domain.ArticleComment.Entities.ArticleComment", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
