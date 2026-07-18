using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using Raylib_cs;
namespace challenge;

public class Draw
{
    public StationRepo stationRepo { get; } = new StationRepo();
   
    public void initialDraw()
    {
        Raylib.InitWindow(800, 480, "Train Simulation");
        List<Station> interactedStations = new List<Station>();
        Station lastInteractedStation = null;

        //Texture2D background = Raylib.LoadTexture("textures/trainsimbackground.png");
        Raylib.SetTargetFPS(30);
        
        double lastSpawn = 0;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("Train Simulation", 12, 12, 20, Color.White);

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


            foreach(Station currentInteractedStation in interactedStations)
            {
                int interactedStationsAmount = interactedStations.Count - 1;

                Raylib.DrawCircle(currentInteractedStation.StationPlacement.X, currentInteractedStation.StationPlacement.Y, 14, Color.Maroon);
                Raylib.DrawLineEx(currentInteractedStation.StationPlacement.Position, interactedStations[interactedStationsAmount].StationPlacement.Position, 20.0f, Color.Gray);

            }

            foreach (Station currentStation in stationRepo.StationList)
            {
                if  (stationRepo.CollisionCheck(currentStation, mousePosition))
                {
                    if(Raylib.IsMouseButtonDown(MouseButton.Left) && currentStation != lastInteractedStation)
                    {
                        
                        if (!interactedStations.Contains(currentStation))
                        {
                            interactedStations.Add(currentStation);
                            lastInteractedStation = currentStation;
                        }
                    } 
                    else if (!Raylib.IsMouseButtonDown(MouseButton.Left))
                    {
                        Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 13, Color.SkyBlue);
                    }
                }
                else if (!Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    interactedStations.Clear();
                    lastInteractedStation = null;
                }
                
                if (currentStation == lastInteractedStation)
                {
                    Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Green);
                    Raylib.DrawLineEx(currentStation.StationPlacement.Position, mousePosition, 20.0f, Color.Gray);
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