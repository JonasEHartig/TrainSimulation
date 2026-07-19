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
        
        double lastSpawn = 0;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("Train Simulation", 12, 12, 20, Color.White);


            Raylib.DrawCircle(30, 75, 14, new Color(92, 16, 22, 255));
            Raylib.DrawCircle(65, 75, 14, new Color(0, 91, 19, 255));
            Raylib.DrawCircle(100, 75, 14, new Color(101, 100, 0, 255));
            Raylib.DrawCircle(135, 75, 14, new Color(0, 48, 96, 255));

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

            

            foreach (Station currentStation in stationRepo.StationList)
            {
                if  (stationRepo.CollisionCheck(currentStation, mousePosition))
                {
                    if(Raylib.IsMouseButtonDown(MouseButton.Left) && currentStation != stationRepo.lastInteractedStation && railRepo.newRailsAvalible)
                    {
                        if (!stationRepo.interactedStations.Contains(currentStation))
                        {
                            stationRepo.interactedStations.Add(currentStation);
                            stationRepo.lastInteractedStation = currentStation;

                            //railRepo.AddRail(currentStation, stationRepo.lastInteractedStation);
                        }
                    } 
                    else if (!Raylib.IsMouseButtonDown(MouseButton.Left))
                    {
                        Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 13, Color.SkyBlue);
                    }
                }
                else if (!Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    stationRepo.interactedStations.Clear();
                    stationRepo.lastInteractedStation = null;
                }
                
                if (stationRepo.interactedStations.Contains(currentStation))
                {
                    Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Maroon);

                    if (currentStation == stationRepo.lastInteractedStation)
                    {
                        Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Green);
                        Raylib.DrawLineEx(currentStation.StationPlacement.Position, mousePosition, 20.0f, Color.Gray);
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