using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class POSInfo
    {
        private string _strCorreo;

        public string StrCorreo
        {
            get { return _strCorreo; }
            set { _strCorreo = value; }
        }


        private string _strNombre;

        public string StrNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        private string _strDireccion;

        public string StrDireccion
        {
            get { return _strDireccion; }
            set { _strDireccion = value; }
        }

        private string _strTelefono;

        public string StrTelefono
        {
            get { return _strTelefono; }
            set { _strTelefono = value; }
        }

        private string _strRFC;

        public string StrRFC
        {
            get { return _strRFC; }
            set { _strRFC = value; }
        }

        private string _strNombreLegal;

        public string StrNombreLegal
        {
            get { return _strNombreLegal; }
            set { _strNombreLegal = value; }
        }

    }
}
