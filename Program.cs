using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace VectorMatrixDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Vector & Matrix Transformation Demo",
            };

            using (var game = new Game(GameWindowSettings.Default, nativeWindowSettings))
            {
                game.Run();
            }
        }
    }
}