using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace challenge;

public class RailRepo
{
    public List<RailColor> TakenColors = new List<RailColor>();
    public List<RailLine> RailLineList = new List<RailLine>();

    public RailLine? currentRailLine = null;
    public bool canDrawRails = true;
    public bool nextRailIsNewRail = true;


    private readonly Random rng = new();
    
    public void AddRail(Station currentInteractedStation, Station lastInteractedStation)
    {
        if (nextRailIsNewRail)
        {
            foreach (RailLine railLine in RailLineList)
            {
                if (!railLine.IsActive)
                {
                    canDrawRails = true;
                    currentRailLine = railLine;
                    
                    railLine.IsActive = true;
                    railLine.StartPoint = true;
                    railLine.StartPointStation = lastInteractedStation;
                    railLine.Rails.Add(new Rail(currentInteractedStation, lastInteractedStation));
                    if (currentInteractedStation == railLine.EndPointStation)
                    {
                        railLine.IsLoop = true;
                    }
                    break;
                }
            }

            canDrawRails = RailLineList.Any(r => !r.IsActive);

        }
        else
        {
            currentRailLine.Rails.Add(new Rail(currentInteractedStation, lastInteractedStation));
            //currentRailLine.EndPointStation = lastInteractedStation;
        }

    }

    public void CreateRailLines()
    {
        RailLine railLineRed = new RailLine(new List<Rail>(), RailColor.Red, Color.Red);
        RailLineList.Add(railLineRed);
        RailLine railLineGreen = new RailLine(new List<Rail>(), RailColor.Green, Color.Green);
        RailLineList.Add(railLineGreen);
        RailLine railLineYellow = new RailLine(new List<Rail>(), RailColor.Yellow, Color.Yellow);
        RailLineList.Add(railLineYellow);
        RailLine railLineBlue = new RailLine(new List<Rail>(), RailColor.Blue, Color.Blue);
        RailLineList.Add(railLineBlue);

        currentRailLine = railLineRed;
    }

}

/*
 do
            {
                RailLocationTries++;
                if (RailLocationTries < 50)
                {
                    (railColorEnum, railColorVaild) = GetUnusedColor();
                    currentRailColor = railColorEnum;
                }
                else
                {
                    newRailsAvalible = false;
                    return;
                }
            } while (!railColorVaild);  

            */