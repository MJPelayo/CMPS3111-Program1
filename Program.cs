using System;

class Program
{
    static void Main()
    {
        Validator validator = new Validator();

        Console.WriteLine("Instruction Validator Test");
        Console.WriteLine();

        Console.WriteLine("SQR A1-C4: "
            + validator.ValidateInstruction("SQR A1-C4"));

        Console.WriteLine("TRI A1-C6-G3: "
            + validator.ValidateInstruction("TRI A1-C6-G3"));

        Console.WriteLine("SQR A1-C4-G3: "
            + validator.ValidateInstruction("SQR A1-C4-G3"));

        Console.WriteLine("TRI A1-C4: "
            + validator.ValidateInstruction("TRI A1-C4"));

        Console.WriteLine("CIR A1-C4: "
            + validator.ValidateInstruction("CIR A1-C4"));

        Console.WriteLine("SQR A1+C4: "
            + validator.ValidateInstruction("SQR A1+C4"));

        Console.WriteLine("SQR H1-C4: "
            + validator.ValidateInstruction("SQR H1-C4"));

        Console.WriteLine("TRI A1-C6-G8: "
            + validator.ValidateInstruction("TRI A1-C6-G8"));
    }
}