namespace TriPeakSolitaire.Gameplay
{

    using System.Collections.Generic;

    public static class CardShuffler
    {
        public static void Shuffle<T>(T[] list)
        {
            System.Random rng = new System.Random();
            int n = list.Length;
            while (n > 0)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
