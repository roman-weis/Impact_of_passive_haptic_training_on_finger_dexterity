using System;
namespace Midi_Analyzer
{
    public static class Levenshtein
    {
        private static int Compute(
            string first,
            string second
        )
        {
            if (first.Length == 0)
            {
                return second.Length;
            }

            if (second.Length == 0)
            {
                return first.Length;
            }

            var d = new int[first.Length + 1, second.Length + 1];
            for (var i = 0; i <= first.Length; i++)
            {
                d[i, 0] = i;
            }

            for (var j = 0; j <= second.Length; j++)
            {
                d[0, j] = j;
            }

            for (var i = 1; i <= first.Length; i++)
            {
                for (var j = 1; j <= second.Length; j++)
                {
                    var cost = (second[j - 1] == first[i - 1]) ? 0 : 1;
                    d[i, j] = Min(
                         d[i - 1, j] + 1,
                         d[i, j - 1] + 1,
                         d[i - 1, j - 1] + cost
                    );
                }
            }
            return d[first.Length, second.Length];
        }

        private static int Min(int e1, int e2, int e3) =>
            Math.Min(Math.Min(e1, e2), e3);


        public static int Compute_error(string exercise)
        {
            string third = "C G F E F E D A G F G F E B A G A G F C B A B A G D C B C B A E D C D C B F E D E D C";
            string second = "C E D F E G D F E G F A E G F A G B F A G B A C G B A C B D A C B D C E B D C E D F C";
            string first = "C E F G A G F E D F G A B A G F E G A B C B A G F A B C D C B A G B C D E D C B A C D E F E D C B D E F G F E D C";
            int i_first = Compute(exercise, first);
            int i_second = Compute(exercise, second);
            int i_third = Compute(exercise, third);
            return Math.Min(Math.Min(i_first, i_second), Math.Min(i_first, i_third));
        }
    }
}
