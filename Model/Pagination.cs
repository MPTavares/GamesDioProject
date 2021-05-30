using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Model
{
    public class Pagination
    {
        public int PageSize { get; set; }
        public int ActualPage { get; set; }
        public Pagination()
        {
            PageSize = 10;
            ActualPage = 1;
        }
    }
}
