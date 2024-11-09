using System;

namespace Base.Model
{
    public class PagingRespone<T> where T : class
    {
        public IEnumerable<T> Datas { get; set; }
        public int Total { get; set; }
    }
}
