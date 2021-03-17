using DLWMS.WinForms.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLWMS.WinForms.IspitIB180028
{
    public class StudentiPotvrdeIB180028
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public string Datum { get; set; }
        public string Svrha { get; set; }
        public bool Izdata { get; set; }
    }
}
