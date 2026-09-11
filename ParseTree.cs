using System;

public class ParseTree
{
    // ============================================================
    // DISPLAY PARSE TREE
    //
    // Grammar:
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

        // Remove "begin " and " end" from the complete input.
        string instructionText = input.Substring(
            6,
            input.Length - 10
        );

        // Multiple instructions are separated by periods.
        string[] instructions = instructionText.Split('.');

        Console.WriteLine("<program>");
        Console.WriteLine("|");
        Console.WriteLine("+-- begin");
        Console.WriteLine("|");
        Console.WriteLine("+-- <instructions>");

        // Display each instruction as part of <instructions>.
        for (int i = 0; i < instructions.Length; i++)
        {
            string instruction = instructions[i].Trim();

            bool lastInstruction = i == instructions.Length - 1;

            DisplayInstructionTree(
                instruction,
                lastInstruction
            );
        }

        Console.WriteLine("|");
        Console.WriteLine("+-- end");

        Console.WriteLine();
        Console.WriteLine("==================================================");
    }


    // ============================================================
    // DISPLAY INSTRUCTION TREE
    // ============================================================
    private void DisplayInstructionTree(
        string instruction,
        bool lastInstruction)
    {
        string[] parts = instruction.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries
        );

        string command = parts[0];
        string coordinatesText = parts[1];

        string[] coordinates = coordinatesText.Split('-');

        Console.WriteLine("|");
        Console.WriteLine("|   +-- <instruction>");

        if (command == "SQR")
        {
            DisplaySqrTree(coordinates);
        }
        else if (command == "TRI")
        {
            DisplayTriTree(coordinates);
        }

        // If another instruction follows, show the period
        // required by the grammar.
        if (!lastInstruction)
        {
            Console.WriteLine("|");
            Console.WriteLine("|   +-- .");
        }
    }


    // ============================================================
    // DISPLAY SQR PARSE TREE
    //
    // SQR <coord>-<coord>
    // ============================================================
    private void DisplaySqrTree(string[] coordinates)
    {
        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- SQR");
        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- <coord>");
        DisplayCoordinateTree(coordinates[0], "|       |    ");
        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- -");
        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- <coord>");
        DisplayCoordinateTree(coordinates[1], "|            ");
    }


    // ============================================================
    // DISPLAY TRI PARSE TREE
    //
    // TRI <coord>-<coord>-<coord>
    // ============================================================
    private void DisplayTriTree(string[] coordinates)
    {
        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- TRI");
        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- <coord>");
        DisplayCoordinateTree(coordinates[0], "|       |    ");

        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- -");
        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- <coord>");
        DisplayCoordinateTree(coordinates[1], "|       |    ");

        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- -");
        Console.WriteLine("|       |");
        Console.WriteLine("|       +-- <coord>");
        DisplayCoordinateTree(coordinates[2], "|            ");
    }


    // ============================================================
    // DISPLAY COORDINATE TREE
    //
    // <coord> → <x><y>
    // ============================================================
    private void DisplayCoordinateTree(
        string coordinate,
        string prefix)
    {
        char x = coordinate[0];
        char y = coordinate[1];

        Console.WriteLine(prefix + "|");
        Console.WriteLine(prefix + "+-- <x>");
        Console.WriteLine(prefix + "|    ");
        Console.WriteLine(prefix + "+-- " + x);
        Console.WriteLine(prefix);
        Console.WriteLine(prefix + "+-- <y>");
        Console.WriteLine(prefix + "|    ");
        Console.WriteLine(prefix + "+-- " + y);
    }
}