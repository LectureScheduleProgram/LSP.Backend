using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public const string transfer_err = "transfer"; // transfer yapılamadı

        public const string invalid_filter = "Invalid Filter!"; // geçersiz filtre
        public const string invalid_filter_code = "invalid_filter"; // geçersiz filtre

        public static string invalid_value = "Invalid Value!";
        public static string invalid_value_code = "invalid_value";

        public static string user_not_found = "User Couldn't Found"; // Kullanıcı Bulunamadı
        public static string user_not_found_code = "user_not_found"; // Kullanıcı Bulunamadı

        public static string already_tel_updated = "Phone number already updated!"; //Telefon numarası zaten daha önce güncellenmiş sadece 1 kere güncellenebilir
        public static string already_tel_updated_code = "already_tel_updated_code"; //Telefon numarası zaten daha önce güncellenmiş sadece 1 kere güncellenebilir
        public static string name_already_same = "This is already your name!"; //Güncellenmek istenen isim önceki güncellenen isimle aynı.
        public static string name_already_same_code = "name_already_same_code"; //Güncellenmek istenen isim önceki güncellenen isimle aynı.

        public static string surname_already_same = "This is already your surname!"; //Güncellenmek istenen soyisim önceki  güncellenen soyisimle aynı.
        public static string surname_already_same_code = "surname_already_same_code"; //Güncellenmek istenen soyisim önceki güncellenen soyisim ile aynı.

        public static string phone_number_already_same = "This is already your phone number!"; //Güncellenmek istenen tel numarası önceki  güncellenen tel numarası ile aynı.
        public static string phone_number_already_same_code = "phone_number_already_same"; //Güncellenmek istenen tel numarası önceki  güncellenen tel numarası ile aynı.

        public static string user_details_not_found = "User Details Couldn't Found!"; // Kullanıcı detayı Bulunamadı
        public static string user_details_not_found_code = "user_detail_not_found"; // Kullanıcı detayı Bulunamadı

        public static string security_history_not_found = "Security History Couldn't Found!";
        public static string security_history_not_found_code = "security_history_not_found";

        public static string token_expired = "token_expired"; // token süresi doldu
        public static string missing_parameter = "missing_parameter";  // eskik parametre

        public static string add_failed = "Adding Operation Has Failed!"; // Ekleme işlemi başarısız
        public static string add_failed_code = "add_failed"; // Ekleme işlemi başarısız

        public static string update_failed = "Update Operation Has Failed!"; // Güncelleme işlemi başarısız
        public static string update_failed_code = "update_failed"; // Güncelleme işlemi başarısız

        public static string balance_not_found = "Balance Couldn't Found!"; // Bakiye bulunamadı
        public static string balance_not_found_code = "balance_not_found"; // Bakiye bulunamadı

        public static string balance_already_exists = "Balance Already Exists!"; // Bakiye bulunamadı
        public static string balance_already_exists_code = "balance_already_exists"; // Bakiye bulunamadı

        public static string future_balance_not_found = "Future Balance Couldn't Found!"; // Bakiye bulunamadı
        public static string future_balance_not_found_code = "future_balance_not_found"; // Bakiye bulunamadı

        public static string future_balance_already_exists = "Future Balance Already Exists!"; // Bakiye bulunamadı
        public static string future_balance_already_exists_code = "future_balance_already_exists"; // Bakiye bulunamadı

        public static string balancies_cannot_created = "Balancies Cannot Created!"; // Bakiyeler bulunamadı(kullanılmıyor)
        public static string balancies_cannot_created_code = "balancies_cannot_created!"; // Bakiyeler bulunamadı(kullanılmıyor)
        public static string insufficient_balance = "Insufficient Balance"; // yetersiz bakiye
        public static string insufficient_balance_code = "insufficient_balance"; // yetersiz bakiye
        public static string description_not_found = "description_not_found"; // açıklama bulunamadı
        public static string descriptions_not_found = "descriptions_not_found"; // açıklamalar bulunamadı
        public static string operation_claim_not_found = "operation_claim_not_found"; // operation claim bulunamadı
        public static string operation_claims_not_found = "operation_claims_not_found"; // operation claimler bulunamadı(kullanılmıyor)
        public static string name_update_failed = "name_update"; // İsim soyisim güncellenemez(kullanılmıyore)

        public static string wrong_code = "Wrong Code!";
        public static string wrong_code_code = "wrong_code";

        public static string wrong_email = "Wrong Email!"; // Yanlış email
        public static string wrong_email_code = "wrong_email"; // Yanlış email

        public static string empty_security_code = "empty_security_code"; // güvenlik kodu boş 

        public static string wrong_security_code = "Security Code is Wrong!";
        public static string wrong_security_code_code = "wrong_security_code";

        public static string wrong_auth_code = "wrong_auth_code"; // auth kodu yanlış
                                                                  //public static string wait_for_new_code = "wait_for_new_code"; //Yeni kod için bekleyin
                                                                  //public static string not_found_security_type = "not_found_security_type";
        public static string kyc_required = "kyc_required"; // kyc gerekli
        public static string kyc_required_code = "kyc_required"; // kyc gerekli

        public static string kyc_not_active = " kyc_not_active"; // kyc gerekli
        public static string kyc_not_active_code = " kyc_not_active"; // kyc gerekli

        public static string kyc_not_found = "kyc_not_found"; // kyc Bulunamadı
        public static string kyc_not_found_code = "kyc_not_found"; // kyc Bulunamadı

        public static string kyc_waiting_confirmed = "kyc_waiting_confirmed"; // kyc durumunuz onay bekliyor
        public static string kyc_is_confirmed = "kyc_is_confirmed"; // kyc onaylanmıs
        public static string subnetwork_is_not_active = "Subnetwork is not active"; // subnetwork aktif değil
        public static string subnetwork_is_not_active_code = "subnetwork_is_not_active"; // subnetwork aktif değil
        public static string network_not_active = "network_not_active"; // network aktif değil
        public static string mainnetwork_not_found = "Main Network Not Found"; // mainnetwork bulunamadı
        public static string mainnetwork_not_found_code = "mainnetwork_not_found"; // mainnetwork bulunamad
        public static string mainnetwork_already_exists = "Main Network Already Exists"; // mainnetwork zaten ekli
        public static string mainnetwork_already_exists_code = "mainnetwork_already_exists"; // mainnetwork zaten ekli

        public static string mainnetwork_is_not_active = "mainnetwork_is_not_active"; // mainnetwork aktif değil
        public static string crypto_withdraw_not_active = "Crypto withdraw not active"; // kripto çekimi aktif değil
        public static string crypto_withdraw_not_active_code = "crypto_withdraw_not_active"; // kripto çekimi aktif değil
        public static string min_withdrawal_limit = "min_withdrawal_limit"; // minimun çekim limitinden az
        public static string not_enough_information_for_withdraw = "not_enough_information_for_withdraw"; // çekim için yetersiz bilgi
        public static string inactive_crypto_deposit = "inactive_crypto_deposit"; // kripto yatırma aktif değil
        public static string withdraw_not_enough = "The amount you want to withdraw does not enough for the commission and network fee"; // çekmek istediğiniz tutar komisyon ve network fee için yeterli değil
        public static string withdraw_not_enough_code = "withdraw_not_enough"; // çekmek istediğiniz tutar komisyon ve network fee için yeterli değil
        public static string inactive_crypto_withdraw = "inactive_crypto_withdraw"; // kripto çekme aktif değil
        public static string user_transfer_adress_not_found = "user_transfer_adress_not_found"; // kullanıcı transfer adresi bulunamadı
        public static string user_transfer_adresses_not_found = "user_transfer_adresses_not_found"; // kullanıcı transfer adresleri bulunamadı
        public static string transfer_wallet_already_added = "transfer_wallet_already_added"; // transfer cüzdanı çoktan eklendi
        public static string wallet_already_created = "Wallet has already creadted"; //cüzdan çoktan oluşuruldu
        public static string wallet_already_created_code = "wallet_already_created"; //cüzdan çoktan oluşuruldu
        public static string wallet_not_created = "Wallet could not created"; //cüzdan oluşturulamadı
        public static string wallet_not_created_code = "wallet_not_created"; //cüzdan oluşturulamadı
        public static string wallet_not_found = "Wallet not found"; // kullanıcı cüzdanı bulunamadı
        public static string wallet_not_found_code = "wallet_not_found"; // kullanıcı cüzdanı bulunamadı
        public static string user_wallets_not_found = "user_wallets_not_found"; // kullanıcı cüzdanları bulunamadı
        public static string wallet_cannot_be_empty = "Wallet cannot be empty"; //cüzdan adres boş
        public static string wallet_cannot_be_empty_code = "wallet_cannot_be_empty"; //cüzdan adres boş
        public static string commision_is_null = "commision_is_null"; // Komisyon Bulunmadı
        public static string countries_not_found = "countries_not_found";// Ülke bulunamadı...
        public static string states_not_found = "states_not_found";// Bölge bulunamadı...
        public static string user_not_active = "User is not active";// Kullanıcı aktif değil...
        public static string user_not_active_code = "user_not_active";// Kullanıcı aktif değil...

        public static string wrong_password = "Password is Wrong!";// Yanlış parola...
        public static string wrong_password_code = "wrong_password";// Yanlış parola...

        public static string token_generate_failed = "Token Generation Has Failed!";// Token oluştururken hata oldu...
        public static string token_generate_failed_code = "token_generate_failed";// Token oluştururken hata oldu...

        public static string not_found_data = "Data not found";// Veri bulunamadı...
        public static string not_found_data_code = "not_found_data";// Veri bulunamadı...
        public static string already_registered = "already_registered";// Zaten kayıtlı...
                                                                       //public static string already_mail_registered = "mail_already_registered";// Mail Zaten kayıtlı...
                                                                       //public static string already_tel_registered = "tel_already_registered";// Telno Zaten kayıtlı...
        public static string already_added = "Data has already added!";// Zaten ekli...
        public static string already_added_code = "already_added";// Zaten ekli...
        public static string err_null = "null";// Boş dönme hatası...

        public static string token_not_found = "Token is missing!";// Token bulunamadı...
        public static string token_not_found_code = "token_not_found";// Token bulunamadı...

        public static string err_datetime = "datetime";// Zaman hatası...(kullanılmıyor)
        public static string err_code_expired = "err_code_expired"; //Zaman aşımı kod hatası...
        public static string err_invalid_value = "invalid_value";// Geçersiz değer hatası...
        public static string err_parity_status = "parity_status";// Parity aktif değil...
        public static string err_zero_amount = "zero_amount";// Miktar sıfır olamaz...
        public static string err_min_amount = "min_amount";// Minimum satın almadan düşük olamaz... ???
        public static string err_market_buy_limit = "market_buy_limit";// Satın alma limiti toplamdan düşük olamaz... ???
        public static string err_insufficient_balance = "Insufficient Balance";//Yetersiz limit...
        public static string err_insufficient_balance_code = "insufficient_balance";//Yetersiz limit...

        public static string order_not_found = "Order Couldn't Found!";
        public static string order_not_found_code = "order_not_found";

        public static string err_ico_amount_end = "ico_amount_end";// ???
        public static string err_order_is_processing = "order_is_processing";// Sipariş işleme hatası...
        public static string ico_not_active = "ico_not_active";// ico aktif değil...

        public static string parity_not_found = "Parity Couldn't Found!";// Parite bulunamadı...
        public static string parity_not_found_code = "parity_not_found ";// Parite bulunamadı...

        public static string invalid_parity_name = "Invalid Parity Name!";// Geçersiz Parite ...
        public static string invalid_parity_name_code = "invalid_parity_name ";// Geçersiz Parite...

        public static string parity_already_exists = "Parity Already Exists!";// Parite zaten ekli...
        public static string parity_already_exists_code = "parity_already_exists ";// Parite zaten ekli...

        public static string parities_cant_same = "Parity Can't Be Same!";// Pariteler aynı olamaz
        public static string parities_cant_same_code = "parities_cant_same";//  

        public static string delete_failed = "Deleting Operation Has Failed!"; // delete işlemi başarısız
        public static string delete_failed_code = "delete_failed"; // delete işlemi başarısız

        public static string deposit_not_found = "Deposit not found";// Crypto deposit bulunamadı...
        public static string deposit_not_found_code = "deposit_not_found";// Crypto deposit bulunamadı...
        public static string r_add_failed = "r_add_failed"; // robot buy order add başarısız
        public static string r_delete_failed = "r_delete_failed"; // robot buy order delete başarısız
        public static string r_update_failed = "r_update_failed"; // robot buy order update başarısız
        public static string r_buy_order_not_found = "r_buy_order_not_found"; // robot buy order bulunamadı
        public static string r_sell_order_not_found = "r_sell_order_not_found"; // robot sell order bulunamadı;
        public static string crypto_parity_not_found = "crypto_parity_not_found"; // crypto parity bulunamadı
        public static string exchange_rate_not_found = "exchange_rate_not_found"; // exchange rate bulunamadı
        public static string withdraw_not_found = "Withdraw not found"; // crypto withdraw bulunamadı
        public static string withdraw_not_found_code = "withdraw_not_found"; // crypto withdraw bulunamadı
        public static string delegation_not_found = "delegation_not_found"; // delegation bulunamadı
        public static string any_photo_not_found = "any_photo_not_found"; // herhangi bir fotoğrafı eklenemedi 
        public static string limit_not_found = "limit_not_found"; // limit bulunamadı
        public static string locked_balance_not_found = "locked_balance_not_found"; // kilitlenmiş bakiye bulunamadı
        public static string level_commission_not_found = "level_commission_not_found"; // komisyon leveli bulunamadı
        public static string reference_not_found = "reference_not_found"; // referans bulunamadı

        public static string security_not_found = "Security Type Couldn't Found!"; // security bulunamadı
        public static string security_not_found_code = "security_not_found"; // security bulunamadı

        public static string password_history_not_found = "Password History Couldn't Found!"; // security bulunamadı
        public static string password_history_not_found_code = "password_history_not_found"; // security bulunamadı


        public static string support_not_found = "support_not_found"; // support bulunamadı
        public static string support_category_not_found = "support_category_not_found"; // support category bulunamadı

        public static string sell_order_not_found = "SellOrder Couldn't Found!"; // sell Order bulunamadı
        public static string sell_order_not_found_code = "sell_order_not_found"; // sell Order bulunamadı

        public static string buy_order_not_found = "BuyOrder Couldn't Found!"; // buy Order bulunamadı
        public static string buy_order_not_found_code = "buy_order_not_found"; // buy Order bulunamadı

        public static string currency_not_found_code = "currency_not_found"; // crypto currency bulunamadı
        public static string currency_not_found = "Currency Couldn't Found!"; // crypto currency bulunamadı

        public static string currency_already_exists_code = "currency_already_exists"; // crypto currency zaten ekli
        public static string currency_already_exists = "Currency Already Exists!"; // crypto currency zaten ekli

        public static string permission_not_found = "permission_not_found"; // permission bulunamadı
        public static string employee_permission_not_found = "employee_permission_not_found"; // employee permission bulunamadı(kullanılmıyor)
        public static string employee_role_not_found = "employee_role_not_found"; // employee role bulunamadı
        public static string tr_bank_not_found = "tr_bank_not_found_not_found"; // TR Bank bulunamadı
        public static string crypto_parity_already_exists = "crypto_parity_already_exists"; // crypto parity zaten mevcut(kullanılmıyor)
        public static string crypto_deposit_isnt_waiting = "deposit_isnt_waiting"; // crypto deposit bekleyen durumunda değil(kullanılmıyor)
        public static string crypto_withdraw_isnt_in_waiting = "crypto_withdraw_isnt_in_waiting"; // crypto withdraw bekleyen durumunda değil(kullanılmıyor)
        public static string crypto_withdraw_isnt_in_preapproval = "withdraw_isnt_in_preapproval"; // crypto withdraw ön onayda durumunda değil(kullanılmıyor)
        public static string crypto_deposit_already_refused = "crypto_deposit_already_refused"; // crypto deposit zaten reddedildi(kullanılmıyor)
        public static string crypto_deposit_turkish_lira = "crypto_deposit_turkish_lira"; //önce tl yatırması gerekli
        public static string balance_not_enough = "balance_not_enough"; // bakiye yetersiz(kullanılmıyor)
        public static string identity_image_could_not_be_added = "identity_image_could_not_be_added"; // kimlik fotoğrafı eklenemedi 
        public static string selfie_image_could_not_be_added = "selfie_image_could_not_be_added"; // özçekim fotoğrafı eklenemedi 
        public static string cities_not_found = "cities_not_found";
        public static string at_least_one_option_must_be_active = "At Least One Option Must Be Active";//en az bir tane güvenlik seçeneği aktif olmalıdır.
        public static string at_least_one_option_must_be_active_code = "at_least_one_option_must_be_active";//en az bir tane güvenlik seçeneği aktif olmalıdır.
        public static string cannot_update_more_than_one_security_option = "Cannot Update More Than One Security Option At The Samet Time";//tek bir seferde 1 den fazla güvenlik seçeneği güncellenemez.
        public static string cannot_update_more_than_one_security_option_code = "cannot_update_more_than_one_security_option_at_the_same_time";//tek bir seferde 1 den fazla güvenlik seçeneği güncellenemez.
        public static string passwords_cannot_be_the_same = "passwords_cannot_be_the_same";//tek bir seferde 1 den fazla güvenlik seçeneği güncellenemez.
        public static string you_cannot_make_multiple_withdrawals = "You cannot make multiple withdrawals. please wait for your transaction to be processed";

        public const string leads_not_found = "leads_not_found";// lead bulunamadı.
        public const string lead_notes_not_found = "lead_notes_not_found"; // lead notları bulunamadı.
        public const string calling_statuses_not_found = "calling_statuses_not_found"; // arama durumları bulunamadı.
        public const string customer_representatives_not_found = "customer_representatives_not_found"; // müşteri temsilcisi bulunamadı.(kullanılmıyor)
        public const string ib_references_not_found = "ib_references_not_found"; // ib referansları bulunamadı.
        public const string lead_callings_not_found = "lead_callings_not_found"; // lead aramaları bulunamadı.
        public const string leads_or_users_not_found = "leads_or_users_not_found";// leadsorusers bulunamadı.
        public const string lead_status_not_found = "lead_status_not_found";// lead statüleri bulunamadı.
        public const string lead_already_exists = "lead_already_exists"; // böyle bir lead zaten var
        public const string lead_source_already_exists = "lead_source_already_exists"; // böyle bir lead source zaten var
        public const string lead_source_not_found = "lead_source_not_found";// lead source bulunamadı
        public const string lead_history_already_exists = "lead_history_already_exists";// lead history zaten mevcut(kullanılmıyor)
        public const string lead_history_not_found = "lead_history_not_found";// lead history bulunamadı
        public const string lead_already_hasnt_any_customer_representative = "lead_already_hasnt_any_customer_representative";// lead zaten herhangi bir mt ye sahip değil
        public const string lead_has_already_a_customer_representative = "lead_has_already_a_customer_representative ";// lead zaten 
        public const string lead_note_type_not_found = "lead_note_type_not_found";// lead note type bulunamadı
        public const string teams_not_found = "teams_not_found";// takım bulunamadı.(kullanılmıyor)
        public const string page_size_compulsory = "page_size_compulsory";// sayfa ve eleman sayısı zorunlu alanlar.(kullanılmıyor)
        public const string verification_status_not_found = "verification_status_not_found";// doğrulama durumu bulunamadı(Kullanılmıyor)
        public const string email_or_phone_already_used_by_someone = "email_or_phone_already_used_by_someone";// girilen mail veya numara zaten kullanılıyor

        public const string ib_notes_not_found = "ib_notes_not_found";// ib notları bulunamadı.
        public const string crm_users_ib_histories_not_found = "crm_users_ib_histories_not_found";// müşteri temsilcisinin ib geçmişi bulunamadı.(kullanılmıyor)
        public const string ib_callings_not_found = "ib_callings_not_found"; // ib aramaları bulunamadı.
        public const string client_verification_statuses_not_found = "client_verification_statuses_not_found";//müşteri durumları bulunamadı. (kullanılmıyorum)

        public const string subnetwork_is_not_found = "Subnetwork is not found";// subnetwork bulunamadı.
        public const string subnetwork_is_not_found_code = "subnetwork_is_not_found";// subnetwork bulunamadı.

        public const string company_iban_address_not_found = "company_iban_address_not_found";// şirket iban adresi bulunamadı.

        public const string you_need_wait_for_new_messages = "you need wait for new messages 10 second...";
        public const string user_ticket_not_found = "user ticket not found";

        public const string expire_date_cant_be_leave_empty = "expire_date_cant_be_leave_empty"; // expire date boş bırakılamaz eğer crmUser tarafından ekleniyor veya onaylanıyor ise.
        public const string document_uploading_failed = "document_uploading_failed"; // döküman amazon server a yüklenemedi
        public const string document_not_found = "document_not_found"; // döküman bulunamadı
        public const string confirmed_refused_just_delete = "confirmed_refused_just_delete"; // onaylanmış veya red edilmiş dökümanlar sadece silinebilir.
        public const string document_already_confirmed = "api_document_already_confirmed"; // döküman zaten onaylanmış(kullanılmıyor)
        public const string document_already_deleted = "document_already_deleted"; // döküman zaten silinmiş
        public const string document_already_refused = "document_already_refused"; // döküman onayı zaten geri çevirilmiş     
        public const string description_cant_be_leave_empty = "description_cant_be_leave_empty"; // description boş kalamaz
        public const string cant_upload_when_have_waiting = "cant_upload_when_have_waiting"; // onaylanmayı bekleyen dosya varken yeni dosya yüklenemez


        public const string ib_note_type_not_found = "ib_note_type_not_found";// ib not tipi bulunamadı(kullanılmıyor)
        public const string ib_source_not_found = "ib_source_not_found";// ib kaynağı bulunamadı.
        public const string ib_source_already_exists = "ib_source_already_exists";// ib kaynağı zaten mevcut.(kullanılmıyor)
        public const string ib_status_not_found = "ib_status_not_found";// ib statüsü bulunamadı.(kullanılmıyor)

        public const string ib_history_not_found = "ib_history_not_found";// ib geçmişi bulunamadı.(kullanılmıyo)

        public const string ib_already_hasnt_any_customer_representative = "ib_already_hasnt_any_customer_representative";// ib'nin müşteri temsilcisine ataması yok.(kullanılmıyor)

        public const string ib_has_already_a_customer_representative = "ib_has_already_a_customer_representative";// ib'nin müşteri temsilcisi ataması var.(kullanılmıyor)

        public const string ib_not_found = "ib_not_found";// ib bulunamadı.

        public const string compulsory_fileds_cant_empty = "compulsory_fileds_cant_empty"; // zorunlu alanlar boş bırakılamaz (kullılmıyor)


        public const string doc_cant_deleted_by_users = "doc_cant_deleted_by_users"; // user tarafından oluşturulan dökümanlar silinemez

        public const string password_cant_contain_name = "Password Can't Contain Name";
        public const string password_cant_contain_name_code = "password_cant_contain_name";

        public const string password_cant_contain_surname = "Password Can't Contain Surname";
        public const string password_cant_contain_surname_code = "password_cant_contain_surname";

        public const string password_cant_contain_email = "Password Can't Contain Email";
        public const string password_cant_contain_email_code = "password_cant_contain_email";

        public const string password_cant_contain_last4phone = "Password Can't Contain Last 4 digit of Phone Number!"; // şifre telefon numarasının son 4 hanesini içeremez
        public const string password_cant_contain_last4phone_code = "password_cant_contain_last4phone"; // şifre telefon numarasının son 4 hanesini içeremez

        public const string password_cant_contain_birth_year = "password_cant_contain_birth_year"; // şifre doğum yılını içeremez(kullanılmıyor)

        public const string password_cant_contain_identity_number = "Password Can't Contain Identity Number";
        public const string password_cant_contain_identity_number_code = "password_cant_contain_identity_number";

        public const string blog_category_couldnt_found = "blog_category_couldnt_found"; // blog category bulunamadı(kullanılmıyor)
        public const string country_city_state_not_found = "country_city_state_not_found"; // şehir ülke veya bölge bulunamadı(kullanılmıyor)
        public const string blog_image_not_found = "blog_image_not_found"; // blog resmi bulunamadı (kullanılmıyor)

        public const string passwords_cant_same_last3 = "Password Can't be Same with Last 3!"; // şifre son 3ünden biriyle aynı olamaz(kullanılmıyor)
        public const string passwords_cant_same_last3_code = "password_cant_same_last3"; // şifre son 3ünden biriyle aynı olamaz(kullanılmıyor)

        public const string multiple_wrong_code = "multiple_wrong_code"; // Birden fazla yanlış kod girilmiş.(kullanılmıyor)

        public const string user_status_list_not_found = "user_status_list_not_found"; // User status list bulunamadı.(kullanılmıyor)

        public const string user_status_not_found = "User Status Couldn't Found!"; // User status  bulunamadı.(kullanılmıyor)
        public const string user_status_not_found_code = "user_status_not_found"; // User status  bulunamadı.(kullanılmıyor)

        public const string user_status_history_list_not_found = "user_status_history_list_not_found"; // User status history listesi bulunamadı.(Kullanılmıyor)
        public const string user_status_history_not_found = "user_status_history_not_found"; // User status history bulunamadı.(kullanılmılyor)

        public const string blocked_permanently = "blocked_permanently"; // Kalıcı olarak bloklandı biz tekrar açana kadar. (kullanılmıyor)


        public const string identity_back_image_could_not_be_added = "identity_back_image_could_not_be_added"; // kimlik arka fotoğrafı eklenemdi. 
        public const string invalid_identity = "invalid_identity"; // Geçersiz Kimlik Bilgileri
        public const string user_infos_cant_change = "user_infos_cant_change";// Kycs onaylı veya bekler durumdaysa kullanıcı bilgileri güncellenemez.
        public const string agreement_content_not_found = "agreement_content_not_found";// Sözleşme bulunamadı
        public const string agreement_content_list_not_found = "agreement_content_list_not_found";// Sözleşme listesi bulunamadı
        public const string agreement_category_not_found = "agreement_category_not_found";// Sözleşme kategorisi bulunamadı
        public const string agreement_category_list_not_found = "agreement_category_list_not_found";// Sözleşme kategori listesi bulunamadı
        public const string user_password_history_not_found = "user_password_history_not_found"; //kullanıcı şifre geçmişi bulunamadı
        public const string user_otp_history_not_found = "user_otp_history_not_found"; // kullanıcının herhangi bir otp geçmişi bulunamadı(kullanılmıyor)
        public const string invalid_phone_number = "invalid_phone_number"; // Telefon numarası geçerli bir numara değil
        public const string invalid_phone_number_code = "invalid_phone_number_code"; // Telefon numarası geçerli bir numara değil

        public const string invalid_recaptcha = "invalid_recaptcha"; // Geçersiz Recaptcha
        public const string agreement_content_version_increased = "agreement_content_version_increased";// Sözleşme versiyonu arttı.(kullanılmıyor)
        public const string user_wrong_security_answer = "user_wrong_security_answer";// yanlış güvenlik sorusu cevabı

        public const string user_department_group_not_found = "user_department_group_not_found";// Kullanıcı departman grubu bulunamadı.
        public const string department_group_not_found = "department_group_not_found";//  Departman grubu bulunamadı.
        public const string department_group_not_active = "department_group_not_active";//  Departman grubu aktif değil.


        public const string user_security_question_not_found = "user_security_question_not_found";// güvenlik sorusu bulunamadı
        public const string answer_length_max50 = "answer_length_max50";// güvenlik soru cevabı maks 50 karakter (şu an kullanılmıyor)


        public const string black_list_user_list_not_found = "black_list_user_list_not_found"; //Kara liste kullanıcı listesi bulunamadı.
        public const string black_list_user_not_found = "black_list_user_not_found"; //Kara listede kullanıcı bulunamadı.
        public const string black_list_information_type_list_not_found = "black_list_information_type_list_not_found"; //Kara liste bilgi tipi listesi bulunamadı.
        public const string black_list_information_type_not_found = "black_list_information_type_not_found"; //Kara liste bilgi tipi bulunamadı.
        public const string black_list_user_information_list_not_found = "black_list_user_information_list_not_found"; //Kara liste kullanıcı bilgi listesi bulunamadı.
        public const string black_list_user_information_not_found = "black_list_user_information_not_found"; //Kara liste kullanıcı bilgisi bulunamadı.
        public const string profile_infos_missing = "profile_infos_missing"; // profil bilgileri eksik
        public const string agreement_category_not_able_to_update = "agreement_category_not_able_to_update"; // sözleşme kategorisi güncellenemez.
        public const string author_not_found = "author_not_found"; // Yazar bulunmaadı
        public const string author_is_not_active = "author_is_not_active"; // Yazar aktif değil
        public const string author_image_could_not_be_added = "author_image_could_not_be_added"; // Yazar fotoğrafı yüklenemedi (kullanılmıyor)
        public const string err_black_list = "black_list"; // Kullanıcı black list listesinde bulunmakta.
        public const string user_information_not_found = "user_information_not_found"; // Kullanıcı bilgileri bulunamadı.
        public const string user_information_already_exists = "user_information_already_exists"; // Kullanıcı bilgileri zaten öncecen girilmiş.(kullanılmıyor)
        public const string level_status_list_not_found = "level_status_list_not_found"; // Level statü listesi bulunamadı
        public const string wrong_nvi_three_times = "wrong_nvi_three_times"; // 3 kez yanlış nvi girildi
        public const string user_department_not_found = "user_department_not_found"; // Kullanıcının ait olduğu departman bulunamadı.

        public const string security_type_activated = "Security Type Activated"; // Güvenlik tipi aktive edildi.
        public const string security_type_activated_code = "api_security_type_activated"; // Güvenlik tipi aktive edildi.
        public const string security_type_deactivated = "Security Type Deactivated"; // Güvenlik tipi devre dışı bırakıldı.
        public const string security_type_deactivated_code = "api_security_type_deactivated"; // Güvenlik tipi devre dışı bırakıldı.


        public const string agreement_content_url_can_not_duplicated = "agreement_content_url_can_not_duplicated"; // Author translation not found.
        public const string you_have_to_accept_agreements = "you_have_to_accept_agreements"; // You have to accept agreements.
        public const string author_translation_not_found = "api_author_translation_not_found"; // Author translation bulunamadı.
        public const string author_translation_list_not_found = "api_author_translation_list_not_found"; // Author translation listesi bulunamadı.

        public const string document_type_not_found = "api_document_type_not_found"; // Döküman tipi bulunamadı.
        public const string document_type_list_not_found = "api_document_type_list_not_found"; // Döküman tipi listesi bulunamadı.

        public const string denied_process_not_found = "denied_process_not_found"; // Engellenen işlem bulunamadı.
        public const string denied_process_list_not_found = "denied_process_list_not_found"; // Engellenen işlem listesi bulunamadı.(kullanılmıyor)
        public const string denied_process_same_description_entered = "denied_process_same_description_entered"; // Böyle bir açıklama zaten mevcut.
        public const string user_status_denied_process_list_not_found = "user_status_denied_process_list_not_found"; // Kullanıcı statü engellenen işlem listesi bulunamadı.

        public const string memo_is_false = "api_memo_is_false"; // Memo bulunmamakta.
        public const string memo_is_null = "api_memo_is_null"; // Memo null dönmekte.

        public const string invalid_mail = "invalid_mail"; // Crm user geçersiz mail.
        public const string crm_user_added_by_manager = "api_crm_user_added_by_manager"; // Crm user yönetici tarafından eklendi.

        public const string department_group_list_not_found = "department_group_list_not_found"; // Departman grup listesi bulunamadı.
        public const string initial_department_group_list_not_found = "initial_department_group_list_not_found"; // initial departman grup listesi bulunamadı.

        public const string department_not_found = "department_not_found"; //Departman bulunamadı.

        public const string crm_user_not_found = "crm_user_not_found"; //Crm kullanıcısı bulunamadı.
        public const string denied_process_detected = "denied_process_detected"; //Kullanıcı için izin verilmeyen bir işlem gerçekleşti.


        public const string permission_exists = "permission_exists"; // Yetki halihazırda bulunuyor.

        public const string default_permissions_not_found = "default_permissions_not_found"; // Varsayılan izinler bulunamadı.(kullanılmıyor)

        public const string user_approved_agreement_not_found = "user_approved_agreement_not_found";
        public const string user_accepted_agreement_not_same = "user_accepted_agreement_not_same";
        public const string user_iban_not_found = "user_iban_not_found";
        public const string iban_does_not_belon_to_user = "iban_does_not_belon_to_user";
        public const string send_record_not_valid = "send_record_not_valid";//kullanılmıyor
        public const string send_record_id_used_already = "send_record_id_used_already";//kullanılmıyor)
        public const string transfer_not_succesfull = "transfer_not_succesfull"; // transfer başarılı değil
        public const string transfer_not_succesfull_code = "Transfer not succesfull"; // transfer başarılı değil

        public const string pdf_is_required = "pdf_is_required";//Pdf dosyası gerekli.


        public const string kyc_approved_or_waiting = "kyc_approved_or_waiting"; // kullanıcı kyc onaylandıktan sonra yada onay beklerken bilgilerini değiştiremez.

        public const string is_edidtable = "api_is_editable_is_false"; // isEditable alanı false(Kullanılmıyor)

        public const string ticket_assignation_data_not_found = "api_ticket_assignation_data_not_found"; // atama verisi bulunamadı.(kullanılmıyor)

        public const string passwords_doesnt_match = "passwords_doesnt_match";// Şifreler eşleşmiyor.

        public const string carousel_list_not_found = "carousel_list_not_found"; // Carousel listesi bulunamadı
        public const string carousel_not_found = "carousel_not_found"; // Carousel bulunamadı
        public const string carousel_back_image_could_not_be_added = "carousel_back_image_could_not_be_added"; // Carousel arka fotoğrafı eklenemdi. 
        public const string carousel_photo_could_not_be_added = "carousel_photo_could_not_be_added"; // Carousel fotoğrafı eklenemdi. (Kullanılmıyor)


        public const string only_users_living_in_turkey_can_upload_document = "only_users_living_in_turkey_can_upload_a_residence_document"; //Yalnızca Türkiye'de yaşayan kullanıcılar ikametgah belgesi yükleyebilir



        public const string carousel_version_already_exits = "carousel_version_already_exits"; // Carousel versiyonu zaten mevcut. 



        public const string s3_upload_error = "s3_upload_error"; // S3 yükleme işlemi başarısız. 
        public const string s3_upload_error_code = "s3_upload_error"; // S3 yükleme işlemi başarısız. 

        public const string bucket_not_found = "bucket_not_found"; // Bucket bulunamadı. 
        public const string bucket_not_found_code = "bucket_not_found"; // Bucket bulunamadı. 



        public const string get_from_s3_error = "get_from_s3_error"; // S3 indirme işlemi başarısız. (Kullanılmıyor)


        public const string author_list_not_found = "author_list_not_found"; // Yazar lsitesi bulunamadı
        public const string blog_content_not_found = "blog_content_not_found"; // Blog İçeriği bulunamadı . 

        public const string identity_no_exists = "identity_no_exists";// Kimlik numarası bulunuyor.
        public const string no_field_changed = "no_field_changed";// Update apilerinde aynı değerlerle güncellenme yapılmak istendiğinde verilebilir.


        public const string blog_category_list_not_found = "blog_category_list_not_found"; //Blog kategori listesi bulunamadı.

        public const string contact_information_not_found = "contact_information_not_found"; //Kontakt bulunamadı.(kullanılmıyor)

        public const string elliptic_wallet_cant_added = "elliptic_wallet_cant_added"; // Cüzdan analizi elliptiğe eklenemedi.
        public const string contact_category_not_found = "contact_category_not_found"; // kontact kategory bulunamadı.


        public const string custom_url_not_found = "custom_url_not_found";

        public const string user_balance_list_not_found = "user_balance_list_not_found"; // Kullanıcı bakiye listesi bulunamadı.

        public const string user_transaction_history_list_not_found = "user_transaction_history_list_not_found"; // Kullanıcının işlem geçmişi bulunamadı.

        public const string user_token_list_not_found = "user_token_list_not_found"; // Kullanıcının token listesi bulunamadı.
        public const string qrlink_not_found = "qrlink_not_found"; // QRLink bulunamadı.
        public const string qrlink_list_not_found = "qrlink_list_not_found"; // QRLink listesi bulunamadı.

        public const string language_codes_can_not_be_same = "language_codes_can_not_be_same"; // Dil kodları aynı olamaz.
        public const string qr_setting_not_found = "qr_setting_not_found"; // Qr setting bulunamadı.
        public const string qr_setting_list_not_found = "qr_setting_list_not_found"; // Qr setting listesi bulunamadı.

        public const string user_have_unsigned_agreement = "user_have_unasigned_agreement"; // kullanıcının imzalamadığı sözleşmeler var;

        public const string you_have_2_entries_left_please_check_your_information = "you_have_2_entries_left_please_check_your_information"; // nvi 2 hakkınız kaldı;
        public const string you_have_1_entries_left_please_check_your_information = "you_have_1_entries_left_please_check_your_information"; // nvi 1 hakkınız kaldı;


        public const string user_transfer_adresses_document_not_found = "user_transfer_address_document_notfound"; // kullanıcının yüklediği iban dokümanı bulunamadı.
        public const string cant_change_email_sms = "user_cant_change_sms_and_email_securities"; // kullanıcı email ve sms doğrulamalarını kapatamaz.

        public const string company_not_found = "company_not_found"; // Banka bulunamadı

        public const string sms_code_wrong = "sms_code_wrong";//Sms kodu yanlış.
        public const string mail_code_wrong = "mail_code_wrong";//Mail kodu yanlış.

        public const string sms_mfa_not_ready = "SMS Mfa Is Not Ready, Please Continue With Another Type'";//Sms kodu yanlış.
        public const string sms_mfa_not_ready_code = "mail_code_wrong";//Mail kodu yanlış.

        public const string security_question_not_found = "security_question_not_found"; // güvenlik sorusu bulunamadı.
        public const string user_not_allowed_livesupport = "user_not_allowed_livesupport"; // kullanıcı live support için yetkisi yok
        public const string wait_for_validation = "wait_for_validation"; // validasyon için bekleme gerekiyor
        public const string sanction_black_list = "sanction_black_list"; // sanction scanner da black list içerisinde yer alan kullancı!
        public const string sanction_bad_request = "sanction_bad_request"; // sanction scanner http request fail oldu
        public const string parityLimit_already_exist = "parityLimit_already_exist"; // parity limit zaten ekli
        public const string parityLimit_not_found = "parityLimit_not_found"; // parity limit bulunamadı;
        public const string image_already_uploaded = "image_already_uploaded"; // kyc fotoğrafı daha önce yüklendi

        public const string invalid_token = "Invalid Token!";
        public const string invalid_token_code = "api_invalid_token";

        public const string already_mail_registered = "Membership operation was done before with this e-mail. Please continue to membership operation with another e-mail.";
        public const string already_mail_registered_code = "mail_already_registered";

        public const string already_tel_registered = "Membership operation was done before with this phone number. Please continue to membership operation with another phone number.";
        public const string already_tel_registered_code = "tel_already_registered";

        public const string password_cant_contain_last_4_digit_phone_number = "Password Can't Contain Last 4 Digits of Your Phone Number";
        public const string password_cant_contain_last_4_digit_phone_number_code = "password_cant_contain_last_4_digit_phone_number";

        public const string not_found_security_type = "Not Found Security Type";
        public const string not_found_security_type_code = "not_found_security_type";

        public const string wait_for_new_code = "You Have To Wait A Bit For New Code";
        public const string wait_for_new_code_code = "wait_for_new_code";

        public const string user_control_code_null = "User Control Code null";


        public const string transaction_cannot_be_done = "Transaction cannot be done for BTC at the moment";
        public const string transaction_cannot_be_done_code = "transaction_cannot_be_done_for_btc_at_the_moment";

        public const string invalid_wallet_address = "Invalid Wallet Address";
        public const string invalid_wallet_address_code = "invalid_wallet_address";

        public const string google_setup_already_done = "google_setup_already_done";
        public const string google_setup_already_done_code = "Google Setup Has Already Done!";

        public const string google_has_own_auth = "Google Has Own Authenticator App To Getting Code Please Use That App!";
        public const string google_has_own_auth_code = "google_has_own_auth";

        public const string invalid_request = "invalid_request";
        public const string invalid_request_code = "invalid_request_code";


        public const string invalid_order_type = "Invalid Order Type!";
        public const string invalid_order_type_code = "invalid_order_type";


        public const string lecture_not_found = "lecture_not_found";
        public const string classroom_not_found = "classroom_not_found";
        public const string scheduleRecord_not_found = "scheduleRecord_not_found";
    }
}