using System;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using Raylib_cs;

namespace challenge;

public class Draw
{
    public StationRepo stationRepo { get; }= new StationRepo();
   
    public void initialDraw()
    {
        Raylib.InitWindow(800, 480, "Train Simulation");
        
        double lastSpawn = 0;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            if (!stationRepo.mapFull)
            {
                if (Raylib.GetTime() - lastSpawn >= 0.5)
                {
                    stationRepo.AddStation();
                    lastSpawn = Raylib.GetTime();
                }
            }
            else
            {            
                Raylib.DrawText("Map is full", 12, 34, 20, Color.White);
            }

            Raylib.DrawText("Train Simulation", 12, 12, 20, Color.White);

            foreach (Station currentStation in stationRepo.StationList)
            {
                //int RectangleWidth = currentStation.StationXCoverArea2 - currentStation.StationXCoverArea1;
                //int RectangleHeight = currentStation.StationYCoverArea2 - currentStation.StationYCoverArea1;
                //Raylib.DrawRectangleGradientV(currentStation.StationXCoverArea1, currentStation.StationYCoverArea1, RectangleWidth, RectangleHeight, Color.Red, Color.DarkGray);
                Raylib.DrawCircle(currentStation.StationXPosition, currentStation.StationYPosition, 10, Color.Blue);

            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }    
}