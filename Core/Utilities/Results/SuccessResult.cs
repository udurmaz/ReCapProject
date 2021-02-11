using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message) : base (true,message) //Mesaj doğruysa base sınıfından mesajı al 
        {

        }
        public SuccessResult():base(true) //Mesaj yazdırmayabilir sadece doğru olduğunu gösterir
        {

        }
    }
}
