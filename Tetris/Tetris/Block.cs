namespace Tetris
{
    class Block
    {
        private int category; // block type (# or #^2)
        private int posX;
        private int posY;

        public Block(int category, int posX, int posY)
        {
            this.Category = category;
            this.PosX = posX;
            this.PosY = posY;
        }

        public int Category { get => category; set => category = value; }
        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }
    }
}
