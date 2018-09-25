using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public enum TipoTransaccionCuentaCorriente
    {
        IngresoPagoEfectivo,
        IngresoPagoCheque,
        IngresoPagoTarjetaCredito,
        IngresoPagoTarjetaDebito,
        SalidaReembolsoEfectivo
    }
}
