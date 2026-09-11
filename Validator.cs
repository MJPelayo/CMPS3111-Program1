using System;

public class Validator
{
    // ============================================================
    // VALIDATE COORDINATE
    //
    // Grammar:
    // <coord> → <x><y>
    // <x> → A | B | C | D | E | F | G
    // <y> → 1 | 2 | 3 | 4 | 5 | 6
    // ============================================================
    public bool ValidateCoordinate(string coordinate)
    {
        return string.IsNullOrEmpty(
            GetCoordinateError(coordinate)
        );
    }


    // ============================================================
    // GET COORDINATE ERROR
    // ============================================================
    public string GetCoordinateError(string coordinate)
    {
        // A coordinate must contain exactly two characters.
        if (coordinate.Length != 2)
        {
            return $"Invalid coordinate '{coordinate}'. " +
                   "A coordinate must contain exactly two characters.";
        }

        char x = coordinate[0];
        char y = coordinate[1];

        // X must be A through G.
        bool validX = x >= 'A' && x <= 'G';

        // Y must be 1 through 6.
        bool validY = y >= '1' && y <= '6';


        // Both X and Y are invalid.
        if (!validX && !validY)
        {
            return $"Invalid coordinate '{coordinate}'. " +
                   $"'{x}' is not a valid X value and '{y}' " +
                   "is not a valid Y value. " +
                   "X must be A through G and Y must be 1 through 6.";
        }


        // X is invalid.
        if (!validX)
        {
            return $"Invalid coordinate '{coordinate}'. " +
                   $"'{x}' is not a valid X value. " +
                   "X must be A through G.";
        }


        // Y is invalid.
        if (!validY)
        {
            return $"Invalid coordinate '{coordinate}'. " +
                   $"{x} is a valid X value, but {y} is not a valid Y value. " +
                   "Y must be 1 through 6.";
        }


        // The coordinate is valid.
        return "";
    }


    // ============================================================
    // VALIDATE INSTRUCTION
    //
    // Grammar:
    // <instruction> → SQR <coord>-<coord>
    //                | TRI <coord>-<coord>-<coord>
    // ============================================================
    public bool ValidateInstruction(string instruction)
    {
        return string.IsNullOrEmpty(
            GetInstructionError(instruction)
        );
    }


    // ============================================================
    // GET INSTRUCTION ERROR
    // ============================================================
    public string GetInstructionError(string instruction)
    {
        string[] parts = instruction.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries
        );

        // An instruction requires a command and coordinates.
        if (parts.Length != 2)
        {
            return $"Invalid instruction '{instruction}'. " +
                   "The instruction must contain a command " +
                   "followed by its coordinates.";
        }

        string command = parts[0];
        string coordinates = parts[1];


        // ========================================================
        // SQR
        // ========================================================
        if (command == "SQR")
        {
            string[] coords = coordinates.Split('-');

            // SQR requires exactly two coordinates.
            if (coords.Length != 2)
            {
                return "Invalid SQR instruction. " +
                       "SQR requires exactly two coordinates " +
                       "separated by a dash (-).";
            }

            // Validate first coordinate.
            string error = GetCoordinateError(coords[0]);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            // Validate second coordinate.
            error = GetCoordinateError(coords[1]);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return "";
        }


        // ========================================================
        // TRI
        // ========================================================
        if (command == "TRI")
        {
            string[] coords = coordinates.Split('-');

            // TRI requires exactly three coordinates.
            if (coords.Length != 3)
            {
                return "Invalid TRI instruction. " +
                       "TRI requires exactly three coordinates " +
                       "separated by dashes (-).";
            }

            // Validate first coordinate.
            string error = GetCoordinateError(coords[0]);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            // Validate second coordinate.
            error = GetCoordinateError(coords[1]);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            // Validate third coordinate.
            error = GetCoordinateError(coords[2]);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return "";
        }


        // Unknown command.
        return $"Invalid command '{command}'. " +
               "The only valid commands are SQR and TRI.";
    }


    // ============================================================
    // VALIDATE COMPLETE PROGRAM
    //
    // Grammar:
    // <program> → begin <instructions> end
    //
    // <instructions> → <instruction>
    //                 | <instruction> . <instructions>
    // ============================================================
    public bool ValidateProgram(string input)
    {
        return string.IsNullOrEmpty(
            GetProgramError(input)
        );
    }


    // ============================================================
    // GET PROGRAM ERROR
    // ============================================================
    public string GetProgramError(string input)
    {
        // The program must begin with "begin ".
        if (!input.StartsWith("begin "))
        {
            return "Invalid program. " +
                   "The input must begin with 'begin'.";
        }

        // The program must end with " end".
        if (!input.EndsWith(" end"))
        {
            return "Invalid program. " +
                   "The input must end with 'end'.";
        }

        // Extract everything between begin and end.
        string instructionText = input.Substring(
            6,
            input.Length - 10
        );

        // At least one instruction is required.
        if (string.IsNullOrWhiteSpace(instructionText))
        {
            return "Invalid program. " +
                   "At least one instruction is required.";
        }

        // Multiple instructions are separated by periods.
        string[] instructions = instructionText.Split('.');

        // Validate every instruction.
        for (int i = 0; i < instructions.Length; i++)
        {
            string instruction = instructions[i].Trim();

            // Check for an empty instruction.
            if (string.IsNullOrEmpty(instruction))
            {
                return "Invalid program. " +
                       "An empty instruction was found.";
            }

            // Validate the instruction.
            string error = GetInstructionError(instruction);

            if (!string.IsNullOrEmpty(error))
            {
                return $"Instruction {i + 1}: {error}";
            }
        }

        // Everything follows the grammar.
        return "";
    }
}