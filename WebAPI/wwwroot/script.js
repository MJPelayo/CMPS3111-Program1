// ============================================================
// CMPS 3111 — LANGUAGE RECOGNIZER
// Frontend JavaScript
//
// This file communicates with the C# ASP.NET Core API.
// The actual recognition logic remains in the C# backend.
// ============================================================


// ============================================================
// ELEMENT REFERENCES
// ============================================================

const programInput = document.getElementById("programInput");
const validateButton = document.getElementById("validateButton");

const resultContent = document.getElementById("resultContent");
const connectionStatus = document.getElementById("connectionStatus");

const outputSection = document.getElementById("outputSection");

const derivationOutput =
    document.getElementById("derivationOutput");

const parseTreeOutput =
    document.getElementById("parseTreeOutput");


// ============================================================
// API REQUEST
// ============================================================

async function recognizeProgram() {

    const input = programInput.value.trim();

    // --------------------------------------------------------
    // Make sure the user entered something.
    // --------------------------------------------------------

    if (!input) {

        showError(
            "Please enter a program before selecting Validate Program."
        );

        return;
    }


    // --------------------------------------------------------
    // Update interface while the request is being processed.
    // --------------------------------------------------------

    validateButton.disabled = true;

    validateButton.innerHTML = `
        <span>⟳</span>
        VALIDATING...
    `;

    connectionStatus.textContent = "CONNECTING";


    try {

        // ----------------------------------------------------
        // Send the program to the C# API.
        //
        // Because the frontend is served by the same
        // ASP.NET Core application, we can use a relative URL.
        // ----------------------------------------------------

        const response = await fetch("/api/recognize", {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                input: input
            })

        });


        // ----------------------------------------------------
        // Check for an HTTP error.
        // ----------------------------------------------------

        if (!response.ok) {

            throw new Error(
                `Server returned HTTP ${response.status}`
            );

        }


        // ----------------------------------------------------
        // Convert the response to JavaScript data.
        // ----------------------------------------------------

        const data = await response.json();


        // ----------------------------------------------------
        // Display the result.
        // ----------------------------------------------------

        if (data.valid) {

            showSuccess(data);

        } else {

            showError(data.error);

        }


        connectionStatus.textContent = "CONNECTED";


    } catch (error) {

        console.error("API Error:", error);

        connectionStatus.textContent = "OFFLINE";

        showError(
            "Unable to connect to the C# backend. " +
            "Make sure the ASP.NET Core server is running."
        );

    } finally {

        validateButton.disabled = false;

        validateButton.innerHTML = `
            <span>▶</span>
            VALIDATE PROGRAM
        `;

    }

}


// ============================================================
// SUCCESS RESULT
// ============================================================

function showSuccess(data) {

    resultContent.className =
        "result-content result-success";


    resultContent.innerHTML = `

        <div class="result-icon">
            ✓
        </div>

        <h3>VALID PROGRAM</h3>

        <p>
            The C# recognizer accepted the program
            according to the supplied BNF grammar.
        </p>

    `;


    // --------------------------------------------------------
    // Display the actual C# derivation.
    // --------------------------------------------------------

    derivationOutput.textContent =
        data.derivation || "No derivation returned.";


    // --------------------------------------------------------
    // Display the actual C# parse tree.
    // --------------------------------------------------------

    parseTreeOutput.textContent =
        data.parseTree || "No parse tree returned.";


    // --------------------------------------------------------
    // Reveal the output section.
    // --------------------------------------------------------

    outputSection.classList.remove("hidden");


    // --------------------------------------------------------
    // Always show the derivation first.
    // --------------------------------------------------------

    showTabById("derivation");

}


// ============================================================
// ERROR RESULT
// ============================================================

function showError(message) {

    resultContent.className =
        "result-content result-error";


    resultContent.innerHTML = `

        <div class="result-icon">
            !
        </div>

        <h3>INVALID PROGRAM</h3>

        <p>
            The C# recognizer rejected the input.
        </p>

        <div class="error-message">
            ${escapeHtml(message)}
        </div>

    `;


    // --------------------------------------------------------
    // Hide previous successful output.
    // --------------------------------------------------------

    outputSection.classList.add("hidden");

}


// ============================================================
// CLEAR PROGRAM
// ============================================================

function clearProgram() {

    programInput.value = "";

    resultContent.className =
        "result-content empty-result";


    resultContent.innerHTML = `

        <div class="result-icon">
            &lt;?&gt;
        </div>

        <h3>Waiting for input</h3>

        <p>
            Enter a program and select
            <strong>Validate Program</strong>.
        </p>

    `;


    connectionStatus.textContent = "READY";

    outputSection.classList.add("hidden");

    programInput.focus();

}


// ============================================================
// LOAD EXAMPLES
// ============================================================

function loadExample(type) {

    const examples = {

        sqr:
            "begin SQR A1-C4 end",

        tri:
            "begin TRI A1-C6-G3 end",

        multiple:
            "begin SQR A1-C4. TRI A1-C6-G3 end"

    };


    if (examples[type]) {

        programInput.value =
            examples[type];

        programInput.focus();

    }

}


// ============================================================
// TAB SWITCHING
// ============================================================

function showTab(button, element) {

    // --------------------------------------------------------
    // Remove active state from all tab buttons.
    // --------------------------------------------------------

    document
        .querySelectorAll(".tab-button")
        .forEach(tab => {

            tab.classList.remove("active");

        });


    // --------------------------------------------------------
    // Remove active state from all tab contents.
    // --------------------------------------------------------

    document
        .querySelectorAll(".tab-content")
        .forEach(tab => {

            tab.classList.remove("active");

        });


    // --------------------------------------------------------
    // Activate selected button.
    // --------------------------------------------------------

    element.classList.add("active");


    // --------------------------------------------------------
    // Activate selected content.
    // --------------------------------------------------------

    document
        .getElementById(button + "Tab")
        .classList.add("active");

}


// ============================================================
// TAB SWITCHING BY ID
// ============================================================

function showTabById(tabId) {

    document
        .querySelectorAll(".tab-button")
        .forEach(tab => {

            tab.classList.remove("active");

        });


    document
        .querySelectorAll(".tab-content")
        .forEach(tab => {

            tab.classList.remove("active");

        });


    document
        .getElementById(tabId + "Tab")
        .classList.add("active");


    const buttons =
        document.querySelectorAll(".tab-button");


    if (tabId === "derivation" && buttons.length > 0) {

        buttons[0].classList.add("active");

    }

    if (tabId === "tree" && buttons.length > 1) {

        buttons[1].classList.add("active");

    }

}


// ============================================================
// HTML ESCAPING
//
// Prevents API error messages from being interpreted as HTML.
// ============================================================

function escapeHtml(value) {

    const div = document.createElement("div");

    div.textContent = value;

    return div.innerHTML;

}


// ============================================================
// KEYBOARD SHORTCUT
//
// Ctrl + Enter or Cmd + Enter validates the program.
// ============================================================

programInput.addEventListener(
    "keydown",
    function (event) {

        if (
            (event.ctrlKey || event.metaKey) &&
            event.key === "Enter"
        ) {

            event.preventDefault();

            recognizeProgram();

        }

    }
);