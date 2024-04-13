namespace WebApplication1;

public class Db
{
    public static List<Animal> Animals = new()
    {
        new Animal { Id = 1, Imie = "Leo", Kategoria = "Pies", Masa = 100, KolorSiersci = "Czarny" },
        new Animal { Id = 2, Imie = "Słoń", Kategoria = "Królik", Masa = 60, KolorSiersci = "Brązowy" },
        new Animal { Id = 3, Imie = "Neo", Kategoria = "Kot", Masa = 30, KolorSiersci = "Biały" }

    };
    
    public static readonly List<Wizyta> Wizyty = new List<Wizyta>
    {
        new Wizyta{DataWizyty = new DateTime(2024, 4, 15),AnimalId = 1,OpisWizyty = "Sprawdzenie zdrowia",CenaWizyty = 100.00m},
        new Wizyta{DataWizyty = new DateTime(2024, 4, 20),AnimalId = 1,OpisWizyty =  "Konsultacja lekarska",CenaWizyty =  150.00m},
        new Wizyta{DataWizyty = new DateTime(2024, 4, 25),AnimalId = 2,OpisWizyty =  "Badanie krwi",CenaWizyty =  200.00m}
    };

    
}