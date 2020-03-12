
namespace Inovare.CLP
{
    public static class Protocol
    {
        // *******************************************************
        // DEFINICÕES DE ESTRUTURAS DE DADOS / VARIÁVEIS GLOBAIS:
        // *******************************************************
        // -------------------------------------------------------
        // --- COMENTARIOS SOBRE O PROTOCOLO
        // -------------------------------------------------------
        public static byte COD_CAB = 0xA7;         // Codigo do cabeçalho (start frame)
        public static byte COD_FIM = 0x7A;         // Codigo do FIM
        
        // Sendo A INFORMCAO menor ou igual a 16 bytes
        public static byte NUM_MAX_INFORM = 16;

        public static bool FRAME_rec_ok;

        public static int CheckSum_Recebido = 0;

        // Status da conexão usb
        public static bool HardwareOn;
        
        public static EstruturaFrameDadosUSB FrameDadosUSB = new EstruturaFrameDadosUSB();

    }
}
