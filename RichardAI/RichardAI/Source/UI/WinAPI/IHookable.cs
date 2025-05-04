using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichardAI.Source.UI.WinAPI
{
    internal interface IHookable // should be implemented by all Hook classes
    { 
        void Hook();
        void Unhook();
    }
}
