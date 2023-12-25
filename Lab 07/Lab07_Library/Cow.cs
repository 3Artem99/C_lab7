namespace Lab07_Library;

[MyComment("Inheritable class Cow")] //наследуемый класс
public class Cow : Animal
{
    public Cow(string country)
    {
        Country = country;
        HideFromOtherAnimals = true;
        Name = "Cow";
        WhatAnimal = eClassificationAnimal.Herbivores;
    }
}