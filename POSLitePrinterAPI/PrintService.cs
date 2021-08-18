using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Entidades;

namespace POSLitePrinterAPI
{
    public class PrintService
    {
        public static String Line = "------------------------------------------------";
        public static void Apertura(Printer printer, POSRegister register)
        {
            Registro(printer, "TICKET APERTURA DE CAJA", register);
        }
        public static void Arqueo(Printer printer, POSRegister register)
        {
            Registro(printer, "TICKET DE ARQUEO", register);
        }
        public static void Cierre(Printer printer, POSRegister register)
        {
            Registro(printer, "TICKET CIERRE DE CAJA", register);
        }
        public static void Preparacion(Printer physical_printer, POSEnc orden)
        {
            //Aqui modificamos el codigo para separar las tres opciones de impresora
            var printer_name = physical_printer.address.Split(":");

            if (printer_name.Length == 1)
            {
                //Por valor predeterminado es NETWORK
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();

                printer.Write(
                  ByteSplicer.Combine(
                    GetEncabezado(e, orden),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine("TICKET DE ORDEN"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c")),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c")),
                    e.PrintLine("Descuento:                    (" + orden.DblDescuento.ToString("c") + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c")),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine("Gracias por su preferencia!"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }
            else if (printer_name[0] == "NETWORK")
            {
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();
                printer.Write(
                  ByteSplicer.Combine(
                    GetEncabezado(e, orden),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine("TICKET DE ORDEN"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c")),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c")),
                    e.PrintLine("Descuento:                    (" + orden.DblDescuento.ToString("c") + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c")),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine("Gracias por su preferencia!"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }
            else if (printer_name[0] == "USB")
            {
                var printer = new SerialPrinter(printer_name[1], physical_printer.port);
                var e = new EPSON();
                printer.Write(
                  ByteSplicer.Combine(
                    GetEncabezado(e, orden),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine("TICKET DE ORDEN"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c")),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c")),
                    e.PrintLine("Descuento:                    (" + orden.DblDescuento.ToString("c") + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c")),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine("Gracias por su preferencia!"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }
            else if (printer_name[0] == "FILE")
            {
                var printer = new FilePrinter(printer_name[1]);
                var e = new EPSON();
                printer.Write(
                  ByteSplicer.Combine(
                    GetEncabezado(e, orden),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine("TICKET DE ORDEN"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c")),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c")),
                    e.PrintLine("Descuento:                    (" + orden.DblDescuento.ToString("c") + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c")),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine("Gracias por su preferencia!"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }

        }
        public static void Cocina(Printer physical_printer, POSEnc orden)
        {
            //Aqui modificamos el codigo para separar las tres opciones de impresora
            var printer_name = physical_printer.address.Split(":");

            if (printer_name.Length == 1)
            {
                //Por valor predeterminado es NETWORK
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();

                printer.Write(
                   ByteSplicer.Combine(
                     e.CenterAlign(),
                     e.PrintLine(Line),
                     e.SetStyles(PrintStyle.Bold),
                     e.PrintLine("TICKET DE COCINA"),
                     e.SetStyles(PrintStyle.None),
                     e.PrintLine(Line),
                     e.LeftAlign(),
                     e.PrintLine("No. Ticket:            " + orden.IntOrden),
                     GetServicio(e, orden),
                     e.PrintLine("Caja:                  " + orden.StrTerminal),
                     e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                     GetDetallesConfiguracion(e, orden.POSDet),
                     e.FullCutAfterFeed(4)
                     )
                 );

                printer.Dispose();
            }
            else if (printer_name[0] == "NETWORK")
            {
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();
                printer.Write(
                    ByteSplicer.Combine(
                      e.CenterAlign(),
                      e.PrintLine(Line),
                      e.SetStyles(PrintStyle.Bold),
                      e.PrintLine("TICKET DE COCINA"),
                      e.SetStyles(PrintStyle.None),
                      e.PrintLine(Line),
                      e.LeftAlign(),
                      e.PrintLine("No. Ticket:            " + orden.IntOrden),
                      GetServicio(e, orden),
                      e.PrintLine("Caja:                  " + orden.StrTerminal),
                      e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                      GetDetallesConfiguracion(e, orden.POSDet),
                      e.FullCutAfterFeed(4)
                      )
                  );

                printer.Dispose();
            }
            else if (printer_name[0] == "USB")
            {
                var printer = new SerialPrinter(printer_name[1], physical_printer.port);
                var e = new EPSON();
                printer.Write(
                    ByteSplicer.Combine(
                      e.CenterAlign(),
                      e.PrintLine(Line),
                      e.SetStyles(PrintStyle.Bold),
                      e.PrintLine("TICKET DE COCINA"),
                      e.SetStyles(PrintStyle.None),
                      e.PrintLine(Line),
                      e.LeftAlign(),
                      e.PrintLine("No. Ticket:            " + orden.IntOrden),
                      GetServicio(e, orden),
                      e.PrintLine("Caja:                  " + orden.StrTerminal),
                      e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                      GetDetallesConfiguracion(e, orden.POSDet),
                      e.FullCutAfterFeed(4)
                      )
                  );

                printer.Dispose();
            }
            else if (printer_name[0] == "FILE")
            {
                var printer = new FilePrinter(printer_name[1]);
                var e = new EPSON();
                printer.Write(
                   ByteSplicer.Combine(
                     e.CenterAlign(),
                     e.PrintLine(Line),
                     e.SetStyles(PrintStyle.Bold),
                     e.PrintLine("TICKET DE COCINA"),
                     e.SetStyles(PrintStyle.None),
                     e.PrintLine(Line),
                     e.LeftAlign(),
                     e.PrintLine("No. Ticket:            " + orden.IntOrden),
                     GetServicio(e, orden),
                     e.PrintLine("Caja:                  " + orden.StrTerminal),
                     e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                     GetDetallesConfiguracion(e, orden.POSDet),
                     e.FullCutAfterFeed(4)
                     )
                 );

                printer.Dispose();
            }

        }
        public static void Pago(Printer physical_printer, POSEnc orden)
        {
            //Aqui modificamos el codigo para separar las tres opciones de impresora
            var printer_name = physical_printer.address.Split(":");

            if (printer_name.Length == 1)
            {
                //Por valor predeterminado es NETWORK
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();

                printer.Write(
                ByteSplicer.Combine(
                    GetEncabezado(e, orden),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine("TICKET DE PAGO"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c")),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c")),
                    e.PrintLine("Descuento:                    (" + orden.DblDescuento.ToString("c") + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c")),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.PrintLine("Pagos"),
                    e.PrintLine(Line),
                    GetPagos(e, orden.POSEncPago, orden.DblTotal),
                    e.PrintLine(""),
                    e.PrintLine(Line),
                    e.PrintLine("Gracias por su preferencia!"),
                    e.FullCutAfterFeed(4)
                )
            );

                printer.Dispose();
            }
            else if (printer_name[0] == "NETWORK")
            {
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();
                printer.Write(
              ByteSplicer.Combine(
                GetEncabezado(e, orden),
                e.PrintLine(Line),
                e.SetStyles(PrintStyle.Bold),
                e.PrintLine("TICKET DE PAGO"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("No. Ticket:            " + orden.IntOrden),
                GetServicio(e, orden),
                e.PrintLine("Caja:                  " + orden.StrTerminal),
                e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("#           PRECIO             TOTAL"),
                GetDetalles(e, orden.POSDet),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c")),
                e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c")),
                e.PrintLine("Descuento:                    (" + orden.DblDescuento.ToString("c") + ")"),
                e.PrintLine("Total:                         " + orden.DblTotal.ToString("c")),
                e.PrintLine(""),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.PrintLine("Pagos"),
                e.PrintLine(Line),
                GetPagos(e, orden.POSEncPago, orden.DblTotal),
                e.PrintLine(""),
                e.PrintLine(Line),
                e.PrintLine("Gracias por su preferencia!"),
                e.FullCutAfterFeed(4)
                )
            );

                printer.Dispose();
            }
            else if (printer_name[0] == "USB")
            {
                var printer = new SerialPrinter(printer_name[1], physical_printer.port);
                var e = new EPSON();
                printer.Write(
              ByteSplicer.Combine(
                GetEncabezado(e, orden),
                e.PrintLine(Line),
                e.SetStyles(PrintStyle.Bold),
                e.PrintLine("TICKET DE PAGO"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("No. Ticket:            " + orden.IntOrden),
                GetServicio(e, orden),
                e.PrintLine("Caja:                  " + orden.StrTerminal),
                e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("#           PRECIO             TOTAL"),
                GetDetalles(e, orden.POSDet),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c")),
                e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c")),
                e.PrintLine("Descuento:                    (" + orden.DblDescuento.ToString("c") + ")"),
                e.PrintLine("Total:                         " + orden.DblTotal.ToString("c")),
                e.PrintLine(""),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.PrintLine("Pagos"),
                e.PrintLine(Line),
                GetPagos(e, orden.POSEncPago, orden.DblTotal),
                e.PrintLine(""),
                e.PrintLine(Line),
                e.PrintLine("Gracias por su preferencia!"),
                e.FullCutAfterFeed(4)
                )
            );

                printer.Dispose();
            }
            else if (printer_name[0] == "FILE")
            {
                var printer = new FilePrinter(printer_name[1]);
                var e = new EPSON();
                printer.Write(
                  ByteSplicer.Combine(
                    GetEncabezado(e, orden),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine("TICKET DE PAGO"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFechaImpresion.ToShortDateString() + " " + orden.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c")),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c")),
                    e.PrintLine("Descuento:                    (" + orden.DblDescuento.ToString("c") + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c")),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.PrintLine("Pagos"),
                    e.PrintLine(Line),
                    GetPagos(e, orden.POSEncPago, orden.DblTotal),
                    e.PrintLine(""),
                    e.PrintLine(Line),
                    e.PrintLine("Gracias por su preferencia!"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }
        }
        private static void Registro(Printer physical_printer, String strTitulo, POSRegister register)
        {
            //Aqui modificamos el codigo para separar las tres opciones de impresora
            var printer_name = physical_printer.address.Split(":");

            if (printer_name.Length == 1)
            {
                //Por valor predeterminado es NETWORK
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();
                printer.Write(
                  ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine(strTitulo),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Terminal:              " + register.StrTerminal),
                    e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                    e.PrintLine("Fecha:                 " + register.DatFechaImpresion.ToShortDateString()),
                    e.PrintLine("Hora:                  " + register.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                    e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                    e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                    e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                    e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                    e.PrintLine("Monedas:               " + register.DblMonedas),
                    e.PrintLine("Total:                 " + register.DblImporte.ToString("c")),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Comentario:            " + register.StrComentario),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine("Firma:"),
                    e.PrintLine(""),
                    e.PrintLine(""),
                    e.PrintLine("_________________________"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }
            else if (printer_name[0] == "NETWORK")
            {
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();
                printer.Write(
                  ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine(strTitulo),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Terminal:              " + register.StrTerminal),
                    e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                    e.PrintLine("Fecha:                 " + register.DatFechaImpresion.ToShortDateString()),
                    e.PrintLine("Hora:                  " + register.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                    e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                    e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                    e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                    e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                    e.PrintLine("Monedas:               " + register.DblMonedas),
                    e.PrintLine("Total:                 " + register.DblImporte.ToString("c")),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Comentario:            " + register.StrComentario),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine("Firma:"),
                    e.PrintLine(""),
                    e.PrintLine(""),
                    e.PrintLine("_________________________"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }
            else if (printer_name[0] == "USB")
            {
                var printer = new SerialPrinter(printer_name[1], physical_printer.port);
                var e = new EPSON();
                printer.Write(
                  ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine(strTitulo),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Terminal:              " + register.StrTerminal),
                    e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                    e.PrintLine("Fecha:                 " + register.DatFechaImpresion.ToShortDateString()),
                    e.PrintLine("Hora:                  " + register.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                    e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                    e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                    e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                    e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                    e.PrintLine("Monedas:               " + register.DblMonedas),
                    e.PrintLine("Total:                 " + register.DblImporte.ToString("c")),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Comentario:            " + register.StrComentario),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine("Firma:"),
                    e.PrintLine(""),
                    e.PrintLine(""),
                    e.PrintLine("_________________________"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }
            else if (printer_name[0] == "FILE")
            {
                var printer = new FilePrinter(printer_name[1]);
                var e = new EPSON();
                printer.Write(
                  ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine(strTitulo),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Terminal:              " + register.StrTerminal),
                    e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                    e.PrintLine("Fecha:                 " + register.DatFechaImpresion.ToShortDateString()),
                    e.PrintLine("Hora:                  " + register.DatFechaImpresion.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                    e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                    e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                    e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                    e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                    e.PrintLine("Monedas:               " + register.DblMonedas),
                    e.PrintLine("Total:                 " + register.DblImporte.ToString("c")),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Comentario:            " + register.StrComentario),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine("Firma:"),
                    e.PrintLine(""),
                    e.PrintLine(""),
                    e.PrintLine("_________________________"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }
        }
        private static byte[] GetPagos(EPSON e, List<POSEncPago> pagos, Double apagar)
        {
            byte[] result = ByteSplicer.Combine(e.LeftAlign());
            Double importe_total = 0;
            Double cambio_total = 0;
            foreach (var pago in pagos)
            {
                
                importe_total = importe_total + pago.DblImporte;
                cambio_total = cambio_total + pago.DblCambio;
                result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("("+pago.IntFolio+")" + pago.StrNombre + "      " + pago.StrReferencia + "      " + pago.DblImporte.ToString("c"))
                  );
            }
            result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("A Pagar:                       " + apagar.ToString("c")),
                    e.PrintLine("Pagado:                        " + importe_total.ToString("c")),
                    e.PrintLine("Cambio:                        " + cambio_total.ToString("c"))
                  );
            return result;
        }
        private static byte[] GetDetallesConfiguracion(EPSON e, List<POSDet> detalles)
        {
            byte[] result = ByteSplicer.Combine(e.CenterAlign(), e.PrintLine(Line), e.LeftAlign());
            foreach (var detalle in detalles)
            {
                result = ByteSplicer.Combine(
                    result,
                    e.LeftAlign(),
                    e.PrintLine("("+ detalle.DblCantidad + ")           " + detalle.StrNombre),
                    e.PrintLine(detalle.StrNota),
                    GetConfiguracion(e, detalle.POSDetArticuloConfiguracion)
                  );
            }
            return result;
        }
        private static byte[] GetConfiguracion(EPSON e, List<POSDetArticuloConfiguracion> configuraciones)
        {
            byte[] result = ByteSplicer.Combine(e.RightAlign());
            foreach (var configuracion in configuraciones)
            {
                result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("->" + configuracion.StrNombre)
                  );
            }
            return result;
        }
        private static byte[] GetDetalles(EPSON e, List<POSDet> detalles) {
            byte[] result = ByteSplicer.Combine(e.CenterAlign(), e.PrintLine(Line), e.LeftAlign());
            foreach (var detalle in detalles)
            {
                result = ByteSplicer.Combine(
                    result,
                    e.CenterAlign(),
                    e.PrintLine(detalle.StrNombre),
                    e.LeftAlign(),
                    e.PrintLine(detalle.DblCantidad + "           " + detalle.DblPU.ToString("c") + "            " + detalle.DblTotal.ToString("c"))
                  );
            }
            return result;
        }
        private static byte[] GetServicio(EPSON e, POSEnc orden)
        {
            byte[] result = ByteSplicer.Combine(e.LeftAlign());
            if (orden.IntPOSTipoServicio == 1)
            {
                result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("Tipo Servicio:         MESA"),
                    e.PrintLine("Mesa:                  " + orden.StrMesa)
                  );
            }
            else if (orden.IntPOSTipoServicio == 2)
            {
                result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("Tipo Servicio:                 DOMICILIO"),
                    e.PrintLine("Mesa:                         " + orden.StrMesa)
                  );
            }
            else if (orden.IntPOSTipoServicio == 3)
            {
                result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("Tipo Servicio:                 LLEVAR"),
                    e.PrintLine("Clave:                        " + orden.StrMesa)
                    );

            }
            return result;
        }
        private static byte[] GetEncabezado(EPSON e, POSEnc orden)
        {
            byte[] result = ByteSplicer.Combine(e.CenterAlign());

            if (orden.BolReimpresion == null)
            {
                result = ByteSplicer.Combine(
                   result,
                   e.PrintLine(orden.POSInfo.StrNombre),
                   e.PrintLine(orden.POSInfo.StrNombreLegal),
                   e.PrintLine(orden.POSInfo.StrRFC),
                   e.PrintLine(orden.POSInfo.StrDireccion),
                   e.PrintLine("TEL. " + orden.POSInfo.StrTelefono),
                   e.PrintLine("CORREO. " + orden.POSInfo.StrCorreo)
                );
                return result;
            }
            else
            {
                if (Convert.ToBoolean(orden.BolReimpresion))
                {
                    result = ByteSplicer.Combine(
                   result,
                   e.PrintLine(Line),
                   e.PrintLine("REIMPRESION"),
                   e.PrintLine(Line),
                   e.PrintLine(orden.POSInfo.StrNombre),
                   e.PrintLine(orden.POSInfo.StrNombreLegal),
                   e.PrintLine(orden.POSInfo.StrRFC),
                   e.PrintLine(orden.POSInfo.StrDireccion),
                   e.PrintLine("TEL. " + orden.POSInfo.StrTelefono),
                   e.PrintLine("CORREO. " + orden.POSInfo.StrCorreo)
                );
                    return result;
                }
                else
                {
                    result = ByteSplicer.Combine(
                       result,
                       e.PrintLine(orden.POSInfo.StrNombre),
                       e.PrintLine(orden.POSInfo.StrNombreLegal),
                       e.PrintLine(orden.POSInfo.StrRFC),
                       e.PrintLine(orden.POSInfo.StrDireccion),
                       e.PrintLine("TEL. " + orden.POSInfo.StrTelefono),
                       e.PrintLine("CORREO. " + orden.POSInfo.StrCorreo)
                    );
                    return result;
                }
            }

           
        }
        
    }
}
