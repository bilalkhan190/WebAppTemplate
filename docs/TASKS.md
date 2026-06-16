# WebAppTemplate — Next Steps (GitHub Showcase)

Production-ready ASP.NET Core 8 Web API template with Clean Architecture, JWT auth, EF Core, Docker.

Use this checklist to take the repo from **working template** → **strong portfolio showcase**.

---

## Already Done

- [x] Clean Architecture layers (API, Presentation, Application, Infrastructure, Domain)
- [x] JWT authentication (sign-in, refresh token)
- [x] User registration with password hashing (BCrypt)
- [x] Role + user-role assignment endpoints
- [x] `ServiceResult<T>` + unified `ApiResponse<T>` pattern
- [x] FluentValidation on all POST endpoints
- [x] Consistent validation error format (`ApiResponse` via `ApiBehaviorOptions`)
- [x] Request/Response DTO separation (`DTOs/Requests`, `DTOs/Responses`)
- [x] AutoMapper (inbound + outbound)
- [x] Docker Compose (API + SQL Server)
- [x] Example appsettings for public repo (secrets in local `appsettings.json` only)

---

## Phase 1 — Must Have (Public Repo Ready)

> Do these first. Without them the repo looks unfinished on GitHub.

### 1.1 README

- [ ] Project description (2–3 lines: what it is, who it's for)
- [ ] Tech stack table (.NET 8, EF Core, JWT, FluentValidation, Docker)
- [ ] Architecture diagram (mermaid or image)
- [ ] Prerequisites (.NET 8 SDK, Docker Desktop)
- [ ] Quick start:
  1. Clone repo
  2. Copy `appsettings.example.json` → `appsettings.json`
  3. `docker compose up -d`
  4. Run migrations
  5. Open Swagger
- [ ] API endpoints table
- [ ] Folder structure tree
- [ ] Environment variables / connection string notes
- [ ] Badges (build status, .NET version, license)

### 1.2 Security Fixes

- [ ] `GET /api/account/get-user` — add `[Authorize]` or remove (duplicate of `/api/user/users`)
- [ ] Remove duplicate user-list endpoint (keep one: admin-only on `UserController`)
- [ ] Ensure no secrets in git history before making repo public
- [ ] Rotate JWT secret + DB password if they were ever committed
- [ ] Add `appsettings.example.Development.json` usage note in README

### 1.3 Database Seed Data

- [ ] Seed default roles: `Administrator`, `User`
- [ ] Seed admin user (document default credentials in README for dev only)
- [ ] Run seed on app startup or via dedicated migration/seeder class
- [ ] Document: "change admin password after first login" (when change-password API exists)

### 1.4 Global Exception Handling

- [ ] `ExceptionHandlingMiddleware` — catch unhandled errors
- [ ] Return same `ApiResponse` shape for 500 errors
- [ ] Never expose stack trace in production responses
- [ ] Register middleware early in pipeline (before controllers)

### 1.5 Health Check

- [ ] `GET /health` — SQL Server connectivity check
- [ ] Register in `Program.cs`: `app.MapHealthChecks("/health")`
- [ ] Mention in README (useful for Docker/K8s)

### 1.6 LICENSE

- [ ] Add `LICENSE` file (MIT recommended for templates)
- [ ] Enable "Template repository" in GitHub repo settings

---

## Phase 2 — Core APIs (Template Completeness)

> These make the template actually usable for real projects.

### 2.1 Account APIs

| Endpoint | Auth | Status |
|----------|------|--------|
| `POST /api/account/register` | Public | Done |
| `POST /api/account/sign-in` | Public | Done |
| `POST /api/account/refresh-token` | Public | Done |
| `POST /api/account/logout` | JWT | Todo — revoke refresh token |
| `GET /api/account/me` | JWT | Todo — current user profile |
| `POST /api/account/change-password` | JWT | Todo |

### 2.2 User Management APIs

| Endpoint | Auth | Status |
|----------|------|--------|
| `GET /api/user/users` | Admin | Done — add pagination |
| `GET /api/user/users/{id}` | Admin | Todo |
| `PUT /api/user/users/{id}` | Admin | Todo |
| `DELETE /api/user/users/{id}` | Admin | Todo — soft delete |
| `GET /api/user/roles` | Admin | Todo |
| `GET /api/user/profile` | JWT | Todo |
| `PUT /api/user/profile` | JWT | Todo |

### 2.3 Pagination

- [ ] Use existing `PaginatedList<T>` in Domain
- [ ] `GET /api/user/users?page=1&pageSize=10`
- [ ] Paginated `ApiResponse` shape in docs

### 2.4 Authorization Policies

- [ ] Define policies: `AdminOnly`, `UserOrAdmin`
- [ ] Apply consistently on all protected endpoints
- [ ] Document policies in README

---

## Phase 3 — Quality & DevOps (Portfolio Impact)

> Recruiters notice these. High ROI for showcase.

### 3.1 Logging (Serilog)

- [ ] `Serilog.AspNetCore` — structured logging
- [ ] Console + rolling file sinks
- [ ] Log auth events (login success/fail, token refresh)
- [ ] Do not log passwords or tokens

### 3.2 Testing

- [ ] `tests/WebAppTemplate.UnitTests` — services, validators
- [ ] `tests/WebAppTemplate.IntegrationTests` — API with `WebApplicationFactory`
- [ ] Optional: `Testcontainers.MsSql` for real DB in CI
- [ ] Target: meaningful coverage on auth + user flows

### 3.3 CI/CD (GitHub Actions)

- [ ] `.github/workflows/ci.yml`
- [ ] Steps: restore → build → test
- [ ] Run on push to `main` and PRs
- [ ] Add build badge to README

### 3.4 Docker Improvements

- [ ] SQL Server volume (`sqldata`) so data persists across restarts
- [ ] Optional: auto-run migrations on API container start
- [ ] Document host vs container connection strings:
  - Host/SSMS: `localhost,1433`
  - API container: `sqlserver,1433`

---

## Phase 4 — Polish (Stand Out)

> Nice-to-have features that differentiate the template.

### 4.1 API Quality

- [ ] API versioning (`/api/v1/...`)
- [ ] Rate limiting on auth endpoints (login, register)
- [ ] CORS policy (configurable per environment)
- [ ] Correlation ID middleware (request tracing)

### 4.2 Advanced Auth

- [ ] Account lockout after N failed logins
- [ ] Forgot password / reset password flow
- [ ] Email verification (optional — Mailhog in Docker for dev)

### 4.3 Observability

- [ ] OpenTelemetry (optional — strong for DevOps roles)
- [ ] Audit log entity + admin endpoint

### 4.4 Code Cleanup

- [ ] Remove `WeatherForecast` scaffold from API project
- [ ] Fix typo: `Opeation Successfull` → `Operation Successful` in `ApiExtension`
- [ ] Rename `Persistance` → `Persistence` (folder typo)
- [ ] Upgrade AutoMapper (NU1903 vulnerability warning)

---

## Recommended Build Order

```
1. README + LICENSE + seed data
2. Security fixes (auth on endpoints, remove duplicates)
3. Exception middleware + health check
4. Logout + /me + change-password APIs
5. User CRUD + pagination
6. Serilog
7. Unit + integration tests
8. GitHub Actions CI
9. Docker volume + migration docs
10. Advanced features (rate limit, email, audit)
```

---

## Quick Reference — GitHub Repo Settings

| Setting | Value |
|---------|-------|
| Description | Production-ready ASP.NET Core 8 Web API template — Clean Architecture, JWT, EF Core |
| Topics | `aspnet-core`, `clean-architecture`, `jwt-authentication`, `dotnet8`, `web-api`, `entity-framework`, `template` |
| Template repository | Enable |
| Default branch | `main` |

---

## Local Dev Commands

```bash
# Start stack
docker compose up -d

# Migrations (from solution root)
dotnet ef database update --project WebAppTemplate.Infrastructure --startup-project WebAppTemplate.API

# Run API locally (without Docker)
dotnet run --project WebAppTemplate.API

# Swagger
http://localhost:5000/swagger
```

---

*Last updated: reflects DTO/validation/response consistency work on `bugfix/maintain-consistancy-fixes`.*
