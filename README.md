# Modern Warfare 2 - RTE Tool

This repository contains a Windows Forms application designed for Real-Time Editing (RTE) of Modern Warfare 2, likely for use with a PlayStation 3 console. The tool allows interaction with the game's memory to retrieve and potentially modify game details.

## Repository Structure

The repository is organized as follows:

- **Modern Warfare 2 - 444xMoDz - RTE/**: Main project directory
  - **Modern Warfare 2/**: Source code and project files for the Windows Forms application
    - **Properties/**: Project properties and resource files
    - **bin/**: Compiled binaries (Debug/Release)
    - **obj/**: Intermediate build files
    - `Form1.cs`: Main application form (likely the primary UI)
    - `Server Details Mw2.cs`: Form or class related to fetching server details
    - `Program.cs`: Application entry point
    - `Modern Warfare 2.csproj`: C# project file
    - `App.config`: Application configuration file
    - ... other project files (.resx, .Designer.cs, etc.)
  - `Modern Warfare 2.sln`: Visual Studio solution file
  - `README.md`: This file

## Features

The Modern Warfare 2 RTE Tool offers functionalities related to interacting with the game, potentially including:

- **Server Details Retrieval**: Displaying information about the current game session, such as:
  - Host Name
  - Map Name
  - Gamemode
  - Hardcore status
  - Max Players / Host status
- **PS3 Connectivity**: Utilizes libraries like PS3Lib for communication with a PlayStation 3 console running Modern Warfare 2.
- ... (Add other specific features if known, e.g., modding options, player lists, etc.)

## How to Use

1.  **Connect to PS3**: Ensure your PlayStation 3 console is connected and running Modern Warfare 2.
2.  **Launch the Tool**: Run the `Modern Warfare 2.exe` executable from the `bin/Debug` or `bin/Release` directory after building the project.
3.  **Connect via Tool**: Use the tool's interface to connect to your PS3 (details may vary based on the UI in `Form1.cs`).
4.  **Access Features**: Once connected, use the tool's buttons and controls to access features like "Get Server Details".

## Technical Implementation

The application is built using:

-   **C#**: The primary programming language.
-   **.NET Framework**: The framework for building Windows applications.
-   **Windows Forms**: Used for the graphical user interface.
-   **PS3Lib**: A library for interacting with PlayStation 3 consoles (requires TMAPI or CCAPI setup on the PS3).
-   ... (Mention any other significant libraries or technologies)

## Requirements

-   **Operating System**: Windows
-   **.NET Framework**: Version 4.8 or compatible.
-   **PlayStation 3**: A PS3 console running Modern Warfare 2.
-   **PS3 API**: TMAPI or CCAPI installed and configured on the PS3 for the tool to connect.
-   **PS3Lib**: The PS3Lib library (included or referenced in the project).
-   **Visual Studio**: Required to build the project from source.

## Getting Started

To get started with the Modern Warfare 2 RTE Tool:

1.  **Clone the Repository**:
    ```bash
    git clone <repository_url>
    ```
    (Replace `<repository_url>` with the actual URL of your repository)
2.  **Open in Visual Studio**: Open the `Modern Warfare 2.sln` file in Visual Studio.
3.  **Restore Dependencies**: Visual Studio should automatically restore NuGet packages if any are used (PS3Lib might be included directly).
4.  **Build the Project**: Build the solution in Visual Studio (Build -> Build Solution).
5.  **Run the Application**: The executable will be created in the `bin/Debug` or `bin/Release` folder. Run `Modern Warfare 2.exe`.

---

*This tool is intended for educational or personal use only. Use responsibly.*