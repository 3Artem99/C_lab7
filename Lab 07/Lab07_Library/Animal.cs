namespace Lab07_Library;

[MyComment("Base class Animal")]  //наш комментарий (метаданные)
public class Animal
{
    public string? Country { get; set; } //знак вопроса допускает null-строку
    public bool HideFromOtherAnimals { get; set; }
    public string? Name { get; set; }
    public eClassificationAnimal WhatAnimal { get; set; } //свойство, которое указывает на классификацию животного

    protected Animal()
    {
    }

    public Animal(string? country, bool hideFromOtherAnimals, string? name, eClassificationAnimal whatAnimal)
    {
        Country = country;
        HideFromOtherAnimals = hideFromOtherAnimals;
        Name = name;
        WhatAnimal = whatAnimal;
    }

    public eClassificationAnimal GetClassificationAnimal() //возвращаем классификацию животного
    {
        return WhatAnimal;
    }

    public eFavoriteFood GetFavoriteFood()
    {
        return WhatAnimal switch
        {
            eClassificationAnimal.Carnivores => eFavoriteFood.Meat,
            eClassificationAnimal.Herbivores => eFavoriteFood.Plants,
            _ => eFavoriteFood.Everything
        };
    }

    public void SayHello()
    {
        Console.WriteLine($"Hello, i'm {Name}!");
    }
}