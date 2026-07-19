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


            // rød
            if (railRepo.TakenColors.Contains(RailColors.Red)) { Raylib.DrawCircle(30, 90, 14, Color.Red); } else { Raylib.DrawCircle(30, 90, 14, new Color(92, 16, 22, 255)); }

            // grøn
            if (railRepo.TakenColors.Contains(RailColors.Green)) { Raylib.DrawCircle(65, 90, 14, Color.Green); } else { Raylib.DrawCircle(65, 90, 14, new Color(0, 91, 19, 255)); }

            // gul
            if (railRepo.TakenColors.Contains(RailColors.Yellow)) { Raylib.DrawCircle(100, 90, 14, Color.Yellow); } else { Raylib.DrawCircle(100, 90, 14, new Color(101, 100, 0, 255)); }

            // blå
            if (railRepo.TakenColors.Contains(RailColors.Blue)) { Raylib.DrawCircle(135, 90, 14, Color.Blue); } else { Raylib.DrawCircle(135, 90, 14, new Color(0, 48, 96, 255)); }
            
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

            if (!railRepo.newRailsAvalible)
            {
                Raylib.DrawText("Out of rails!", 12, 54, 20, Color.White);
            }

            foreach (Station currentStation in stationRepo.StationList)
            {
                if  (stationRepo.CollisionCheck(currentStation, mousePosition))
                {
                    if(Raylib.IsMouseButtonDown(MouseButton.Left) && currentStation != stationRepo.lastInteractedStation && railRepo.newRailsAvalible)
                    {
                        //TILFØJE - HVIS DENNE RAIL IKKE HAR ET ENDPOINT MÅ MAN GODT GÅ I GENNEM DEN 2 GANGE 
                        
                        //if (!stationRepo.interactedStations.Contains(currentStation))
                        //{
                            stationRepo.interactedStations.Add(currentStation);
                            int listAmount = stationRepo.interactedStations.Count();

                            if (listAmount > 1)
                            {
                                railRepo.AddRail(currentStation, stationRepo.lastInteractedStation);
                                railRepo.newRail = false;
                            }     
                            
                            stationRepo.lastInteractedStation = currentStation;
                        //}
                    }
                    else
                    {
                        Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 13, Color.SkyBlue);
                    }
                }
                else if (!Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    stationRepo.interactedStations.Clear();
                    railRepo.newRail = true;
                    stationRepo.lastInteractedStation = null;
                }
                
                if (stationRepo.interactedStations.Contains(currentStation) && railRepo.newRailsAvalible)
                {
                    Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Maroon);

                    if (currentStation == stationRepo.lastInteractedStation)
                    {
                        Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Green);
                        Raylib.DrawLineEx(currentStation.StationPlacement.Position, mousePosition, 20.0f, Color.Gray);
                    }
                }

                foreach (Rail rail in railRepo.RailList)
                {
                    Raylib.DrawLineEx(rail.Destination1.StationPlacement.Position, rail.Destination2.StationPlacement.Position, 15.0f, rail.RailColor);
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