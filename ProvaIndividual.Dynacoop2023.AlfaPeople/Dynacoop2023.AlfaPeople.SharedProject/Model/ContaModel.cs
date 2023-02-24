using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop2023.AlfaPeople.Model
{
    public class ContaModel
    {
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public int PreferenciaContato { get; set; }
        public int NumTotalOpp { get; set; }
        public decimal ValorTotalOpp { get; set; }
        public string IdSegmento { get; set; }
    }
}
