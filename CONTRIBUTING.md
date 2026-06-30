# Contributing to WebAppTemplate

Thanks for your interest in improving this template.

## Getting started

1. Fork the repository and clone your fork.
2. Copy `WebAppTemplate.API/appsettings.example.json` to `appsettings.json`.
3. Run `dotnet restore` and `dotnet build` from the solution root.
4. Run tests with `dotnet test`.

## Pull request guidelines

- Keep changes focused and aligned with the template's scope.
- Follow existing Clean Architecture boundaries and naming conventions.
- Add or update tests for behavioral changes.
- Update `README.md` when setup steps, endpoints, or configuration change.

## Code style

- Use Minimal API endpoint classes implementing `IEndpoint`.
- Keep business logic in Application services; avoid leaking EF types into Presentation.
- Return `ServiceResult<T>` from services and map to `ApiResponse<T>` at the HTTP boundary.

## Reporting issues

Open an issue with steps to reproduce, expected behavior, and your environment (.NET version, OS).
