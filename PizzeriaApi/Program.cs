using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pizzéria.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PizzaEhodDB>(options =>
    options.UseInMemoryDatabase("Item"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Pizzéria",
        Description = "Faire les pizzas que vous aimez",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzéria API V1");
});

app.MapGet("/Pizza", async (PizzaEhodDB db) => await db.Pizzas.ToListAsync());

app.MapGet("/pizza/{id}", async (PizzaEhodDB db, int id) =>
    await db.Pizzas.FindAsync(id) is PizzaEhod pizza
        ? Results.Ok(pizza)
        : Results.NotFound());

app.MapPost("/pizza", async (PizzaEhodDB db, PizzaEhod pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

app.MapPut("/pizza/{id}", async (PizzaEhodDB db, PizzaEhod updatepizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();

    pizza.Nom = updatepizza.Nom;
    pizza.Description = updatepizza.Description;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/pizza/{id}", async (PizzaEhodDB db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();

    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();