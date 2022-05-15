namespace GraphTest.Extensions
{
    public static class BasicTypesExtensions
    {
        // int 
        public static bool IsInRange(this int i, float min, float max) 
            => i >= min && i <= max;
        // float
        public static bool IsInRange(this float i, float min, float max)
            => i >= min && i <= max;
        // double 
        public static bool IsInRange(this double i, float min, float max) 
            => i >= min && i <= max;
        // decimal 
        public static bool IsInRange(this decimal i, float min, float max) 
            => i >=(decimal) min && i <=(decimal) max;

    }
}
