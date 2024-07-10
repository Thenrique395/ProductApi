using Microsoft.EntityFrameworkCore;
using ProductManagement.Infrastructure.DTOs;

namespace ProductManagement.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<ProductEntityDTO> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração de mapeamento das entidades
        modelBuilder.Entity<ProductEntityDTO>(entity =>
        {
            entity.ToTable("Product"); // Define o nome da tabela no banco de dados
            entity.HasKey(e => e.Id); // Define a chave primária
            entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Define que o Id é gerado automaticamente
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // Exemplo de configuração de propriedade
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)"); // Exemplo de configuração de propriedade
        });

        base.OnModelCreating(modelBuilder);
    }
}
