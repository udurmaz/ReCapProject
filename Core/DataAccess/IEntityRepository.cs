//Core başka bir classları referans olarak kullanmaz benim istediğim Core genel bir class olması lazım başka bir yere bağımlı olmaması lazım.
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess //Bu isimlendirme Core Projesinde / DataAccess klasörüne erişmek için kullanılır.
{
    public interface IEntityRepository <T> where T: class, IEntity , new()
    {
        //Generic yapı kullanma
        // T içine ben ne verirsem sen onu çağır demek , onun için kuıllandık
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter); //filtre vermemişse tüm datayı getir demektir
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
