using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class POSEnc
    {

        #region Private Variables
        private System.Int32 _intPOSEnc;
        private System.Int32 _intPOSRegister;
        private System.Int32 _intFolio;
        private System.String _datFecha;
        private System.Int32 _intCliente;
        private System.Int32 _intEmpleado;
        private System.Double _dblSubtotal;
        private System.Double _dblIVA;
        private System.Double _dblIEPS;
        private System.Double _dblTotal;
        private System.Double _dblPorcentajeDescuento;
        private System.Int32 _intGrupo;
        private System.String _strNota;
        private System.String _strUsuarioPOS;
        private System.Int32 _intEstatus;
        private System.Int32 _intPOSEncSincronizado;
        private System.Int32 _intSincronizado;
        private System.String _strTipoServicio;
        private System.String _datHora;
        private System.String _strMesero;
        private System.String _strRepartidor;
        private System.Int32 _intPOSTipoServicio;
        private System.Int32 _intDomicilioEmpleado;
        private System.String _datDomicilioFechaSale;
        private System.Double _dblDomicilioPagaCon;
        private System.Double _dblPropina;
        private System.String _strMesa;
        private System.Int32 _intOrden;
        private System.String _strUsuarioCancelacion;
        private System.String _datFechaCancelacion;
        private System.Int32 _intPOSMotivoCancelacion;
        private System.String _strComentarioCancelacion;
        private System.String _datFechaPrepara;
        private System.Int32 _intCancelacionDevolucion;
        private System.String _datFechaFin;
        private System.String _strCantidadLetra;
        private System.Double _dblTotalPagado;
        private System.Double _dblCambio;
        private System.Int32 _intPOSDescuento;
        private System.String _datFechaDescuento;
        private System.String _strUsuarioDescuento;
        private System.String _strClaveDescuento;
        private System.Int32 _intPOSMotivoDescuento;
        private System.String _strReferenciaDescuento;
        private System.Int32 _intPOSRegisterOriginal;
        private System.Int32 _intTipoDescuento;
        private System.String _uidGuid;
        private System.String _strNombre;
        private System.String _strTelefono;
        private System.String _strDireccion;
        private System.String _strPoblacion;
        private System.String _strColonia;
        private System.String _strReferencia;
        private System.String _strTelefono2;
        private System.String _strEntreCalle;
        private System.Int32 _intRequiereFactura;
        private System.Double _dblEnvio1;
        private System.Double _dblEnvio2;
        private System.Double _dblEnvio3;
        private System.Double _dblEnvio4;
        private System.Double _dblEnvio5;
        private System.Int32 _intPedidoQP;
        private System.Int32 _intFolioEmpleado;
        private System.String _strCardId;
        private System.String _strUId;
        private System.Int32? _intPOSCobraComanda;
        private System.String _strBiometrico;
        private System.Int32 _intPOSMesero;
        private System.Int32 _intPOSTipoVenta;
        private List<POSDet> _POSDet;
        private List<POSEncPago> _POSEncPago;

        private Boolean? _bolReimpresion;

        public Boolean? BolReimpresion
        {
            get { return _bolReimpresion; }
            set { _bolReimpresion = value; }
        }


        private DateTime _datFechaImpresion;

        public DateTime DatFechaImpresion
        {
            get { return _datFechaImpresion; }
            set { _datFechaImpresion = value; }
        }

        #endregion
        private POSInfo _POSInfo;

        public POSInfo POSInfo
        {
            get { return _POSInfo; }
            set { _POSInfo = value; }
        }
        private Entidades.Printer _POSPrinter;

        public Entidades.Printer POSPrinter
        {
            get { return _POSPrinter; }
            set { _POSPrinter = value; }
        }



        #region Public Properties

        private double _dblDescuento;

        public double DblDescuento
        {
            get { return _dblDescuento; }
            set { _dblDescuento = value; }
        }


        private String _strTerminal;

        public String StrTerminal
        {
            get { return _strTerminal; }
            set { _strTerminal = value; }
        }


        public System.Int32 IntPOSEnc
        {
            get { return _intPOSEnc; }
            set { _intPOSEnc = value; }
        }


        public System.Int32 IntPOSRegister
        {
            get { return _intPOSRegister; }
            set { _intPOSRegister = value; }
        }


        public System.Int32 IntFolio
        {
            get { return _intFolio; }
            set { _intFolio = value; }
        }


        public System.String DatFecha
        {
            get { return _datFecha; }
            set { _datFecha = value; }
        }


        public System.Int32 IntCliente
        {
            get { return _intCliente; }
            set { _intCliente = value; }
        }


        public System.Int32 IntEmpleado
        {
            get { return _intEmpleado; }
            set { _intEmpleado = value; }
        }


        public System.Double DblSubtotal
        {
            get { return _dblSubtotal; }
            set { _dblSubtotal = value; }
        }


        public System.Double DblIVA
        {
            get { return _dblIVA; }
            set { _dblIVA = value; }
        }


        public System.Double DblIEPS
        {
            get { return _dblIEPS; }
            set { _dblIEPS = value; }
        }


        public System.Double DblTotal
        {
            get { return _dblTotal; }
            set { _dblTotal = value; }
        }


        public System.Double DblPorcentajeDescuento
        {
            get { return _dblPorcentajeDescuento; }
            set { _dblPorcentajeDescuento = value; }
        }


        public System.Int32 IntGrupo
        {
            get { return _intGrupo; }
            set { _intGrupo = value; }
        }


        public System.String StrNota
        {
            get { return _strNota; }
            set { _strNota = value; }
        }


        public System.String StrUsuarioPOS
        {
            get { return _strUsuarioPOS; }
            set { _strUsuarioPOS = value; }
        }


        public System.Int32 IntEstatus
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }


        public System.Int32 IntPOSEncSincronizado
        {
            get { return _intPOSEncSincronizado; }
            set { _intPOSEncSincronizado = value; }
        }


        public System.Int32 IntSincronizado
        {
            get { return _intSincronizado; }
            set { _intSincronizado = value; }
        }


        public System.String StrTipoServicio
        {
            get { return _strTipoServicio; }
            set { _strTipoServicio = value; }
        }


        public System.String DatHora
        {
            get { return _datHora; }
            set { _datHora = value; }
        }


        public System.String StrMesero
        {
            get { return _strMesero; }
            set { _strMesero = value; }
        }

        public System.String StrRepartidor
        {
            get { return _strRepartidor; }
            set { _strRepartidor = value; }
        }

        public System.Int32 IntPOSTipoServicio
        {
            get { return _intPOSTipoServicio; }
            set { _intPOSTipoServicio = value; }
        }


        public System.Int32 IntDomicilioEmpleado
        {
            get { return _intDomicilioEmpleado; }
            set { _intDomicilioEmpleado = value; }
        }


        public System.String DatDomicilioFechaSale
        {
            get { return _datDomicilioFechaSale; }
            set { _datDomicilioFechaSale = value; }
        }


        public System.Double DblDomicilioPagaCon
        {
            get { return _dblDomicilioPagaCon; }
            set { _dblDomicilioPagaCon = value; }
        }


        public System.Double DblPropina
        {
            get { return _dblPropina; }
            set { _dblPropina = value; }
        }


        public System.String StrMesa
        {
            get { return _strMesa; }
            set { _strMesa = value; }
        }


        public System.Int32 IntOrden
        {
            get { return _intOrden; }
            set { _intOrden = value; }
        }


        public System.String StrUsuarioCancelacion
        {
            get { return _strUsuarioCancelacion; }
            set { _strUsuarioCancelacion = value; }
        }


        public System.String DatFechaCancelacion
        {
            get { return _datFechaCancelacion; }
            set { _datFechaCancelacion = value; }
        }


        public System.Int32 IntPOSMotivoCancelacion
        {
            get { return _intPOSMotivoCancelacion; }
            set { _intPOSMotivoCancelacion = value; }
        }


        public System.String StrComentarioCancelacion
        {
            get { return _strComentarioCancelacion; }
            set { _strComentarioCancelacion = value; }
        }


        public System.String DatFechaPrepara
        {
            get { return _datFechaPrepara; }
            set { _datFechaPrepara = value; }
        }


        public System.Int32 IntCancelacionDevolucion
        {
            get { return _intCancelacionDevolucion; }
            set { _intCancelacionDevolucion = value; }
        }


        public System.String DatFechaFin
        {
            get { return _datFechaFin; }
            set { _datFechaFin = value; }
        }


        public System.String StrCantidadLetra
        {
            get { return _strCantidadLetra; }
            set { _strCantidadLetra = value; }
        }


        public System.Double DblTotalPagado
        {
            get { return _dblTotalPagado; }
            set { _dblTotalPagado = value; }
        }


        public System.Double DblCambio
        {
            get { return _dblCambio; }
            set { _dblCambio = value; }
        }


        public System.Int32 IntPOSDescuento
        {
            get { return _intPOSDescuento; }
            set { _intPOSDescuento = value; }
        }


        public System.String DatFechaDescuento
        {
            get { return _datFechaDescuento; }
            set { _datFechaDescuento = value; }
        }


        public System.String StrUsuarioDescuento
        {
            get { return _strUsuarioDescuento; }
            set { _strUsuarioDescuento = value; }
        }


        public System.String StrClaveDescuento
        {
            get { return _strClaveDescuento; }
            set { _strClaveDescuento = value; }
        }


        public System.Int32 IntPOSMotivoDescuento
        {
            get { return _intPOSMotivoDescuento; }
            set { _intPOSMotivoDescuento = value; }
        }


        public System.String StrReferenciaDescuento
        {
            get { return _strReferenciaDescuento; }
            set { _strReferenciaDescuento = value; }
        }


        public System.Int32 IntPOSRegisterOriginal
        {
            get { return _intPOSRegisterOriginal; }
            set { _intPOSRegisterOriginal = value; }
        }


        public System.Int32 IntTipoDescuento
        {
            get { return _intTipoDescuento; }
            set { _intTipoDescuento = value; }
        }


        public System.String UidGuid
        {
            get { return _uidGuid; }
            set { _uidGuid = value; }
        }


        public System.String StrNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        public System.String StrTelefono
        {
            get { return _strTelefono; }
            set { _strTelefono = value; }
        }

        public System.String StrDireccion
        {
            get { return _strDireccion; }
            set { _strDireccion = value; }
        }


        public System.String StrPoblacion
        {
            get { return _strPoblacion; }
            set { _strPoblacion = value; }
        }

        public System.String StrColonia
        {
            get { return _strColonia; }
            set { _strColonia = value; }
        }

        public System.String StrReferencia
        {
            get { return _strReferencia; }
            set { _strReferencia = value; }
        }


        public System.String StrTelefono2
        {
            get { return _strTelefono2; }
            set { _strTelefono2 = value; }
        }


        public System.String StrEntreCalle
        {
            get { return _strEntreCalle; }
            set { _strEntreCalle = value; }
        }


        public System.Int32 IntRequiereFactura
        {
            get { return _intRequiereFactura; }
            set { _intRequiereFactura = value; }
        }

        public System.Double DblEnvio1
        {
            get { return _dblEnvio1; }
            set { _dblEnvio1 = value; }
        }

        public System.Double DblEnvio2
        {
            get { return _dblEnvio2; }
            set { _dblEnvio2 = value; }
        }

        public System.Double DblEnvio3
        {
            get { return _dblEnvio3; }
            set { _dblEnvio3 = value; }
        }

        public System.Double DblEnvio4
        {
            get { return _dblEnvio4; }
            set { _dblEnvio4 = value; }
        }

        public System.Double DblEnvio5
        {
            get { return _dblEnvio5; }
            set { _dblEnvio5 = value; }
        }

        public System.Int32 IntPedidoQP
        {
            get { return _intPedidoQP; }
            set { _intPedidoQP = value; }
        }

        public System.Int32 IntFolioEmpleado
        {
            get { return _intFolioEmpleado; }
            set { _intFolioEmpleado = value; }
        }

        public System.String StrCardId
        {
            get { return _strCardId; }
            set { _strCardId = value; }
        }

        public System.String StrUId
        {
            get { return _strUId; }
            set { _strUId = value; }
        }

        public System.Int32? IntPOSCobraComanda
        {
            get { return _intPOSCobraComanda; }
            set { _intPOSCobraComanda = value; }
        }


        public System.String StrBiometrico
        {
            get { return _strBiometrico; }
            set { _strBiometrico = value; }
        }


        public List<POSDet> POSDet
        {
            get { return _POSDet; }
            set { _POSDet = value; }
        }


        public List<POSEncPago> POSEncPago
        {
            get { return _POSEncPago; }
            set { _POSEncPago = value; }
        }

        public System.Int32 IntPOSMesero
        {
            get { return _intPOSMesero; }
            set { _intPOSMesero = value; }
        }

        public System.Int32 IntPOSTipoVenta
        {
            get { return _intPOSTipoVenta; }
            set { _intPOSTipoVenta = value; }
        }



        #endregion







    }
}
