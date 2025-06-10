# Project: SoundPalette

**Goal:** To develop a micro-API that converts HEX/RGB color codes into musical chords. The service will accept a color input, convert it to HSL, and derive musical properties like pitch class, mode, MIDI notes, and frequencies. The project emphasizes Test-Driven Development (TDD), a "pure-function core," and a robust CI/CD pipeline for deployment to Azure Container Apps.

---

## Components

### Environment/Hosting
* **Local Development Machine:** Windows/macOS/Linux
* **IDE:** Visual Studio, Visual Studio Code, or JetBrains Rider
* **Version Control:** Git
* **Cloud Hosting:** Azure Container Apps

### Software Components
* **Web Application Backend**
    * **Framework:** ASP.NET Core Minimal API
    * **Language:** C#
    * **API Interaction:** Exposes RESTful endpoints for conversion.
    * **JSON Processing:** `System.Text.Json`
* **Core Logic Services**
    * `ColorParser.cs` (Handles color string parsing and conversion to HSL)
    * `MusicTheoryService.cs` (Determines pitch class and chord quality from HSL values)
    * `ChordBuilder.cs` (Constructs MIDI notes and frequencies from musical properties)

### External APIs
* None. The application is algorithmically self-contained.

---

## Core Services and Data Structures

### ColorParser.cs (Service)
* **Responsibilities:**
    * Accepts a color string (e.g., `"#RRGGBB"`).
    * Validates the input format.
    * Parses the color string into RGB values.
    * Converts RGB values into HSL (Hue, Saturation, Lightness) values.
* **Key Methods (Conceptual):**
    * `ParseToHsl(string colorString)`: Returns an `HslColor` object.
* **Implied Data Models:**
    * `ColorInput` (record for API request, e.g., `public record ColorInput(string ColorValue);`).
    * `HslColor` (record for internal HSL representation, e.g., `public record HslColor(double H, double S, double L);`).

### MusicTheoryService.cs (Service)
* **Responsibilities:**
    * Maps Hue (0-360°) to one of 12 musical pitch classes (e.g., C, C♯/D♭, ...).
    * Determines chord quality (e.g., major, minor) based on Saturation.
    * Determines chord extensions (e.g., 7th, 9th) based on Lightness.
* **Key Methods (Conceptual):**
    * `GetPitchClassFromHue(double hue)`: Returns a pitch class string.
    * `GetCombinedModeAndExtension(double saturation, double lightness)`: Returns a combined mode and extension string (e.g., "major-7").

### ChordBuilder.cs (Service)
* **Responsibilities:**
    * Accepts a pitch class and chord quality/extension string.
    * Maps the pitch class to a base MIDI note in a "comfortable octave."
    * Constructs a chord by adding appropriate musical intervals (semitones) based on the quality and extensions.
    * Calculates the frequency (Hz) for each MIDI note.
* **Key Methods (Conceptual):**
    * `BuildMusicalProperties(string pitchClass, string combinedModeAndExtension)`: Returns MIDI notes and their corresponding frequencies.
* **Implied Data Models:**
    * `ChordOutput` (record for the final API response, containing Hex, HSL, PitchClass, Mode, MidiNotes, and FrequencyHz).

### Configuration (`appsettings.json` / Environment Variables)
* **(Potential) HSL Thresholds:** Configurable values for "low" vs. "high" saturation or "dark" vs. "bright" lightness.
* **(Potential) ComfortableOctaveBaseMidi:** The base MIDI note to build chords from (e.g., 60 for C4).

---

## Development Plan

### Phase 0: Core Setup & Project Initialization
- [x] **Step 0.1: Project Initialization & Environment Setup**
    - [x] Create new solution `SoundPalette.sln`.
    - [x] Create ASP.NET Core Minimal API project `SoundPalette.Api`.
    - [x] Create xUnit test project `SoundPalette.Api.Tests`.
    - [x] Add project reference from Tests to Api.
- [x] **Step 0.2: Initial Git Setup**
    - [x] Initialize Git repository.
    - [x] Create and configure `.gitignore`.
    - [x] Make initial commit of scaffolded project structure.
- [x] **Step 0.3: Define Core Domain Models**
    - [x] Create `ColorInput`, `HslColor`, and `ChordOutput` records.

### Phase 1: Core Logic Implementation via TDD
- [x] **Step 1.1: Implement Color Parser**
    - [x] Define `IColorParser` interface and `ColorParser` class.
    - [x] Write failing unit test for HEX to HSL conversion.
    - [x] Implement `ParseToHsl` method to make the test pass.
    - [x] Refactor and add tests for edge cases (black, white) and invalid inputs.
- [ ] **Step 1.2: Implement Music Theory Service**
    - [ ] Define `IMusicTheoryService` interface and `MusicTheoryService` class.
    - [ ] Write failing unit tests for Hue to Pitch Class mapping for all 12 segments.
    - [ ] Write failing unit tests for Saturation/Lightness to Mode/Extension mapping based on defined thresholds.
    - [ ] Implement the methods to make tests pass.
- [ ] **Step 1.3: Implement Chord Builder**
    - [ ] Define `IChordBuilder` interface and `ChordBuilder` class.
    - [ ] Write failing unit tests for generating MIDI notes and frequencies for various chords (e.g., C major, E♭ minor-7).
    - [ ] Implement the chord construction and frequency calculation logic.
- [ ] **Step 1.4: Dependency Injection Setup**
    - [ ] Register `IColorParser`, `IMusicTheoryService`, and `IChordBuilder` as services in `Program.cs`.

### Phase 2: API Layer Development & Testing
- [ ] **Step 2.1: Implement API Endpoints**
    - [ ] Implement `POST /convert` endpoint, injecting and using the core logic services.
    - [ ] Implement `GET /chords/{pitch}/{quality}` utility endpoint.
    - [ ] Implement `GET /health` endpoint using `AddHealthChecks`.
- [ ] **Step 2.2: Implement Input Validation and Error Handling**
    - [ ] Ensure services throw `ArgumentException` for invalid inputs.
    - [ ] Add try-catch blocks in API handlers to return `Results.BadRequest()` or `Results.Problem()` for errors.
- [ ] **Step 2.3: Integrate OpenAPI (Swagger)**
    - [ ] Ensure `AddEndpointsApiExplorer` and `AddSwaggerGen` are configured.
    - [ ] Configure SwaggerUI for a better development experience.
- [ ] **Step 2.4: Comprehensive Testing**
    - [ ] Add Property-Based Tests with FsCheck for color conversion consistency and chord structure invariants.
    - [ ] Add Integration Tests with `WebApplicationFactory` to test the full request pipeline for all endpoints.

### Phase 3: CI/CD Pipeline and Deployment
- [ ] **Step 3.1: Create Multi-stage Dockerfile**
    - [ ] Create a `Dockerfile` in the `SoundPalette.Api` project for building and running the application.
    - [ ] Include a `HEALTHCHECK` instruction pointing to the `/health` endpoint.
- [ ] **Step 3.2: Create GitHub Actions Workflow**
    - [ ] Create `.github/workflows/soundpalette-ci.yml`.
    - [ ] Define `build-test` job to build the project and run all tests on pushes/PRs.
    - [ ] Configure test job to upload code coverage reports to a service like Codecov.
- [ ] **Step 3.3: Implement Docker Build/Push Job**
    - [ ] Define `docker` job that depends on `build-test`.
    - [ ] Configure job to log into GitHub Container Registry (GHCR).
    - [ ] Configure job to build and push the Docker image, tagged with the Git SHA.
- [ ] **Step 3.4: Implement Deployment Job**
    - [ ] Define `deploy-aca` job that depends on `docker`.
    - [ ] Configure job to log into Azure using a service principal.
    - [ ] Add script to run `az containerapp update` to deploy the new image to Azure Container Apps.

### Phase 4: Documentation and Finalization
- [ ] **Step 4.1: Craft Excellent README.md**
    - [ ] Add project description, CI/CD status badges, and link to the live demo.
    - [ ] Document all API endpoints with examples.
    - [ ] Provide clear instructions for local setup, running the project, and running tests.
    - [ ] List the technology stack.
- [ ] **Step 4.2: Prepare Interview Talking Points**
    - [ ] Document highlights of the TDD/PBT process, CI/CD pipeline design, and software design principles used.

### Phase 5: (Optional) Advanced Enhancements
- [ ] **Step 5.1: Generate Audio/Notation Previews**
    - [ ] Investigate libraries like NAudio (for WAV) or MusicXml.NET.
    - [ ] Extend the API to optionally generate and return audio files or notation snippets.
- [ ] **Step 5.2: Develop Simple Blazor Frontend**
    - [ ] Create a new Blazor Wasm project in the solution.
    - [ ] Build a simple UI to interact with the API and display results.