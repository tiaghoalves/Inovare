using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovaree.Dominio
{
    public class Lote
    {
        public int LoteId { get; set; }
        public int TipoLoteId { get; set; }
        public string Tipo { get; set; }
        public string Serial { get; set; }
        public int Total { get; set; }
        public StatusLote Status { get; set; }
        public EstadoLote Estado { get; set; }

        #region Flags Leitura
        public bool LoteFinalizado { get; set; }
        #endregion

        public enum StatusLote 
        {
            Recebido = 1,
            EmProducao = 2,
            Finalizando = 3,
            Concluido = 4
        }

        public enum EstadoLote
        {
            Nenhum = -1,
            Invalido = 0,
            Valido = 1
        }

        public enum TipoLote
        {
            Carro = 1,
            Moto = 2
        }

        public List<Blank> Blanks { get; set; }

    }
}
