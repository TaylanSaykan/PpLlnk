using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public static class ApprovalStatus 
    {
        public static string Approved => "Onaylandı.";

        public static string  WaitingApproved => "Onay Bekliyor.";

        public static string Denied => "Reddedildi.";
    }
}
