using FluentValidation.Results;
using System;

namespace LSP.Core.Utilities.Constants
{
    public static class AspectMessages
    {
        public static string InvalidEmail = "error invalid email";
        public static string MinimumPasswordSix = "error minimum password lenght.(min password lenght: 6)";
        public static string MinimumPasswordEight = "error minimum password lenght.(min password lenght: 8)";
        public static string SecurityCodeNotNull = "security code can not be null";
        public static string cannot_be_null_or_empty = "cannot be null or empty";
        public static string cant_contain_turkish_characters = "Password Can't Contain Turkish Characters";

        public static string cant_start_with_zero = "Password Can't Start With 0";
        public static string cant_start_with_nineteen = "Password Can't Start With 19";
        public static string cant_start_with_twenty = "Password Can't Start With 20";
        public static string cant_contain_repeating_characters = "Password Can't Contain Repeating Characters";
        public static string cant_contain_four_sequential_numbers = "Password Can't Contain 4 Sequential Numbers";
        public static string cant_update_birthdate = "Invalid Birthdate Information Entered";

        public static string list_cant_empty = "List Can't Be Empty!";
        public static string invalid_status = "Invalid Status Value!";
        public static string invalid_security_code = "Security Code Can't Be Empty!";
        public static string invalid_security_type = "Invalid Security Type!";
    }
}