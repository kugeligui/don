namespace DON.Utils
{
    public class NumberUtils
    {
        public static double Parse(string val)
        {
            if (val == null)
            {
                return 0;
            }
            double va;
            double.TryParse(val, out va);
            return va;
        }
    }
}
