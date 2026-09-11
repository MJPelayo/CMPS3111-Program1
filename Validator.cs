using System;

public class Validator
{
    // ============================================================
    // VALIDATE COORDINATE
    // Grammar:
    // <coord> → <x><y>
    // <x> → A | B | C | D | E | F | G
    // <y> → 1 | 2 | 3 | 4 | 5 | 6
    // ============================================================
    public bool ValidateCoordinate(string coordinate)
    {
        // A coordinate must contain exactly two characters.
        if (coordinate.Length != 2)
        {
            return false;
        }

        // The first character represents the X coordinate.
        char x = coordinate[0];

        // The second character represents the Y coordinate.
        char y = coordinate[1];

        // X must be a letter from A through G.
        bool validX = x >= 'A' && x <= 'G';

        // Y must be a number from 1 through 6.
        bool validY = y >= '1' && y <= '6';

        // The coordinate is valid only when both X and Y are valid.
        return validX && validY;
    }


    // ============================================================
    // VALIDATE INSTRUCTION
    // Grammar:
    // <instruction> → SQR <coord>-<coord>
    //                | TRI <coord>-<coord>-<coord>
    // ============================================================
    public bool ValidateInstruction(string instruction)
    {
        // Separate the command from the coordinates.
        string[] parts = instruction.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries
        );

        // An instruction must contain a command and coordinates.
        if (parts.Length != 2)
        {
            return false;
        }

        string command = parts[0];
        string coordinates = parts[1];


        // --------------------------------------------------------
        // SQR
        // --------------------------------------------------------
        if (command == "SQR")
        {
            // Separate the two coordinates using the dash.
            string[] coords = coordinates.Split('-');

            // SQR requires exactly two coordinates.
            if (coords.Length != 2)
            {
                return false;
            }

            // Both coordinates must be valid.
            return ValidateCoordinate(coords[0]) &&
                   ValidateCoordinate(coords[1]);
        }


        // --------------------------------------------------------
        // TRI
        // --------------------------------------------------------
        if (command == "TRI")
        {
            // Separate the three coordinates using the dashes.
            string[] coords = coordinates.Split('-');

            // TRI requires exactly three coordinates.
            if (coords.Length != 3)
            {
                return false;
            }

            // All three coordinates must be valid.
            return ValidateCoordinate(coords[0]) &&
                   ValidateCoordinate(coords[1]) &&
                   ValidateCoordinate(coords[2]);
        }


        // The command was neither SQR nor TRI.
        return false;
    }


    // ============================================================
    // VALIDATE COMPLETE PROGRAM
    // Grammar:
    // <program> → begin <instructions> end
    //
    // <instructions> → <instruction>
    //                 | <instruction> . <instructions>
    // ============================================================
    public bool ValidateProgram(string input)
    {
        // The complete program must begin with "begin "
        // and finish with " end".
        if (!input.StartsWith("begin ") ||
            !input.EndsWith(" end"))
        {
            return false;
        }


        // Remove "begin " from the beginning and
        // " end" from the end.
        string instructionText = input.Substring(
            6,
            input.Length - 10
        );


        // Multiple instructions are separated by periods.
        string[] instructions = instructionText.Split('.');


        // At least one instruction must exist.
        if (instructions.Length == 0)
        {
            return false;
        }


        // Validate every instruction.
        foreach (string instruction in instructions)
        {
            // Remove extra spaces around an instruction.
            string cleanedInstruction = instruction.Trim();

            // Every instruction must follow the grammar.
            if (!ValidateInstruction(cleanedInstruction))
            {
                return false;
            }
        }


        // All parts of the program are valid.
        return true;
    }
}