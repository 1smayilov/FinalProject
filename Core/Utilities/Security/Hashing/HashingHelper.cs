using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    // Saltın Yaradılması:
    // HMACSHA512 alqoritmi istifadə edildikdə, bu alqoritm avtomatik olaraq bir açar(salt) yaradır.Bu açar passwordSalt olaraq saxlanır.
    // Parolun Hash Edilməsi:
    // ComputeHash metodu, parolu UTF8 formatında byte massivinə çevirir və bu byte massivini HMACSHA512 alqoritmi ilə hash edir.
    // Burada HMACSHA512 alqoritmi, parolun hash edilməsi zamanı passwordSalt(açar) istifadə edir.Bu deməkdir ki,
    // passwordSalt hash hesablanması prosesinə daxil edilir.
    public  class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) // out - çölə veriləcək dəyər
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) // Alqoritm
            {
                passwordSalt = hmac.Key; // alqoritmin key dəyəri, hər istifadəçi üçün ayrıca salt(key) oluşturur
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // string byte a çevrilərək heşləyir
            }
        }

        // təkrar girərkən aldığımız parolu yenidən heşliyəciyik və database - dəki hash ilə qarşılaştıracıyıq
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // Eyni salt istifadə edildikdə, həm orijinal, həm də təsdiqlənən parol üçün eyni hash dəyəri yaranır.
            // Bu metod daxil edilən parolun düzgün olub olmadığını yoxlamaq üçün passwordSalt-ı istifadə edir ki,
            // bu da hash-nin doğru olub olmadığını müəyyənləşdirməyə kömək edir.
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {   
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); 
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }

        }
    }
}
// Buranı başa düşə bilməsən ən yuxarıdakı komentlərə bax
