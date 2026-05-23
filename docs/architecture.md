# Diagrama de Arquitetura — PlantCare API

## Visão em camadas

```
┌─────────────────────────────────────────────────────────────┐
│                    PlantCare-api (API)                       │
│  Controllers │ Middleware │ Swagger │ Serilog │ Health      │
└──────────────────────────┬──────────────────────────────────┘
                           │ depende de
┌──────────────────────────▼──────────────────────────────────┐
│               PlantCare.Application                            │
│  Services │ DTOs │ Interfaces │ HATEOAS │ Pagination          │
└──────────────────────────┬──────────────────────────────────┘
                           │ depende de
┌──────────────────────────▼──────────────────────────────────┐
│                  PlantCare.Domain                              │
│  Planta │ Usuario │ RegistroCuidado │ Exceptions              │
└─────────────────────────────────────────────────────────────┘
                           ▲
                           │ implementa
┌──────────────────────────┴──────────────────────────────────┐
│              PlantCare.Infrastructure                          │
│  Oracle Repos │ Mongo Repos │ JWT │ PasswordHasher           │
└─────────────────────────────────────────────────────────────┘
```

## Fluxo de uma requisição GET /api/planta

1. `PlantaController` recebe `PlantaQuery` (paginação/filtros)
2. `PlantaService` chama `IPlantaRepository.GetPagedAsync`
3. `PlantaRepository` executa query EF Core no Oracle
4. `LinkBuilderService` monta `PagedResource` com links HATEOAS
5. Resposta JSON retorna ao cliente

## Persistência híbrida

| Dado | Banco | Repositório |
|------|-------|-------------|
| Usuários, Plantas | Oracle (EF Core) | `PlantaRepository`, `UsuarioRepository` |
| Registros de Cuidado | MongoDB | `RegistroCuidadoRepository` |
