using System;

public class Validator
{
    // Validates a coordinate according to the grammar:
    // <coord> → <x><y>
    // <x> → A | B | C | D | E | F | G
    // <y> → 1 | 2 | 3 | 4 | 5 | 6
    public bool ValidateCoordinate(string coordinate)
    {
        // A coordinate must contain exactly two characters.
        if (coordinate.Length != 2)
        {
            return false;
        }

        // The first character represents the X value.
        char x = coordinate[0];

        // The second character represents the Y value.
        char y = coordinate[1];

        // X must be a letter from A through G.
        bool validX = x >= 'A' && x <= 'G';

        // Y must be a number from 1 through 6.
        bool validY = y >= '1' && y <= '6';

        // The coordinate is valid only if both parts are valid.
        return validX && validY;
    }
}