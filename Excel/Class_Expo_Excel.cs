﻿using ConectDB.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.Globalization;

namespace ConectDB.Excel
{
    public class Class_Expo_Excel
    {
        public FileResult Excel_export(List<ControlRep> datos, string NombreExcel)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte");

                // Añade los encabezados de las columnas
                string[] columnHeaders = { "SOLICITUD", "FECHA", "TIPO FALLA", "UNIDAD", "ALIAS", "NUM_OPERADOR", "OPERADOR", "TEL OPERADOR", "FALLA REAL", "COMENTARIO", "FINALIZAR", "REMOLQUE 1", "REMOLQUE 2", "RUTA", "TRAMO CARRETERO", "TIPO CARGA", "TIPO APOYO", "TIPO EQUIPO", "UBICACIÓN REPORTADA", "ULTIMA POSICION GPS", "OBSERVACIONES DE LA OPERACIÓN", "INICIO REPORTE", "FINALIZO REPORTE" };

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = columnHeaders[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                }
                int row = 2;
                foreach (var item in datos)
                {
                    for (int i = 0; i < item.Solicitudes.Count; i++)
                    {
                        worksheet.Cells[row, 1].Value = item.Solicitudes[i].ClaveControlReparaciones;
                        if (item.Solicitudes[i].InicioReporte == DateTime.MinValue)
                        {
                            worksheet.Cells[row, 2].Value = "";
                        }
                        else
                        {
                            worksheet.Cells[row, 2].Value = item.Solicitudes[i].InicioReporte;
                            worksheet.Cells[row, 2].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                        }
                        worksheet.Cells[row, 3].Value = item.Solicitudes[i].TipoFalla;
                        worksheet.Cells[row, 4].Value = item.Solicitudes[i].Numero;
                        worksheet.Cells[row, 5].Value = item.Solicitudes[i].Alias;
                        worksheet.Cells[row, 6].Value = item.Solicitudes[i].NumOP;
                        worksheet.Cells[row, 7].Value = item.Solicitudes[i].Nombre;
                        worksheet.Cells[row, 8].Value = item.Solicitudes[i].Tel_Operador;
                        worksheet.Cells[row, 9].Value = item.Solicitudes[i].TipoFallaReal;
                        worksheet.Cells[row, 10].Value = item.Solicitudes[i].ObsOperacion;
                        if (item.Solicitudes[i].FinReporte == DateTime.MinValue)
                        {
                            worksheet.Cells[row, 11].Value = "";
                        }
                        else
                        {
                            worksheet.Cells[row, 11].Value = item.Solicitudes[i].FinReporte;
                            worksheet.Cells[row, 11].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                        }
                        worksheet.Cells[row, 12].Value = item.Solicitudes[i].Remolque1;
                        worksheet.Cells[row, 13].Value = item.Solicitudes[i].Remolque2;
                        worksheet.Cells[row, 14].Value = item.Solicitudes[i].Ruta;
                        worksheet.Cells[row, 15].Value = item.Solicitudes[i].TramoCarretero;
                        worksheet.Cells[row, 16].Value = item.Solicitudes[i].TipoCarga;
                        worksheet.Cells[row, 17].Value = item.Solicitudes[i].TipoApoyo;
                        worksheet.Cells[row, 18].Value = item.Solicitudes[i].TipoEquipo;
                        worksheet.Cells[row, 19].Value = item.Solicitudes[i].UbicacionReportada;
                        worksheet.Cells[row, 20].Value = item.Solicitudes[i].UltimaPosicionGps;
                        worksheet.Cells[row, 21].Value = item.Solicitudes[i].ObsMantenimiento;
                        worksheet.Cells[row, 22].Value = item.Solicitudes[i].UsuarioIni;
                        worksheet.Cells[row, 23].Value = item.Solicitudes[i].UsuarioFin;
                        row++;
                    }
                }
                var contentBytes = package.GetAsByteArray();
                return new FileContentResult(contentBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = $"{NombreExcel}.xlsx"
                };
            }
        }

        public FileResult Excel_exportOper(ConsulTipoOpera datos, string NombreExcel)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte");

                // Añade los encabezados de las columnas
                string[] columnHeaders = {"UNE","Tipo Operación","Numero Ticket","Estatus","Fecha Alta","Usuario Asignado","Fecha Asignado","Fecha Termino","Tiempo de Reparacion","Equipo","Diesel","Grua","Tipo de Ticket","Tipo de Falla","Comentario de Reprogramacion","Operador","Ruta","Telefono del Operador","Atencion Parcial" };

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = columnHeaders[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                }
                int row = 2;

                for (int i = 0; i < datos.CSxTipoOeracion.Count; i++)
                {
                    worksheet.Cells[row, 1].Value = datos.CSxTipoOeracion[i].UNE;
                    worksheet.Cells[row, 2].Value = datos.CSxTipoOeracion[i].TipoOperacion;
                    worksheet.Cells[row, 3].Value = datos.CSxTipoOeracion[i].NumTicket;
                    worksheet.Cells[row, 4].Value = datos.CSxTipoOeracion[i].Estatus;
                    worksheet.Cells[row, 5].Value = datos.CSxTipoOeracion[i].Alta?.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[row, 6].Value = datos.CSxTipoOeracion[i].UsuarioAsignado;
                    worksheet.Cells[row, 7].Value = datos.CSxTipoOeracion[i].Asignado?.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[row, 8].Value = datos.CSxTipoOeracion[i].FechaTermino?.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[row, 9].Value = datos.CSxTipoOeracion[i].TimerReparacion;
                    worksheet.Cells[row, 10].Value = datos.CSxTipoOeracion[i].Equipo;
                    worksheet.Cells[row, 11].Value = datos.CSxTipoOeracion[i].Diesel;
                    worksheet.Cells[row, 12].Value = datos.CSxTipoOeracion[i].Grua;
                    worksheet.Cells[row, 13].Value = datos.CSxTipoOeracion[i].TipoTicket;
                    worksheet.Cells[row, 14].Value = datos.CSxTipoOeracion[i].TipoFalla;
                    worksheet.Cells[row, 15].Value = datos.CSxTipoOeracion[i].NotasMonitoreo;
                    worksheet.Cells[row, 16].Value = datos.CSxTipoOeracion[i].ClaveOperador + "|" + datos.CSxTipoOeracion[i].NombOperador;
                    worksheet.Cells[row, 17].Value = datos.CSxTipoOeracion[i].Ruta;
                    worksheet.Cells[row, 18].Value = datos.CSxTipoOeracion[i].Tel_Operador;
                    worksheet.Cells[row, 19].Value = datos.CSxTipoOeracion[i].AtencionParcial;
                    row++;
                }
                var contentBytes = package.GetAsByteArray();
                return new FileContentResult(contentBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = $"{NombreExcel}.xlsx"
                };
            }
        }
    }
}
