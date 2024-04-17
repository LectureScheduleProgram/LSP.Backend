namespace LSP.Business.Utilities
{
    public static class MaxLengthHelper
    {
        public static long MaxLength(byte length)
        {
            long decimals = 0;
            switch (length)
            {
                case 0:
                    decimals = 1;
                    break;

                case 1:
                    decimals = 10;
                    break;

                case 2:
                    decimals = 100;
                    break;

                case 3:
                    decimals = 1000;
                    break;

                case 4:
                    decimals = 10000;
                    break;

                case 5:
                    decimals = 100000;
                    break;

                case 6:
                    decimals = 1000000;
                    break;

                case 7:
                    decimals = 10000000;
                    break;

                case 8:
                    decimals = 100000000;
                    break;

                case 9:
                    decimals = 1000000000;
                    break;

                case 10:
                    decimals = 10000000000;
                    break;

                case 11:
                    decimals = 100000000000;
                    break;

                case 12:
                    decimals = 1000000000000;
                    break;

                case 13:
                    decimals = 10000000000000;
                    break;

                case 14:
                    decimals = 100000000000000;
                    break;

                case 15:
                    decimals = 1000000000000000;
                    break;

                case 16:
                    decimals = 10000000000000000;
                    break;

                case 17:
                    decimals = 100000000000000000;
                    break;

                case 18:
                    decimals = 1000000000000000000;
                    break;

                default:
                    decimals = 1000000000000000000;
                    break;
            }
            return decimals;
        }
    }
}
