using System.Collections.Generic;
using System.Linq;
using Dapper;
using Doitclick.Controllers;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
namespace Doitclick.Data.Repository
{

    public interface ILaboratorioRepository
    {
        IEnumerable<IDataResult> Data();
        
    }
    public class LaboratorioRepository:ILaboratorioRepository
    {

        private readonly IConfiguration configuration;
        public LaboratorioRepository(IConfiguration config)
        {
            this.configuration=config;

        }
        public IEnumerable<IDataResult> Data()
        {
            string sql = @"
                        select usr.*, tareas.*
                        from ( select UserName rut, Nombres nombre 
                        from usuarios ) usr
                        inner join (
                        select A.Id id, B.Resumen descr, C.Nombre estado, A.AsignadoA rut
                        from tareas A
                        inner join solicitudes B on a.solicitudid=b.id
                        inner join Etapas C on a.etapaid=c.id
                        where a.estado='Activada' and b.procesoid=1
                        and C.NombreInterno in ('EVALUA_TRABAJO','EJECUCION_DE_TRABAJO')
                        ) tareas on usr.rut = tareas.rut";
            var cousers = new List<IDataResult>();
            var lookup = new Dictionary<string, IDataResult>();
            using (var connection = new MySqlConnection(configuration.GetConnectionString("DoItClickConnection")))
            {	
                connection.Open();
                IDataResult result;
            	var list = connection.Query<IDataResult, ITarea,IDataResult>(sql,(data,tarea) => {
                    if (!lookup.TryGetValue(data.rut, out result)){
                        result = data;
                        if(data.tareas == null){
                            data.tareas = new List<ITarea>();
                        }
                        lookup.Add(result.rut, result);
                    }
                    result.tareas.Add(tarea);
                    return result;
                }).AsQueryable();
                
            }
            return lookup.Values;
        }
    }
}