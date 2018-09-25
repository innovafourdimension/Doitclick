using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Doitclick.Models.Security;
using Doitclick.Models.Workflow;
using Doitclick.Models.Application;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Doitclick.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Rol, string>
    {
        public DbSet<Organizacion> Organizaciones { get; set; }

        public DbSet<Etapa> Etapas { get; set; }
        public DbSet<Proceso> Procesos { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TareaAutomatica> TareasAutomaticas { get; set; }
        public DbSet<Transito> Transiciones { get; set; }
        public DbSet<Variable> Variables { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PrevisionSalud> PrevisionesSalud { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<CuentaCorriente> CuentasCorrientes { get; set; }
        public DbSet<ItemCotizar> ItemsCorizar { get; set; }
        public DbSet<MaterialDisponible> MaterialesDiponibles { get; set; }
        public DbSet<MaterialPresupuestado> MaterialesPresupuestados { get; set; }
        public DbSet<MetaDatosCliente> MetadatosClientes { get; set; }
        public DbSet<MetaDatosContacto> MetadatosContactos { get; set; }
        public DbSet<MovimientoCuentaCorriente> MovimientosCuentasCorrientes { get; set; }
        public DbSet<MovimientoMaterialDisponoble> MovimientosMaterialesDisponibles { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Instrumento> Instrumentos { get; set; }
        public DbSet<MaterialMensual> MaterialesMensuales {get;set;}
        public DbSet<MovimientoMaterialMensual> MovimientosMaterialesMensuales{get;set;}
        public DbSet<TipoUnidadMedida> TiposUnidadMedidas{get;set;}
        public DbSet<Marca> Marcas { get; set; }
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /*Identity*/
            builder.Entity<Usuario>().ToTable("Usuarios");
            builder.Entity<Rol>().ToTable("Roles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("NotificacionesUsuarios");
            builder.Entity<IdentityUserRole<string>>().ToTable("RolesUsuarios");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AccesosUsuarios");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("NotificacionesRoles");
            builder.Entity<IdentityUserToken<string>>().ToTable("TokensUsuarios");

            builder.Entity<Cliente>()
                .Property("PrevisionSaludId")
                .IsRequired(false);
            

            builder.Entity<Organizacion>()
                .HasMany(f => f.Roles)
                .WithOne(f => f.Orzanizacion)
                .IsRequired();

            builder.Entity<Organizacion>()
                .HasOne(f => f.Padre)
                .WithMany(e => e.Hijos)
                .IsRequired(false);

            builder.Entity<Organizacion>()
                .Property(p => p.Nombre)
                .IsRequired();

            builder.Entity<Organizacion>()
                .Property(o => o.TipoOrganizacion)
                .HasConversion(
                    c => c.ToString(),
                    c => (TipoOrganizacion)Enum.Parse(typeof(TipoOrganizacion), c)
                )
                .IsRequired();
                  
            builder.Entity<MaterialDisponible>()
                .HasOne(m => m.UnidadMedida)
                .WithMany(u => u.MAterialDisponible)
                .IsRequired();
                

            /*WF*/
            builder.Entity<Proceso>()
                .HasMany(d => d.Etapas)
                .WithOne(d => d.Proceso)
                .IsRequired();

            builder.Entity<Etapa>()
               .HasMany(d => d.TareasAutomaticas)
               .WithOne(d => d.Etapa);

            builder.Entity<Etapa>()
                .HasMany(d => d.Destinos)
                .WithOne(d => d.EtapaDestino)
                .IsRequired();

            builder.Entity<Etapa>()
                .HasMany(d => d.Actuales)
                .WithOne(d => d.EtapaActaual)
                .IsRequired();

            

            builder.Entity<Etapa>()
                .Property(e => e.TipoUsuarioAsignado)
                .HasConversion(new ValueConverter<TipoUsuarioAsignado, string>(
                    v => v.ToString(),
                    v => (TipoUsuarioAsignado)Enum.Parse(typeof(TipoUsuarioAsignado), v))
                ).IsRequired();

            builder.Entity<TipoUnidadMedida>()
            .HasMany(c => c.MaterialMensual)
            .WithOne(c => c.UnidadMedida)
            .IsRequired();

            builder.Entity<Etapa>()
                .Property(d => d.TipoEtapa)
                .HasConversion( new ValueConverter<TipoEtapa, string>(
                    v => v.ToString(),
                    v => (TipoEtapa)Enum.Parse(typeof(TipoEtapa), v))
                ).IsRequired();

            builder.Entity<Etapa>()
                .Property(d => d.TipoDuracion)
                .HasConversion(new ValueConverter<TipoDuracion, string>(
                    v => v.ToString(),
                    v => (TipoDuracion)Enum.Parse(typeof(TipoDuracion), v))
                );

            builder.Entity<Etapa>()
                .Property(d => d.TipoDuracionRetardo)
                .HasConversion(new ValueConverter<TipoDuracion, string>(
                    v => v.ToString(),
                    v => (TipoDuracion)Enum.Parse(typeof(TipoDuracion), v))
                );

            builder.Entity<Solicitud>()
                .Property(d => d.Estado)
                .HasConversion( new ValueConverter<EstadoSolicitud, string>(
                    v => v.ToString(),
                    v => (EstadoSolicitud)Enum.Parse(typeof(EstadoSolicitud), v))
                )
                .IsRequired();

            builder.Entity<Solicitud>()
                .HasMany(d => d.Tareas)
                .WithOne(d => d.Solicitud)
                .IsRequired();

            builder.Entity<Tarea>()
                .Property(d=>d.Estado)
                .HasConversion(
                new ValueConverter<EstadoTarea, string>(
                    v => v.ToString(),
                    v => (EstadoTarea)Enum.Parse(typeof(EstadoTarea), v))
                )
                .IsRequired();



            builder.Entity<Etapa>()
                .Property(s => s.ValorUsuarioAsignado)
                .IsRequired();

            builder.Entity<Proceso>()
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Entity<Solicitud>()
                .Property(s => s.NumeroTicket)
                .IsRequired();

            builder.Entity<Solicitud>()
                .Property(s => s.FechaTermino)
                .IsRequired(false);

            builder.Entity<Solicitud>()
               .HasIndex(s => s.NumeroTicket)
               .IsUnique();

            builder.Entity<Solicitud>()
                .Property(s => s.InstanciadoPor)
                .IsRequired();

            builder.Entity<Solicitud>()
               .HasIndex(s => s.InstanciadoPor);

            /*App*/
            builder.Entity<Cliente>()
                .HasMany(c => c.MetaDatosCliente)
                .WithOne(m => m.Cliente)
                .IsRequired(false);

            builder.Entity<Cliente>()
                .HasMany(c => c.Cotizaciones)
                .WithOne(m => m.Cliente)
                .IsRequired(false);


            builder.Entity<Cliente>()
                .Property(d=>d.TipoCliente)
                .HasConversion(
                new ValueConverter<TipoCliente, string>(
                    v => v.ToString(),
                    v => (TipoCliente)Enum.Parse(typeof(TipoCliente), v))
                )
                .IsRequired();

            builder.Entity<Contacto>()
                .HasMany(c => c.MetaDatosContacto)
                .WithOne(m => m.Contacto)
                .IsRequired(false);

            builder.Entity<Cotizacion>()
                .HasMany(c => c.ItemsCotizar)
                .WithOne(m => m.Cotizacion)
                .IsRequired(false);

            builder.Entity<CuentaCorriente>()
                .HasMany(c => c.Movimientos)
                .WithOne(m => m.CuentaCorriente)
                .IsRequired(false);

            builder.Entity<CuentaCorriente>()
                .HasOne(d => d.Cliente)
                .WithMany();

            builder.Entity<MaterialDisponible>()
                .HasMany(m => m.Movimientos)
                .WithOne(m => m.MaterialDisponible);


            builder.Entity<MaterialDisponible>()
                .HasMany(m => m.Presupuestado)
                .WithOne(m => m.MaterialDisponible);

            builder.Entity<PrevisionSalud>()
                .HasMany(t => t.Clientes)
                .WithOne(c => c.PrevisionSalud)
                .IsRequired();

            builder.Entity<Tarea>()
                .Property(d => d.Estado)
                .HasMaxLength(200);

            builder.Entity<Solicitud>()
                .Property(s => s.Estado)
                .HasMaxLength(200);
              

        }
    }
}