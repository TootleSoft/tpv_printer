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
        private System.Int32 _intPOSEncPago;
        private System.Int32 _intFolio;
        private System.Int32 _intPOSEnc;
        private System.Int32 _intNumeroCuenta;
        private System.Int32 _intTipoPago;
        private System.String _strNombre;
        private System.Double _dblImporte;
        private System.String _strReferencia;
        private System.Double _dblCambio;
        private System.Int32 _intGrupo;
        private System.Int32 _intEstatus;
        private System.String _uidGuid;
        private System.Int32 _intPOSRequiereFirma;
        private System.String _strUsuario;
        private System.String _strContrasenia;
        private System.String _strBiometrico;
        private System.String _strEmpleadoBiometrico;
        private System.Int32 _intEmpleadoBiometrico;
        #endregion


        #region Public Properties
        public System.Int32 IntPOSEncPago
        {
            get { return _intPOSEncPago; }
            set { _intPOSEncPago = value; }
        }


        public System.Int32 IntFolio
        {
            get { return _intFolio; }
            set { _intFolio = value; }
        }


        public System.Int32 IntPOSEnc
        {
            get { return _intPOSEnc; }
            set { _intPOSEnc = value; }
        }


        public System.Int32 IntNumeroCuenta
        {
            get { return _intNumeroCuenta; }
            set { _intNumeroCuenta = value; }
        }


        public System.Int32 IntTipoPago
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


        public System.Int32 IntGrupo
        {
            get { return _intGrupo; }
            set { _intGrupo = value; }
        }


        public System.String UidGuid
        {
            get { return _uidGuid; }
            set { _uidGuid = value; }
        }

        public System.Int32 IntEstatus
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }

        public System.Int32 IntPOSRequiereFirma
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

        public System.Int32 IntEmpleadoBiometrico
        {
            get { return _intEmpleadoBiometrico; }
            set { _intEmpleadoBiometrico = value; }
        }



        #endregion



    }
}
