@echo off
for %%p in ("Gatherer.Tests", "Gatherer.Tests.EndToEnd") do (
echo Running tests for: %%p
echo.
echo.
dotnet test %%p/%%p.csproj
echo.
echo.
echo.
echo.
echo.
)