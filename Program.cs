using System;

class Program
{
    static void Main()
    {
        // Create objects for each major part of the recognizer.
        Validator validator = new Validator();
        Derivation derivation = new Derivation();
        ParseTree parseTree = new ParseTree();

        // Display instructions when the program starts.
        ShowInstructions();

        // MAIN INPUT LOOP
        
        while (true)
        {
            Console.WriteLine();

            Console.Write(
                "Enter string (EXIT = quit, INFO = instructions): "
            );

            string input = Console.ReadLine() ?? "";


            
            // EXIT COMMAND
            
            if (input.Equals(
                "EXIT",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                Console.WriteLine("Program terminated.");
                break;
            }


            
            // INFO COMMAND
            
            if (input.Equals(
                "INFO",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                ShowInstructions();
                continue;
            }


            
            // EMPTY INPUT
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "ERROR: No input was entered."
                );

                continue;
            }


           
            // VALIDATE COMPLETE PROGRAM
            
            bool isValid = validator.ValidateProgram(input);


           
            // INVALID INPUT
           
            if (!isValid)
            {
                Console.WriteLine();

                Console.WriteLine(
                    "ERROR: " +
                    validator.GetProgramError(input)
                );

                Console.WriteLine();

                Console.WriteLine("Examples of valid input:");
                Console.WriteLine(
                    "begin SQR A1-C4 end"
                );
                Console.WriteLine(
                    "begin TRI A1-C6-G3 end"
                );

                Console.WriteLine();

                Console.Write(
                    "Enter Y to try again or EXIT to quit: "
                );

                string choice = Console.ReadLine() ?? "";


                // Exit after an invalid input.
                if (choice.Equals(
                    "EXIT",
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine();
                    Console.WriteLine("Program terminated.");
                    break;
                }


                // Retry after Y.
                if (choice.Equals(
                    "Y",
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }


                // Handle an invalid response.
                Console.WriteLine();

                Console.WriteLine(
                    "Invalid choice. Returning to input prompt."
                );

                continue;
            }


           
            // VALID INPUT
            
            Console.WriteLine();

            Console.WriteLine(
                "SUCCESS: Valid string!"
            );

            Console.WriteLine(
                "The input follows the required grammar."
            );


           
            // RIGHTMOST DERIVATION
            
            derivation.DisplayRightmostDerivation(input);


           
            // PARSE TREE CHOICE
            
            Console.WriteLine();

            Console.Write(
                "Display parse tree? (Y/N): "
            );

            string treeChoice = Console.ReadLine() ?? "";


            if (treeChoice.Equals(
                "Y",
                StringComparison.OrdinalIgnoreCase))
            {
                parseTree.DisplayParseTree(input);
            }
            else if (treeChoice.Equals(
                "N",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Parse tree skipped."
                );
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Invalid choice. Parse tree skipped."
                );
            }


            Console.WriteLine();

            Console.WriteLine(
                "Ready for another input."
            );
        }
    }


   
    // DISPLAY PROGRAM INSTRUCTIONS
    
    static void ShowInstructions()
    {
        Console.WriteLine(
            "=================================================="
        );

        Console.WriteLine(
            "       CMPS 3111 LANGUAGE RECOGNIZER"
        );

        Console.WriteLine(
            "=================================================="
        );

        Console.WriteLine();


        
        // GRAMMAR
        

        Console.WriteLine(
            "<program> → begin <instructions> end"
        );

        Console.WriteLine();

        Console.WriteLine(
            "<instructions> → <instruction>"
        );

        Console.WriteLine(
            "               | <instruction> . <instructions>"
        );

        Console.WriteLine();

        Console.WriteLine(
            "<instruction> → SQR <coord>-<coord>"
        );

        Console.WriteLine(
            "              | TRI <coord>-<coord>-<coord>"
        );

        Console.WriteLine();

        Console.WriteLine(
            "<coord> → <x><y>"
        );

        Console.WriteLine(
            "<x> → A | B | C | D | E | F | G"
        );

        Console.WriteLine(
            "<y> → 1 | 2 | 3 | 4 | 5 | 6"
        );

        Console.WriteLine();


        
        // INPUT INSTRUCTIONS
        
        Console.WriteLine("INPUT INSTRUCTIONS");

        Console.WriteLine(
            "1. Input must begin with: begin"
        );

        Console.WriteLine(
            "2. Input must end with: end"
        );

        Console.WriteLine(
            "3. SQR requires exactly 2 coordinates."
        );

        Console.WriteLine(
            "4. TRI requires exactly 3 coordinates."
        );

        Console.WriteLine(
            "5. Coordinates use letters A-G and numbers 1-6."
        );

        Console.WriteLine(
            "6. Coordinates are separated by a dash (-)."
        );

        Console.WriteLine(
            "7. Multiple instructions use a period (.)."
        );

        Console.WriteLine();


       
        // VALID EXAMPLES
        
        Console.WriteLine("VALID EXAMPLES");

        Console.WriteLine(
            "begin SQR A1-C4 end"
        );

        Console.WriteLine(
            "begin TRI A1-C6-G3 end"
        );

        Console.WriteLine(
            "begin SQR A1-C4. TRI A1-C6-G3 end"
        );

        Console.WriteLine();


       
        // COMMANDS
       
        Console.WriteLine("COMMANDS");

        Console.WriteLine(
            "EXIT = Quit the program"
        );

        Console.WriteLine(
            "INFO = Display instructions again"
        );

        Console.WriteLine();

        Console.WriteLine(
            "=================================================="
        );
    }
}