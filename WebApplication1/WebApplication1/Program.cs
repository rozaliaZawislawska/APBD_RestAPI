using Microsoft.AspNetCore.Mvc;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET api/animals
app.MapGet("/api/animals", () =>
{
    var animal = Db.Animals;
    return Results.Ok(animal);
});

// GET api/animals/10
app.MapGet("/api/animals/{id:int}", (int id) =>
{
    var animal = Db.Animals.FirstOrDefault(animal => animal.Id == id);
    return animal is null ? Results.NotFound($"Animal with id {id} not Found"): Results.Ok(animal);
});

//POST api/animals
app.MapPost("api/animals", ([FromBody] Animal data) =>
{
    var animal = Db.Animals.Exists(s => s.Id == data.Id);
    if (animal) return Results.Conflict($"Animal with id {data.Id} already exists");
    Db.Animals.Add(data);
    return Results.Created($"/api/animals/{data.Id}", data);
});

// DELETE api/animals/10
app.MapDelete("/api/animals/{id:int}", ([FromQuery] int id) =>
{
    var animalExists = Db.Animals.Exists(s => s.Id == id);
    if (animalExists)
    {
        Db.Animals.RemoveAt(Db.Animals.FindIndex(animals => animals.Id == id));
        return Results.Ok(animalExists);
    }
    else return Results.NotFound($"Animal with id {id} not Found");
});
    
// PUT api/animalId=10
app.MapPut("/api/animals/{id:int}", ([FromBody] Animal data) =>
{
    var animalExists = Db.Animals.Exists(s => s.Id == data.Id);
    if (animalExists)
    {
        Db.Animals.RemoveAt(Db.Animals.FindIndex(animals => animals.Id == data.Id));
        Db.Animals.Add(data);
        return Results.Created($"/api/animal/{data.Id}", data);
    }
    else return Results.NotFound($"Animal with id {data.Id} not Found");
});

// GET /api/appointments?animalId=10
app.MapGet("/api/appointments", ([FromQuery] int animalId) =>
{
    List<Wizyta> listaWizyt = new List<Wizyta>();
    foreach (var wizyta in Db.Wizyty)
    {
        if(wizyta.AnimalId == animalId)
            listaWizyt.Add(wizyta);
    }
    if(listaWizyt.Count==0) return Results.NotFound($"Animal with id {animalId} not Found in appointments");
    return Results.Ok(listaWizyt);
});

// POST /api/appointments
app.MapPost("api/appointments", ([FromBody] Wizyta data) =>
{
    Db.Wizyty.Add(data);
    return Results.Ok(data);
});


app.Run();
