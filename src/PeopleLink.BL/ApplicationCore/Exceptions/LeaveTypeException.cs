using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class LeaveTypeException : Exception
    {
        public LeaveTypeException() : base("Hatalı izin türü girdiniz.")
        {
            
        }
    }
}
