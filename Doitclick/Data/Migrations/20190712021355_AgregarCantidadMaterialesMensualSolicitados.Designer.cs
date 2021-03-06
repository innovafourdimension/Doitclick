﻿// <auto-generated />
using System;
using Doitclick.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doitclick.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190712021355_AgregarCantidadMaterialesMensualSolicitados")]
    partial class AgregarCantidadMaterialesMensualSolicitados
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Doitclick.Models.Application.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("EntidadFacturacionId");

                    b.Property<bool>("EsPersonalidadJuridica");

                    b.Property<string>("Nombres");

                    b.Property<int?>("PrevisionSaludId");

                    b.Property<string>("Rut");

                    b.Property<string>("TipoCliente")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EntidadFacturacionId");

                    b.HasIndex("PrevisionSaludId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Contacto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClienteId");

                    b.Property<bool>("EsPrincipal");

                    b.Property<string>("Resumen");

                    b.Property<int>("TipoContacto");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Contactos");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Cotizacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClienteId");

                    b.Property<string>("DrSolicitante");

                    b.Property<bool>("EsOT");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaExpiracion");

                    b.Property<string>("FolioSolicitante");

                    b.Property<string>("ImagenOrdenSolicitante");

                    b.Property<string>("NumeroTicket");

                    b.Property<int>("PrecioCotizacion");

                    b.Property<string>("Resumen");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Cotizaciones");
                });

            modelBuilder.Entity("Doitclick.Models.Application.CotizacionExterno", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("DireccionRetiro");

                    b.Property<string>("EntidadSolicitante");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<string>("NombrePaciente");

                    b.Property<string>("NumeroTicket");

                    b.Property<string>("OrdenFolio");

                    b.Property<string>("OrdenImagen");

                    b.Property<float>("PrecioTotal");

                    b.Property<bool>("RequiereRetiro");

                    b.Property<string>("Resumen")
                        .HasMaxLength(500);

                    b.Property<string>("RutPaciente")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("CotizacionesExternos");
                });

            modelBuilder.Entity("Doitclick.Models.Application.CuentaCorriente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClienteId");

                    b.Property<string>("Numero");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("CuentasCorrientes");
                });

            modelBuilder.Entity("Doitclick.Models.Application.EntidadFacturacion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Direccion");

                    b.Property<string>("Giro");

                    b.Property<string>("RazonSocial");

                    b.Property<string>("Rut");

                    b.Property<string>("Telefono");

                    b.HasKey("Id");

                    b.ToTable("EntidadesFacturacion");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Factura", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<bool>("Cerrada");

                    b.Property<string>("CotizacionFacturaId");

                    b.Property<DateTime>("FechaFacturacion");

                    b.Property<Guid?>("PagadorFacturaId");

                    b.Property<float>("ValorFactura");

                    b.HasKey("Id");

                    b.HasIndex("CotizacionFacturaId");

                    b.HasIndex("PagadorFacturaId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Instrumento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activa");

                    b.Property<string>("Codigo");

                    b.Property<string>("Descripcion");

                    b.Property<string>("Estado");

                    b.Property<int?>("MarcaId");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.ToTable("Instrumentos");
                });

            modelBuilder.Entity("Doitclick.Models.Application.ItemCotizar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cantidad");

                    b.Property<string>("CotizacionExternoId");

                    b.Property<int?>("CotizacionId");

                    b.Property<string>("Descripcion");

                    b.Property<int?>("ServicioId");

                    b.HasKey("Id");

                    b.HasIndex("CotizacionExternoId");

                    b.HasIndex("CotizacionId");

                    b.HasIndex("ServicioId");

                    b.ToTable("ItemsCorizar");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activa");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MaterialDisponible", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activa");

                    b.Property<string>("Codigo");

                    b.Property<int?>("MarcaId");

                    b.Property<string>("Nombre");

                    b.Property<int>("PrecioUnidad");

                    b.Property<int>("StockAlerta");

                    b.Property<int?>("UnidadMedidaId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.HasIndex("UnidadMedidaId");

                    b.ToTable("MaterialesDiponibles");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MaterialMensual", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activa");

                    b.Property<int>("Cantidad");

                    b.Property<string>("Codigo");

                    b.Property<string>("Descripcion");

                    b.Property<int?>("MarcaId");

                    b.Property<string>("Nombre");

                    b.Property<int>("StockAlerta");

                    b.Property<int?>("UnidadMedidaId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.HasIndex("UnidadMedidaId");

                    b.ToTable("MaterialesMensuales");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MaterialMensualSolicitado", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CantidadMateriales");

                    b.Property<int?>("MaterialMensualId");

                    b.Property<string>("SolicitudMaterialMensualId");

                    b.HasKey("Id");

                    b.HasIndex("MaterialMensualId");

                    b.HasIndex("SolicitudMaterialMensualId");

                    b.ToTable("MaterialesMensualesSolicitados");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MaterialPresupuestado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CantidadMaterial");

                    b.Property<int?>("MaterialDisponibleId");

                    b.Property<int?>("ServicioId");

                    b.HasKey("Id");

                    b.HasIndex("MaterialDisponibleId");

                    b.HasIndex("ServicioId");

                    b.ToTable("MaterialesPresupuestados");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MetaDatosCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Clave");

                    b.Property<int?>("ClienteId");

                    b.Property<int>("Orden");

                    b.Property<string>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("MetadatosClientes");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MetaDatosContacto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Clave");

                    b.Property<int?>("ContactoId");

                    b.Property<int>("Orden");

                    b.Property<string>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ContactoId");

                    b.ToTable("MetadatosContactos");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MovimientoCuentaCorriente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CuentaCorrienteId");

                    b.Property<DateTime>("FechaTransaccion");

                    b.Property<long>("MontoTransaccion");

                    b.Property<string>("NumeroDocumento");

                    b.Property<string>("NumeroTicket");

                    b.Property<string>("NumeroTransaccion");

                    b.Property<string>("Resumen");

                    b.Property<int>("TipoTransanccion");

                    b.HasKey("Id");

                    b.HasIndex("CuentaCorrienteId");

                    b.ToTable("MovimientosCuentasCorrientes");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MovimientoMaterialDisponoble", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CantidadMaterial");

                    b.Property<DateTime>("FechaTransaccion");

                    b.Property<int?>("MaterialDisponibleId");

                    b.Property<string>("NumeroTicket");

                    b.Property<string>("NumeroTransaccion");

                    b.Property<string>("Resumen");

                    b.Property<int>("TipoTransaccion");

                    b.HasKey("Id");

                    b.HasIndex("MaterialDisponibleId");

                    b.ToTable("MovimientosMaterialesDisponibles");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MovimientoMaterialMensual", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cantidad");

                    b.Property<string>("Estado");

                    b.Property<DateTime>("FechaEstado");

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<string>("Folio");

                    b.Property<int?>("MaterialId");

                    b.Property<string>("RutSolicitante");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("MovimientosMaterialesMensuales");
                });

            modelBuilder.Entity("Doitclick.Models.Application.PrevisionSalud", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activa");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("PrevisionesSalud");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Servicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activa");

                    b.Property<string>("Codigo");

                    b.Property<string>("Nombre");

                    b.Property<int>("PorcentajeComision");

                    b.Property<string>("Resumen");

                    b.Property<int>("ValorComision");

                    b.Property<int>("ValorCosto");

                    b.Property<int>("ValorManoObra");

                    b.Property<int>("ValorMateriales");

                    b.Property<int>("ValorTotal");

                    b.HasKey("Id");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("Doitclick.Models.Application.SolicitudMaterialMensual", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comentarios");

                    b.Property<string>("ComentariosFin");

                    b.Property<string>("Estado");

                    b.Property<DateTime>("FechaFinalizacion");

                    b.Property<DateTime>("FechaSolicitud");

                    b.HasKey("Id");

                    b.ToTable("SolicitudesMaterialesMensuales");
                });

            modelBuilder.Entity("Doitclick.Models.Application.TipoUnidadMedida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activa");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("TiposUnidadMedidas");
                });

            modelBuilder.Entity("Doitclick.Models.Security.Organizacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<int?>("PadreId");

                    b.Property<string>("TipoOrganizacion")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PadreId");

                    b.ToTable("Organizaciones");
                });

            modelBuilder.Entity("Doitclick.Models.Security.Rol", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<bool>("Comisionista");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<int?>("OrzanizacionId")
                        .IsRequired();

                    b.Property<string>("PadreId");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.HasIndex("OrzanizacionId");

                    b.HasIndex("PadreId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Doitclick.Models.Security.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<bool>("Eliminado");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("EstadoCuenta");

                    b.Property<string>("Identificador");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nombres");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<float>("PorcentajeComision");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("TokenRecuerdaAcceso");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Etapa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Enlace");

                    b.Property<string>("Nombre");

                    b.Property<string>("NombreInterno");

                    b.Property<int?>("ProcesoId")
                        .IsRequired();

                    b.Property<int>("Secuencia");

                    b.Property<string>("TipoDuracion")
                        .IsRequired();

                    b.Property<string>("TipoDuracionRetardo")
                        .IsRequired();

                    b.Property<string>("TipoEtapa")
                        .IsRequired();

                    b.Property<string>("TipoUsuarioAsignado")
                        .IsRequired();

                    b.Property<string>("ValorDuracion");

                    b.Property<string>("ValorDuracionRetardo");

                    b.Property<string>("ValorUsuarioAsignado")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProcesoId");

                    b.ToTable("Etapas");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Proceso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<string>("ClaseGeneraTickets");

                    b.Property<string>("MetodoGeneraTickets");

                    b.Property<string>("NamespaceGeneraTickets");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("NombreInterno");

                    b.HasKey("Id");

                    b.ToTable("Procesos");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Solicitud", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("FechaInicio");

                    b.Property<DateTime?>("FechaTermino");

                    b.Property<string>("InstanciadoPor")
                        .IsRequired();

                    b.Property<string>("NumeroTicket")
                        .IsRequired();

                    b.Property<int?>("ProcesoId");

                    b.Property<string>("Resumen");

                    b.HasKey("Id");

                    b.HasIndex("InstanciadoPor");

                    b.HasIndex("NumeroTicket")
                        .IsUnique();

                    b.HasIndex("ProcesoId");

                    b.ToTable("Solicitudes");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Tarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AsignadoA");

                    b.Property<string>("EjecutadoPor");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("EtapaId");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<DateTime?>("FechaTerminoEstimada");

                    b.Property<DateTime?>("FechaTerminoFinal");

                    b.Property<string>("ReasignadoA");

                    b.Property<int?>("SolicitudId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EtapaId");

                    b.HasIndex("SolicitudId");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.TareaAutomatica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Clase");

                    b.Property<string>("Descripcion");

                    b.Property<int?>("EtapaId");

                    b.Property<int>("EventoDisparador");

                    b.Property<string>("Metodo");

                    b.Property<string>("Namespace");

                    b.Property<int>("Secuencia");

                    b.HasKey("Id");

                    b.HasIndex("EtapaId");

                    b.ToTable("TareasAutomaticas");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Transito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaseValidacion");

                    b.Property<int?>("EtapaActaualId")
                        .IsRequired();

                    b.Property<int?>("EtapaDestinoId")
                        .IsRequired();

                    b.Property<string>("MetodoValidacion");

                    b.Property<string>("NamespaceValidacion");

                    b.HasKey("Id");

                    b.HasIndex("EtapaActaualId");

                    b.HasIndex("EtapaDestinoId");

                    b.ToTable("Transiciones");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Variable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Clave");

                    b.Property<string>("NumeroTicket");

                    b.Property<string>("Tipo");

                    b.Property<string>("Valor");

                    b.HasKey("Id");

                    b.ToTable("Variables");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("NotificacionesRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("NotificacionesUsuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AccesosUsuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolesUsuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("TokensUsuarios");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Cliente", b =>
                {
                    b.HasOne("Doitclick.Models.Application.EntidadFacturacion", "EntidadFacturacion")
                        .WithMany()
                        .HasForeignKey("EntidadFacturacionId");

                    b.HasOne("Doitclick.Models.Application.PrevisionSalud", "PrevisionSalud")
                        .WithMany("Clientes")
                        .HasForeignKey("PrevisionSaludId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Contacto", b =>
                {
                    b.HasOne("Doitclick.Models.Application.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Cotizacion", b =>
                {
                    b.HasOne("Doitclick.Models.Application.Cliente", "Cliente")
                        .WithMany("Cotizaciones")
                        .HasForeignKey("ClienteId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.CuentaCorriente", b =>
                {
                    b.HasOne("Doitclick.Models.Application.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Factura", b =>
                {
                    b.HasOne("Doitclick.Models.Application.CotizacionExterno", "CotizacionFactura")
                        .WithMany()
                        .HasForeignKey("CotizacionFacturaId");

                    b.HasOne("Doitclick.Models.Application.EntidadFacturacion", "PagadorFactura")
                        .WithMany()
                        .HasForeignKey("PagadorFacturaId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.Instrumento", b =>
                {
                    b.HasOne("Doitclick.Models.Application.Marca", "Marca")
                        .WithMany("Instrumento")
                        .HasForeignKey("MarcaId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.ItemCotizar", b =>
                {
                    b.HasOne("Doitclick.Models.Application.CotizacionExterno", "CotizacionExterno")
                        .WithMany("ItemsCotizar")
                        .HasForeignKey("CotizacionExternoId");

                    b.HasOne("Doitclick.Models.Application.Cotizacion", "Cotizacion")
                        .WithMany("ItemsCotizar")
                        .HasForeignKey("CotizacionId");

                    b.HasOne("Doitclick.Models.Application.Servicio", "Servicio")
                        .WithMany("ServiciosCotizar")
                        .HasForeignKey("ServicioId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MaterialDisponible", b =>
                {
                    b.HasOne("Doitclick.Models.Application.Marca", "Marca")
                        .WithMany("MaterialDiponible")
                        .HasForeignKey("MarcaId");

                    b.HasOne("Doitclick.Models.Application.TipoUnidadMedida", "UnidadMedida")
                        .WithMany("MAterialDisponible")
                        .HasForeignKey("UnidadMedidaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doitclick.Models.Application.MaterialMensual", b =>
                {
                    b.HasOne("Doitclick.Models.Application.Marca", "Marca")
                        .WithMany("MaterialMensual")
                        .HasForeignKey("MarcaId");

                    b.HasOne("Doitclick.Models.Application.TipoUnidadMedida", "UnidadMedida")
                        .WithMany("MaterialMensual")
                        .HasForeignKey("UnidadMedidaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doitclick.Models.Application.MaterialMensualSolicitado", b =>
                {
                    b.HasOne("Doitclick.Models.Application.MaterialMensual", "MaterialMensual")
                        .WithMany()
                        .HasForeignKey("MaterialMensualId");

                    b.HasOne("Doitclick.Models.Application.SolicitudMaterialMensual", "SolicitudMaterialMensual")
                        .WithMany()
                        .HasForeignKey("SolicitudMaterialMensualId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MaterialPresupuestado", b =>
                {
                    b.HasOne("Doitclick.Models.Application.MaterialDisponible", "MaterialDisponible")
                        .WithMany("Presupuestado")
                        .HasForeignKey("MaterialDisponibleId");

                    b.HasOne("Doitclick.Models.Application.Servicio", "Servicio")
                        .WithMany("MaterialesPresupuestados")
                        .HasForeignKey("ServicioId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MetaDatosCliente", b =>
                {
                    b.HasOne("Doitclick.Models.Application.Cliente", "Cliente")
                        .WithMany("MetaDatosCliente")
                        .HasForeignKey("ClienteId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MetaDatosContacto", b =>
                {
                    b.HasOne("Doitclick.Models.Application.Contacto", "Contacto")
                        .WithMany("MetaDatosContacto")
                        .HasForeignKey("ContactoId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MovimientoCuentaCorriente", b =>
                {
                    b.HasOne("Doitclick.Models.Application.CuentaCorriente", "CuentaCorriente")
                        .WithMany("Movimientos")
                        .HasForeignKey("CuentaCorrienteId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MovimientoMaterialDisponoble", b =>
                {
                    b.HasOne("Doitclick.Models.Application.MaterialDisponible", "MaterialDisponible")
                        .WithMany("Movimientos")
                        .HasForeignKey("MaterialDisponibleId");
                });

            modelBuilder.Entity("Doitclick.Models.Application.MovimientoMaterialMensual", b =>
                {
                    b.HasOne("Doitclick.Models.Application.MaterialMensual", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId");
                });

            modelBuilder.Entity("Doitclick.Models.Security.Organizacion", b =>
                {
                    b.HasOne("Doitclick.Models.Security.Organizacion", "Padre")
                        .WithMany("Hijos")
                        .HasForeignKey("PadreId");
                });

            modelBuilder.Entity("Doitclick.Models.Security.Rol", b =>
                {
                    b.HasOne("Doitclick.Models.Security.Organizacion", "Orzanizacion")
                        .WithMany("Roles")
                        .HasForeignKey("OrzanizacionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doitclick.Models.Security.Rol", "Padre")
                        .WithMany("Hijos")
                        .HasForeignKey("PadreId");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Etapa", b =>
                {
                    b.HasOne("Doitclick.Models.Workflow.Proceso", "Proceso")
                        .WithMany("Etapas")
                        .HasForeignKey("ProcesoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Solicitud", b =>
                {
                    b.HasOne("Doitclick.Models.Workflow.Proceso", "Proceso")
                        .WithMany("Solicitudes")
                        .HasForeignKey("ProcesoId");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Tarea", b =>
                {
                    b.HasOne("Doitclick.Models.Workflow.Etapa", "Etapa")
                        .WithMany("Tareas")
                        .HasForeignKey("EtapaId");

                    b.HasOne("Doitclick.Models.Workflow.Solicitud", "Solicitud")
                        .WithMany("Tareas")
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.TareaAutomatica", b =>
                {
                    b.HasOne("Doitclick.Models.Workflow.Etapa", "Etapa")
                        .WithMany("TareasAutomaticas")
                        .HasForeignKey("EtapaId");
                });

            modelBuilder.Entity("Doitclick.Models.Workflow.Transito", b =>
                {
                    b.HasOne("Doitclick.Models.Workflow.Etapa", "EtapaActaual")
                        .WithMany("Actuales")
                        .HasForeignKey("EtapaActaualId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doitclick.Models.Workflow.Etapa", "EtapaDestino")
                        .WithMany("Destinos")
                        .HasForeignKey("EtapaDestinoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Doitclick.Models.Security.Rol")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Doitclick.Models.Security.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Doitclick.Models.Security.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Doitclick.Models.Security.Rol")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Doitclick.Models.Security.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Doitclick.Models.Security.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
