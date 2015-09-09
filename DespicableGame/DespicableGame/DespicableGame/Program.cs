using System;

namespace DespicableGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DespicableGame game = new DespicableGame())
            {
                game.Run();
            }
        }
    }
#endif
}

