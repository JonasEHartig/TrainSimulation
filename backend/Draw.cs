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
        sec3Timer.Interval = 500;
        sec3Timer.Enabled = true;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.DrawText("Train Simulation", 12, 12, 20, Color.White);

            foreach (Station currentStation in StationList.ToList())
            {
                int RectangleWidth = currentStation.StationXCoverArea2 - currentStation.StationXCoverArea1;
                int RectangleHeight = currentStation.StationYCoverArea2 - currentStation.StationYCoverArea1;
                Raylib.DrawRectangleGradientV(currentStation.StationXCoverArea1, currentStation.StationYCoverArea1, RectangleWidth, RectangleHeight, Color.Red, Color.DarkGray);
                Raylib.DrawCircle(currentStation.StationXPosition, currentStation.StationYPosition, 10, Color.Blue);

            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    public void AddStation(object source, ElapsedEventArgs e)
    {
        (int stationXPosition, int stationYPosition, int stationXCoverArea1, int stationXCoverArea2, int stationYCoverArea1,int stationYCoverArea2) = CalculateStationPlacement();
        bool addStationBool = false;
        
        Station station = new Station(stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2);

        if (!StationList.Any())
        {
            addStationBool = true;

        }

        foreach (Station currentStation in StationList)
        {
            if (stationXPosition <= currentStation.StationXCoverArea1 & stationXPosition >= currentStation.StationXCoverArea2 & stationYPosition <= currentStation.StationYCoverArea1 & stationYPosition >= currentStation.StationYCoverArea2)
            {
                addStationBool = false;
                AddStation(this, null);
            }
            else
            {
                addStationBool = true;
                break;
            }
        }

        if (addStationBool == true)
        {
            StationList.Add(station);
        }
    }

    public Tuple<int, int, int, int, int, int> CalculateStationPlacement()
    {
        Random rng = new Random();

        int stationXPosition = rng.Next(100,700);
        int stationYPosition = rng.Next(100,380);

        int stationXCoverArea1 = stationXPosition - 50;
        int stationXCoverArea2 = stationXPosition + 50;

        int stationYCoverArea1 = stationYPosition - 50;
        int stationYCoverArea2 = stationYPosition + 50;

        return Tuple.Create(stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2);
    }
}