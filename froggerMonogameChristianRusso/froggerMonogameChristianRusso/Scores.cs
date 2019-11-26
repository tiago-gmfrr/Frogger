using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace froggerMonogameChristianRusso
{
    [Serializable]
   public class Scores
    {
        public string nom;
        public string temps;

        public Scores()
        {

        }
        public Scores(string n, string t)
        {
            nom = n;
            temps = t;
                }
    }
}
