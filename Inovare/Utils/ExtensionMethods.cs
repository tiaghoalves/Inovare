using Inovaree.Dominio;
using System;
using System.ComponentModel;

namespace Inovaree.Utils
{
    public static class ExtensionMethods
    {
        public static void InvokeExtension<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                action(@this);
            }
        }

        public static int GetIdQrCodeFromFilename(this string file)
        {
            if (file.Contains(".png")) // filtra apenas png
            {
                try
                {
                    return Convert.ToInt32(file.Trim().Substring(0, file.IndexOf("."))); // Retira extensão do nome do arquivo
                }
                catch (Exception)
                {
                    return default;
                }
            }
            else
            {
                return default;
            }
        }

        public static string BlankStatusToString(this Blank.StatusBlank status)
        {
            switch (status)
            {
                case Blank.StatusBlank.Recebido:
                    return "Recebido";
                case Blank.StatusBlank.Gravando:
                    return "Gravando";
                case Blank.StatusBlank.Gravado:
                    return "Gravado";
                case Blank.StatusBlank.NaoOk:
                    return "Não ok";
                case Blank.StatusBlank.Validado:
                    return "Validado";
                case Blank.StatusBlank.EnviadoImpressao:
                    return "Enviado";
                default:
                    return "Nenhum";
            }
        }
    }
}
