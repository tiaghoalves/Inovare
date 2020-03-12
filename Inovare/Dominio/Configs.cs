using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovaree.Dominio
{
    public class Configs
    {
        public int MatId { get; set; }
        public int TimeoutRead { get; set; }
        public string ScannerSerialPort { get; set; }
        public string NetworkPath { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string ApiVersion { get; set; }
        public bool Logging { get; set; }
        public string InkjetDirection { get; set; }
        public decimal InkjetSpeed { get; set; }
        public decimal InkjetDelayAfter { get; set; }
        public decimal InkjetDelayBefore { get; set; }
        public int InkjetDensity { get; set; }
        public int InkjetResolution { get; set; }
        public bool FlagsInfo { get; set; }

    }
}
