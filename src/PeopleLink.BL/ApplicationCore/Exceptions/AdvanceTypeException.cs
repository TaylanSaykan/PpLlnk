using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class AdvanceTypeException : Exception
    {
        public AdvanceTypeException() : base("Hatalı avans türü girdiniz")
        {
            
        }
    }
}
