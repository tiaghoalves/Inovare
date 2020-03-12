namespace Inovare.CLP
{
    public class EstruturaFrameDadosUSB
    {
        /// <summary>
        ///     Estrutura do frame de dados.
        /// </summary>
        public byte Start { get; set; }
        public byte NumBytesInfo { get; set; }
        public byte Command { get; set; }
        public byte[] Inform { get; set; }
        public byte BCC_HIGH { get; set; }
        public byte BCC_LOW { get; set; }
        public byte End { get; set; }
        
    }
}
