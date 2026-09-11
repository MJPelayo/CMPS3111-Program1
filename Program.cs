using System;

class Program
{
    static void Main()
    {
        // Display the instructions when the program starts.
        ShowInstructions();

        // Keep the program running until the user enters EXIT.
        while (true)
        {
            Console.WriteLine();
            Console.Write("Enter string (EXIT = quit, INFO = instructions): ");

            string input = Console.ReadLine() ?? "";

            // Check whether the user wants to exit.
            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                Console.WriteLine("Program terminated.");
                break;
            }

            // Check whether the user wants to see the instructions again.
            if (input.Equals("INFO", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                ShowInstructions();
                continue;
            }

            // Temporary message.
            // Grammar validation will be added in a later stage.
            Console.WriteLine();
            Console.WriteLine("Input received: " + input);
            Console.WriteLine("Validation will be added next.");
        }
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