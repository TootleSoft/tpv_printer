using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public class POSDetArticuloConfiguracion
    {

        #region Private Variables
        private System.Int32? _intPOSDetArticuloConfiguracion;
        private System.Int32? _intPOSDet;
        private System.String _uidGUID;
        private System.Int32? _intArticuloConfiguracion;
        private System.Int32? _intArticuloConfiguracionPadre;
        private System.String _strNombre;
        private System.String _strGuidDet;
        private System.String _strNombreCorto;
        private System.Int32? _intOrdenImpresion;
        private System.Double _dblPrecio;
        private System.Int32? _intRequierePreparacion;
        private System.Int32? _intExtra;
        private System.Int32? _intSaltoLinea;
        private System.Int32? _intCantidadInferior;
        private System.String _strClaveArticulo;


        #endregion


        #region Public Properties
        public System.Int32? IntPOSDetArticuloConfiguracion
        {
            get { return _intPOSDetArticuloConfiguracion; }
            set { _intPOSDetArticuloConfiguracion = value; }
        }


        public System.Int32? IntPOSDet
        {
            get { return _intPOSDet; }
            set { _intPOSDet = value; }
        }


        public System.String UidGUID
        {
            get { return _uidGUID; }
            set { _uidGUID = value; }
        }


        public System.Int32? IntArticuloConfiguracion
        {
            get { return _intArticuloConfiguracion; }
            set { _intArticuloConfiguracion = value; }
        }


        public System.Int32? IntArticuloConfiguracionPadre
        {
            get { return _intArticuloConfiguracionPadre; }
            set { _intArticuloConfiguracionPadre = value; }
        }


        public System.String StrGUIDDet
        {
            get { return _strGuidDet; }
            set { _strGuidDet = value; }
        }



        public System.String StrNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }


        public System.String StrNombreCorto
        {
            get { return _strNombreCorto; }
            set { _strNombreCorto = value; }
        }


        public System.Int32? IntOrdenImpresion
        {
            get { return _intOrdenImpresion; }
            set { _intOrdenImpresion = value; }
        }


        public System.Double DblPrecio
        {
            get { return _dblPrecio; }
            set { _dblPrecio = value; }
        }


        public System.Int32? IntRequierePreparacion
        {
            get { return _intRequierePreparacion; }
            set { _intRequierePreparacion = value; }
        }


        public System.Int32? IntExtra
        {
            get { return _intExtra; }
            set { _intExtra = value; }
        }


        public System.Int32? IntSaltoLinea
        {
            get { return _intSaltoLinea; }
            set { _intSaltoLinea = value; }
        }


        public System.Int32? IntCantidadInferior
        {
            get { return _intCantidadInferior; }
            set { _intCantidadInferior = value; }
        }


        public System.String StrClaveArticulo
        {
            get { return _strClaveArticulo; }
            set { _strClaveArticulo = value; }
        }



        #endregion



    }
}
