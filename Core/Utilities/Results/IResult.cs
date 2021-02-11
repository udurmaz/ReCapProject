using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; } // Başarılı mı başarısız mı olduğunu anlamak için
        string Message { get; } // Yazdırılacak mesaj türü
    }
}
