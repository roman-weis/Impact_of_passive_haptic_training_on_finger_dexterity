using System;
using IronXL;
using System.Linq;

namespace Midi_to_Excel
{
    public class Excel_Adapter
    {
        public Excel_Adapter()
        {

        }
        private WorkBook workbook;

        public void Create_New_Workbook()
        {
            workbook = WorkBook.Create(ExcelFileFormat.XLSX);
            workbook.Metadata.Title = "Experimental_Results";
            var newWorkSheet = workbook.CreateWorkSheet("Sheet 1");
            newWorkSheet["A1"].Value = "Hello World";
            newWorkSheet["A2"].Style.BottomBorder.SetColor("#ff6600");
            newWorkSheet["A2"].Style.BottomBorder.Type = IronXL.Styles.BorderType.Dashed;
        }

        public void Write_Table_To_WorkSheet(string sheet_name)
        {
            if (workbook == null)
            {
                Create_New_Workbook();
            }

        }
    }
}
