using Raylib_cs;
using System;

namespace challenge;

public static class Program
{
    [System.STAThread]
    public static void Main()
    {
        Draw draw = new Draw();

        draw.initialDraw();
    }
}