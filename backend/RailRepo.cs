using Raylib_cs;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace challenge;

public class RailRepo
{
    public List<RailLine> RailLineList = new List<RailLine>();

    public RailLine? currentRailLine = null;
    public bool canDrawRails = true;
    public bool nextRailIsNewRail = true;
    
    public void TryAddRail(Station currentStation)
    {
        if (nextRailIsNewRail) 
        {
            foreach (RailLine railLine in RailLineList)
            {
                if (!railLine.IsActive)
                {
                    canDrawRails = true;
                    currentRailLine = railLine;
                    break;
                }
            }

            
            currentRailLine.Stations.Add(currentStation);

            canDrawRails = RailLineList.Any(r => !r.IsActive);
        }
        else
        {
            if (currentRailLine.Stations.Contains(currentStation))
            {
                return;
            }
            currentRailLine.Stations.Add(currentStation);
        }
    }

    public void CreateRailLines()
    {
        RailLine railLineRed = new RailLine(new List<Station>(), RailColor.Red, Color.Red);
        RailLineList.Add(railLineRed);
        RailLine railLineGreen = new RailLine(new List<Station>(), RailColor.Green, Color.Green);
        RailLineList.Add(railLineGreen);
        RailLine railLineYellow = new RailLine(new List<Station>(), RailColor.Yellow, Color.Yellow);
        RailLineList.Add(railLineYellow);
        RailLine railLineBlue = new RailLine(new List<Station>(), RailColor.Blue, Color.Blue);
        RailLineList.Add(railLineBlue);

        currentRailLine = railLineRed;
    }

    public bool IsStationStartPointOfCurrentRailLine(Station currentStation)
    {
        if (currentStation == currentRailLine.StartPointStation)
        {
            return true;
        }

        return false;
    }

}