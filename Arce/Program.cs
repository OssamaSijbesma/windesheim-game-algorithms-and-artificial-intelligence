using System;

namespace Arce
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (GameWorld game = GameWorld.Instance)
                game.Run();
        }
    }
}
