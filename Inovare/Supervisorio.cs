using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inovare.Services;
using Inovaree.Dominio;
using Inovaree.Laser;
using Inovaree.Scanner;
using Inovaree.Utils;
using Newtonsoft.Json;

namespace Inovare
{
    public partial class frmSupervisorio : Form
    {
        private DeviceServico _deviceService = new DeviceServico();
        public FlagsEscrita FlagsEscrita { get; set; }
        public FlagsLeitura FlagsLeitura { get; set; }
        public ScannerService _scannerService { get; set; }
        public Configs Configs { get; set; }
        public string NetworkPath { get; set; }
        public int TimeoutLeitura { get; set; }
        public FileSystemWatcher AccessFolderWatcher { get; set; }
        public NetworkCredential Credentials { get; set; }
        public CancellationTokenSource TaskController { get; set; }
        public CancellationToken Token { get; set; }
        public Lote LoteTeste { get; set; }
        public Lote LoteEmProducao { get; set; }
        public List<Lote> LotesEnfileirados { get; set; }
        public Queue FilaLotes { get; set; }

        bool bolEmProducao = false;

        public frmSupervisorio()
        {
            InitializeComponent();
            FlagsEscrita = new FlagsEscrita();
            FlagsLeitura = new FlagsLeitura();
            
            if (File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "configs.json")))
            {
                Configs = JsonConvert.DeserializeObject<Configs>(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "configs.json")));
            }

            if (!(Configs is null))
            {
                Credentials = new NetworkCredential(Configs.Username, Configs.Password, Configs.Domain);
                NetworkPath = Configs.NetworkPath;
                TimeoutLeitura = Configs.TimeoutRead;
            }

            #region Scanner
            _scannerService = new ScannerService(Configs.ScannerSerialPort, FlagsEscrita, FlagsLeitura);
            _scannerService.StartListening(out string statusScanner);

            if (statusScanner != string.Empty)
            {
                //GravarLog(statusScanner);
            }
            #endregion
        }

        private void frmSupervisorio_Load(object sender, EventArgs e)
        {
            FilaLotes = CriarFilaLotes(new Queue());

            LotesEnfileirados = FilaLotes.OfType<Lote>().ToList();
            InicializaConexaoLaser();
            CancelaProcesso();

            TaskController = new CancellationTokenSource();
            Token = TaskController.Token;

            this.label1.Text = "";
        }

        private Queue CriarFilaLotes(Queue filaLotes)
        {
            filaLotes.Enqueue(new Lote { LoteId = 1, Serial = "2400000001234", Total = 50, Estado = Lote.EstadoLote.Nenhum,
                Blanks = new List<Blank> {
                    new Blank { BlankId=1, Code="XYou7QUAEQBHMEUCIB50SNrhMvojXV5RO1Ayt0jCYNxEUkvZF+AqoOGCx7L1AiEAolsksP3TD2joHTsci42ZVG0fsB+hJC+PhqAmEqHY8yIAAAAMRZQZZVQQZRQXUSWA", Serial="200295012701248", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\1.png", },
                    new Blank { BlankId=2, Code="XYou7AUAEQBHMEUCIQCH5COxvff8kG+l8CP6nFKcETQv1/cUwQeC+xKPsFHugQIgNTr7itHwtLx9zE+mhKvKhIPI5B3/IvzI/JnfLqEam3YAAAAMRZQZZVQQZRQXUSVA", Serial="200295012701249", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\2.png", },
                    new Blank { BlankId=3, Code="XYou7AUAEQBGMEQCIB+QxT2LNaG9DNp0bOa4XnMpVvKpWwLMaCwbKu/eBLzpAiA8lieaObpZQpp+wy570GtCuuedDhmlyiZ0Egf8i89gsQAAAAxFlBllVBBlFBdRJQA=", Serial="200295012701250", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\3.png", },
                    new Blank { BlankId=4, Code="XYou7AUAEQBHMEUCIQCErexBlMij2KZoPq53dHjDRrgDJ1zGgBCyE2CDtcpQrQIgWoPu0zt5Zj26Ho5pAncF1NFSERqCceYjBP68umoNiGEAAAAMRZQZZVQQZRQXUSTA", Serial="200295012701251", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\4.png", },
                    new Blank { BlankId=5, Code="XYou6wUAEQBGMEQCIEkrEnZ6ywQKBvS+xAAupSq8s6R4ARR8Q2AGLQmqCIxUAiAMTr50NWG4O0MJsVU3Oiwpc8A1IXsDtJhw2zfQCfhZRwAAAAxFlBllVBBlFBdRJIA=", Serial="200295012701252", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\5.png", },
                }
            });

            filaLotes.Enqueue(new Lote { LoteId = 2, Serial = "2400000004321", Total = 50, Estado = Lote.EstadoLote.Nenhum, 
                Blanks = new List<Blank>
                {
                    new Blank { BlankId=6, Code="XYou7QUAEQBHMEUCIB50SNrhMvojXV5RO1Ayt0jCYNxEUkvZF+AqoOGCx7L1AiEAolsksP3TD2joHTsci42ZVG0fsB+hJC+PhqAmEqHY8yIAAAAMRZQZZVQQZRQXUSWA", Serial="200295012697265", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\6.png" },
                    new Blank { BlankId=7, Code="XYou7AUAEQBHMEUCIQCH5COxvff8kG+l8CP6nFKcETQv1/cUwQeC+xKPsFHugQIgNTr7itHwtLx9zE+mhKvKhIPI5B3/IvzI/JnfLqEam3YAAAAMRZQZZVQQZRQXUSVA", Serial="200295012697264", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\7.png" },
                    new Blank { BlankId=8, Code="XYou7AUAEQBGMEQCIB+QxT2LNaG9DNp0bOa4XnMpVvKpWwLMaCwbKu/eBLzpAiA8lieaObpZQpp+wy570GtCuuedDhmlyiZ0Egf8i89gsQAAAAxFlBllVBBlFBdRJQA=", Serial="200295012697263", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\8.png" },
                    new Blank { BlankId=9, Code="XYou7AUAEQBHMEUCIQCErexBlMij2KZoPq53dHjDRrgDJ1zGgBCyE2CDtcpQrQIgWoPu0zt5Zj26Ho5pAncF1NFSERqCceYjBP68umoNiGEAAAAMRZQZZVQQZRQXUSTA", Serial="200295012697262", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\9.png" },
                    new Blank { BlankId=10, Code="XYou6wUAEQBGMEQCIEkrEnZ6ywQKBvS+xAAupSq8s6R4ARR8Q2AGLQmqCIxUAiAMTr50NWG4O0MJsVU3Oiwpc8A1IXsDtJhw2zfQCfhZRwAAAAxFlBllVBBlFBdRJIA=", Serial="200295012697261", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\10.png" },
                }
            });

            filaLotes.Enqueue(new Lote { LoteId = 3, Serial = "2400000005678", Total = 50, Estado = Lote.EstadoLote.Nenhum, 
                Blanks = new List<Blank> 
                {
                    new Blank { BlankId=11, Code="XYou7QUAEQBHMEUCIB50SNrhMvojXV5RO1Ayt0jCYNxEUkvZF+AqoOGCx7L1AiEAolsksP3TD2joHTsci42ZVG0fsB+hJC+PhqAmEqHY8yIAAAAMRZQZZVQQZRQXUSWA", Serial="200295012697256", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\11.png" },
                    new Blank { BlankId=12, Code="XYou7AUAEQBHMEUCIQCH5COxvff8kG+l8CP6nFKcETQv1/cUwQeC+xKPsFHugQIgNTr7itHwtLx9zE+mhKvKhIPI5B3/IvzI/JnfLqEam3YAAAAMRZQZZVQQZRQXUSVA", Serial="200295012697255", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\12.png" },
                    new Blank { BlankId=13, Code="XYou7AUAEQBGMEQCIB+QxT2LNaG9DNp0bOa4XnMpVvKpWwLMaCwbKu/eBLzpAiA8lieaObpZQpp+wy570GtCuuedDhmlyiZ0Egf8i89gsQAAAAxFlBllVBBlFBdRJQA=", Serial="200295012697254", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\13.png" },
                    new Blank { BlankId=14, Code="XYou7AUAEQBHMEUCIQCErexBlMij2KZoPq53dHjDRrgDJ1zGgBCyE2CDtcpQrQIgWoPu0zt5Zj26Ho5pAncF1NFSERqCceYjBP68umoNiGEAAAAMRZQZZVQQZRQXUSTA", Serial="200295012697253", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\14.png" },
                    new Blank { BlankId=15, Code="XYou6wUAEQBGMEQCIEkrEnZ6ywQKBvS+xAAupSq8s6R4ARR8Q2AGLQmqCIxUAiAMTr50NWG4O0MJsVU3Oiwpc8A1IXsDtJhw2zfQCfhZRwAAAAxFlBllVBBlFBdRJIA=", Serial="200295012697252", ImagePath = @"C:\ProgramData\Tecsistel\Inovare\Images\2400000004653\15.png" },
                }
            });

            lblLote1.Text = "2400000001234";
            lblLote2.Text = "2400000004321";
            lblLote3.Text = "2400000005678";

            return filaLotes;
        }

        private void frmSupervisorio_FormClosed(object sender, FormClosedEventArgs e)
        {
            CancelaProcesso();
            TaskController.Cancel();
        }
        
        private async void CallCLP()
        {
            foreach (Lote lote in LotesEnfileirados)
            {
                bolEmProducao = true;
                lote.Status = Lote.StatusLote.EmProducao;
                LoteEmProducao = lote;
                List<Blank> blanks = lote.Blanks;

                while (bolEmProducao)
                {
                    FindDeviceConnection();
                    SetAutomaticMode();
                    
                    // Envia comando de Leitra de Status
                    byte[] response = _deviceService.ReadStatus(FlagsEscrita.GetHiFlags(), FlagsEscrita.GetLoFlags());
                    bool statusFrame = _deviceService.StatusFrameIsOk();

                    TrataResposta(response, statusFrame);

                    // Quando Leitura QR Pendente 0 -> 1
                    if ((FlagsLeitura.UltimoLeituraQrPendente != FlagsLeitura.ImpressaoQrPendente) && FlagsLeitura.ImpressaoQrPendente)
                    {
                        FlagsEscrita.QrLeituraConcluida = false;
                    }

                    // Quando lote finalizado 0 -> 1
                    if ((FlagsLeitura.UltimoLoteFinalizado != FlagsLeitura.LoteFinalizado) && FlagsLeitura.LoteFinalizado)
                    {
                        // Concluir o lote atual
                        Lote loteFinalizando = LotesEnfileirados.Where(l => l.Status == Lote.StatusLote.Finalizando).FirstOrDefault();
                        loteFinalizando.Status = Lote.StatusLote.Concluido;
                        FlagsEscrita.FinalLote = false;
                        FlagsEscrita.LoteValido = false;
                    }

                    if (FlagsLeitura.SistemaModoManual)
                    {
                        // Se o sistema estiver em manual mandar comando de modo Auto novamente
                        SetAutomaticMode();
                    }

                    if (!Directory.GetFiles(NetworkPath).Any(file => file.Contains(".png")))
                    {
                        CopiaQrParaPastaLaser(blanks);
                    }

                    if (FilaLotes.Count > 0)
                    {
                        FlagsEscrita.TemProximoLote = true;
                    }
                    else
                    {
                        FlagsEscrita.TemProximoLote = false;
                    }

                    if (blanks.All(b => b.Status == Blank.StatusBlank.Validado || b.Status == Blank.StatusBlank.NaoOk))
                    {
                        bolEmProducao = false;
                        FilaLotes.Dequeue();
                        LoteEmProducao.Status = Lote.StatusLote.Finalizando;
                        FlagsEscrita.FinalLote = true;
                        FlagsEscrita.LoteValido = blanks.Any(b => b.Status == Blank.StatusBlank.NaoOk) ? false : true;

                        if (FilaLotes.Count == 0)
                        {
                            //Recarregar nova fila de lotes para produção
                            //LotesEnfileirados.Clear();
                        }
                    }

                    if (Token.IsCancellationRequested)
                    {
                        break;
                    }
                    Token.WaitHandle.WaitOne(1000);
                    await Task.Delay(100);
                }
            }
        }

        private void TrataResposta(byte[] response, bool statusFrame)
        {
            if (statusFrame)
            {
                if (response[3] == 101 && response[2] == 4)
                {
                    // Estado atual
                    FlagsLeitura.ImpressaoQrPendente = ((response[5] & 16) == 0) ? false : true;
                    FlagsLeitura.ImpressaoQrConcluida = ((response[5] & 32) == 0) ? false : true;
                    FlagsLeitura.LeituraQrPendente = ((response[5] & 64) == 0) ? false : true;
                    FlagsLeitura.LoteFinalizado = ((response[4] & 64) == 0) ? false : true;
                    FlagsLeitura.SistemaModoManual = ((response[6] & 2) == 0) ? false : true;

                    /**
                    // Quando Leitura QR Pendente 0 -> 1
                    if ((FlagsLeitura.UltimoLeituraQrPendente != FlagsLeitura.ImpressaoQrPendente) && FlagsLeitura.ImpressaoQrPendente)
                    {
                        FlagsEscrita.QrLeituraConcluida = false;
                    }

                    // Quando lote finalizado 0 -> 1
                    if ((FlagsLeitura.UltimoLoteFinalizado != FlagsLeitura.LoteFinalizado) && FlagsLeitura.LoteFinalizado)
                    {
                        // Concluir o lote atual
                        LoteEmProducao.Status = Lote.StatusLote.Concluido;
                        FlagsEscrita.FinalLote = false;
                        FlagsEscrita.LoteValido = false;
                    }

                    if (FlagsLeitura.SistemaModoManual)
                    {
                        // Se o sistema estiver em manual mandar comando de modo Auto novamente
                        SetAutomaticMode();
                    }
                    **/

                    this.InvokeExtension(f => f.txtFlags.Text = string.Format(DateTime.Now.ToString(), Environment.NewLine));
                    this.InvokeExtension(f => f.txtFlags.Text += "\r\n" + string.Format(FlagsLeitura.ImpressaoQrPendente.ToString(), Environment.NewLine));
                    this.InvokeExtension(f => f.txtFlags.Text += "\r\n" + string.Format(FlagsLeitura.ImpressaoQrConcluida.ToString(), Environment.NewLine));
                    this.InvokeExtension(f => f.txtFlags.Text += "\r\n" + string.Format(FlagsLeitura.LeituraQrPendente.ToString(), Environment.NewLine));
                    this.InvokeExtension(f => f.txtFlags.Text += "\r\n" + string.Format(FlagsLeitura.LoteFinalizado.ToString(), Environment.NewLine));
                    this.InvokeExtension(f => f.txtFlags.Text += "\r\n" + string.Format(FlagsLeitura.SistemaModoManual.ToString(), Environment.NewLine));

                    txtFlags.Text += $"\r\n==================================================== {Environment.NewLine}";

                    FlagsLeitura.UltimoImpressaoQrPendente = FlagsLeitura.ImpressaoQrPendente;
                    FlagsLeitura.UltimoImpressaoQrConcluida = FlagsLeitura.ImpressaoQrConcluida;
                    FlagsLeitura.UltimoLeituraQrPendente = FlagsLeitura.LeituraQrPendente;
                    FlagsLeitura.UltimoLoteFinalizado = FlagsLeitura.LoteFinalizado;
                    FlagsLeitura.UltimoSistemaModoManual = FlagsLeitura.SistemaModoManual;
                }
            }
            else
            {
                //this.InvokeExtension(f => f.txtFlags.Text = $"Leitura de Status erro comunicação StatusFrameIsOk => {_deviceService.StatusFrameIsOk()} \r\n" +
                //                                            $"Leitura de Status CMD recebido => {response[3]} \r\n" +
                //                                            $"Leitura de Status Numero ByteInfo => {response[2]} \r\n");
            }

            FlagsLeitura.UltimoImpressaoQrPendente = FlagsLeitura.ImpressaoQrPendente;
            FlagsLeitura.UltimoImpressaoQrConcluida = FlagsLeitura.ImpressaoQrConcluida;
            FlagsLeitura.UltimoLeituraQrPendente = FlagsLeitura.LeituraQrPendente;
            FlagsLeitura.UltimoLoteFinalizado = FlagsLeitura.LoteFinalizado;
            FlagsLeitura.UltimoSistemaModoManual = FlagsLeitura.SistemaModoManual;
        }

        private void CancelaProcesso()
        {
            try
            {
                if (!bolEmProducao)
                {
                    if (Directory.GetFiles(NetworkPath).Where(file => file.IndexOf("png", StringComparison.OrdinalIgnoreCase) > 0).Count() > 0)
                    {
                        foreach (FileInfo fileInfo in new DirectoryInfo(NetworkPath).EnumerateFiles())
                        {
                            if (fileInfo.Name.Contains(".png"))
                            {
                                FlagsEscrita.FinalLote = false;
                                int blankId = fileInfo.Name.GetIdQrCodeFromFilename();

                                if (File.Exists(fileInfo.FullName))
                                {
                                    File.Delete(fileInfo.FullName);
                                    FlagsEscrita.QrEnviadoImpressao = false;
                                }

                                if (!(LoteEmProducao is null))
                                {
                                    Blank blank = LoteEmProducao.Blanks.Where(b => b.BlankId == blankId).FirstOrDefault();

                                    if (!(blank is null))
                                    {
                                        blank.Status = Blank.StatusBlank.Recebido;

                                        if (LoteEmProducao.Blanks.All(b => b.Status == Blank.StatusBlank.Recebido))
                                        {
                                            LoteEmProducao.Status = Lote.StatusLote.Recebido;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Laser
        private void InicializaConexaoLaser()
        {
            try
            {
                if (NetworkPath.StartsWith(@"\\"))
                {
                    using (new NetworkConnection(NetworkPath, Credentials))
                    {
                        AccessFolderWatcher = new FileSystemWatcher
                        {
                            Path = NetworkPath,
                            Filter = "*.png",
                            NotifyFilter = NotifyFilters.FileName,
                            EnableRaisingEvents = true,
                        };

                        AccessFolderWatcher.Deleted += new FileSystemEventHandler(AccessFolderWatcher_Deleted);
                    }
                }
                else
                {
                    AccessFolderWatcher = new FileSystemWatcher
                    {
                        Path = NetworkPath,
                        Filter = "*.png",
                        NotifyFilter = NotifyFilters.FileName,
                        EnableRaisingEvents = true,
                    };

                    AccessFolderWatcher.Deleted += new FileSystemEventHandler(AccessFolderWatcher_Deleted);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AccessFolderWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (bolEmProducao)
                {
                    // Busca QR Code por Id com o nome da imagem na pasta
                    Blank blank = LoteEmProducao.Blanks.Where(b => b.BlankId == e.Name.GetIdQrCodeFromFilename()).FirstOrDefault();

                    if (!(blank is null) && (blank.Status == Blank.StatusBlank.EnviadoImpressao))
                    {
                        //QrCode qrUpdated = _superService.UpdateStatusCode(qrToPrinted.IdQrCode, (int)QrCodeStatus.StatusQr.Printed);
                        blank.Status = Blank.StatusBlank.Validado;
                        blank.ImpressaoCompleta = true;
                        this.InvokeExtension(f => f.txtFlags.Text += $"QR Code Id => {blank.BlankId} Status => {blank.Status.BlankStatusToString()} {Environment.NewLine}");
                    }

                    FlagsEscrita.QrEnviadoImpressao = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void CopiaQrParaPastaLaser(List<Blank> blanks)
        {
            /** Em rede
            //using (new NetworkConnection(NetworkPath, Credentials))
            //{
            //}
            **/
            if (blanks.Count > 0)
            {
                // Pega o primeiro QR Codes da lista
                Blank blankReceived = blanks.Where(b => b.Status == Blank.StatusBlank.Recebido).FirstOrDefault();

                if (!(blankReceived is null))
                {
                    // Monta caminho completo com nome da imagem
                    string imgToSave = Path.Combine(NetworkPath, $"{blankReceived.BlankId}.png");

                    Thread.Sleep(1000);
                
                    if (!File.Exists(imgToSave))
                    {
                        // Copia pra pasta da laser
                        File.Copy(blankReceived.ImagePath, imgToSave, false);
                        // Atualiza status para 'Enviado'
                        blankReceived.Status = Blank.StatusBlank.EnviadoImpressao;
                        blankReceived.UpdatedAt = DateTime.Now;
                        FlagsEscrita.QrEnviadoImpressao = true;
                        this.InvokeExtension(f => f.txtFlags.Text += $"QR Code Id => {blankReceived.BlankId} Status => {blankReceived.Status.BlankStatusToString()} {Environment.NewLine}");
                    }
                }
            }
        }
        #endregion

        #region Comandos CLP
        private void SetAutomaticMode()
        {
            try
            {
                byte[] FrameInfo = new byte[2];
                FrameInfo[0] = (byte)0;
                FrameInfo[1] = (byte)0;
                //isAutoMode = true;

                _deviceService.ControlOperationMode(FrameInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FindDeviceConnection()
        {
            try
            {
                _deviceService.FindDevice(0x4D8, 0x42);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        private async void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = "Produção iniciada...";
            bolEmProducao = true;
            //CallCLP();
            await Task.Run(() => CallCLP(), Token);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bolEmProducao = false;
            TaskController.Cancel();
            TaskController = new CancellationTokenSource();
            Token = TaskController.Token;

            CancelaProcesso();
            this.label1.Text = "Produção parada...";
        }

        #region Simulação de flags de leitura CLP
        private void cbImpressaoPendente_CheckedChanged(object sender, EventArgs e)
        {
            if (cbImpressaoPendente.Checked)
            {
                FlagsLeitura.ImpressaoQrPendente = true;
            }
            else
            {
                FlagsLeitura.ImpressaoQrPendente = false;
            }
        }

        private void cbImpressaoConcluida_CheckedChanged(object sender, EventArgs e)
        {
            if (cbImpressaoConcluida.Checked)
            {
                FlagsLeitura.ImpressaoQrConcluida = true;
            }
            else
            {
                FlagsLeitura.ImpressaoQrConcluida = false;
            }
        }

        private void cbLoteFinalizado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLoteFinalizado.Checked)
            {
                FlagsLeitura.LoteFinalizado = true;
            }
            else
            {
                FlagsLeitura.LoteFinalizado = false;
            }
        }
        #endregion

    }
}
