using System;
using System.Collections.Generic;

public class Derivation
{
    // ============================================================
    // DISPLAY RIGHTMOST DERIVATION
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
    public void DisplayRightmostDerivation(string input)
    {
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("             RIGHTMOST DERIVATION");
        Console.WriteLine("==================================================");
        Console.WriteLine();

        // Start with the start symbol of the grammar.
        string current = "<program>";

        Console.WriteLine(current);

        // --------------------------------------------------------
        // STEP 1
        // <program> → begin <instructions> end
        // --------------------------------------------------------
        current = "begin <instructions> end";
        Console.WriteLine("=> " + current);

        // --------------------------------------------------------
        // Get the instructions from the original input.
        // --------------------------------------------------------
        string instructionText = input.Substring(
            6,
            input.Length - 10
        );

        string[] instructions = instructionText.Split('.');

        // --------------------------------------------------------
        // If there is only one instruction:
        //
        // <instructions> → <instruction>
        // --------------------------------------------------------
        if (instructions.Length == 1)
        {
            current = "begin <instruction> end";
            Console.WriteLine("=> " + current);

            DisplayInstructionDerivation(
                instructions[0].Trim()
            );
        }
        else
        {
            // ----------------------------------------------------
            // Multiple instructions:
            //
            // <instructions>
            //     → <instruction> . <instructions>
            //
            // Because this is a RIGHTMOST derivation, the
            // rightmost <instructions> is expanded first.
            // ----------------------------------------------------
            DisplayMultipleInstructionDerivation(
                instructions
            );
        }

        Console.WriteLine();
        Console.WriteLine("Final generated sentence:");
        Console.WriteLine(input);

        Console.WriteLine();
        Console.WriteLine("==================================================");
    }


    // ============================================================
    // DISPLAY ONE INSTRUCTION DERIVATION
    // ============================================================
    private void DisplayInstructionDerivation(string instruction)
    {
        string[] parts = instruction.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries
        );

        string command = parts[0];
        string coordinatesText = parts[1];

        string[] coordinates = coordinatesText.Split('-');

        // --------------------------------------------------------
        // SQR
        // --------------------------------------------------------
        if (command == "SQR")
        {
            // <instruction> → SQR <coord>-<coord>
            string current =
                "begin SQR <coord>-<coord> end";

            Console.WriteLine("=> " + current);

            // Expand the RIGHTMOST coordinate first.
            current =
                "begin SQR <coord>-<x><y> end";

            Console.WriteLine("=> " + current);

            // Expand Y.
            current =
                "begin SQR <coord>-" +
                "<x>" +
                coordinates[1][1] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand X.
            current =
                "begin SQR <coord>-" +
                coordinates[1][0] +
                coordinates[1][1] +
                " end";

            Console.WriteLine("=> " + current);

            // Now expand the leftmost coordinate.
            current =
                "begin SQR <x><y>-" +
                coordinates[1] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand Y.
            current =
                "begin SQR <x>" +
                coordinates[0][1] +
                "-" +
                coordinates[1] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand X.
            current =
                "begin SQR " +
                coordinates[0][0] +
                coordinates[0][1] +
                "-" +
                coordinates[1] +
                " end";

            Console.WriteLine("=> " + current);
        }

        // --------------------------------------------------------
        // TRI
        // --------------------------------------------------------
        else if (command == "TRI")
        {
            // <instruction> → TRI <coord>-<coord>-<coord>
            string current =
                "begin TRI <coord>-<coord>-<coord> end";

            Console.WriteLine("=> " + current);

            // Expand the RIGHTMOST coordinate first.
            current =
                "begin TRI <coord>-<coord>-<x><y> end";

            Console.WriteLine("=> " + current);

            // Expand Y.
            current =
                "begin TRI <coord>-<coord>-" +
                "<x>" +
                coordinates[2][1] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand X.
            current =
                "begin TRI <coord>-<coord>-" +
                coordinates[2][0] +
                coordinates[2][1] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand the middle coordinate.
            current =
                "begin TRI <coord>-<x><y>-" +
                coordinates[2] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand Y.
            current =
                "begin TRI <coord>-" +
                "<x>" +
                coordinates[1][1] +
                "-" +
                coordinates[2] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand X.
            current =
                "begin TRI <coord>-" +
                coordinates[1][0] +
                coordinates[1][1] +
                "-" +
                coordinates[2] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand the leftmost coordinate.
            current =
                "begin TRI <x><y>-" +
                coordinates[1] +
                "-" +
                coordinates[2] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand Y.
            current =
                "begin TRI <x>" +
                coordinates[0][1] +
                "-" +
                coordinates[1] +
                "-" +
                coordinates[2] +
                " end";

            Console.WriteLine("=> " + current);

            // Expand X.
            current =
                "begin TRI " +
                coordinates[0][0] +
                coordinates[0][1] +
                "-" +
                coordinates[1] +
                "-" +
                coordinates[2] +
                " end";

            Console.WriteLine("=> " + current);
        }
    }


    // ============================================================
    // DISPLAY MULTIPLE INSTRUCTION DERIVATION
    // ============================================================
    private void DisplayMultipleInstructionDerivation(
        string[] instructions)
    {
        // --------------------------------------------------------
        // Start by expanding <instructions> recursively.
        // --------------------------------------------------------
        string current = "begin <instruction> . <instructions> end";

        Console.WriteLine("=> " + current);

        // Continue expanding the rightmost <instructions>.
        for (int i = 1; i < instructions.Length - 1; i++)
        {
            current += " . <instructions>";
            Console.WriteLine("=> " + current);
        }

        // --------------------------------------------------------
        // Expand the rightmost instruction first.
        // --------------------------------------------------------
        string lastInstruction = instructions[^1].Trim();

        Console.WriteLine();
        Console.WriteLine(
            "Expanding rightmost instruction: " +
            lastInstruction
        );

        DisplayInstructionStepsOnly(lastInstruction);

        // --------------------------------------------------------
        // Display the remaining instructions.
        // --------------------------------------------------------
        for (int i = instructions.Length - 2; i >= 0; i--)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Expanding instruction: " +
                instructions[i].Trim()
            );

            DisplayInstructionStepsOnly(
                instructions[i].Trim()
            );
        }
    }


    // ============================================================
    // DISPLAY INSTRUCTION STEPS
    //
    // This helper displays the grammar expansion for an
    // individual SQR or TRI instruction.
    // ============================================================
    private void DisplayInstructionStepsOnly(string instruction)
    {
        string[] parts = instruction.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries
        );

        string command = parts[0];
        string coordinatesText = parts[1];

        string[] coordinates = coordinatesText.Split('-');

        if (command == "SQR")
        {
            Console.WriteLine(
                "    <instruction> => SQR <coord>-<coord>"
            );

            Console.WriteLine(
                "    Rightmost <coord> => " +
                coordinates[1]
            );

            Console.WriteLine(
                "    Leftmost <coord> => " +
                coordinates[0]
            );
        }
        else if (command == "TRI")
        {
            Console.WriteLine(
                "    <instruction> => TRI <coord>-<coord>-<coord>"
            );

            Console.WriteLine(
                "    Rightmost <coord> => " +
                coordinates[2]
            );

            Console.WriteLine(
                "    Middle <coord> => " +
                coordinates[1]
            );

            Console.WriteLine(
                "    Leftmost <coord> => " +
                coordinates[0]
            );
        }
    }
}