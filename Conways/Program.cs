using System;

namespace Conways
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ConwayGameRunner())
                game.Run();
        }
    }
}
