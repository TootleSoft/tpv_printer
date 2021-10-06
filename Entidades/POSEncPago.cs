using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class POSEncPago
    {

        #region Private Variables
        private int? _intPOSEncPago;
        private int? _intFolio;
        private int? _intPOSEnc;
        private int? _intNumeroCuenta;
        private int? _intTipoPago;
        private System.String _strNombre;
        private System.Double _dblImporte;
        private System.String _strReferencia;
        private System.Double _dblCambio;
        private int? _intGrupo;
        private int? _intEstatus;
        private System.String _uidGuid;
        private int? _intPOSRequiereFirma;
        private System.String _strUsuario;
        private System.String _strContrasenia;
        private System.String _strBiometrico;
        private System.String _strEmpleadoBiometrico;
        private int? _intEmpleadoBiometrico;
        #endregion


        #region Public Properties
        public int? IntPOSEncPago
        {
            get { return _intPOSEncPago; }
            set { _intPOSEncPago = value; }
        }


        public int? IntFolio
        {
            get { return _intFolio; }
            set { _intFolio = value; }
        }


        public int? IntPOSEnc
        {
            get { return _intPOSEnc; }
            set { _intPOSEnc = value; }
        }


        public int? IntNumeroCuenta
        {
            get { return _intNumeroCuenta; }
            set { _intNumeroCuenta = value; }
        }


        public int? IntTipoPago
        {
            get { return _intTipoPago; }
            set { _intTipoPago = value; }
        }


        public System.String StrNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }


        public System.Double DblImporte
        {
            get { return _dblImporte; }
            set { _dblImporte = value; }
        }


        public System.String StrReferencia
        {
            get { return _strReferencia; }
            set { _strReferencia = value; }
        }


        public System.Double DblCambio
        {
            get { return _dblCambio; }
            set { _dblCambio = value; }
        }


        public int? IntGrupo
        {
            get { return _intGrupo; }
            set { _intGrupo = value; }
        }


        public System.String UidGuid
        {
            get { return _uidGuid; }
            set { _uidGuid = value; }
        }

        public int? IntEstatus
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }

        public int? IntPOSRequiereFirma
        {
            get { return _intPOSRequiereFirma; }
            set { _intPOSRequiereFirma = value; }
        }

        public System.String StrUsuario
        {
            get { return _strUsuario; }
            set { _strUsuario = value; }
        }

        public System.String StrContrasenia
        {
            get { return _strContrasenia; }
            set { _strContrasenia = value; }
        }


        public System.String StrBiometrico
        {
            get { return _strBiometrico; }
            set { _strBiometrico = value; }
        }

        public int? IntEmpleadoBiometrico
        {
            get { return _intEmpleadoBiometrico; }
            set { _intEmpleadoBiometrico = value; }
        }



        #endregion



    }
}
