using System;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using Raylib_cs;

namespace challenge;

public class Draw
{
    public List<Station> StationList = new List<Station>();
    bool mapFull = false;

    public void initialDraw()
    {
        Raylib.InitWindow(800, 480, "Train Simulation");
        
    
        double lastSpawn = 0;

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.GetTime() - lastSpawn >= 0.5 & !mapFull)
            {
                AddStation();
                lastSpawn = Raylib.GetTime();
            }
            Raylib.BeginDrawing();

            Raylib.DrawText("Train Simulation", 12, 12, 20, Color.White);

            foreach (Station currentStation in StationList)
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

    public void AddStation()
    {
        int stationXPosition;
        int stationYPosition;
        int stationXCoverArea1; 
        int stationXCoverArea2; 
        int stationYCoverArea1; 
        int stationYCoverArea2;

        int stationTries = 0;
        bool stationLocationValidBool = false;

        if (!StationList.Any())
        {
            (stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2, stationLocationValidBool) = CalculateStationPlacement();
            Station firstStation = new Station(stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2);
            StationList.Add(firstStation);
            return;
        }

        do
        {
            stationTries++;
            if (stationTries < 50)
            {
                (stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2, stationLocationValidBool) = CalculateStationPlacement();
            }
            else
            {
                mapFull = true;
                return;
            }
        } while (!stationLocationValidBool);

        Station station = new Station (stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2);

        if (stationLocationValidBool)
        {
            StationList.Add(station);
        }
    }

    public Tuple<int, int, int, int, int, int, bool> CalculateStationPlacement()
    {
        Random rng = new Random();

        int stationXPosition = rng.Next(100,700);
        int stationYPosition = rng.Next(100,380);
        int stationXCoverArea1 = stationXPosition - 100;
        int stationXCoverArea2 = stationXPosition + 100;
        int stationYCoverArea1 = stationYPosition - 100;
        int stationYCoverArea2 = stationYPosition + 100;

        bool stationLocationValidBool = true;

        foreach (Station currentStation in StationList)
        {
            if (stationXPosition >= currentStation.StationXCoverArea1 & 
            stationXPosition <= currentStation.StationXCoverArea2 & 
            stationYPosition >= currentStation.StationYCoverArea1 & 
            stationYPosition <= currentStation.StationYCoverArea2)
            {
                stationLocationValidBool = false;
                break;
            }
        }

        return Tuple.Create(stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2, stationLocationValidBool);
    }
}