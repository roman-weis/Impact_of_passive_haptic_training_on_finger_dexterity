namespace Midi_to_Excel
{
    public class Exercise_Result
    {
        public Exercise_Result(string exercise_name)
        {
            this.Exercise_name = exercise_name;
        }

        public string Exercise_name { get; set; }

        public int Subject_No { get; set; }

        public string filler1 { get; set; }

        public double Initial_Duration { get; set; }

        public int Initial_Errors { get; set; }

        public double Initial_STDEV_Notes { get; set; }

        public double Initial_STDEV_Rests { get; set; }

        public string filler2 { get; set; }

        public double Final_Duration { get; set; }

        public int Final_Errors { get; set; }

        public double Final_STDEV_Notes { get; set; }

        public double Final_STDEV_Rests { get; set; }


    }
}
