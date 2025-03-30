using AvaloniaApplication.Models;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace AvaloniaApplication.Export
{
    public class ModesExport
    {
        private static readonly string _filePath = "Export\\modes_export.xlsx";

        public static void ExportModesToExcel(List<ModeModel> modes)
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Modes");

            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "MaxBottleNumber";
            worksheet.Cell(1, 4).Value = "MaxUsedTips";

            int row = 2;
            foreach (var mode in modes)
            {
                worksheet.Cell(row, 1).Value = mode.Id;
                worksheet.Cell(row, 2).Value = mode.Name;
                worksheet.Cell(row, 3).Value = mode.MaxBottleNumber;
                worksheet.Cell(row, 4).Value = mode.MaxUsedTips;
                row++;
            }

            workbook.SaveAs(_filePath);
        }
    }
}
