using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpIntroWinForms.IspitIB180028
{
    public class KorisniciPorukeIB180028
    {
        public int Id { get; set; }
        public Korisnik Korisnik { get; set; }
        public string Datum { get; set; }
        public string Sadrzaj { get; set; }
        public byte[] Slika { get; set; }
    }
}
