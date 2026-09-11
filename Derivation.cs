using System;

public class Derivation
{
    // ============================================================
    // DISPLAY RIGHTMOST DERIVATION
    //
    // A rightmost derivation always expands the rightmost
    // nonterminal in the current sentential form.
    //
    // Grammar:
    //
    // <program> → begin <instructions> end
    //
    // <instructions> → <instruction>
    //                 | <instruction> . <instructions>
    //
    // <instruction> → SQR <coord>-<coord>
    //                | TRI <coord>-<coord>-<coord>
    //
    // <coord> → <x><y>
    //
    // <x> → A | B | C | D | E | F | G
    //
    // <y> → 1 | 2 | 3 | 4 | 5 | 6
    // ============================================================
    public void DisplayRightmostDerivation(string input)
    {
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("             RIGHTMOST DERIVATION");
        Console.WriteLine("==================================================");
        Console.WriteLine();

        // Extract the instruction section from the valid input.
        string instructionText = input.Substring(
            6,
            input.Length - 10
        );

        string[] instructions = instructionText.Split('.');

        // --------------------------------------------------------
        // Start symbol.
        // --------------------------------------------------------
        string current = "<program>";

        Console.WriteLine(current);

        // --------------------------------------------------------
        // <program> → begin <instructions> end
        // --------------------------------------------------------
        current = "begin <instructions> end";

        Console.WriteLine("=> " + current);


        // --------------------------------------------------------
        // Expand <instructions>.
        //
        // For multiple instructions:
        //
        // <instructions>
        //     → <instruction> . <instructions>
        //
        // The rightmost <instructions> is expanded first.
        // --------------------------------------------------------
        if (instructions.Length == 1)
        {
            current = ReplaceRightmost(
                current,
                "<instructions>",
                "<instruction>"
            );

            Console.WriteLine("=> " + current);
        }
        else
        {
            // Continue applying the recursive rule until there
            // is one <instruction> placeholder for every input
            // instruction.
            for (int i = 0; i < instructions.Length - 1; i++)
            {
                current = ReplaceRightmost(
                    current,
                    "<instructions>",
                    "<instruction> . <instructions>"
                );

                Console.WriteLine("=> " + current);
            }

            // The final <instructions> becomes <instruction>.
            current = ReplaceRightmost(
                current,
                "<instructions>",
                "<instruction>"
            );

            Console.WriteLine("=> " + current);
        }


        // --------------------------------------------------------
        // Expand instructions from RIGHT TO LEFT.
        // --------------------------------------------------------
        for (int i = instructions.Length - 1; i >= 0; i--)
        {
            string instruction = instructions[i].Trim();

            string command = instruction.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries
            )[0];

            string coordinatesText = instruction.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries
            )[1];

            string[] coordinates = coordinatesText.Split('-');

            // ----------------------------------------------------
            // Replace the rightmost <instruction>.
            // ----------------------------------------------------
            string instructionProduction;

            if (command == "SQR")
            {
                instructionProduction =
                    "SQR <coord>-<coord>";
            }
            else
            {
                instructionProduction =
                    "TRI <coord>-<coord>-<coord>";
            }

            current = ReplaceRightmost(
                current,
                "<instruction>",
                instructionProduction
            );

            Console.WriteLine("=> " + current);


            // ----------------------------------------------------
            // Expand the coordinates from RIGHT TO LEFT.
            // ----------------------------------------------------
            for (int coordinateIndex =
                 coordinates.Length - 1;
                 coordinateIndex >= 0;
                 coordinateIndex--)
            {
                string coordinate = coordinates[coordinateIndex];

                // <coord> → <x><y>
                current = ReplaceRightmost(
                    current,
                    "<coord>",
                    "<x><y>"
                );

                Console.WriteLine("=> " + current);

                // <y> → actual Y value
                current = ReplaceRightmost(
                    current,
                    "<y>",
                    coordinate[1].ToString()
                );

                Console.WriteLine("=> " + current);

                // <x> → actual X value
                current = ReplaceRightmost(
                    current,
                    "<x>",
                    coordinate[0].ToString()
                );

                Console.WriteLine("=> " + current);
            }
        }


        Console.WriteLine();
        Console.WriteLine("Final generated sentence:");
        Console.WriteLine(current);

        Console.WriteLine();
        Console.WriteLine("==================================================");
    }


    // ============================================================
    // REPLACE RIGHTMOST OCCURRENCE
    //
    // This helper is what allows the program to perform a true
    // rightmost derivation.
    // ============================================================
    private string ReplaceRightmost(
        string text,
        string oldValue,
        string newValue)
    {
        int position = text.LastIndexOf(oldValue);

        if (position == -1)
        {
            return text;
        }

        return text.Substring(0, position) +
               newValue +
               text.Substring(
                   position + oldValue.Length
               );
    }
}