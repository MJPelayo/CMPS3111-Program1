using System;

class Program
{
    static void Main()
    {
        ShowInstructions();

        Console.WriteLine();
        Console.WriteLine("CMPS 3111 Language Recognizer");
        Console.WriteLine("Program starting...");
    }

    static void ShowInstructions()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("       CMPS 3111 LANGUAGE RECOGNIZER");
        Console.WriteLine("==================================================");
        Console.WriteLine();

        Console.WriteLine("INPUT INSTRUCTIONS");
        Console.WriteLine("1. Input must begin with: begin");
        Console.WriteLine("2. Input must end with: end");
        Console.WriteLine("3. SQR requires exactly 2 coordinates.");
        Console.WriteLine("4. TRI requires exactly 3 coordinates.");
        Console.WriteLine("5. Coordinates use letters A-G and numbers 1-6.");
        Console.WriteLine("6. Coordinates are separated by a dash (-).");
        Console.WriteLine("7. Multiple instructions are separated by a period (.).");
        Console.WriteLine();

        Console.WriteLine("VALID EXAMPLES");
        Console.WriteLine("begin SQR A1-C4 end");
        Console.WriteLine("begin TRI A1-C6-G3 end");
        Console.WriteLine("begin SQR A1-C4. TRI A1-C6-G3 end");
        Console.WriteLine();

        Console.WriteLine("COMMANDS");
        Console.WriteLine("EXIT = Quit the program");
        Console.WriteLine("INFO = Display these instructions again");
        Console.WriteLine();

        Console.WriteLine("==================================================");
    }
}