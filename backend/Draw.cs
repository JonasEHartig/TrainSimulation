using System;
using Raylib_cs;

namespace challenge;

public class Draw
{
    public void initialDraw()
    {
        Raylib.InitWindow(800, 480, "Train Simulation");


        List<Station> StationList = new List<Station>();

        Random rng = new Random();
        Station station = new Station(rng.Next(100,700), rng.Next(100,380));
        StationList.Add(station);


        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.DrawText("Train Simulation", 12, 12, 20, Color.White);


            foreach (Station currentStation in StationList)
            {
                Raylib.DrawCircle(currentStation.StationXPosition, currentStation.StationYPosition, 10, Color.Green);
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}