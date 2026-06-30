# WebAppTemplate — GitHub Showcase Checklist

Production-ready ASP.NET Core 8 Web API template with Clean Architecture, JWT auth, EF Core, Docker.

---

## Completed

- [x] Clean Architecture layers (API, Presentation, Application, Infrastructure, Domain)
- [x] Minimal API endpoints via `IEndpoint` pattern
- [x] JWT authentication (sign-in, refresh, logout, change-password)
- [x] Account profile endpoint (`GET /api/Account/me`)
- [x] User CRUD (list paginated, get by id, update, soft delete)
- [x] Platform RBAC section (`/api/Platform/*`)
- [x] Permission-based authorization policies + JWT permission claims
- [x] Database seed data (Administrator role + admin user)
- [x] Global exception middleware (production-safe messages)
- [x] Health check (`GET /health`)
- [x] Swagger (Development + Docker only)
- [x] Docker Compose with `.env` secrets + SQL volume
- [x] GitHub Actions CI (build + test)
- [x] Unit tests + integration tests
- [x] README, LICENSE, CONTRIBUTING.md
- [x] Demo admin UI (`/admin`)
- [x] FluentValidation (including pagination + permission requests)

---

## Optional Next Steps

### API quality
- [ ] API versioning (`/api/v1/...`)
- [ ] Rate limiting on auth endpoints
- [ ] CORS policy per environment
- [ ] Correlation ID middleware

### Observability
- [ ] Serilog structured logging
- [ ] OpenTelemetry
- [ ] Audit log entity + admin endpoint

### Advanced auth
- [ ] Account lockout after failed logins
- [ ] Forgot / reset password flow
- [ ] Email verification (Mailhog in Docker for dev)

### Code cleanup
- [ ] Rename `Persistance` → `Persistence` folder
- [ ] Upgrade AutoMapper (NU1903 advisory)
- [ ] Resolve dual `AppRoles` enum vs `Roles` table model

---

## GitHub Repo Settings

| Setting | Value |
|---------|-------|
| Description | Production-ready ASP.NET Core 8 Web API template — Clean Architecture, JWT, EF Core |
| Topics | `aspnet-core`, `clean-architecture`, `jwt-authentication`, `dotnet8`, `web-api`, `entity-framework`, `template` |
| Template repository | Enable |
| Default branch | `main` |

---

## Local Commands

```bash
# Restore & build
dotnet build

# Run tests
dotnet test

# Start Docker stack
cp .env.example .env
docker compose up -d

# Migrations
dotnet ef database update --project WebAppTemplate.Infrastructure --startup-project WebAppTemplate.API

# Run API locally
dotnet run --project WebAppTemplate.API
```

---

*Last updated: GitHub showcase implementation complete.*
