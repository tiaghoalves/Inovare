using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovaree.Dominio
{
    public class Blank : INotifyPropertyChanged
    {
        public bool arquivoNaPasta = false;
        public int BlankId { get; set; }
        public string Serial { get; set; }
        public string Code { get; set; }
        public StatusBlank Status { get; set; } = StatusBlank.Recebido;
        public string ImagePath { get; set; }
        public DateTime? UpdatedAt { get; set; }

        #region Flags de leitura
        public bool ImpressaoPendente { get; set; } = true;
        public bool ImpressaoCompleta { get; set; }
        public bool LeituraPendente { get; set; }
        #endregion

        //public bool ArquivoNaPasta
        //{
        //    get
        //    {
        //        return arquivoNaPasta;
        //    }
        //    set
        //    {
        //        arquivoNaPasta = value;
        //        if (!ArquivoNaPasta)
        //        {
        //            OnFileIsOnFolderChanged("FileIsOnFolder");
        //        }
        //    }
        //}

        public enum StatusBlank
        {
            Recebido = 1,
            Gravando = 2,
            Gravado = 3,
            NaoOk = 4,
            Validado = 5,
            EnviadoImpressao = 6
        }

        //protected void OnFileIsOnFolderChanged(string propertyName)
        //{
        //    OnPropertyFileIsOnFolderChanged(new PropertyChangedEventArgs(propertyName));
        //}

        //protected void OnPropertyFileIsOnFolderChanged(PropertyChangedEventArgs e)
        //{
        //    FileIsOnFolderChanged?.Invoke(this, e);
        //}

        public event PropertyChangedEventHandler FileIsOnFolderChanged;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
