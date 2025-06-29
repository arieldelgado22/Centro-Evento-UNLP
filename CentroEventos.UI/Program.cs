using CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios;
using CentroEventos.UI.Components;

using (var context = new CentroEventosContext())
{
    context.Database.EnsureCreated();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Repositorios
builder.Services.AddDbContext<CentroEventosContext>();
builder.Services.AddScoped<IRepositorioPersona, RepositorioPersona>();
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivo>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

// Casos de uso
builder.Services.AddTransient<PersonaAltaUseCase>();
builder.Services.AddTransient<PersonaModificacionUseCase>();
builder.Services.AddTransient<PersonaBajaUseCase>();
builder.Services.AddTransient<PersonaListarUseCase>();

builder.Services.AddTransient<ReservaAltaUseCase>();
builder.Services.AddTransient<ReservaListarUseCase>();
builder.Services.AddTransient<ReservaBajaUseCase>();
builder.Services.AddTransient<ReservaModificacionUseCase>();

builder.Services.AddTransient<UsuarioAltaUseCase>();
builder.Services.AddTransient<UsuarioModificacionUseCase>();
builder.Services.AddTransient<UsuarioBajaUseCase>();
builder.Services.AddTransient<UsuarioListarUseCase>();
builder.Services.AddTransient<UsuarioLoginUseCase>();
builder.Services.AddTransient<UsuarioObtenerPorIdUseCase>();

builder.Services.AddTransient<EventoDeportivoAltaUseCase>();
builder.Services.AddTransient<EventoDeportivoModificacionUseCase>();
builder.Services.AddTransient<EventoDeportivoBajaUseCase>();
builder.Services.AddTransient<EventoDeportivoListarUseCase>();

builder.Services.AddTransient<ListarAsistenciaAEventoUseCase>();
builder.Services.AddTransient<ListarEventosConCupoDisponibleUseCase>();

// Servicios
builder.Services.AddTransient<IServicioAutorizacion, ServicioAutorizacion>();
builder.Services.AddTransient<PersonaValidador>();
builder.Services.AddTransient<EventoDeportivoValidador>();
builder.Services.AddTransient<ReservaValidador>();
builder.Services.AddTransient<UsuarioValidador>();
builder.Services.AddScoped<Sesion>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();