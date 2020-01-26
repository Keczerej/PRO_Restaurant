using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Models
{
    public partial class s16271Context : DbContext
    {
        public s16271Context()
        {
        }

        public s16271Context(DbContextOptions<s16271Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<PizzaDefinition> PizzaDefinition { get; set; }
        public virtual DbSet<PizzaIntegrients> PizzaIntegrients { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<PromotionType> PromotionType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s16271;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("Ingredient_pk");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("Order_pk");

                entity.Property(e => e.Uid)
                    .HasColumnName("uid")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderStatusName)
                    .IsRequired()
                    .HasColumnName("OrderStatus_name")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PizzaDefinition)
                    .IsRequired()
                    .HasColumnName("pizzaDefinition")
                    .HasColumnType("text");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.OrderStatusNameNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderStatusName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_OrderStatus");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("OrderStatus_pk");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<PizzaDefinition>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PizzaDefinition_pk");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<PizzaIntegrients>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IngredientName)
                    .IsRequired()
                    .HasColumnName("Ingredient_name")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.PizzaName)
                    .IsRequired()
                    .HasColumnName("Pizza_name")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.IngredientNameNavigation)
                    .WithMany(p => p.PizzaIntegrients)
                    .HasForeignKey(d => d.IngredientName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PizzaIntegrients_Ingredient");

                entity.HasOne(d => d.PizzaNameNavigation)
                    .WithMany(p => p.PizzaIntegrients)
                    .HasForeignKey(d => d.PizzaName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PizzaIntegrients_Pizza");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("name");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MinPrice).HasColumnName("minPrice");

                entity.Property(e => e.PromotionTypeName)
                    .IsRequired()
                    .HasColumnName("PromotionType_name")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.PromotionTypeNameNavigation)
                    .WithMany(p => p.Promotion)
                    .HasForeignKey(d => d.PromotionTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Promotion_PromotionType");
            });

            modelBuilder.Entity<PromotionType>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PromotionType_pk");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });
        }
    }
}
