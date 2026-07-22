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

            // rød
            if (railRepo.RailLineList[0].IsActive) { Raylib.DrawCircle(30, 90, 14, Color.Red); } else { Raylib.DrawCircle(30, 90, 14, new Color(92, 16, 22, 255)); }

            // grøn
            if (railRepo.RailLineList[1].IsActive) { Raylib.DrawCircle(65, 90, 14, Color.Green); } else { Raylib.DrawCircle(65, 90, 14, new Color(0, 91, 19, 255)); }

            // gul
            if (railRepo.RailLineList[2].IsActive) { Raylib.DrawCircle(100, 90, 14, Color.Yellow); } else { Raylib.DrawCircle(100, 90, 14, new Color(101, 100, 0, 255)); }

            // blå
            if (railRepo.RailLineList[3].IsActive) { Raylib.DrawCircle(135, 90, 14, Color.Blue); } else { Raylib.DrawCircle(135, 90, 14, new Color(0, 48, 96, 255)); }
            
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

            foreach (Station currentStation in stationRepo.StationList)
            {
                if (Raylib.IsMouseButtonDown(MouseButton.Left) && stationRepo.CollisionCheck(currentStation, mousePosition)) 
                {
                    bool addRailSucces = railRepo.TryAddRail(currentStation);
                    if (addRailSucces)
                    {
                        stationRepo.interactedStations.Add(currentStation);
                        railRepo.nextRailIsNewRail = false;
                    }
                }
                else if (!Raylib.IsMouseButtonDown(MouseButton.Left))
                {  
                    if (railRepo.currentRailLine.Stations.Count < 2 && stationRepo.interactedStations.Count < 2)
                    {
                        railRepo.currentRailLine.Stations.Clear();
                    }

                    stationRepo.interactedStations.Clear();
                    railRepo.nextRailIsNewRail = true;
                    railRepo.forcedStopDrawing = false;
                }
                
                if (stationRepo.interactedStations.Contains(currentStation) && railRepo.forcedStopDrawing == false)
                {
                    Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Maroon);

                    if (currentStation == stationRepo.interactedStations[^1])
                    {
                        Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Green);
                        Raylib.DrawLineEx(currentStation.StationPlacement.Position, mousePosition, 20.0f, Color.Gray);
                    }
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

                Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 10, Color.Blue);
                string stationName = currentStation.Name.ToString();
                Raylib.DrawText(stationName, currentStation.StationPlacement.X, currentStation.StationPlacement.Y - 10, 20, Color.White);
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