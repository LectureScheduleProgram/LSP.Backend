namespace LSP.Business.Constants
{
    public static class Messages
    {
        #region ValidatorMessgaes
        public const string minimum_value_must_be_one = "Minimum Value Must Be 1";

        #endregion

        //tüm hata başlıkları '' ile başlayacak. success olan yerler ise sadece success messaggeCode gönderilecektir 

        public const string success = "Operation Completed Successfully!"; // başarılı...
        public const string success_code = "success"; // başarılı...



        public static string invalid_value = "Invalid Value!";
        public static string invalid_value_code = "invalid_value";

        public static string user_not_found = "User Couldn't Found"; // Kullanıcı Bulunamadı
        public static string user_not_found_code = "user_not_found"; // Kullanıcı Bulunamadı

        public static string name_already_same = "This is already your name!"; //Güncellenmek istenen isim önceki güncellenen isimle aynı.
        public static string name_already_same_code = "name_already_same_code"; //Güncellenmek istenen isim önceki güncellenen isimle aynı.

        public static string surname_already_same = "This is already your surname!"; //Güncellenmek istenen soyisim önceki  güncellenen soyisimle aynı.
        public static string surname_already_same_code = "surname_already_same_code"; //Güncellenmek istenen soyisim önceki güncellenen soyisim ile aynı.

        public static string phone_number_already_same = "This is already your phone number!"; //Güncellenmek istenen tel numarası önceki  güncellenen tel numarası ile aynı.
        public static string phone_number_already_same_code = "phone_number_already_same"; //Güncellenmek istenen tel numarası önceki  güncellenen tel numarası ile aynı.


        public static string security_history_not_found = "Security History Couldn't Found!";
        public static string security_history_not_found_code = "security_history_not_found";


        public static string add_failed = "Adding Operation Has Failed!"; // Ekleme işlemi başarısız
        public static string add_failed_code = "add_failed"; // Ekleme işlemi başarısız

        public static string update_failed = "Update Operation Has Failed!"; // Güncelleme işlemi başarısız
        public static string update_failed_code = "update_failed"; // Güncelleme işlemi başarısız

        public static string wrong_code = "Wrong Code!";
        public static string wrong_code_code = "wrong_code";

        public static string user_not_active = "User is not active";// Kullanıcı aktif değil...
        public static string user_not_active_code = "user_not_active";// Kullanıcı aktif değil...

        public static string wrong_password = "Password is Wrong!";// Yanlış parola...
        public static string wrong_password_code = "wrong_password";// Yanlış parola...

        public static string not_found_data = "Data not found";// Veri bulunamadı...
        public static string not_found_data_code = "not_found_data";// Veri bulunamadı...

        public static string token_not_found = "Token is missing!";// Token bulunamadı...
        public static string token_not_found_code = "token_not_found";// Token bulunamadı...

        public static string err_code_expired = "err_code_expired"; //Zaman aşımı kod hatası...

        public static string delete_failed = "Deleting Operation Has Failed!"; // delete işlemi başarısız
        public static string delete_failed_code = "delete_failed"; // delete işlemi başarısız

        public static string security_not_found = "Security Type Couldn't Found!"; // security bulunamadı
        public static string security_not_found_code = "security_not_found"; // security bulunamadı

        public static string password_history_not_found = "Password History Couldn't Found!"; // security bulunamadı
        public static string password_history_not_found_code = "password_history_not_found"; // security bulunamadı


        public static string at_least_one_option_must_be_active = "At Least One Option Must Be Active";//en az bir tane güvenlik seçeneği aktif olmalıdır.
        public static string at_least_one_option_must_be_active_code = "at_least_one_option_must_be_active";//en az bir tane güvenlik seçeneği aktif olmalıdır.
        public static string cannot_update_more_than_one_security_option = "Cannot Update More Than One Security Option At The Samet Time";//tek bir seferde 1 den fazla güvenlik seçeneği güncellenemez.
        public static string cannot_update_more_than_one_security_option_code;
        public const string password_cant_contain_name = "Password Can't Contain Name";
        public const string password_cant_contain_name_code = "password_cant_contain_name";

        public const string password_cant_contain_surname = "Password Can't Contain Surname";
        public const string password_cant_contain_surname_code = "password_cant_contain_surname";

        public const string password_cant_contain_email = "Password Can't Contain Email";
        public const string password_cant_contain_email_code = "password_cant_contain_email";

        public const string password_cant_contain_last4phone = "Password Can't Contain Last 4 digit of Phone Number!"; // şifre telefon numarasının son 4 hanesini içeremez
        public const string password_cant_contain_last4phone_code = "password_cant_contain_last4phone"; // şifre telefon numarasının son 4 hanesini içeremez

        public const string passwords_cant_same_last3 = "Password Can't be Same with Last 3!"; // şifre son 3ünden biriyle aynı olamaz(kullanılmıyor)
        public const string passwords_cant_same_last3_code = "password_cant_same_last3"; // şifre son 3ünden biriyle aynı olamaz(kullanılmıyor)

        public const string multiple_wrong_code = "multiple_wrong_code"; // Birden fazla yanlış kod girilmiş.(kullanılmıyor)

        public const string user_status_not_found = "User Status Couldn't Found!"; // User status  bulunamadı.(kullanılmıyor)
        public const string user_status_not_found_code = "user_status_not_found"; // User status  bulunamadı.(kullanılmıyor)

        public const string invalid_phone_number = "invalid_phone_number"; // Telefon numarası geçerli bir numara değil
        public const string invalid_phone_number_code = "invalid_phone_number_code"; // Telefon numarası geçerli bir numara değil

        public const string security_type_activated = "Security Type Activated"; // Güvenlik tipi aktive edildi.
        public const string security_type_activated_code = "api_security_type_activated"; // Güvenlik tipi aktive edildi.
        public const string security_type_deactivated = "Security Type Deactivated"; // Güvenlik tipi devre dışı bırakıldı.
        public const string security_type_deactivated_code = "api_security_type_deactivated"; // Güvenlik tipi devre dışı bırakıldı.


        public const string s3_upload_error = "s3_upload_error"; // S3 yükleme işlemi başarısız. 
        public const string s3_upload_error_code = "s3_upload_error"; // S3 yükleme işlemi başarısız. 

        public const string bucket_not_found = "bucket_not_found"; // Bucket bulunamadı. 

        public const string invalid_token = "Invalid Token!";
        public const string invalid_token_code = "api_invalid_token";

        public const string already_mail_registered = "Membership operation was done before with this e-mail. Please continue to membership operation with another e-mail.";
        public const string already_mail_registered_code = "mail_already_registered";

        public const string not_found_security_type = "Not Found Security Type";
        public const string not_found_security_type_code = "not_found_security_type";

        public const string wait_for_new_code = "You Have To Wait A Bit For New Code";
        public const string wait_for_new_code_code = "wait_for_new_code";

        public const string google_setup_already_done = "google_setup_already_done";
        public const string google_setup_already_done_code = "Google Setup Has Already Done!";

        public const string google_has_own_auth = "Google Has Own Authenticator App To Getting Code Please Use That App!";
        public const string google_has_own_auth_code = "google_has_own_auth";


        public const string lecture_not_found = "lecture_not_found";
        public const string lecture_name_cant_empty = "lecture_name_cant_empty";
        public const string lecture_name_cant_same = "lecture_name_cant_same";
        public const string lecture_name_already_exist = "lecture_name_already_exist";
        public const string department_faculty_dependendent_each_other = "department_faculty_dependendent_each_other";

        public const string faculty_not_found = "faculty_not_found";
        public const string faculty_name_cant_empty = "faculty_name_cant_empty";

        public const string department_not_found = "department_not_found";
        public const string department_name_cant_empty = "department_name_cant_empty";
        public const string department_already_exists = "department_already_exists";


        public const string classroom_not_found = "classroom_not_found";
        public const string classroom_already_exists = "classroom_already_exists";
        public const string classroom_name_cannot_be_empty = "classroom_name_cannot_be_empty";
        public const string classroom_name_same = "classroom_name_same";


        public const string scheduleRecord_not_found = "scheduleRecord_not_found";
        public const string same_start_end_hour = "same_start_end_hour";
        public const string start_hour_must_smaller = "start_hour_must_smaller";


        public const string classroomType_not_found = "classroomType_not_found";

        public const string classroomCapacity_not_found = "classroomCapacity_not_found";

        public const string scheduleRecord_already_exists = "scheduleRecord_already_exists";

    }
}