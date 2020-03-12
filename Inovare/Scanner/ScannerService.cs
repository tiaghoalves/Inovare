using Inovare;
using Inovare.Services;
using System;
using System.IO.Ports;
using System.Threading;

namespace Inovaree.Scanner
{
    public class ScannerService
    {
        public SerialPort serialPort { get; set; }
        public Timer _enableIdentifyRefresh { get; set; }
        private string PortName { get; set; }
        private int BaudRate { get; set; }
        private Parity Parity { get; set; }
        private int DataBits { get; set; }
        private StopBits StopBits { get; set; }
        public FlagsEscrita flagsEscrita { get; set; }
        public FlagsLeitura flagsLeitura { get; set; }
        public bool EnableIdentifyQr { get; set; }

        /// <summary>
        /// Cria uma instancia da classe SerialListener que tenta escutar a porta serial informada com os seguintes parametros:
        ///   BaudRate = 9600
        ///   Parity = None
        ///   DataBits = 8
        ///   StopBits = One
        /// </summary>
        public ScannerService(string portName, FlagsEscrita escrita, FlagsLeitura leitura)
        {
            PortName = portName;
            BaudRate = 115200;
            Parity = Parity.None;
            DataBits = 8;
            StopBits = StopBits.One;
            flagsEscrita = escrita;
            flagsLeitura = leitura;
        }

        /// <summary>
        /// Cria uma instancia da classe SerialListener que tenta escutar uma porta serial conforme os parâmetros informados.
        /// </summary>
        /// <param name="portName">Nome da porta</param>
        /// <param name="baudRate">BaudRate</param>
        /// <param name="parity">Parity</param>
        /// <param name="dataBits">DataBits</param>
        /// <param name="stopBits">StopBits</param>
        public ScannerService(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            PortName = portName;
            BaudRate = baudRate;
            Parity = parity;
            DataBits = dataBits;
            StopBits = stopBits;
        }

        /// <summary>
        /// Abre a porta serial para iniciar a escuta
        /// </summary>
        public SerialPort StartListening(out string status)
        {
            status = string.Empty;

            // Closing serial port if it is open
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }

            // Setting serial port settings
            serialPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
            //_enableIdentifyRefresh = new Timer(new TimerCallback(EnableIdentifyRefresh_Timer), null, 100, 300);
            // Subscribe to event and open serial port for data
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

            try
            {
                serialPort.Open();

                return serialPort;
            }
            catch (Exception ex)
            {
                status = $"Erro ao inicializar Scanner. {ex.Message}";
                //throw;
                return null;
            }
        }

        /// <summary>
        /// Fecha a porta serial para encerrar a escuta
        /// </summary>
        public void StopListening()
        {
            if (serialPort != null)
            {
                serialPort.Close();
            }
        }

        public void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Eof) return;

            if (flagsLeitura.LeituraHabilitada /**|| reprocessFlags.Rereading || EnableIdentifyQr **/)
            {
                int dataLength = serialPort.BytesToRead;
                byte[] data = new byte[dataLength];
                int nbrDataRead = serialPort.Read(data, 0, dataLength);
                if (nbrDataRead == 0) return;

                // String qrcode lido pelo scanner
                flagsLeitura.ScannedCode = Convert.ToBase64String(data).Trim();

                //if (readFlags.EnableRead)
                //{
                //    readFlags.EnableRead = false;
                //    readFlags.ReadAvailable = true;
                //}

                /** Processo de identificação de QR
                if (EnableIdentifyQr)
                {
                    QrCode qrFinded = _superService.SearchScannedCode(readFlags.ScannedCode);

                    if (!(qrFinded is null))
                    {
                        _systemRepo.SetIdentifyQr(true, qrFinded.IdQrCode);
                    }
                }
                **/

                /** Processo de releitura
                if (reprocessFlags.Rereading)
                {
                    // Valida apenas com lista dos 'Não ok' do lote em reprocessamento
                    ReevaluateQrCode(readFlags.ScannedCode, reprocessFlags.SysReadProcess.IdBoxLot);
                }
                **/
            }
            else
            {
                serialPort.DiscardOutBuffer();
                serialPort.DiscardInBuffer();
            }
        }

        /**
        private void ReevaluateQrCode(string scannedCode, int idBoxLot)
        {
            // Busca QR para validar com o codigo lido pelo scanner
            QrCode qrCode = _superService.ReevaluateCode(scannedCode, idBoxLot);

            if (!(qrCode is null))
            {
                // Altera status para Validado
                QrCode updated = _superService.UpdateStatusCode(qrCode.IdQrCode, (int)QrCodeStatus.StatusQr.Valid);
                reprocessFlags.QrCodesNotOkFromLot = _superService.GetAllNotOkFromLot(idBoxLot);

                _superService.SaveStatusHistory(new QrCodeStatusHistory
                {
                    IdQrCode = updated.IdQrCode,
                    IdQrCodeStatus = updated.IdQrCodeStatus,
                    CreatedAt = DateTime.Now.ToLocalTime(),
                    ScannedCode = "RELEITURA"
                });

                if (reprocessFlags.QrCodesNotOkFromLot.Count == 0)
                {
                    _superService.UpdateLotState(idBoxLot, BoxLot.BoxLotState.Valid);
                }
            }

            reprocessFlags.Rereading = false;
            readFlags.ReadAvailable = false;
        }
        **/
    }
}
