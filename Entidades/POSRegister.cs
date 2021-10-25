using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Pagos {
        public string nombre { get; set; }
        public double? importe { get; set; }
    }

    public class POSRegister
    {
        #region Private Variables
        private System.Int32 _intPOSRegister;
        private System.Int32 _intPOSRegisterPadre;
        private System.Int32 _intFolio;
        private System.String _datFechaInicio;
        private System.String _datFechaFin;
        private System.String _strUsuarioPOS;
        private System.String _strComentario;
        private System.Double _dblImporte;
        private System.Double _dblImporteEntregado;
        private System.Double _dblDiferencia;
        private System.Int32 _intEstatus;
        private System.Int32 _intPOSTipoMovimientoTurno;
        private System.Double _dblDenominacion20;
        private System.Double _dblDenominacion50;
        private System.Double _dblDenominacion100;
        private System.Double _dblDenominacion200;
        private System.Double _dblDenominacion500;
        private System.Double _dblDenominacion1000;
        private System.Double _dblMonedas;
        private System.Double _dblOtrosPagos;
        private int? _intEmpleado;
        private System.String _datHora;
        private int? _intIntentos;
        private int? _intCorteGlobal;
        private System.String _strTerminal;
        private System.DateTime? _datFecha;
        private Entidades.Printer _POSPrinter;
        public List<Pagos> pagos { get; set; }

        #endregion
        private double? _dblVentas;

        public double? DblVentas
        {
            get { return _dblVentas; }
            set { _dblVentas = value; }
        }


        private DateTime _datFechaImpresion;

        public DateTime DatFechaImpresion
        {
            get { return _datFechaImpresion; }
            set { _datFechaImpresion = value; }
        }

        private Boolean? _bolReimpresion;

        public Boolean? BolReimpresion
        {
            get { return _bolReimpresion; }
            set { _bolReimpresion = value; }
        }

        #region Public Properties
        public Entidades.Printer POSPrinter
        {
            get { return _POSPrinter; }
            set { _POSPrinter = value; }
        }
        public System.String StrTerminal
        {
            get { return _strTerminal; }
            set { _strTerminal = value; }
        }
        public System.DateTime? DatFecha
        {
            get { return _datFecha; }
            set { _datFecha = value; }
        }
        public System.Int32 IntPOSRegister
        {
            get { return _intPOSRegister; }
            set { _intPOSRegister = value; }
        }

        public System.Int32 IntPOSRegisterPadre
        {
            get { return _intPOSRegisterPadre; }
            set { _intPOSRegisterPadre = value; }
        }


        public System.Int32 IntFolio
        {
            get { return _intFolio; }
            set { _intFolio = value; }
        }


        public System.String DatFechaInicio
        {
            get { return _datFechaInicio; }
            set { _datFechaInicio = value; }
        }


        public System.String DatFechaFin
        {
            get { return _datFechaFin; }
            set { _datFechaFin = value; }
        }


        public System.String StrUsuarioPOS
        {
            get { return _strUsuarioPOS; }
            set { _strUsuarioPOS = value; }
        }


        public System.String StrComentario
        {
            get { return _strComentario; }
            set { _strComentario = value; }
        }


        public System.Double DblImporte
        {
            get { return _dblImporte; }
            set { _dblImporte = value; }
        }

        public System.Double DblImporteEntregado
        {
            get { return _dblImporteEntregado; }
            set { _dblImporteEntregado = value; }
        }

        public System.Double DblDiferencia
        {
            get { return _dblDiferencia; }
            set { _dblDiferencia = value; }
        }

        public System.Int32 IntEstatus
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }

        public System.Int32 IntPOSTipoMovimientoTurno
        {
            get { return _intPOSTipoMovimientoTurno; }
            set { _intPOSTipoMovimientoTurno = value; }
        }

        public System.Double DblDenominacion20
        {
            get { return _dblDenominacion20; }
            set { _dblDenominacion20 = value; }
        }

        public System.Double DblDenominacion50
        {
            get { return _dblDenominacion50; }
            set { _dblDenominacion50 = value; }
        }

        public System.Double DblDenominacion100
        {
            get { return _dblDenominacion100; }
            set { _dblDenominacion100 = value; }
        }

        public System.Double DblDenominacion200
        {
            get { return _dblDenominacion200; }
            set { _dblDenominacion200 = value; }
        }

        public System.Double DblDenominacion500
        {
            get { return _dblDenominacion500; }
            set { _dblDenominacion500 = value; }
        }

        public System.Double DblDenominacion1000
        {
            get { return _dblDenominacion1000; }
            set { _dblDenominacion1000 = value; }
        }

        public System.Double DblMonedas
        {
            get { return _dblMonedas; }
            set { _dblMonedas = value; }
        }

        public System.Double DblOtrosPagos
        {
            get { return _dblOtrosPagos; }
            set { _dblOtrosPagos = value; }
        }

        public int? IntEmpleado
        {
            get { return _intEmpleado; }
            set { _intEmpleado = value; }
        }

        public System.String DatHora
        {
            get { return _datHora; }
            set { _datHora = value; }
        }

        public int? IntIntentos
        {
            get { return _intIntentos; }
            set { _intIntentos = value; }
        }

        public int? IntCorteGlobal
        {
            get { return _intCorteGlobal; }
            set { _intCorteGlobal = value; }
        }

        public String StrMaquinaAlta { get; set; }

        public DateTime DatFechaAlta { get; set; }

        #endregion


    }
}
