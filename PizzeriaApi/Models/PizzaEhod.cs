using Microsoft.EntityFrameworkCore;

namespace Pizzéria.Models
{
    public class PizzaEhod
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Description { get; set; }
    }

    public class PizzaEhodDB : DbContext
    {
        public PizzaEhodDB(DbContextOptions<PizzaEhodDB> options)
            : base(options) { }

        public DbSet<PizzaEhod> Pizzas { get; set; }
    }
}