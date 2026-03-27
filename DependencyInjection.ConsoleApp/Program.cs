
// Dependency Injection (DI) is a design pattern that allows a class to receive its dependencies from an external source rather than creating them itself. This promotes loose coupling and makes the code more modular and testable.
var cekic = new Cekic();
var civi = new Civic();

var builder = new Builder(cekic, civi);
builder.BuilHouse();

class Cekic // dependecy
{
    public void Use()
    {
        Console.WriteLine("Cekic kullanildi");
    }
}

class Civic // dependecy
{
    public void Use()
    {
        Console.WriteLine("Civic kullanildi");
    }
}

class Builder
{
    Cekic _cekic;
    Civic _civi;

    public Builder(Cekic cekic, Civic civi) // constructor injection
    {
        _cekic = cekic;
        _civi = civi;
    }

    public void BuilHouse()
    {
        _cekic.Use();

        _civi.Use();

        Console.WriteLine("Ev insa edildi");
    }
}