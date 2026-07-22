using Raylib_cs;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace challenge;

public class RailRepo
{
    public List<RailLine> RailLineList = new List<RailLine>();

    public RailLine? currentRailLine = null;
    public bool newRailsAvalible = true;
    public bool nextRailIsNewRail = true;
    public bool forcedStopDrawing = false;

    
    public bool TryAddRail(Station currentStation)
    {
        if ((newRailsAvalible || !nextRailIsNewRail) && !forcedStopDrawing)
        {
            if (nextRailIsNewRail) 
            {
                foreach (RailLine railLine in RailLineList)
                {
                    if (!railLine.IsActive)
                    {
                        currentRailLine = railLine;
                        break;
                    }
                }

                currentRailLine.Stations.Add(currentStation);

                newRailsAvalible = RailLineList.Any(r => !r.IsActive);
            }
            else
            {
                if (!currentRailLine.Stations.Contains(currentStation))
                {
                    currentRailLine.Stations.Add(currentStation);
                }
                else if (currentStation == currentRailLine.Stations[0] && currentRailLine.Stations.Count > 2)
                {
                    currentRailLine.Stations.Add(currentStation);
                    forcedStopDrawing = true;
                }

                if (currentRailLine.IsLoop)
                {
                    forcedStopDrawing = true;
                }
            }

            return true;
        }
        else
        {
            return false;
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
}