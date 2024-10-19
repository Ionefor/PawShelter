﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PawShelter.Infrastructure;

#nullable disable

namespace PawShelter.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PawShelter.Domain.PetsManagement.Aggregate.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PawShelter.Domain.PetsManagement.Aggregate.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "PawShelter.Domain.PetsManagement.Aggregate.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Experience", "PawShelter.Domain.PetsManagement.Aggregate.Volunteer.Experience#Experience", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasMaxLength(100)
                                .HasColumnType("integer")
                                .HasColumnName("experience");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PawShelter.Domain.PetsManagement.Aggregate.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("phoneNumber");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("Volunteers", (string)null);
                });

            modelBuilder.Entity("PawShelter.Domain.PetsManagement.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsCastrated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_castrated");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("publication_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PawShelter.Domain.PetsManagement.Entities.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("country");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("houseNumber");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Birthday", "PawShelter.Domain.PetsManagement.Entities.Pet.Birthday#Birthday", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly>("Value")
                                .HasColumnType("date")
                                .HasColumnName("birthday");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PawShelter.Domain.PetsManagement.Entities.Pet.Color#Color", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PawShelter.Domain.PetsManagement.Entities.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("HealthInfo", "PawShelter.Domain.PetsManagement.Entities.Pet.HealthInfo#HealthInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("health_Info");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PawShelter.Domain.PetsManagement.Entities.Pet.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PetCharacteristics", "PawShelter.Domain.PetsManagement.Entities.Pet.PetCharacteristics#PetCharacteristics", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<double>("Height")
                                .HasMaxLength(100)
                                .HasColumnType("double precision")
                                .HasColumnName("height");

                            b1.Property<double>("Width")
                                .HasMaxLength(100)
                                .HasColumnType("double precision")
                                .HasColumnName("width");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PawShelter.Domain.PetsManagement.Entities.Pet.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("phoneNumber");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Position", "PawShelter.Domain.PetsManagement.Entities.Pet.Position#Position", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("position");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesBreedsId", "PawShelter.Domain.PetsManagement.Entities.Pet.SpeciesBreedsId#SpeciesBreedsId", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_breeds_id_species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("Pets", (string)null);
                });

            modelBuilder.Entity("PawShelter.Domain.SpeciesManagement.Aggregate.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("species");

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("Species", (string)null);
                });

            modelBuilder.Entity("PawShelter.Domain.SpeciesManagement.Entities.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("breed");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("Breeds", (string)null);
                });

            modelBuilder.Entity("PawShelter.Domain.PetsManagement.Aggregate.Volunteer", b =>
                {
                    b.OwnsOne("PawShelter.Domain.PetsManagement.ValueObjects.Shared.Requisites", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("Volunteers");

                            b1.ToJson("volunteer_requisites");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PawShelter.Domain.PetsManagement.ValueObjects.Shared.Requisite", "Values", b2 =>
                                {
                                    b2.Property<Guid>("RequisitesVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)")
                                        .HasColumnName("requisite_description");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)")
                                        .HasColumnName("requisite_name");

                                    b2.HasKey("RequisitesVolunteerId", "Id");

                                    b2.ToTable("Volunteers");

                                    b2.ToJson("volunteer_requisites");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisitesVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_requisites_volunteer_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.OwnsOne("PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("Volunteers");

                            b1.ToJson("full_name");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");
                        });

                    b.OwnsOne("PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer.SocialNetworks", "SocialNetworks", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("Volunteers");

                            b1.ToJson("volunteer_social_networks");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer.SocialNetwork", "Values", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworksVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)")
                                        .HasColumnName("socialNetwork_link");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)")
                                        .HasColumnName("socialNetwork_name");

                                    b2.HasKey("SocialNetworksVolunteerId", "Id");

                                    b2.ToTable("Volunteers");

                                    b2.ToJson("volunteer_social_networks");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworksVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_networks_volunteer_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.Navigation("FullName")
                        .IsRequired();

                    b.Navigation("Requisites")
                        .IsRequired();

                    b.Navigation("SocialNetworks")
                        .IsRequired();
                });

            modelBuilder.Entity("PawShelter.Domain.PetsManagement.Entities.Pet", b =>
                {
                    b.HasOne("PawShelter.Domain.PetsManagement.Aggregate.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("PawShelter.Domain.PetsManagement.ValueObjects.Shared.Requisites", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId")
                                .HasName("pk_pets");

                            b1.ToTable("Pets");

                            b1.ToJson("pet_requisites");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_pet_id");

                            b1.OwnsMany("PawShelter.Domain.PetsManagement.ValueObjects.Shared.Requisite", "Values", b2 =>
                                {
                                    b2.Property<Guid>("RequisitesPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("RequisitesPetId", "Id");

                                    b2.ToTable("Pets");

                                    b2.ToJson("pet_requisites");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisitesPetId")
                                        .HasConstraintName("fk_pets_pets_requisites_pet_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.OwnsOne("PawShelter.Domain.Shared.ValueObjectList<PawShelter.Domain.PetsManagement.ValueObjects.ForPet.PetPhoto>", "Photos", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId");

                            b1.ToTable("Pets");

                            b1.ToJson("pet_photos");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("PawShelter.Domain.PetsManagement.ValueObjects.ForPet.PetPhoto", "Values", b2 =>
                                {
                                    b2.Property<Guid>("ValueObjectListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<bool>("IsMain")
                                        .HasColumnType("boolean");

                                    b2.Property<string>("Path")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("ValueObjectListPetId", "Id");

                                    b2.ToTable("Pets");

                                    b2.ToJson("pet_photos");

                                    b2.WithOwner()
                                        .HasForeignKey("ValueObjectListPetId")
                                        .HasConstraintName("fk_pets_pets_value_object_list_pet_id");
                                });

                            b1.Navigation("Values");
                        });

                    b.Navigation("Photos");

                    b.Navigation("Requisites")
                        .IsRequired();
                });

            modelBuilder.Entity("PawShelter.Domain.SpeciesManagement.Entities.Breed", b =>
                {
                    b.HasOne("PawShelter.Domain.SpeciesManagement.Aggregate.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_breeds_species_species_id");
                });

            modelBuilder.Entity("PawShelter.Domain.PetsManagement.Aggregate.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PawShelter.Domain.SpeciesManagement.Aggregate.Species", b =>
                {
                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}
