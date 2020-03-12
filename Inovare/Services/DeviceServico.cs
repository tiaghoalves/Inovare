using System;
using System.Diagnostics;
using Inovare.CLP;

namespace Inovare.Services
{
    public class DeviceServico
    {
        public USBDeviceClass Device { get; set; }

        public byte[] SendCommand(byte CMD, byte NumBytesInfo, byte[] ByteInfo = null)
        {
            return Device.SendCommandUSB(CMD, NumBytesInfo, ByteInfo);
        }

        public bool DeviceStatus()
        {
            return Protocol.HardwareOn;
        }

        public bool GetSendSuccess()
        {
            return Device.SendSuccess;
        }

        public bool StatusFrameIsOk()
        {
            return Device.StatusFrameOk;
        }

        public void FindDevice(int vID, int pID)
        {
            try
            {
                if (Device is null)
                {
                    // Create the USB reference device object (passing VID and PID)
                    Device = new USBDeviceClass(vID, pID);

                    Device.usbEvent += Device_usbEvent;

                    // Perform an initial search for the target device
                    Device.findTargetDevice(); // se caiu a comunicação, tenta re-conectar imediatamente
                }
                else
                {
                    Device.findTargetDevice(); // se caiu a comunicação, tenta re-conectar imediatamente
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Device_usbEvent(object sender, EventArgs e)
        {
            // Atualiza o status da conexão com o dispositivo
            if (Device.isDeviceAttached)
            {
                Protocol.HardwareOn = true;
            }
            else
            {
                Protocol.HardwareOn = false;

                Device.findTargetDevice(); // se caiu a comunicação, tenta re-conectar imediatamente
            }
        }

        public string GetStatusMessage(byte[] frame)
        {
            string statusMessage = "Recepção de dados Tecsistel ativa.";

            byte[] BCC_HL = new byte[2];

            if (Protocol.FrameDadosUSB.End != Protocol.COD_FIM)
            {
                statusMessage = "Falha na recepção - Packet Error.";
            }

            BCC_HL = Device.CalculateCheckSum(frame);

            if ((BCC_HL[1] * 256 + BCC_HL[0]) != Protocol.CheckSum_Recebido)
                statusMessage = "Erro de CheckSum.";

            return statusMessage;
        }

        public byte[] ReadStatus(bool[] hiFlags, bool loFlags)
        {
            byte[] byteInfo = new byte[2];

            byteInfo[0] = ConvertBoolArrayToByte(hiFlags); // HI FLAGS SW
            byteInfo[1] = loFlags ? (byte)1 : (byte)0; // LO FLAGS SW

            byte command = 1;
            byte numBytesInfo = 2;

            return Device.SendCommandUSB(command, numBytesInfo, byteInfo);
        }

        private static byte ConvertBoolArrayToByte(bool[] source)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 8 - source.Length;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (byte)(1 << (7 - index));

                index++;
            }

            return result;
        }

        public byte[] SetupConfigurations(byte[] byteInfo)
        {
            Protocol.FrameDadosUSB.Command = 2;
            Protocol.FrameDadosUSB.NumBytesInfo = 12;
            return Device.SendCommandUSB(Protocol.FrameDadosUSB.Command, Protocol.FrameDadosUSB.NumBytesInfo, byteInfo);
        }

        public byte[] ControlOperationMode(byte[] byteInfo)
        {
            byte command = 3;
            byte numBytesInfo = 2;
            return Device.SendCommandUSB(command, numBytesInfo, byteInfo);
        }

        public byte[] ControlArm(byte[] byteInfo)
        {
            Protocol.FrameDadosUSB.Command = 4;
            Protocol.FrameDadosUSB.NumBytesInfo = 3;
            return Device.SendCommandUSB(Protocol.FrameDadosUSB.Command, Protocol.FrameDadosUSB.NumBytesInfo, byteInfo);
        }

        public byte[] ControlMats(byte[] byteInfo)
        {
            Protocol.FrameDadosUSB.Command = 5;
            Protocol.FrameDadosUSB.NumBytesInfo = 7;
            return Device.SendCommandUSB(Protocol.FrameDadosUSB.Command, Protocol.FrameDadosUSB.NumBytesInfo, byteInfo);
        }

    }
}
