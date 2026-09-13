using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// SERVICES
// ------------------------------------------------------------

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

// ------------------------------------------------------------
// DEVELOPMENT API DOCUMENTATION
// ------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ------------------------------------------------------------
// BASIC API INFORMATION
// ------------------------------------------------------------

app.MapGet("/api/status", () =>
{
    return Results.Ok(new
    {
        application = "CMPS 3111 Language Recognizer API",
        status = "Running",
        version = "1.0"
    });
});

// ------------------------------------------------------------
// RECOGNIZE PROGRAM
// ------------------------------------------------------------
//
// POST /api/recognize
//
// Receives a program string and uses the existing C#
// Validator, Derivation, and ParseTree classes.
//
// ------------------------------------------------------------

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
            error = "Please enter a program."
        });
    }

    // --------------------------------------------------------
    // Use the existing Validator class.
    // --------------------------------------------------------

    Validator validator = new Validator();

    bool valid = validator.ValidateProgram(request.Input);

    // --------------------------------------------------------
    // Invalid program.
    // --------------------------------------------------------

    if (!valid)
    {
        string error = validator.GetProgramError(request.Input);

        return Results.Ok(new
        {
            valid = false,
            error = error,
            derivation = (string?)null,
            parseTree = (string?)null
        });
    }

    // --------------------------------------------------------
    // Valid program.
    //
    // The existing Derivation and ParseTree classes currently
    // display their results using Console.WriteLine().
    //
    // We temporarily capture that output so the API can return
    // it to the web browser.
    // --------------------------------------------------------

    string derivation;
    string parseTree;

    lock (Console.Out)
    {
        var originalOutput = Console.Out;

        using var derivationWriter = new StringWriter();

        Console.SetOut(derivationWriter);

        Derivation derivationGenerator = new Derivation();

        derivationGenerator.DisplayRightmostDerivation(request.Input);

        derivation = derivationWriter.ToString();

        using var parseTreeWriter = new StringWriter();

        Console.SetOut(parseTreeWriter);

        ParseTree parseTreeGenerator = new ParseTree();

        parseTreeGenerator.DisplayParseTree(request.Input);

        parseTree = parseTreeWriter.ToString();

        Console.SetOut(originalOutput);
    }

    // --------------------------------------------------------
    // Return everything to the frontend as JSON.
    // --------------------------------------------------------

    return Results.Ok(new
    {
        valid = true,
        error = (string?)null,
        derivation = derivation,
        parseTree = parseTree
    });
});

// ------------------------------------------------------------
// START APPLICATION
// ------------------------------------------------------------

app.Run();

// ------------------------------------------------------------
// REQUEST MODEL
// ------------------------------------------------------------

public record RecognizeRequest(
    [property: JsonPropertyName("input")] string Input
);