namespace Tetris
{
    class GameManager
    {
        private static readonly Game _game = new Game();

        public static Game GameInstance
        {
            get { return _game; }
        }
    }
}
