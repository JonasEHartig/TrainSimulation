using Raylib_cs;
using System.Numerics;

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
            Vector2 mousePosition = Raylib.GetMousePosition();

            foreach (RailLine currentRailLine in railRepo.RailLineList)
            {
                if (currentRailLine.IsActive)
                {
                    if (railRepo.RailCircleCollisionCheck(currentRailLine, mousePosition))
                    {   
                        Raylib.DrawCircle(currentRailLine.CircleX, currentRailLine.CircleY, currentRailLine.CircleRadius + 3, currentRailLine.Color);

                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            railRepo.ResetRaillineStations(currentRailLine);
                        }
                    }
                    else
                    {
                        Raylib.DrawCircle(currentRailLine.CircleX, currentRailLine.CircleY, currentRailLine.CircleRadius, currentRailLine.Color);
                    }
                }
                else
                {
                    if (railRepo.RailCircleCollisionCheck(currentRailLine, mousePosition))
                    {
                        Raylib.DrawCircle(currentRailLine.CircleX, currentRailLine.CircleY, currentRailLine.CircleRadius + 3, currentRailLine.DimmedColor);
                    }
                    else
                    {
                        Raylib.DrawCircle(currentRailLine.CircleX, currentRailLine.CircleY, currentRailLine.CircleRadius, currentRailLine.DimmedColor);
                    }
                }
            }
    
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

            if (!railRepo.newRailsAvailable && railRepo.nextRailIsNewRail)
            {
                Raylib.DrawText("Out of rails!", 12, 54, 20, Color.White);
            }

            foreach (RailLine railLine in railRepo.RailLineList)
            {
                if(railLine.IsActive && railLine.Stations.Count > 1)
                {
                    for (int i = 0; i < railLine.Stations.Count - 1; i++)
                    {
                        int splitTime = railRepo.SplitRaillineVisual(railLine.Stations[i], railLine.Stations[i + 1], railLine);
                        if (splitTime == 1)
                        {
                            Raylib.DrawLineEx(railLine.Stations[i].StationPlacement.Position + new Vector2(10,0), railLine.Stations[i + 1].StationPlacement.Position + new Vector2(10,0), 15.0f, railLine.Color);
                        }
                        else
                        {
                            Raylib.DrawLineEx(railLine.Stations[i].StationPlacement.Position, railLine.Stations[i + 1].StationPlacement.Position, 15.0f, railLine.Color);
                        }
                    }
                }
            }

            foreach (Station currentStation in stationRepo.StationList)
            {
                if (Raylib.IsMouseButtonDown(MouseButton.Left) && stationRepo.StationCollisionCheck(currentStation, mousePosition)) 
                {
                    railRepo.TryAddRail(currentStation);
                }

                if (railRepo.currentRailLine != null && railRepo.currentRailLine.Stations.Contains(currentStation) && railRepo.forcedStopDrawing == false)
                {
                    Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Maroon);

                    if (currentStation == railRepo.currentRailLine.Stations[^1])
                    {
                        Raylib.DrawCircle(currentStation.StationPlacement.X, currentStation.StationPlacement.Y, 14, Color.Green);
                        Raylib.DrawLineEx(currentStation.StationPlacement.Position, mousePosition, 20.0f, railRepo.currentRailLine.DimmedColor);
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