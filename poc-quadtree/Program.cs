using System;

namespace poc_quadtree
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (var game = new QuadTreeGame())
            {
                game.Run();
            }
        }
    }
}