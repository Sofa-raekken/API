using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models
{
    public partial class kbh_zooContext : DbContext
    {
        public kbh_zooContext()
        {
        }

        public kbh_zooContext(DbContextOptions<kbh_zooContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalHasDiet> AnimalHasDiets { get; set; }
        public virtual DbSet<AnimalHasEvent> AnimalHasEvents { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Diet> Diets { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Species> Species { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=sofadb.mysql.database.azure.com;database=kbh_zoo;uid=sde;pwd=ET2Kc},%)8t:5>jh;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.IdAnimal)
                    .HasName("PRIMARY");

                entity.ToTable("animal");

                entity.HasIndex(e => e.SpeciesIdSpecies, "fk_Animal_Species1_idx");

                entity.Property(e => e.IdAnimal)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_animal");

                entity.Property(e => e.BirthWeight)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("birth_weight");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("description");

                entity.Property(e => e.Heigth)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("heigth");

                entity.Property(e => e.LatinName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("latin_name");

                entity.Property(e => e.LifeExpectancy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("life_expectancy");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Pregnancy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("pregnancy");

                entity.Property(e => e.Qr)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("QR");

                entity.Property(e => e.SpeciesIdSpecies)
                    .HasColumnType("int(11)")
                    .HasColumnName("Species_id_species");

                entity.Property(e => e.Weight)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("weight");

                entity.HasOne(d => d.SpeciesIdSpeciesNavigation)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.SpeciesIdSpecies)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Animal_Species1");
            });

            modelBuilder.Entity<AnimalHasDiet>(entity =>
            {
                entity.HasKey(e => new { e.AnimalIdAnimal, e.DietIdDiet })
                    .HasName("PRIMARY");

                entity.ToTable("animal_has_diet");

                entity.HasIndex(e => e.AnimalIdAnimal, "fk_Animal_has_Diet_Animal1_idx");

                entity.HasIndex(e => e.DietIdDiet, "fk_Animal_has_Diet_Diet1_idx");

                entity.Property(e => e.AnimalIdAnimal)
                    .HasColumnType("int(11)")
                    .HasColumnName("Animal_id_animal");

                entity.Property(e => e.DietIdDiet)
                    .HasColumnType("int(11)")
                    .HasColumnName("Diet_id_diet");

                entity.HasOne(d => d.AnimalIdAnimalNavigation)
                    .WithMany(p => p.AnimalHasDiets)
                    .HasForeignKey(d => d.AnimalIdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Animal_has_Diet_Animal1");

                entity.HasOne(d => d.DietIdDietNavigation)
                    .WithMany(p => p.AnimalHasDiets)
                    .HasForeignKey(d => d.DietIdDiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Animal_has_Diet_Diet1");
            });

            modelBuilder.Entity<AnimalHasEvent>(entity =>
            {
                entity.HasKey(e => new { e.AnimalIdAnimal, e.EventIdEvent })
                    .HasName("PRIMARY");

                entity.ToTable("animal_has_event");

                entity.HasIndex(e => e.AnimalIdAnimal, "fk_Animal_has_Event_Animal1_idx");

                entity.HasIndex(e => e.EventIdEvent, "fk_Animal_has_Event_Event1_idx");

                entity.Property(e => e.AnimalIdAnimal)
                    .HasColumnType("int(11)")
                    .HasColumnName("Animal_id_animal");

                entity.Property(e => e.EventIdEvent)
                    .HasColumnType("int(11)")
                    .HasColumnName("Event_id_event");

                entity.HasOne(d => d.AnimalIdAnimalNavigation)
                    .WithMany(p => p.AnimalHasEvents)
                    .HasForeignKey(d => d.AnimalIdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Animal_has_Event_Animal1");

                entity.HasOne(d => d.EventIdEventNavigation)
                    .WithMany(p => p.AnimalHasEvents)
                    .HasForeignKey(d => d.EventIdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Animal_has_Event_Event1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.Property(e => e.IdCategory)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_category");

                entity.Property(e => e.Category1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("category");
            });

            modelBuilder.Entity<Diet>(entity =>
            {
                entity.HasKey(e => e.IdDiet)
                    .HasName("PRIMARY");

                entity.ToTable("diet");

                entity.Property(e => e.IdDiet)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_diet");

                entity.Property(e => e.Diet1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("diet");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.IdEvent)
                    .HasName("PRIMARY");

                entity.ToTable("event");

                entity.Property(e => e.IdEvent)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_event");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.IdFeedback)
                    .HasName("PRIMARY");

                entity.ToTable("feedback");

                entity.HasIndex(e => e.CategoryIdCategory, "fk_Feedback_Category1_idx");

                entity.Property(e => e.IdFeedback)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_feedback");

                entity.Property(e => e.CategoryIdCategory)
                    .HasColumnType("int(11)")
                    .HasColumnName("Category_id_category");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("comment");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Rate)
                    .HasColumnType("int(11)")
                    .HasColumnName("rate");

                entity.HasOne(d => d.CategoryIdCategoryNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CategoryIdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Feedback_Category1");
            });

            modelBuilder.Entity<Information>(entity =>
            {
                entity.HasKey(e => e.IdInformation)
                    .HasName("PRIMARY");

                entity.ToTable("information");

                entity.Property(e => e.IdInformation)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_information");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.HasKey(e => e.IdSpecies)
                    .HasName("PRIMARY");

                entity.ToTable("species");

                entity.Property(e => e.IdSpecies)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_species");

                entity.Property(e => e.SpeciesName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("species_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
