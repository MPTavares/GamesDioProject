using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Exceptions
{
    internal class GameAlreadyExistsException :Exception
    {
        public GameAlreadyExistsException()
            :base("Game already exists")
        {

        }
    }
}
