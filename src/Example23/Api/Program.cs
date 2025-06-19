var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DataContext>();

var app = builder.Build();

// http://localhost:5053/products/
app.MapGet("/products", async (DataContext db, HttpRequest req) =>
{
    await Task.Delay(Random.Shared.Next(1000, 2000));
    var baseUrl = $"{req.Scheme}://{req.Host}";
    var list = db.Products
        .Select(p => $"{baseUrl}/products/{p.Id}")
        .ToList();
    return Results.Ok(list);
});

// http://localhost:5053/products/1
app.MapGet("/products/{id:int}", async (int id, DataContext db, HttpRequest req) =>
{
    await Task.Delay(Random.Shared.Next(1000, 2000));
    var prod = db.Products.FirstOrDefault(p => p.Id == id);
    if (prod is null)
        return Results.NotFound();

    var baseUrl = $"{req.Scheme}://{req.Host}";
    var result = new
    {
        prod.Id,
        prod.Title,
        prod.Price,
        prod.Description,
        Category = new
        {
            descriptionUrl = $"{baseUrl}{prod.Category.DescriptionUrl}"
        },
        prod.CreationAt,
        prod.UpdatedAt
    };
    return Results.Ok(result);
});

// http://localhost:5053/categories/1/description
app.MapGet("/categories/{id:int}/description", async (int id, DataContext db) =>
{
    await Task.Delay(Random.Shared.Next(1000, 2000));
    var cat = db.Categories.FirstOrDefault(c => c.Id == id);
    return cat is not null
        ? Results.Ok(cat)
        : Results.NotFound();
});

app.Run();
