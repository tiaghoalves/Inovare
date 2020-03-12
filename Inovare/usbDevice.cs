using System;
using usbGenericHidCommunications;
using Inovare.CLP;

namespace Inovare
{
    public class USBDeviceClass : usbGenericHidCommunication
    {
        public const byte CMD_READ_DATA = 11, RESP_READ_DATA = 31;

        public byte CheckSum_HIGH = 0, CheckSum_LOW = 0;

        public static bool EnvioOk = false;

        public bool StatusFrameOk { get; set; }

        public bool SendSuccess { get; set; }

        public USBDeviceClass(int Vid, int Pid) : base(Vid, Pid) { }

        /// <summary>
        ///     Envia comando via USB.
        /// </summary>
        /// <param name="CMD"> Código do comando que está sendo enviado. </param>
        /// <param name="NumBytesInfo"> Número de bytes de informação do pacote. </param>
        /// <param name="ByteInfo"> Vetor com os bytes de informação. </param>
        public byte[] SendCommandUSB(byte CMD, byte NumBytesInfo, byte[] ByteInfo = null)
        {

            // Declare our output buffer
            byte[] outputBuffer = new byte[65];

            // Declare our input buffer
            byte[] inputBuffer = new byte[65];

            byte[] BCC_HL = new byte[2];

            // 6 = START + NUM_INFO + CMD + BCCH + BCCL + END
            byte[] FrameEnvioUSB = new byte[NumBytesInfo + 6 + 1];

            byte[] FrameResposta = new byte[inputBuffer.Length + 1];

            // Monta Pacote
            FrameEnvioUSB[0] = 0;
            FrameEnvioUSB[1] = Protocol.COD_CAB;
            FrameEnvioUSB[2] = NumBytesInfo;
            FrameEnvioUSB[3] = CMD;

            if (NumBytesInfo > 0)
            {
                for (var i = 0; i <= (NumBytesInfo - 1); i++)
                {
                    FrameEnvioUSB[i + 4] = ByteInfo[i];
                }
            }

            FrameEnvioUSB[NumBytesInfo + 6] = Protocol.COD_FIM;

            BCC_HL = CalculateCheckSum(FrameEnvioUSB);
            FrameEnvioUSB[NumBytesInfo + 4] = BCC_HL[1];
            FrameEnvioUSB[NumBytesInfo + 5] = BCC_HL[0];

            // Byte 0 deve ser setado para 0. Não faz parte do protocolo, trata-se de definição da USB
            outputBuffer[0] = 0;

            outputBuffer[1] = FrameEnvioUSB[1]; // StartByte
            outputBuffer[2] = FrameEnvioUSB[2]; // NumBytes no Frame Info
            outputBuffer[3] = FrameEnvioUSB[3]; // Comando
            for (var i = 0; i <= (NumBytesInfo + 2); i++)
            {
                outputBuffer[i + 4] = FrameEnvioUSB[i + 4]; // Bytes Info, Byte CheckSum e Byte End
            }

            // Perform the write command
            SendSuccess = writeRawReportToDevice(outputBuffer);

            TimeoutReceive = 200;               // Configura o timeout de recepção USB para 100 milissegundos
            // Only proceed if the write was successful
            if (SendSuccess)
            {
                // Perform the read
                SendSuccess = readSingleReportFromDevice(ref inputBuffer);
            }

            for (var i = 0; i <= (inputBuffer.Length - 1); i++)
            {
                FrameResposta[i] = inputBuffer[i]; // 0 - requerido pela USB
            }

            EnvioOk = SendSuccess;
            StatusFrameOk = GetStatusFrame(FrameResposta); // Confere se o frame está ok

            return FrameResposta;
        }

        public bool GetStatusFrame(byte[] frame)
        {
            bool statusMessage = true;

            byte[] BCC_HL = new byte[2];


            if (frame[6 + frame[2]] != Protocol.COD_FIM)
            {
                statusMessage = false;
            }

            BCC_HL = CalculateCheckSum(frame);

            if ((BCC_HL[1] * 256 + BCC_HL[0]) != (frame[4 + frame[2]] * 256 + frame[5 + frame[2]]))
                statusMessage = false;

            return statusMessage;
        }

        /// <summary>
        ///     Calcula o checksum do pacote.
        /// </summary>
        /// <param name="FrameDados"> Vetor com os bytes do frame de dados. </param>
        public byte[] CalculateCheckSum(byte[] FrameDados)
        {
            ushort CheckSum = 0;
            byte TamPacote = FrameDados[2];
            TamPacote = Convert.ToByte(TamPacote + 6);

            for (int i = 1; i <= TamPacote; i++)
            {
                CheckSum = (ushort)(CheckSum + FrameDados[i]);
                //CheckSum = (CheckSum) & 0xFFFF;
                if (i == (TamPacote - 3))
                {
                    i = i + 2; //para pular o proprio checksum
                }
            }

            return BitConverter.GetBytes(CheckSum);
        }

    }
}
