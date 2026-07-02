using System;
using System.Timers;
using Raylib_cs;

namespace challenge;

public class Draw
{
    public List<Station> StationList = new List<Station>();

    public void initialDraw()
    {
        Raylib.InitWindow(800, 480, "Train Simulation");
        
        System.Timers.Timer sec3Timer = new System.Timers.Timer();
        sec3Timer.Elapsed += new ElapsedEventHandler(AddStation);
        sec3Timer.Interval = 3000;
        sec3Timer.Enabled = true;

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

    public void AddStation(object source, ElapsedEventArgs e)
    {
        
        Random rng = new Random();
        Station station = new Station(rng.Next(100,700), rng.Next(100,380));

        StationList.Add(station);
    }
}