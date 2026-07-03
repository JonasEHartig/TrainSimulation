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

        int stationXPosition = rng.Next(100,700);
        int stationYPosition = rng.Next(100,380);

        int stationXCoverArea1 = stationXPosition - 50;
        int stationXCoverArea2 = stationXPosition + 50;

        int stationYCoverArea1 = stationYPosition - 50;
        int stationYCoverArea2 = stationYPosition + 50;

        foreach (Station currentStation in StationList)
        {
            if (stationXPosition <= currentStation.StationXCoverArea1 & stationXPosition >= currentStation.StationXCoverArea2)
            {
                
            }

            if (stationYPosition <= currentStation.StationYCoverArea1 & stationYPosition >= currentStation.StationYCoverArea2)
            {
                
            }
        }


        Station station = new Station(stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2);

        StationList.Add(station);
    }
}