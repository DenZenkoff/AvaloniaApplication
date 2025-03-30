using AvaloniaApplication.Models;
using ClosedXML.Excel;
using System.Collections.Generic;

namespace AvaloniaApplication.Export
{
    public class StepsExport
    {
        private static readonly string _filePath = "Export\\steps_export.xlsx";

        public static void ExportStepsToExcel(List<StepModel> steps)
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Steps");

            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "ModeId";
            worksheet.Cell(1, 3).Value = "Timer";
            worksheet.Cell(1, 4).Value = "Destination";
            worksheet.Cell(1, 5).Value = "Speed";
            worksheet.Cell(1, 6).Value = "Type";
            worksheet.Cell(1, 7).Value = "Volume";

            int row = 2;
            foreach (var step in steps)
            {
                worksheet.Cell(row, 1).Value = step.Id;
                worksheet.Cell(row, 2).Value = step.ModeId;
                worksheet.Cell(row, 3).Value = step.Timer;
                worksheet.Cell(row, 4).Value = step.Destination;
                worksheet.Cell(row, 5).Value = step.Speed;
                worksheet.Cell(row, 6).Value = step.Type;
                worksheet.Cell(row, 7).Value = step.Volume;
                row++;
            }

            workbook.SaveAs(_filePath);
        }
    }
}
