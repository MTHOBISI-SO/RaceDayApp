# RaceDayWeb

ASP.NET Core MVC web client for the RaceDay API. The project targets .NET 8.

## Requirements
- .NET 8 SDK
- The matching RaceDay API running at the URL configured in `appsettings.json` (default: `https://localhost:7043/`)

## Open and run in VS Code
1. Extract this ZIP file.
2. In VS Code, choose **File > Open Folder** and select the `RaceDayWeb` folder that contains `RaceDayWeb.csproj`.
3. Open a terminal in that folder.
4. Run:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```
5. Open the local URL printed by `dotnet run` in your browser.

## Notes
- `Views/_ViewImports.cshtml` contains Razor imports and ASP.NET Core MVC Tag Helpers. Keep it as a `.cshtml` file.
- `Views/_ViewStart.cshtml` selects the shared `_Layout.cshtml` layout.
- The API must be running and reachable for race, venue, login, registration, and participation features to work.
- If the API uses a different address, update `ApiSettings:BaseUrl` in `appsettings.json`.

This package contains source files only. Run `dotnet build` in your own .NET 8 environment to verify compilation against your installed SDK and API setup.
