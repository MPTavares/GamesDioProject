using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Exceptions
{
    public class GameNotFoundException : Exception 
    {
        public GameNotFoundException()
            :base("Game not found")
        {

        }
    }
}
