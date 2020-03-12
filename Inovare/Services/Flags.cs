using System;
using System.ComponentModel;

namespace Inovare.Services
{
    public class FlagsLeitura
    {

        public DateTime TimeInRead { get; set; }
        // Estado anterior
        public bool UltimoLoteFinalizado { get; set; }
        public bool UltimoImpressaoQrPendente { get; set; }
        public bool UltimoImpressaoQrConcluida  { get; set; }
        public bool UltimoLeituraQrPendente { get; set; }
        public bool UltimoSistemaModoManual { get; set; }

        // LO FLAGS PROCESSO
        public bool ImpressaoQrPendente { get; set; } // Bit4 Impressão QR pendente
        public bool ImpressaoQrConcluida { get; set; } // Bit5 Impressão QR concluida
        public bool LeituraQrPendente { get; set; } // Bit6 Leitura de QR pendente

        // HI FLAGS PROCESSO
        public bool LoteFinalizado { get; set; }   // Bit5 Lote finalizado (indica que finalizou o transporte da ultima placa do lote)
        public bool SistemaModoManual { get; set; } // Bit1 = Sistema em Modo Manual;

        // Flags internas
        public bool FlagReadTimeout { get; set; }
        public bool FlagLoteFinal = false;
        public bool LeituraDisponivel = false;
        public string ScannedCode { get; set; }
        public bool LeituraHabilitada { get; set; }
    }


    public class FlagsEscrita
    {
        // HI FLAGS SW
        public bool ModoManual { get; set; }        // Modo manual via software
        // Bits 1, 2, 3, 4, 5, 6, 7 = false

        // LO FLAGS SW
        public bool QrEnviadoImpressao { get; set; } // QR está carregado p/ impressora laser
        public bool QrLeituraConcluida { get; set; } // Leitura QR concluida
        public bool QrLeituraValida { get; set; }    // QR lido está ok
        public bool TemProximoLote { get; set; }     // Tem lote para produzir
        public bool FinalLote { get; set; }          // Final de lote (indica que a leitura do QR da ultima placa foi realizada)
        public bool LoteValido { get; set; }         // Lote Ok (ultimo lote não teve placas com defeito)

        public FlagsEscrita()
        {
            ModoManual = false;
            QrEnviadoImpressao = false;
            QrLeituraConcluida = false;
            QrLeituraValida = false;
            TemProximoLote = false;
            FinalLote = false;
            LoteValido = false;
        }

        public bool[] GetHiFlags()
        {
            return new bool[] {
                false,
                false,
                LoteValido,
                FinalLote,
                TemProximoLote,
                QrLeituraValida,
                QrLeituraConcluida,
                QrEnviadoImpressao,
            };
        }

        public bool GetLoFlags()
        {
            return ModoManual;
        }
    }
}
