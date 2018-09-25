using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class CommitInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialesDiponibles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    UnidadMedida = table.Column<int>(nullable: false),
                    PrecioUnidad = table.Column<int>(nullable: false),
                    StockAlerta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialesDiponibles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    TipoOrganizacion = table.Column<string>(nullable: false),
                    PadreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizaciones_Organizaciones_PadreId",
                        column: x => x.PadreId,
                        principalTable: "Organizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrevisionesSalud",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevisionesSalud", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Procesos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 150, nullable: false),
                    NombreInterno = table.Column<string>(nullable: true),
                    NamespaceGeneraTickets = table.Column<string>(nullable: true),
                    ClaseGeneraTickets = table.Column<string>(nullable: true),
                    MetodoGeneraTickets = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procesos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Resumen = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(nullable: true),
                    ValorManoObra = table.Column<int>(nullable: false),
                    ValorMateriales = table.Column<int>(nullable: false),
                    ValorComision = table.Column<int>(nullable: false),
                    PorcentajeComision = table.Column<int>(nullable: false),
                    ValorTotal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Identificador = table.Column<string>(nullable: true),
                    EstadoCuenta = table.Column<int>(nullable: false),
                    TokenRecuerdaAcceso = table.Column<string>(nullable: true),
                    Eliminado = table.Column<bool>(nullable: false),
                    Nombres = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosMaterialesDisponibles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroTransaccion = table.Column<string>(nullable: true),
                    TipoTransaccion = table.Column<int>(nullable: false),
                    MaterialDisponibleId = table.Column<int>(nullable: true),
                    FechaTransaccion = table.Column<DateTime>(nullable: false),
                    Resumen = table.Column<string>(nullable: true),
                    CantidadMaterial = table.Column<int>(nullable: false),
                    NumeroTicket = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosMaterialesDisponibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosMaterialesDisponibles_MaterialesDiponibles_Materi~",
                        column: x => x.MaterialDisponibleId,
                        principalTable: "MaterialesDiponibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    OrzanizacionId = table.Column<int>(nullable: false),
                    PadreId = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Organizaciones_OrzanizacionId",
                        column: x => x.OrzanizacionId,
                        principalTable: "Organizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_Roles_PadreId",
                        column: x => x.PadreId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Rut = table.Column<string>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    TipoCliente = table.Column<int>(nullable: false),
                    EsPersonalidadJuridica = table.Column<bool>(nullable: false),
                    PrevisionSaludId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_PrevisionesSalud_PrevisionSaludId",
                        column: x => x.PrevisionSaludId,
                        principalTable: "PrevisionesSalud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Etapas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProcesoId = table.Column<int>(nullable: false),
                    TipoEtapa = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    NombreInterno = table.Column<string>(nullable: true),
                    TipoUsuarioAsignado = table.Column<string>(nullable: false),
                    ValorUsuarioAsignado = table.Column<string>(nullable: false),
                    TipoDuracion = table.Column<string>(nullable: true),
                    ValorDuracion = table.Column<string>(nullable: true),
                    TipoDuracionRetardo = table.Column<int>(nullable: true),
                    ValorDuracionRetardo = table.Column<string>(nullable: true),
                    Secuencia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etapas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etapas_Procesos_ProcesoId",
                        column: x => x.ProcesoId,
                        principalTable: "Procesos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroTicket = table.Column<string>(nullable: false),
                    ProcesoId = table.Column<int>(nullable: true),
                    Resumen = table.Column<string>(nullable: true),
                    InstanciadoPor = table.Column<string>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaTermino = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Procesos_ProcesoId",
                        column: x => x.ProcesoId,
                        principalTable: "Procesos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialesPresupuestados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServicioId = table.Column<int>(nullable: true),
                    MaterialDisponibleId = table.Column<int>(nullable: true),
                    CantidadMaterial = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialesPresupuestados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialesPresupuestados_MaterialesDiponibles_MaterialDispon~",
                        column: x => x.MaterialDisponibleId,
                        principalTable: "MaterialesDiponibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialesPresupuestados_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccesosUsuarios",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesosUsuarios", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AccesosUsuarios_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificacionesUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionesUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificacionesUsuarios_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokensUsuarios",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokensUsuarios", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TokensUsuarios_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificacionesRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionesRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificacionesRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesUsuarios",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsuarios", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoContacto = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: true),
                    Resumen = table.Column<string>(nullable: true),
                    EsPrincipal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contactos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cotizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaExpiracion = table.Column<DateTime>(nullable: false),
                    NumeroTicket = table.Column<string>(nullable: true),
                    Resumen = table.Column<string>(nullable: true),
                    PrecioCotizacion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cotizaciones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CuentasCorrientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<string>(nullable: true),
                    ClienteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasCorrientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuentasCorrientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MetadatosClientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadatosClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetadatosClientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TareasAutomaticas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EtapaId = table.Column<int>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EventoDisparador = table.Column<int>(nullable: false),
                    Namespace = table.Column<string>(nullable: true),
                    Clase = table.Column<string>(nullable: true),
                    Metodo = table.Column<string>(nullable: true),
                    Secuencia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasAutomaticas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareasAutomaticas_Etapas_EtapaId",
                        column: x => x.EtapaId,
                        principalTable: "Etapas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transiciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EtapaActaualId = table.Column<int>(nullable: false),
                    EtapaDestinoId = table.Column<int>(nullable: false),
                    NamespaceValidacion = table.Column<string>(nullable: true),
                    ClaseValidacion = table.Column<string>(nullable: true),
                    MetodoValidacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transiciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transiciones_Etapas_EtapaActaualId",
                        column: x => x.EtapaActaualId,
                        principalTable: "Etapas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transiciones_Etapas_EtapaDestinoId",
                        column: x => x.EtapaDestinoId,
                        principalTable: "Etapas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SolicitudId = table.Column<int>(nullable: false),
                    EtapaId = table.Column<int>(nullable: true),
                    AsignadoA = table.Column<string>(nullable: true),
                    ReasignadoA = table.Column<string>(nullable: true),
                    EjecutadoPor = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaTerminoEstimada = table.Column<DateTime>(nullable: true),
                    FechaTerminoFinal = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Etapas_EtapaId",
                        column: x => x.EtapaId,
                        principalTable: "Etapas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tareas_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetadatosContactos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContactoId = table.Column<int>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadatosContactos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetadatosContactos_Contactos_ContactoId",
                        column: x => x.ContactoId,
                        principalTable: "Contactos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCorizar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CotizacionId = table.Column<int>(nullable: true),
                    ServicioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCorizar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCorizar_Cotizaciones_CotizacionId",
                        column: x => x.CotizacionId,
                        principalTable: "Cotizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsCorizar_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosCuentasCorrientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoTransanccion = table.Column<int>(nullable: false),
                    CuentaCorrienteId = table.Column<int>(nullable: true),
                    NumeroTransaccion = table.Column<string>(nullable: true),
                    FechaTransaccion = table.Column<DateTime>(nullable: false),
                    MontoTransaccion = table.Column<long>(nullable: false),
                    Resumen = table.Column<string>(nullable: true),
                    NumeroDocumento = table.Column<string>(nullable: true),
                    NumeroTicket = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosCuentasCorrientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosCuentasCorrientes_CuentasCorrientes_CuentaCorrien~",
                        column: x => x.CuentaCorrienteId,
                        principalTable: "CuentasCorrientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccesosUsuarios_UserId",
                table: "AccesosUsuarios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PrevisionSaludId",
                table: "Clientes",
                column: "PrevisionSaludId");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ClienteId",
                table: "Contactos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_ClienteId",
                table: "Cotizaciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasCorrientes_ClienteId",
                table: "CuentasCorrientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Etapas_ProcesoId",
                table: "Etapas",
                column: "ProcesoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCorizar_CotizacionId",
                table: "ItemsCorizar",
                column: "CotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCorizar_ServicioId",
                table: "ItemsCorizar",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesPresupuestados_MaterialDisponibleId",
                table: "MaterialesPresupuestados",
                column: "MaterialDisponibleId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesPresupuestados_ServicioId",
                table: "MaterialesPresupuestados",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadatosClientes_ClienteId",
                table: "MetadatosClientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadatosContactos_ContactoId",
                table: "MetadatosContactos",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCuentasCorrientes_CuentaCorrienteId",
                table: "MovimientosCuentasCorrientes",
                column: "CuentaCorrienteId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosMaterialesDisponibles_MaterialDisponibleId",
                table: "MovimientosMaterialesDisponibles",
                column: "MaterialDisponibleId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionesRoles_RoleId",
                table: "NotificacionesRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionesUsuarios_UserId",
                table: "NotificacionesUsuarios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizaciones_PadreId",
                table: "Organizaciones",
                column: "PadreId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_OrzanizacionId",
                table: "Roles",
                column: "OrzanizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_PadreId",
                table: "Roles",
                column: "PadreId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsuarios_RoleId",
                table: "RolesUsuarios",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_InstanciadoPor",
                table: "Solicitudes",
                column: "InstanciadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_NumeroTicket",
                table: "Solicitudes",
                column: "NumeroTicket",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ProcesoId",
                table: "Solicitudes",
                column: "ProcesoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_EtapaId",
                table: "Tareas",
                column: "EtapaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_SolicitudId",
                table: "Tareas",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasAutomaticas_EtapaId",
                table: "TareasAutomaticas",
                column: "EtapaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transiciones_EtapaActaualId",
                table: "Transiciones",
                column: "EtapaActaualId");

            migrationBuilder.CreateIndex(
                name: "IX_Transiciones_EtapaDestinoId",
                table: "Transiciones",
                column: "EtapaDestinoId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Usuarios",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Usuarios",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesosUsuarios");

            migrationBuilder.DropTable(
                name: "ItemsCorizar");

            migrationBuilder.DropTable(
                name: "MaterialesPresupuestados");

            migrationBuilder.DropTable(
                name: "MetadatosClientes");

            migrationBuilder.DropTable(
                name: "MetadatosContactos");

            migrationBuilder.DropTable(
                name: "MovimientosCuentasCorrientes");

            migrationBuilder.DropTable(
                name: "MovimientosMaterialesDisponibles");

            migrationBuilder.DropTable(
                name: "NotificacionesRoles");

            migrationBuilder.DropTable(
                name: "NotificacionesUsuarios");

            migrationBuilder.DropTable(
                name: "RolesUsuarios");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "TareasAutomaticas");

            migrationBuilder.DropTable(
                name: "TokensUsuarios");

            migrationBuilder.DropTable(
                name: "Transiciones");

            migrationBuilder.DropTable(
                name: "Cotizaciones");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "CuentasCorrientes");

            migrationBuilder.DropTable(
                name: "MaterialesDiponibles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Etapas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Organizaciones");

            migrationBuilder.DropTable(
                name: "Procesos");

            migrationBuilder.DropTable(
                name: "PrevisionesSalud");
        }
    }
}
