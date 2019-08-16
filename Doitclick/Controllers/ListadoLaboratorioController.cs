using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Doitclick.Models.Application;
using Microsoft.EntityFrameworkCore;
using Doitclick.Data.Repository;

namespace Doitclick.Controllers
{
   
    public class ListadoLaboratorioController : Controller
    {
         private readonly ApplicationDbContext _context;
         private readonly ILaboratorioRepository _list; 
        public ListadoLaboratorioController(ApplicationDbContext context, ILaboratorioRepository list )
        {
            _context = context;
            _list = list;
        }
          public IActionResult Listado()
        {
            return View();
        }
        
        public IEnumerable<IDataResult> Data()
        {
           
            
            return _list.Data();
        }
       
    }

    public class IDataResult
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public IList<ITarea> tareas { get; set; }
    }
    public class ITarea
    {
        public string id { get; set; }
         public string descr { get; set; }
         public string estado { get; set; }

    }
    
}