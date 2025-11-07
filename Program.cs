using Microsoft.EntityFrameworkCore;
using challenge_3_net.Data;
using challenge_3_net.Repositories;
using challenge_3_net.Repositories.Interfaces;
using challenge_3_net.Services;
using challenge_3_net.Services.Interfaces;
using challenge_3_net.Services.Mapping;
using challenge_3_net.Services.HealthChecks;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar versionamento da API
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver")
    );
    opt.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

// Configurar Application Insights
builder.Services.AddApplicationInsightsTelemetry();

// Configurar Entity Framework (SQL Server)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        options.UseSqlServer(connectionString);
    }
    else
    {
        // Fallback para desenvolvimento local (LocalDB) - substitua pela sua connection string do Azure SQL antes do deploy
        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ChallengeDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
    }
});

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configurar para versionamento
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Sistema de Gestão de Motos API",
        Version = "v1.0",
        Description = "API RESTful para gerenciamento de motos, usuários, operações e status - Versão 1.0",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipe de Desenvolvimento",
            Email = "dev@fiap.com"
        }
    });

    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Sistema de Gestão de Motos API",
        Version = "v2.0",
        Description = "API RESTful para gerenciamento de motos, usuários, operações e status - Versão 2.0 (com ML.NET e JWT)",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipe de Desenvolvimento",
            Email = "dev@fiap.com"
        }
    });

    // Incluir comentários XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Configurar esquemas de segurança JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\""
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // Configurar exemplos de payloads
    // c.EnableAnnotations(); // Comentado pois não está disponível na versão atual
});

// Configurar injeção de dependência - Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IOperacaoRepository, OperacaoRepository>();
builder.Services.AddScoped<IStatusMotoRepository, StatusMotoRepository>();

// Configurar injeção de dependência - Serviços
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IOperacaoService, OperacaoService>();
builder.Services.AddScoped<IStatusMotoService, StatusMotoService>();

// Configurar serviços de autenticação
builder.Services.AddScoped<challenge_3_net.Services.Auth.JwtService>();

// Configurar serviços de ML
builder.Services.AddScoped<challenge_3_net.Services.ML.MotoAnalysisService>();

// Configurar autenticação JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? "TrackZone_Super_Secret_Key_2024_Advanced_Business_Development_With_DotNet";
var issuer = jwtSettings["Issuer"] ?? "TrackZoneAPI";
var audience = jwtSettings["Audience"] ?? "TrackZoneUsers";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Configurar autorização
builder.Services.AddAuthorization();

// Configurar HttpContextAccessor para HATEOAS
builder.Services.AddHttpContextAccessor();

// Configurar Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database", tags: new[] { "database" })
    .AddCheck<MemoryHealthCheck>("memory", tags: new[] { "memory" });

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Log de inicialização
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("=== Iniciando aplicação ===");
logger.LogInformation("Ambiente: {Environment}", app.Environment.EnvironmentName);
logger.LogInformation("Connection String presente: {HasConnectionString}", 
    !string.IsNullOrEmpty(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure the HTTP request pipeline.
// Habilitar Swagger em todos os ambientes para facilitar testes
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Gestão de Motos API v1.0");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Sistema de Gestão de Motos API v2.0");
    if (app.Environment.IsDevelopment())
    {
        c.RoutePrefix = string.Empty; // Para acessar o Swagger na raiz apenas em dev
    }
    c.DocumentTitle = "Sistema de Gestão de Motos API - Versões";
    c.DisplayRequestDuration();
    c.EnableDeepLinking();
    c.EnableFilter();
    c.ShowExtensions();
    c.EnableValidator();
    c.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

// Configurar Health Checks endpoints
app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("database")
});

app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => false
});

// Endpoint de health check detalhado
app.MapGet("/health", (IConfiguration config) => {
    try 
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        
        return Results.Ok(new { 
            Status = "Healthy", 
            Timestamp = DateTime.UtcNow,
            Environment = app.Environment.EnvironmentName,
            HasConnectionString = !string.IsNullOrEmpty(connectionString),
            ConnectionStringLength = connectionString?.Length ?? 0,
            EnvironmentVariables = new {
                DB_SERVER = Environment.GetEnvironmentVariable("DB_SERVER"),
                DB_DATABASE = Environment.GetEnvironmentVariable("DB_DATABASE"),
                DB_USERNAME = Environment.GetEnvironmentVariable("DB_USERNAME"),
                HasPassword = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_PASSWORD")),
                ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            }
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { 
            Status = "Error", 
            Error = ex.Message,
            Timestamp = DateTime.UtcNow 
        });
    }
});

// Endpoint de diagnóstico do banco
app.MapGet("/health/database", async (ApplicationDbContext context) => {
    try 
    {
        var canConnect = await context.Database.CanConnectAsync();
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
        
        return Results.Ok(new { 
            Status = canConnect ? "Connected" : "Cannot Connect",
            CanConnect = canConnect,
            PendingMigrations = pendingMigrations,
            AppliedMigrations = appliedMigrations,
            Timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { 
            Status = "Database Error", 
            Error = ex.Message,
            InnerError = ex.InnerException?.Message,
            Timestamp = DateTime.UtcNow 
        });
    }
});

// Endpoint para verificar dados no banco
app.MapGet("/admin/data", async (ApplicationDbContext context) => {
    try 
    {
        var usuarios = await context.Usuarios.CountAsync();
        var motos = await context.Motos.CountAsync();
        var operacoes = await context.Operacoes.CountAsync();
        var statusMotos = await context.StatusMotos.CountAsync();
        
        return Results.Ok(new { 
            Status = "Success",
            DataCounts = new {
                Usuarios = usuarios,
                Motos = motos,
                Operacoes = operacoes,
                StatusMotos = statusMotos
            },
            Timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { 
            Status = "Error", 
            Error = ex.Message,
            InnerError = ex.InnerException?.Message,
            Timestamp = DateTime.UtcNow 
        });
    }
});

// Configurar banco de dados com tratamento de erro
var dbLogger = app.Services.GetRequiredService<ILogger<Program>>();
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Verificar se o banco está acessível (as tabelas já foram criadas pelo script SQL)
        var canConnect = await context.Database.CanConnectAsync();
        if (canConnect)
        {
            dbLogger.LogInformation("Banco de dados conectado com sucesso!");
            
            // Verificar se existem dados de teste
            var usuarioCount = await context.Usuarios.CountAsync();
            dbLogger.LogInformation("Usuários encontrados no banco: {Count}", usuarioCount);
        }
        else
        {
            dbLogger.LogWarning("Não foi possível conectar ao banco de dados.");
        }
    }
}
catch (Exception ex)
{
    dbLogger.LogError(ex, "Erro ao configurar o banco de dados durante a inicialização");
    // Não parar a aplicação por causa do banco - deixar continuar e tratar nos endpoints
}

// Endpoint para testar diretamente os dados dos usuários
app.MapGet("/debug/usuarios", async (ApplicationDbContext context) => {
    try 
    {
        var usuarios = await context.Usuarios.Take(5).ToListAsync();
        return Results.Ok(new { 
            Status = "Success",
            Count = usuarios.Count,
            Usuarios = usuarios.Select(u => new {
                u.Id,
                u.NomeFilial,
                u.Email,
                u.Cnpj,
                u.Perfil,
                u.DataCriacao
            }),
            Timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { 
            Status = "Error", 
            Error = ex.Message,
            InnerError = ex.InnerException?.Message,
            StackTrace = ex.StackTrace?.Split('\n').Take(5),
            Timestamp = DateTime.UtcNow 
        });
    }
});

app.Run();

// Expor a classe Program para testes
public partial class Program { }
