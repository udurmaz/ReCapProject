using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash , out byte[] passwordSalt) //verdiğimiz passowrdun hashini oluştur
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //Gönderilen passwordün byte değerini almamız gerekiyor
            }
        }
        public static bool VerifyPasswordHash(string password , byte[] passwordHash, byte[] passwordSalt) //Out olmayacak çünkü parolayı biz göndereceğiz
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) //Karşılaştırma
                    {
                        return false; //eşleşmezse false değeri döndür
                    }
                }
            }
            return true; //Eşleşirse true değeri döndür
        }
    }
}
