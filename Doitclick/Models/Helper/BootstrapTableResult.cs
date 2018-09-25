using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper
{
    public class BootstrapTableResult<T> where T : class
    {
        public int total { get; set; }
        public IEnumerable<T> rows { get; set; }
    }
}
