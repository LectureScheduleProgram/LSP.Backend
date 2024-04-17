namespace LSP.Business.Utilities
{
    public static class AuthHelper
    {
        public static Random random = new();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomInteger(int length)
        {
            const string chars = "0123456789";
            var r = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return Convert.ToInt32(r);
        }

        public static string PlainMail(string mail)
        {
            var email = "";
            foreach (var item in mail)
            {
                if (item == '@')
                    break;
                email += item;
            }

            return email;
        }

        public static string Last4DigitOfPhoneNumber(string phoneNumber)
        {
            var last4 = "";
            var counter = 0;
            var queue = phoneNumber.Length - 1;
            while (true)
            {
                last4 += phoneNumber[queue];
                counter++;
                queue--;
                if (counter == 4) break;
            }

            var reverserLast4 = "";
            for (int i = last4.Length - 1; i >= 0; i--)
            {
                reverserLast4 += last4[i];
            }

            return reverserLast4;
        }
    }
}
