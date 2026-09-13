CMPS 3111 — Programming Languages Program 1

Language Recognizer

A C#/.NET 10 language recognizer based on the CMPS 3111 BNF grammar. The project includes a console program and a web interface using ASP.NET Core with HTML, CSS, and JavaScript.

Requirements

.NET 10 SDK

Git

A modern web browser

Visual Studio Code or another code editor

Install .NET

Linux (Ubuntu)

sudo apt update
sudo apt install dotnet-sdk-10.0
dotnet --version

Windows

Using PowerShell:

winget install Microsoft.DotNet.SDK.10
dotnet --version

If winget is unavailable, install the .NET 10 SDK from Microsoft's official .NET download page.

Clone and Build

git clone <REPOSITORY-URL>
cd CMPS3111-Program1
dotnet build

Run the Console Program

dotnet run

Run the Web Interface

Linux

cd WebAPI
dotnet restore
dotnet build
dotnet run --launch-profile http

Windows

cd .\WebAPI
dotnet restore
dotnet build
dotnet run --launch-profile http

The server runs on port 5223.

Open in a browser:

http://localhost:5223

Test the API

Linux

curl http://localhost:5223/api/status

curl -X POST http://localhost:5223/api/recognize \
  -H "Content-Type: application/json" \
  -d '{"input":"begin SQR A1-C4 end"}'

Windows PowerShell

curl.exe http://localhost:5223/api/status

curl.exe -X POST http://localhost:5223/api/recognize -H "Content-Type: application/json" -d "{\"input\":\"begin SQR A1-C4 end\"}"

Test Multiple Instructions

Valid:

begin SQR A1-C4. TRI A1-C6-G3 end

Multiple errors:

begin SQR A1-C4. TRI H1-C6-G3. SQR B2-Z9. TRI A1-C6-G3 end

The recognizer checks every instruction and reports which are valid and which contain errors.

A completely valid program also produces the rightmost derivation and parse tree.

BNF Grammar

<program> → begin <instructions> end

<instructions> → <instruction>
               | <instruction> . <instructions>

<instruction> → SQR <coord>-<coord>
              | TRI <coord>-<coord>-<coord>

<coord> → <x><y>

<x> → A | B | C | D | E | F | G

<y> → 1 | 2 | 3 | 4 | 5 | 6

Access From Another Computer

Both computers must be on the same local network.

On the computer running the WebAPI:

hostname -I

Example:

192.168.18.143

From the other computer, open:

http://192.168.18.143:5223

Use the current IP address shown by hostname -I, since it may change on a different network.

Project Structure

CMPS3111-Program1/
├── Program.cs
├── Validator.cs
├── Derivation.cs
├── ParseTree.cs
├── CMPS3111-Program1.csproj
├── README.md
└── WebAPI/
    ├── Program.cs
    ├── WebAPI.csproj
    ├── Properties/
    │   └── launchSettings.json
    └── wwwroot/
        ├── index.html
        ├── style.css
        └── script.js

Stop the Web Server

Press:

Ctrl + C

in the terminal running the WebAPI.

Git

git add .
git commit -m "Describe your changes"
git push origin main