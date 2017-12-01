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
        private string[][] mat;
        private Block currentBlock;
        private int blockedLines;

        public List<String> Lines { get => Lines; set => Lines = value; }
        public string[][] Mat { get => mat; set => mat = value; } //get
        internal Block CurrentBlock { get => currentBlock; set => currentBlock = value; }
        public int BlockedLines { get => blockedLines; set => blockedLines = value; } //get

        /*public MatriceJeu(string[][] mat, int blockedLines)
        {
            this.mat = mat;
            this.blockedLines = blockedLines;
        }*/

        public void InitGame() //input: , output: 
        {

        }

        public void Updater(String move)
        {
            lines.Add(move); // 30 mins of debug because L != l ok very nice


        }

        public void Display()
        {

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

        public void Supprligne(int indicel) //il prend comme paramètre le numero de la ligne qu'il veut supprimer, puis il fait tomber toute les lignes situé au-dessus (principe de tetris) 
        {
            int i = indicel + 1;
            while (i > 1)
            {
                for (int j = 1; j < this.mat[i].Length; j++)
                {
                    this.mat[i][j] = this.mat[i - 1][j];

                }
                i--;

            }
        }

        public int RechercheLignePleine() // recherche la dernière ligne qui est considéré comme remplie
        {
            int result = -1;
            int cpt = 0;
            for (int i = 0; i < this.mat.Length - this.blockedLines; i++)
            {
                cpt = 1;
                for (int j = 0; j < this.mat[i].Length - 1; j++)
                {

                    if (this.mat[i][j] == this.mat[i][j + 1] && this.mat[i][0] != " ")            // pour l'instant je laisse la definition du tableau a " "
                    {
                        cpt++;
                    }


                }
                if (cpt == this.mat[i].Length)
                {
                    result = i;
                }
            }
            return result;


        }


        public void BlocageLigne()
        {
            for (int i = 0; i < this.mat.Length - 1 - this.blockedLines; i++)
            {
                for (int j = 0; j < this.mat[i].Length; j++)
                {
                    this.mat[i][j] = this.mat[i + 1][j];
                }
            }
            for (int k = this.mat.Length - 1; k >= this.mat.Length - 1 - this.blockedLines; k--)
            {
                for (int l = 0; l < this.mat[k].Length; l++)
                {
                    this.mat[k][l] = "*";
                }

            }
            this.blockedLines++;
        }

        public void DrawcurrentBlock(int x, int y)
        {
            int type = this.currentBlock.Category;
            this.currentBlock.PosX = x;
            this.currentBlock.PosY = y;
            if (type == 1 && x < this.mat.Length && y < this.mat[x].Length && this.mat[x][y] == " ")
            {
                this.mat[x][y] = "#";

            }
            if (type == 2 && x < this.mat.Length - 1 && y < this.mat[x].Length - 1 && this.mat[x][y] == " " && this.mat[x][y + 1] == " " && this.mat[x + 1][y] == "" && this.mat[x + 1][y + 1] == " ")
            {
                this.mat[x][y] = "#";
                this.mat[x][y + 1] = "#";
                this.mat[x + 1][y] = "#";
                this.mat[x + 1][y + 1] = "#";
            }
        }

        public void ClearBlock(int x, int y)
        {
            int type = this.currentBlock.Category;
            if (type == 1 && x < this.mat.Length && y < this.mat[x].Length && this.mat[x][y] == "#")
            {
                this.mat[x][y] = " ";
            }
            if (type == 2 && x < this.mat.Length - 1 && y < this.mat[x].Length - 1 && this.mat[x][y] == "#" && this.mat[x][y + 1] == "#" && this.mat[x + 1][y] == "#" && this.mat[x + 1][y + 1] == "#") //was a mistake here
            {
                this.mat[x][y] = " ";
                this.mat[x][y + 1] = " ";
                this.mat[x + 1][y] = " ";
                this.mat[x + 1][y + 1] = " ";
            }
        }

        public bool IscurrentBlockFix() // traduit le fait de savoir si le Block peut encore être déplacé ou est déja fixé sur les autre bloc 
        {
            bool result = true;
            int x = this.currentBlock.PosX;
            int y = this.currentBlock.PosY;
            int type = this.currentBlock.Category;
            if (type == 1)
            {
                if (this.mat[x + 1][y] == " " && x < this.mat.Length - this.blockedLines)
                {
                    result = false;
                }
            }
            if (type == 2)
            {
                if (this.mat[x + 1][y] == " " && this.mat[x + 1][y + 1] == " " && x < this.mat.Length - 1 - this.blockedLines)
                {
                    result = false;
                }
            }
            return result;
        }

        public void moveCurrentDown()
        {
            int x = this.currentBlock.PosX;
            int y = this.currentBlock.PosY;

            if (!IscurrentBlockFix()) // Si on peut bouger la pièce vers le bas
            {
                ClearBlock(x, y); // On efface la pièce de son ancienne position


                this.DrawcurrentBlock(x + 1, y); //  On incrémente son ordonnée et on la redessine à la nouvelle position.
            }
        }

        public void moveCurrentRight()
        {
            int x = this.currentBlock.PosX;
            int y = this.currentBlock.PosY;

            if (!IscurrentBlockFix() && y + 1 < this.mat[x].Length - 1 && this.mat[x][y + 1] == " " && this.mat[x][y + 2] == " " && this.mat[x][y + 2] == " ") // Si on peut bouger la pièce vers la droite
            {
                ClearBlock(x, y); // On efface la pièce de son ancienne position


                this.DrawcurrentBlock(x + 1, y); //  On incrémente son ordonnée et on la redessine à la nouvelle position.
            }
        }

        public void Affichage()
        {
            for (int i = 0; i < this.mat.Length; i++)
            {
                for (int j = 0; j < this.mat[0].Length; j++)
                {
                    Console.Write(this.mat[i][j] + "   ");
                }
                Console.WriteLine();
            }
        }
    }
}
