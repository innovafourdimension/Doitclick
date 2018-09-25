using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Doitclick.Base
{
    public class BaseController : Controller
    {
        public WorkflowAttributes WF { get; set; }


        public BaseController()
        {
        }

       
    }
}
