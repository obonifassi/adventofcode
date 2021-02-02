namespace adventofcode
{
    public static class Extensions
    {
        public static int Mod(this int a, int n)
        {
            a %= n;

            if (a < 0)
            {
                return n + a;
            }
            else
            {
                return 0 + a;
            }
        }
    }
}
