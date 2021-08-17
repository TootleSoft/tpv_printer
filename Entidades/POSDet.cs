using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class POSDet
    {

        #region Private Variables
        private System.Int32 _intPOSDet;
        private System.Int32 _intPOSEnc;
        private System.Int32 _intFolio;
        private System.Int32 _intArticulo;
        private System.String _strNombreCorto;
        private System.String _strNombre;
        private System.String _strFamilia;
        private System.Double _dblPU;
        private System.Double _dblCantidad;
        private System.Double _dblTotal;
        private System.Double _dblPorcentajeDescuento;
        private System.Double _dblPorcentajeIVA;
        private System.Double _dblPorcentajeIEPS;
        private System.String _strNota;
        private System.String _uidGuid;
        private System.Int32 _intNumeroCuenta;
        private System.Int32 _intEstatus;
        private System.Int32 _intGrupo;
        private System.Boolean _blOcultar;
        private System.Int32 _intArticuloConfiguracion;
        private System.String _datFechaCancelacion;
        private System.String _strUsuarioCancelacion;
        private System.Int32 _intPOSMotivoCancelacion;
        private System.String _strComentarioAutoriza;
        private System.Int32 _intPOSDescuento;
        private System.String _datFechaDescuento;
        private System.String _strUsuarioDescuento;
        private System.String _strClaveDescuento;
        private System.Int32 _intPOSMotivoDescuento;
        private System.String _strReferenciaDescuento;
        private System.Int32 _intTipoDescuento;
        private System.Int32 _intOrdenImpresion;
        private System.Int32 _intCancelacionDevolucion;
        private System.Int32 _intPOSRegister;
        private System.Int32 _intPOSImpresora;
        private List<POSDetArticuloConfiguracion> _POSDetArticuloConfiguracion;




        #endregion


        #region Public Properties
        public System.Int32 IntPOSDet
        {
            get { return _intPOSDet; }
            set { _intPOSDet = value; }
        }


        public System.Int32 IntPOSEnc
        {
            get { return _intPOSEnc; }
            set { _intPOSEnc = value; }
        }


        public System.Int32 IntFolio
        {
            get { return _intFolio; }
            set { _intFolio = value; }
        }

        public System.String UidGuid
        {
            get { return _uidGuid; }
            set { _uidGuid = value; }
        }


        public System.Int32 IntArticulo
        {
            get { return _intArticulo; }
            set { _intArticulo = value; }
        }


        public System.String StrNombreCorto
        {
            get { return _strNombreCorto; }
            set { _strNombreCorto = value; }
        }


        public System.String StrNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }


        public System.String StrFamilia
        {
            get { return _strFamilia; }
            set { _strFamilia = value; }
        }


        public System.Double DblPU
        {
            get { return _dblPU; }
            set { _dblPU = value; }
        }


        public System.Double DblCantidad
        {
            get { return _dblCantidad; }
            set { _dblCantidad = value; }
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


        public System.Double DblPorcentajeIVA
        {
            get { return _dblPorcentajeIVA; }
            set { _dblPorcentajeIVA = value; }
        }


        public System.Double DblPorcentajeIEPS
        {
            get { return _dblPorcentajeIEPS; }
            set { _dblPorcentajeIEPS = value; }
        }


        public System.String StrNota
        {
            get { return _strNota; }
            set { _strNota = value; }
        }


        public System.Int32 IntNumeroCuenta
        {
            get { return _intNumeroCuenta; }
            set { _intNumeroCuenta = value; }
        }


        public System.Int32 IntEstatus
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }


        public System.Boolean BlOcultar
        {
            get { return _blOcultar; }
            set { _blOcultar = value; }
        }


        public System.Int32 IntGrupo
        {
            get { return _intGrupo; }
            set { _intGrupo = value; }
        }


        public System.Int32 IntArticuloConfiguracion
        {
            get { return _intArticuloConfiguracion; }
            set { _intArticuloConfiguracion = value; }
        }


        public System.String DatFechaCancelacion
        {
            get { return _datFechaCancelacion; }
            set { _datFechaCancelacion = value; }
        }


        public System.String StrUsuarioCancelacion
        {
            get { return _strUsuarioCancelacion; }
            set { _strUsuarioCancelacion = value; }
        }


        public System.Int32 IntPOSMotivoCancelacion
        {
            get { return _intPOSMotivoCancelacion; }
            set { _intPOSMotivoCancelacion = value; }
        }


        public System.String StrComentarioCancelacion
        {
            get { return _strComentarioAutoriza; }
            set { _strComentarioAutoriza = value; }
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


        public System.Int32 IntTipoDescuento
        {
            get { return _intTipoDescuento; }
            set { _intTipoDescuento = value; }
        }

        public System.Int32 IntOrdenImpresion
        {
            get { return _intOrdenImpresion; }
            set { _intOrdenImpresion = value; }
        }

        public System.Int32 IntCancelacionDevolucion
        {
            get { return _intCancelacionDevolucion; }
            set { _intCancelacionDevolucion = value; }
        }

        public System.Int32 IntPOSRegister
        {
            get { return _intPOSRegister; }
            set { _intPOSRegister = value; }
        }

        public System.Int32 IntPOSImpresora
        {
            get { return _intPOSImpresora; }
            set { _intPOSImpresora = value; }
        }

        public List<POSDetArticuloConfiguracion> POSDetArticuloConfiguracion
        {
            get { return _POSDetArticuloConfiguracion; }
            set { _POSDetArticuloConfiguracion = value; }
        }


        #endregion



    }
}
