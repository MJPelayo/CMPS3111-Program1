using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// SERVICES
// ============================================================

builder.Services.AddOpenApi();

var app = builder.Build();


// ============================================================
// STATIC WEB FRONTEND
// ============================================================

app.UseDefaultFiles();
app.UseStaticFiles();


// ============================================================
// DEVELOPMENT API DOCUMENTATION
// ============================================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// ============================================================
// API STATUS
// ============================================================

app.MapGet("/api/status", () =>
{
    return Results.Ok(new
    {
        application = "CMPS 3111 Language Recognizer API",
        status = "Running",
        version = "1.0"
    });
});


// ============================================================
// RECOGNIZE PROGRAM
// ============================================================
//
// POST /api/recognize
//
// Receives a program string and uses the existing C#
// Validator, Derivation, and ParseTree classes.
//
// ============================================================

app.MapPost("/api/recognize", (RecognizeRequest request) =>
{
    // --------------------------------------------------------
    // Check that input was provided.
    // --------------------------------------------------------

    if (string.IsNullOrWhiteSpace(request.Input))
    {
        return Results.BadRequest(new
        {
            valid = false,
            error = "Please enter a program.",
            instructionResults = Array.Empty<object>(),
            derivation = (string?)null,
            parseTree = (string?)null
        });
    }


    // --------------------------------------------------------
    // Create the existing validator.
    // --------------------------------------------------------

    Validator validator = new Validator();


    // --------------------------------------------------------
    // Get results for EVERY instruction.
    // --------------------------------------------------------

    var instructionResults =
        validator.GetInstructionResults(request.Input);


    // --------------------------------------------------------
    // Validate the complete program.
    // --------------------------------------------------------

    bool valid =
        validator.ValidateProgram(request.Input);


    // --------------------------------------------------------
    // INVALID PROGRAM
    //
    // Return every instruction result.
    //
    // No derivation or parse tree is generated because the
    // complete program is invalid.
    // --------------------------------------------------------

    if (!valid)
    {
        string error =
            validator.GetProgramError(request.Input);


        return Results.Ok(new
        {
            valid = false,
            error = error,
            instructionResults = instructionResults,
            derivation = (string?)null,
            parseTree = (string?)null
        });
    }


    // ========================================================
    // VALID PROGRAM
    // ========================================================

    string derivation;
    string parseTree;


    // --------------------------------------------------------
    // Capture the existing console output from Derivation.cs
    // and ParseTree.cs.
    // --------------------------------------------------------

    lock (Console.Out)
    {
        var originalOutput =
            Console.Out;


        // ----------------------------------------------------
        // Capture derivation.
        // ----------------------------------------------------

        using var derivationWriter =
            new StringWriter();

        Console.SetOut(derivationWriter);


        Derivation derivationGenerator =
            new Derivation();

        derivationGenerator
            .DisplayRightmostDerivation(
                request.Input
            );


        derivation =
            derivationWriter.ToString();


        // ----------------------------------------------------
        // Capture parse tree.
        // ----------------------------------------------------

        using var parseTreeWriter =
            new StringWriter();

        Console.SetOut(parseTreeWriter);


        ParseTree parseTreeGenerator =
            new ParseTree();

        parseTreeGenerator
            .DisplayParseTree(
                request.Input
            );


        parseTree =
            parseTreeWriter.ToString();


        // ----------------------------------------------------
        // Restore normal console output.
        // ----------------------------------------------------

        Console.SetOut(originalOutput);
    }


    // --------------------------------------------------------
    // Return the complete successful response.
    // --------------------------------------------------------

    return Results.Ok(new
    {
        valid = true,
        error = (string?)null,
        instructionResults = instructionResults,
        derivation = derivation,
        parseTree = parseTree
    });
});


// ============================================================
// START APPLICATION
// ============================================================

app.Run();


// ============================================================
// REQUEST MODEL
// ============================================================

public record RecognizeRequest(
    [property: JsonPropertyName("input")]
    string Input
);