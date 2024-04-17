namespace LSP.Core.Extensions
{
    public static class DecimalExtensions
    {
        public static string DecimalToString(this decimal value)
        {
            return value.ToString("0.00000000");
        }
    }
}
