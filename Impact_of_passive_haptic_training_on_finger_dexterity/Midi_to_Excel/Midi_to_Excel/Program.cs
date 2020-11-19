using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Midi_to_Excel
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Exercise_Result> results = new List<Exercise_Result>();
            new Experimental_Directory_Reader().process_directory(@"/Users/romanweis/Desktop/Bachelorexperiment_Daten/Subject 09", ref results);
            var xmldataset = results.ToDataSet();
            var workbook = IronXL.WorkBook.Load(xmldataset);
            var sheet = workbook.WorkSheets.First();
            workbook.SaveAs(@"/Users/romanweis/Desktop/Results.xlsx");
        }
    }
}
