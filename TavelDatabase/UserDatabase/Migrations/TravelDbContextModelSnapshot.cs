﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelDatabase.DataAccess.SqLite;

#nullable disable

namespace TravelDatabase.Migrations
{
    [DbContext(typeof(TravelDbContext))]
    partial class TravelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("TravelDatabase.Entities.Capital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CapitalName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Continent")
                        .HasColumnType("TEXT");

                    b.Property<int>("Latitude")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Longitude")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Capital");
                });

            modelBuilder.Entity("TravelDatabase.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArrivalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartureId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalId");

                    b.HasIndex("DepartureId");

                    b.HasIndex("UserId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("TravelDatabase.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Admin")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TravelDatabase.Entities.Trip", b =>
                {
                    b.HasOne("TravelDatabase.Entities.Capital", "Arrival")
                        .WithMany()
                        .HasForeignKey("ArrivalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelDatabase.Entities.Capital", "Departure")
                        .WithMany()
                        .HasForeignKey("DepartureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelDatabase.Entities.User", "User")
                        .WithMany("Trip")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Arrival");

                    b.Navigation("Departure");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TravelDatabase.Entities.User", b =>
                {
                    b.HasOne("TravelDatabase.Entities.Capital", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("TravelDatabase.Entities.User", b =>
                {
                    b.Navigation("Trip");
                });
#pragma warning restore 612, 618
        }
    }
}
