using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Midi_Analyzer;

namespace Midi_to_Excel
{
    public class Midi_Reader
    {

        public int error { get; private set; }

        public double duration { get; private set; }

        public double STDEV_Notes { get; private set; }

        public double STDEV_Rests { get; private set; }


        public void Analyze_MidiFile(string fullpath)
        {
            var midiFile = MidiFile.Read(fullpath);
            var tempoMap = midiFile.GetTempoMap();

            List<double> Lengths_of_each_note = new List<double>();
            List<double> Lengths_of_each_Pause = new List<double>();
            List<string> piece;

            using (NotesManager notesManager = midiFile.GetTrackChunks()
                                                  .First()
                                                  .ManageNotes())
            {
                NotesCollection notes = notesManager.Notes;
                piece = notes.Select(x => x.NoteName.ToString()).ToList();
                int count = notes.Count() - 1;
                for (int i = 0; i < count; i++)
                {

                    var note = notes.ElementAt(i);
                    var next_note = notes.ElementAt(i + 1);

                    var note_start = note.TimeAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 1000000f;
                    var note_length = note.LengthAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 1000000f;

                    var next_note_start = next_note.TimeAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 1000000f;

                    var pause_length = next_note_start - (note_start + note_length);


                    Lengths_of_each_note.Add(note_length);
                    if (pause_length > 0)
                    {
                        Lengths_of_each_Pause.Add(pause_length);
                    }
                }

                this.duration = Math.Round((notes.ElementAt(count).TimeAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 1000000f +
                                notes.ElementAt(count).LengthAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 1000000f -
                                notes.ElementAt(0).TimeAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 1000000f), 3);
            }
            this.STDEV_Notes = StdDev(Lengths_of_each_note);
            this.STDEV_Rests = StdDev(Lengths_of_each_Pause, 0);
            this.error = Levenshtein.Compute_error(String.Join(" ", piece));
        }

        private double StdDev(IEnumerable<double> values)
        {
            var count = values?.Count() ?? 0;
            if (count <= 1) return 0;

            var avg = values.Average();
            var sum = values.Sum(d => Math.Pow(d - avg, 2));

            return Math.Sqrt(sum / count);
        }

        private double StdDev(IEnumerable<double> values, double avg)
        {
            var count = values?.Count() ?? 0;
            if (count <= 1) return 0;
            var sum = values.Sum(d => Math.Pow(d - avg, 2));
            return Math.Sqrt(sum / count);
        }
    }
}