using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Midi_to_Excel
{
    public class Experimental_Directory_Reader
    {
        public Experimental_Directory_Reader()
        {
        }

        public void process_directory(string path, ref List<Exercise_Result> results)
        {

            string[] fileEntries = Directory.GetFiles(path);
            string[] dirEntries = Directory.GetDirectories(path);

            if (fileEntries.Count() > 0)
            {
                string FolderName = new DirectoryInfo(System.IO.Path.GetDirectoryName(fileEntries[0])).Name;
                if (FolderName.Contains("Subject"))
                {
                    string resultString = Regex.Match(FolderName, @"\d+").Value;
                    int subject_no = int.Parse(resultString);

                    Exercise_Result result1 = new Exercise_Result("Exercise01") { Subject_No = subject_no };
                    Exercise_Result result2 = new Exercise_Result("Exercise02") { Subject_No = subject_no };
                    Exercise_Result result3 = new Exercise_Result("Exercise03") { Subject_No = subject_no };


                    foreach (string fileName in fileEntries)
                    {
                        try
                        {
                            switch (Path.GetFileName(fileName))
                            {
                                case ("Spur 1.mid"):
                                    Get_Results(fileName, ref result1, true);
                                    break;
                                case ("Spur 2.mid"):
                                    Get_Results(fileName, ref result2, true);
                                    break;
                                case ("Spur 3.mid"):
                                    Get_Results(fileName, ref result3, true);
                                    break;
                                case ("Spur 4.mid"):
                                    Get_Results(fileName, ref result1, false);
                                    break;
                                case ("Spur 5.mid"):
                                    Get_Results(fileName, ref result2, false);
                                    break;
                                case ("Spur 6.mid"):
                                    Get_Results(fileName, ref result3, false);
                                    break;
                            }


                        }
                        catch
                        {
                            continue;
                        }
                    }
                    results.Add(result1);
                    results.Add(result2);
                    results.Add(result3);
                }
            }

            foreach (string dirName in dirEntries)
            {
                process_directory(dirName, ref results);
            }
        }


        private void Get_Results(string filename, ref Exercise_Result result, bool is_initial)
        {
            Midi_Reader reader = new Midi_Reader();
            reader.Analyze_MidiFile(filename);
            if (is_initial)
            {
                result.Initial_Duration = reader.duration;
                result.Initial_Errors = reader.error;
                result.Initial_STDEV_Notes = reader.STDEV_Notes;
                result.Initial_STDEV_Rests = reader.STDEV_Rests;
            }
            else
            {
                result.Final_Duration = reader.duration;
                result.Final_Errors = reader.error;
                result.Final_STDEV_Notes = reader.STDEV_Notes;
                result.Final_STDEV_Rests = reader.STDEV_Rests;
            }

        }
    }
}
