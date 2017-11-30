using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Game
    {
        private List<String> lines = new List<String>();

        /*public Game(List<string> lines)
        {
            this.lines = lines;
        }*/

        public List<String> Lines { get => Lines; set => Lines = value; }

        public void InitGame()
        {

        }

        public void Updater(String move)
        {
            lines.Add(move); // 30 mins of debug because L != l ok very nice
        }

        public override string ToString()
        {
            //return base.ToString();
            return string.Join(", ", lines.ToArray());
        }

        public void Shower()
        {

        }
        public void Checker()
        {

        }

    }
}
