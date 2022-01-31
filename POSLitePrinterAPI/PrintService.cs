using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Entidades;
using System.Text.Json;
using System.Globalization;

namespace POSLitePrinterAPI
{
    public class PrintService
    {
        public static String Line = "------------------------------------------------";

        public static void Demo(Printer physical_printer)
        {
            var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
            var e = new EPSON();
            printer.Write(ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.PrintLine("HOLAAAAA"),
                    e.FullCutAfterFeed(4)));
            printer.Dispose();
        }
        private static void StatusChangedCasher(object sender, EventArgs ps)
        {
            var status = (PrinterStatusEventArgs)ps;
            if (status.IsCashDrawerOpen)
                throw new Exception("No se puede abrir el cajon, ya se encuentra abierto");
        }
        public static void OpenCashDrawer(Printer physical_printer)
        {
            var printer_name = physical_printer.address.Split(":");
            if (printer_name.Length == 1)
            {
                //Por valor predeterminado es NETWORK
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();
                //Asignamos la funcion del estatus del cajero
                printer.StatusChanged += StatusChangedCasher;
                printer.StartMonitoring();
                printer.Write(ByteSplicer.Combine(e.CashDrawerOpenPin2(),
                    e.CashDrawerOpenPin5()));
                printer.StopMonitoring();
                printer.Dispose();
            }
            else if (printer_name[0] == "NETWORK")
            {
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON(); 
                printer.StartMonitoring();
                printer.Write(ByteSplicer.Combine(e.CashDrawerOpenPin2(),
                    e.CashDrawerOpenPin5()));
                printer.StopMonitoring();
                printer.Dispose();
            }
            else if (printer_name[0] == "USB")
            {
                var printer = new SerialPrinter(printer_name[1], physical_printer.port);
                var e = new EPSON();
                printer.StartMonitoring();
                printer.Write(ByteSplicer.Combine(e.CashDrawerOpenPin2(),
                     e.CashDrawerOpenPin5()));
                printer.StopMonitoring();
                printer.Dispose();
            }
            else if (printer_name[0] == "FILE")
            {
                var printer = new FilePrinter(printer_name[1]);
                var e = new EPSON();
                printer.StartMonitoring();
                printer.Write(ByteSplicer.Combine(e.CashDrawerOpenPin2(),
                    e.CashDrawerOpenPin5()));
                printer.StopMonitoring();
                printer.Dispose();
            }
        }
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
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    GetPolitica(e, orden.ticket, "visible_preparacion"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_preparacion"),
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
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    GetPolitica(e, orden.ticket, "visible_preparacion"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_preparacion"),
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
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    GetPolitica(e, orden.ticket, "visible_preparacion"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_preparacion"),
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
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    GetPolitica(e, orden.ticket, "visible_preparacion"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_preparacion"),
                    e.FullCutAfterFeed(4)
                    )
                );

                printer.Dispose();
            }

        }
        public static void Prepago(Printer physical_printer, POSEnc orden)
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
                    e.PrintLine("TICKET CLIENTE PREPAGO"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.PrintLine("Propina:                       ________________"),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    GetPolitica(e, orden.ticket, "visible_preparacion"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_preparacion"),
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
                    e.PrintLine("TICKET CLIENTE PREPAGO"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.PrintLine("Propina:                       ________________"),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    GetPolitica(e, orden.ticket, "visible_preparacion"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_preparacion"),
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
                    e.PrintLine("TICKET CLIENTE PREPAGO"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.PrintLine("Propina:                       ________________"),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    GetPolitica(e, orden.ticket, "visible_preparacion"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_preparacion"),
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
                    e.PrintLine("TICKET CLIENTE PREPAGO"),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("No. Ticket:            " + orden.IntOrden),
                    GetServicio(e, orden),
                    e.PrintLine("Caja:                  " + orden.StrTerminal),
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.PrintLine("Propina:                       ________________"),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    GetPolitica(e, orden.ticket, "visible_preparacion"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_preparacion"),
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
                     e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
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
                      e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
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
                      e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
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
                     e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
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
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.PrintLine("Pagos"),
                    e.PrintLine(Line),
                    GetPagos(e, orden.POSEncPago, orden.DblTotal),
                    e.PrintLine(""),
                    e.PrintLine(Line),
                    GetPolitica(e, orden.ticket, "visible_pago"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_pago"), 
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
                e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("#           PRECIO             TOTAL"),
                GetDetalles(e, orden.POSDet),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                e.PrintLine(""),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.PrintLine("Pagos"),
                e.PrintLine(Line),
                GetPagos(e, orden.POSEncPago, orden.DblTotal),
                e.PrintLine(""),
                e.PrintLine(Line),
                GetPolitica(e, orden.ticket, "visible_pago"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_pago"),
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
                e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("#           PRECIO             TOTAL"),
                GetDetalles(e, orden.POSDet),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.LeftAlign(),
                e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                e.PrintLine(""),
                e.CenterAlign(),
                e.PrintLine(Line),
                e.PrintLine("Pagos"),
                e.PrintLine(Line),
                GetPagos(e, orden.POSEncPago, orden.DblTotal),
                e.PrintLine(""),
                e.PrintLine(Line),
                GetPolitica(e, orden.ticket, "visible_pago"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_pago"),
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
                    e.PrintLine("Fecha:                 " + orden.DatFecha.Value.ToShortDateString() + " " + orden.DatFecha.Value.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("#           PRECIO             TOTAL"),
                    GetDetalles(e, orden.POSDet),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Subtotal:                      " + orden.DblSubtotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("IVA:                           " + orden.DblIVA.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Descuento:                    (" + orden.DblImporteDescuento.ToString("c", new CultureInfo("en-US")) + ")"),
                    e.PrintLine("Total:                         " + orden.DblTotal.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine(""),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.PrintLine("Pagos"),
                    e.PrintLine(Line),
                    GetPagos(e, orden.POSEncPago, orden.DblTotal),
                    e.PrintLine(""),
                    e.PrintLine(Line),
                    GetPolitica(e, orden.ticket, "visible_pago"),
                    e.PrintLine("Gracias por su preferencia!"),
                    GetFirma(e, orden.ticket, "visible_pago"),
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

                if (strTitulo == "TICKET CIERRE DE CAJA")
                {
                    printer.Write(
                    ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine(strTitulo),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Terminal:              " + register.StrMaquinaAlta),
                    e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                    e.PrintLine("Fecha:                 " + register.DatFechaAlta.ToShortDateString()),
                    e.PrintLine("Hora:                  " + register.DatFechaAlta.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                    e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                    e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                    e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                    e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                    e.PrintLine("Monedas:               " + register.DblMonedas),
                    e.PrintLine("Otros Pagos:           " + register.DblOtrosPagos),
                    e.PrintLine("Total:                 " + register.DblImporte.ToString("c", new CultureInfo("en-US"))),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Ventas:            " + register.DblVentas.Value.ToString("c", new CultureInfo("en-US"))),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    GetPagos(e,register),
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
                }
                else
                {
                    printer.Write(
                 ByteSplicer.Combine(
                   e.CenterAlign(),
                   e.PrintLine(Line),
                   e.SetStyles(PrintStyle.Bold),
                   e.PrintLine(strTitulo),
                   e.SetStyles(PrintStyle.None),
                   e.PrintLine(Line),
                   e.LeftAlign(),
                   e.PrintLine("Terminal:              " + register.StrMaquinaAlta),
                   e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                   e.PrintLine("Fecha:                 " + register.DatFechaAlta.ToShortDateString()),
                   e.PrintLine("Hora:                  " + register.DatFechaAlta.ToShortTimeString()),
                   e.CenterAlign(),
                   e.PrintLine(Line),
                   e.LeftAlign(),
                   e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                   e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                   e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                   e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                   e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                   e.PrintLine("Monedas:               " + register.DblMonedas),
                   e.PrintLine("Total:                 " + register.DblImporte.ToString("c", new CultureInfo("en-US"))),
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
                }

               

                printer.Dispose();
            }
            else if (printer_name[0] == "NETWORK")
            {
                var printer = new NetworkPrinter(ipAddress: physical_printer.address, port: physical_printer.port, true);
                var e = new EPSON();
                if (strTitulo == "TICKET CIERRE DE CAJA")
                {
                    printer.Write(
                    ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine(strTitulo),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Terminal:              " + register.StrMaquinaAlta),
                    e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                    e.PrintLine("Fecha:                 " + register.DatFechaAlta.ToShortDateString()),
                    e.PrintLine("Hora:                  " + register.DatFechaAlta.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                    e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                    e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                    e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                    e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                    e.PrintLine("Monedas:               " + register.DblMonedas),
                    e.PrintLine("Otros Pagos:           " + register.DblOtrosPagos),
                    e.PrintLine("Total:                 " + register.DblImporte.ToString("c", new CultureInfo("en-US"))),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Ventas:            " + register.DblVentas.Value.ToString("c", new CultureInfo("en-US"))),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    GetPagos(e, register),
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
                }
                else
                {
                    printer.Write(
                 ByteSplicer.Combine(
                   e.CenterAlign(),
                   e.PrintLine(Line),
                   e.SetStyles(PrintStyle.Bold),
                   e.PrintLine(strTitulo),
                   e.SetStyles(PrintStyle.None),
                   e.PrintLine(Line),
                   e.LeftAlign(),
                   e.PrintLine("Terminal:              " + register.StrMaquinaAlta),
                   e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                   e.PrintLine("Fecha:                 " + register.DatFechaAlta.ToShortDateString()),
                   e.PrintLine("Hora:                  " + register.DatFechaAlta.ToShortTimeString()),
                   e.CenterAlign(),
                   e.PrintLine(Line),
                   e.LeftAlign(),
                   e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                   e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                   e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                   e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                   e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                   e.PrintLine("Monedas:               " + register.DblMonedas),
                   e.PrintLine("Total:                 " + register.DblImporte.ToString("c", new CultureInfo("en-US"))),
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
                }
                printer.Dispose();
            }
            else if (printer_name[0] == "USB")
            {
                var printer = new SerialPrinter(printer_name[1], physical_printer.port);
                var e = new EPSON();
                if (strTitulo == "TICKET CIERRE DE CAJA")
                {
                    printer.Write(
                    ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine(strTitulo),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Terminal:              " + register.StrMaquinaAlta),
                    e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                    e.PrintLine("Fecha:                 " + register.DatFechaAlta.ToShortDateString()),
                    e.PrintLine("Hora:                  " + register.DatFechaAlta.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                    e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                    e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                    e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                    e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                    e.PrintLine("Monedas:               " + register.DblMonedas),
                    e.PrintLine("Otros Pagos:           " + register.DblOtrosPagos),
                    e.PrintLine("Total:                 " + register.DblImporte.ToString("c", new CultureInfo("en-US"))),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Ventas:            " + register.DblVentas.Value.ToString("c", new CultureInfo("en-US"))),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    GetPagos(e, register),
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
                }
                else
                {
                    printer.Write(
                 ByteSplicer.Combine(
                   e.CenterAlign(),
                   e.PrintLine(Line),
                   e.SetStyles(PrintStyle.Bold),
                   e.PrintLine(strTitulo),
                   e.SetStyles(PrintStyle.None),
                   e.PrintLine(Line),
                   e.LeftAlign(),
                   e.PrintLine("Terminal:              " + register.StrMaquinaAlta),
                   e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                   e.PrintLine("Fecha:                 " + register.DatFechaAlta.ToShortDateString()),
                   e.PrintLine("Hora:                  " + register.DatFechaAlta.ToShortTimeString()),
                   e.CenterAlign(),
                   e.PrintLine(Line),
                   e.LeftAlign(),
                   e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                   e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                   e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                   e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                   e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                   e.PrintLine("Monedas:               " + register.DblMonedas),
                   e.PrintLine("Total:                 " + register.DblImporte.ToString("c", new CultureInfo("en-US"))),
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
                }

                printer.Dispose();
            }
            else if (printer_name[0] == "FILE")
            {
                var printer = new FilePrinter(printer_name[1]);
                var e = new EPSON();
                if (strTitulo == "TICKET CIERRE DE CAJA")
                {
                    printer.Write(
                    ByteSplicer.Combine(
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine(strTitulo),
                    e.SetStyles(PrintStyle.None),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Terminal:              " + register.StrMaquinaAlta),
                    e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                    e.PrintLine("Fecha:                 " + register.DatFechaAlta.ToShortDateString()),
                    e.PrintLine("Hora:                  " + register.DatFechaAlta.ToShortTimeString()),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                    e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                    e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                    e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                    e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                    e.PrintLine("Monedas:               " + register.DblMonedas),
                    e.PrintLine("Otros Pagos:           " + register.DblOtrosPagos),
                    e.PrintLine("Total:                 " + register.DblImporte.ToString("c", new CultureInfo("en-US"))),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    e.LeftAlign(),
                    e.PrintLine("Ventas:            " + register.DblVentas.Value.ToString("c", new CultureInfo("en-US"))),
                    e.CenterAlign(),
                    e.PrintLine(Line),
                    GetPagos(e, register),
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
                }
                else
                {
                    printer.Write(
                 ByteSplicer.Combine(
                   e.CenterAlign(),
                   e.PrintLine(Line),
                   e.SetStyles(PrintStyle.Bold),
                   e.PrintLine(strTitulo),
                   e.SetStyles(PrintStyle.None),
                   e.PrintLine(Line),
                   e.LeftAlign(),
                   e.PrintLine("Terminal:              " + register.StrMaquinaAlta),
                   e.PrintLine("Usuario:               " + register.StrUsuarioPOS),
                   e.PrintLine("Fecha:                 " + register.DatFechaAlta.ToShortDateString()),
                   e.PrintLine("Hora:                  " + register.DatFechaAlta.ToShortTimeString()),
                   e.CenterAlign(),
                   e.PrintLine(Line),
                   e.LeftAlign(),
                   e.PrintLine("Billetes 500:          " + register.DblDenominacion500),
                   e.PrintLine("Billetes 200:          " + register.DblDenominacion200),
                   e.PrintLine("Billetes 100:          " + register.DblDenominacion100),
                   e.PrintLine("Billetes 50:           " + register.DblDenominacion50),
                   e.PrintLine("Billetes 20:           " + register.DblDenominacion20),
                   e.PrintLine("Monedas:               " + register.DblMonedas),
                   e.PrintLine("Total:                 " + register.DblImporte.ToString("c", new CultureInfo("en-US"))),
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
                }

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
                    e.PrintLine("("+pago.IntFolio+")" + pago.StrNombre + "      " + pago.StrReferencia + "      " + pago.DblImporte.ToString("c", new CultureInfo("en-US")))
                  );
            }
            result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("A Pagar:                       " + apagar.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Pagado:                        " + importe_total.ToString("c", new CultureInfo("en-US"))),
                    e.PrintLine("Cambio:                        " + cambio_total.ToString("c", new CultureInfo("en-US")))
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
                    e.PrintLine(detalle.DblCantidad + "           " + detalle.DblPU.ToString("c", new CultureInfo("en-US")) + "            " + detalle.DblTotal.ToString("c", new CultureInfo("en-US")))
                  );
            }
            return result;
        }
        private static byte[] GetPolitica(EPSON e, List<object> ticket, String propiedad)
        {
            byte[] result = ByteSplicer.Combine(e.LeftAlign());
            if (ticket != null)
            {
                try
                {

                    var politica = (JsonElement)ticket.Find(x => ((JsonElement)x).GetProperty("id").ToString() == "politicas_inferior");
                    var texto = politica.GetProperty("valor").ToString();
                    var validacion = politica.GetProperty(propiedad).ToString();

                    if (texto != "" && validacion.ToUpper() == "TRUE")
                    {
                        result = ByteSplicer.Combine(
                            result,
                            e.PrintLine(texto)
                          );
                    }
                }
                catch (Exception ex)
                {
                    return result;
                }
            }
            return result;
        }
        private static byte[] GetFirma(EPSON e, List<object> ticket, String propiedad)
        {
            byte[] result = ByteSplicer.Combine(e.CenterAlign());
            if (ticket != null)
            {
                try
                {
                    var firma = (JsonElement)ticket.Find(x => ((JsonElement)x).GetProperty("id").ToString() == "firma");
                    var validacion = firma.GetProperty(propiedad).ToString();
                    if (validacion.ToUpper() == "TRUE")
                    {
                        result = ByteSplicer.Combine(
                            result,
                            e.PrintLine(""),
                            e.PrintLine(""),
                            e.PrintLine(""),
                            e.PrintLine(""),
                            e.PrintLine(""),
                            e.PrintLine("_____________________________"),
                            e.PrintLine(""),
                            e.PrintLine("Firma"),
                            e.PrintLine(""),
                            e.PrintLine(""),
                            e.PrintLine(""),
                            e.PrintLine(""),
                            e.PrintLine(""),
                            e.PrintLine("_____________________________"),
                            e.PrintLine(""),
                            e.PrintLine("Nombre"),
                            e.PrintLine("")
                          );
                    }
                }
                catch (Exception ex)
                {
                    return result;
                }
            }
            return result;
        }
        private static byte[] GetServicio(EPSON e, POSEnc orden)
        {
            byte[] result = ByteSplicer.Combine(e.LeftAlign());
            if (orden.IntPOSTipoServicio == 1)
            {
                if (orden.strHabitacion != null && orden.strHabitacion != "")
                {
                    result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("Tipo Servicio:         MESA"),
                    e.PrintLine("Mesa:                  " + orden.StrMesa),
                    e.PrintLine("Habitacion:            " + orden.strHabitacion),
                    e.PrintLine("Apellido:              " + orden.strApellidoReferencia)
                  );
                }
                else
                {
                    result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("Tipo Servicio:         MESA"),
                    e.PrintLine("Mesa:                  " + orden.StrMesa)
                  );
                }
                
            }
            else if (orden.IntPOSTipoServicio == 2)
            {
                result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("Tipo Servicio:                 DOMICILIO")
                  );
            }
            else if (orden.IntPOSTipoServicio == 3)
            {
                if (orden.strHabitacion != null && orden.strHabitacion != "")
                {
                    result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("Tipo Servicio:                 LLEVAR"),
                    e.PrintLine("Clave:                        " + orden.StrMesa),
                    e.PrintLine("Habitacion:                   " + orden.strHabitacion),
                    e.PrintLine("Apellido:                     " + orden.strApellidoReferencia)
                    );
                }
                else
                {
                    result = ByteSplicer.Combine(
                    result,
                    e.PrintLine("Tipo Servicio:                 LLEVAR"),
                    e.PrintLine("Clave:                        " + orden.StrMesa)
                    );
                }
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
        private static byte[] GetPagos(EPSON e, POSRegister register)
        {
            byte[] result = ByteSplicer.Combine(e.CenterAlign(), e.PrintLine("Desglose de Pagos"), e.LeftAlign());
            if (register.pagos.Count > 0)
            {
                foreach (var pago in register.pagos)
                {
                    result = ByteSplicer.Combine(
                       result,
                       e.PrintLine(pago.nombre + ":         " + pago.importe.Value.ToString("c", new CultureInfo("en-US")))
                     );
                }
            }
            return result;
        }
    }
}
