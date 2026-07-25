using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using Raylib_cs;
namespace challenge;

public class Draw
{
    public StationRepo stationRepo { get; } = new StationRepo();
    public RailRepo railRepo { get; } = new RailRepo();
   
    public void initialDraw()
    {
        Raylib.InitWindow(800, 480, "Train Simulation");
        //Texture2D background = Raylib.LoadTexture("textures/trainsimbackground.png");
        Raylib.SetTargetFPS(30);

        railRepo.CreateRailLines();

        double lastSpawn = 0;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("Train Simulation", 12, 12, 20, Color.White);

            foreach (RailLine currentRailLine in railRepo.RailLineList)
            {
                if (currentRailLine.IsActive)
                {
                    Raylib.DrawCircle(currentRailLine.CircleX, currentRailLine.CircleY, currentRailLine.CircleRadius, currentRailLine.Color);
                }
                else
                {
                    Raylib.DrawCircle(currentRailLine.CircleX, currentRailLine.CircleY, currentRailLine.CircleRadius, currentRailLine.DimmedColor);
                }
            }
            
            Vector2 mousePosition = Raylib.GetMousePosition();
    
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

            if (!railRepo.newRailsAvalible && railRepo.nextRailIsNewRail)
            {
                Raylib.DrawText("Out of rails!", 12, 54, 20, Color.White);
            }

            foreach (RailLine railLine in railRepo.RailLineList)
            {
                if(railLine.IsActive && railLine.Stations.Count > 1)
                {
                    for (int i = 0; i < railLine.Stations.Count - 1; i++)
                    {
                        Raylib.DrawLineEx(railLine.Stations[i].StationPlacement.Position, railLine.Stations[i + 1].StationPlacement.Position, 15.0f, railLine.Color);
                    }
                }
            }

            foreach (Station currentStation in stationRepo.StationList)
            {
                if (Raylib.IsMouseButtonDown(MouseButton.Left) && stationRepo.CollisionCheck(currentStation, mousePosition)) 
                {
                    railRepo.TryAddRail(currentStation);
                }

                if (railRepo.currentRailLine != null && railRepo.currentRailLine.Stations.Contains(currentStation) && railRepo.forcedStopDrawing == false)
                {
                    Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Maroon);

                    if (currentStation == railRepo.currentRailLine.Stations[^1])
                    {
                        Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Green);
                        Raylib.DrawLineEx(currentStation.StationPlacement.Position, mousePosition, 20.0f, Color.Gray);
                    }
                }


                Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 10, Color.Blue);
                string stationName = currentStation.Name.ToString();
                Raylib.DrawText(stationName, currentStation.StationPlacement.X, currentStation.StationPlacement.Y - 10, 20, Color.White);
            }

            if (!Raylib.IsMouseButtonDown(MouseButton.Left))
            {  
                railRepo.EndDrag();
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }    
}


/*
int RectangleWidth = currentStation.StationPlacement.InteractXCoverArea2 - currentStation.StationPlacement.InteractXCoverArea1;
int RectangleHeight = currentStation.StationPlacement.InteractYCoverArea2 - currentStation.StationPlacement.InteractYCoverArea1;

Raylib.DrawRectangleGradientV(currentStation.StationPlacement.InteractXCoverArea1, currentStation.StationPlacement.InteractYCoverArea1, RectangleWidth, RectangleHeight, Color.Red, Color.DarkGray);
*/