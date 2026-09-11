using System;

class Program
{
    static void Main()
    {
        Validator validator = new Validator();

        Console.WriteLine("Coordinate Validator Test");
        Console.WriteLine();

        Console.WriteLine("A1: " + validator.ValidateCoordinate("A1"));
        Console.WriteLine("C4: " + validator.ValidateCoordinate("C4"));
        Console.WriteLine("G6: " + validator.ValidateCoordinate("G6"));
        Console.WriteLine("H2: " + validator.ValidateCoordinate("H2"));
        Console.WriteLine("A8: " + validator.ValidateCoordinate("A8"));
        Console.WriteLine("A10: " + validator.ValidateCoordinate("A10"));
        Console.WriteLine("1A: " + validator.ValidateCoordinate("1A"));
    }
}