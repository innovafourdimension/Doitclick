using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Services.Notification
{
    public interface INotificationKernel
    {
        void EnviaMail();

        void EnviaPush();
    }
}
