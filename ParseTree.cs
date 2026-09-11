using System;

public class ParseTree
{
    // ============================================================
    // DISPLAY PARSE TREE
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
    public void DisplayParseTree(string input)
    {
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("                    PARSE TREE");
        Console.WriteLine("==================================================");
        Console.WriteLine();

        // Extract the instruction section.
        string instructionText = input.Substring(
            6,
            input.Length - 10
        );

        // Separate instructions using the period.
        string[] instructions = instructionText.Split('.');

        Console.WriteLine("<program>");
        Console.WriteLine("├── begin");

        // <instructions> is a child of <program>.
        Console.WriteLine("├── <instructions>");

        DisplayInstructionsTree(
            instructions,
            0,
            "│   "
        );

        // end is the final child of <program>.
        Console.WriteLine("└── end");

        Console.WriteLine();
        Console.WriteLine("==================================================");
    }


    // ============================================================
    // DISPLAY <instructions> TREE
    //
    // Grammar:
    //
    // <instructions> → <instruction>
    //                 | <instruction> . <instructions>
    //
    // This method recursively displays the structure of
    // multiple instructions.
    // ============================================================
    private void DisplayInstructionsTree(
        string[] instructions,
        int index,
        string prefix)
    {
        string instruction = instructions[index].Trim();

        bool lastInstruction =
            index == instructions.Length - 1;

        // --------------------------------------------------------
        // <instruction>
        // --------------------------------------------------------
        if (lastInstruction)
        {
            Console.WriteLine(
                prefix + "└── <instruction>"
            );

            DisplayInstructionTree(
                instruction,
                prefix + "    "
            );
        }
        else
        {
            Console.WriteLine(
                prefix + "├── <instruction>"
            );

            DisplayInstructionTree(
                instruction,
                prefix + "│   "
            );

            // ----------------------------------------------------
            // Period between instructions.
            // ----------------------------------------------------
            Console.WriteLine(
                prefix + "├── ."
            );

            // ----------------------------------------------------
            // Recursive <instructions>.
            // ----------------------------------------------------
            Console.WriteLine(
                prefix + "└── <instructions>"
            );

            DisplayInstructionsTree(
                instructions,
                index + 1,
                prefix + "    "
            );
        }
    }


    // ============================================================
    // DISPLAY <instruction> TREE
    // ============================================================
    private void DisplayInstructionTree(
        string instruction,
        string prefix)
    {
        string[] parts = instruction.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries
        );

        string command = parts[0];
        string coordinatesText = parts[1];

        string[] coordinates = coordinatesText.Split('-');


        // ========================================================
        // SQR
        // ========================================================
        if (command == "SQR")
        {
            Console.WriteLine(
                prefix + "├── SQR"
            );

            // First coordinate.
            Console.WriteLine(
                prefix + "├── <coord>"
            );

            DisplayCoordinateTree(
                coordinates[0],
                prefix + "│   "
            );

            // Dash.
            Console.WriteLine(
                prefix + "├── -"
            );

            // Second coordinate.
            Console.WriteLine(
                prefix + "└── <coord>"
            );

            DisplayCoordinateTree(
                coordinates[1],
                prefix + "    "
            );
        }


        // ========================================================
        // TRI
        // ========================================================
        else if (command == "TRI")
        {
            Console.WriteLine(
                prefix + "├── TRI"
            );

            // First coordinate.
            Console.WriteLine(
                prefix + "├── <coord>"
            );

            DisplayCoordinateTree(
                coordinates[0],
                prefix + "│   "
            );

            // First dash.
            Console.WriteLine(
                prefix + "├── -"
            );

            // Second coordinate.
            Console.WriteLine(
                prefix + "├── <coord>"
            );

            DisplayCoordinateTree(
                coordinates[1],
                prefix + "│   "
            );

            // Second dash.
            Console.WriteLine(
                prefix + "├── -"
            );

            // Third coordinate.
            Console.WriteLine(
                prefix + "└── <coord>"
            );

            DisplayCoordinateTree(
                coordinates[2],
                prefix + "    "
            );
        }
    }


    // ============================================================
    // DISPLAY <coord> TREE
    //
    // Grammar:
    //
    // <coord> → <x><y>
    // ============================================================
    private void DisplayCoordinateTree(
        string coordinate,
        string prefix)
    {
        char x = coordinate[0];
        char y = coordinate[1];

        Console.WriteLine(
            prefix + "├── <x>"
        );

        Console.WriteLine(
            prefix + "│   └── " + x
        );

        Console.WriteLine(
            prefix + "└── <y>"
        );

        Console.WriteLine(
            prefix + "    └── " + y
        );
    }
}