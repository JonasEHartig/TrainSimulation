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

    
    public void TryAddRail(Station currentStation)
    {
        if ((newRailsAvalible || !nextRailIsNewRail) && !forcedStopDrawing)
        {
            if (nextRailIsNewRail) 
            {
                currentRailLine = RailLineList.FirstOrDefault(r => !r.IsActive);

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
                else
                {
                    return;
                }

                if (currentRailLine.IsLoop)
                {
                    forcedStopDrawing = true;
                }
            }

            nextRailIsNewRail = false;
            return;
        }
        else
        {
            return;
        }
    }

    public void EndDrag()
    {
        if (currentRailLine != null && currentRailLine.Stations.Count < 2)
        {
            currentRailLine.Stations.Clear();
        }

        nextRailIsNewRail = true;
        forcedStopDrawing = false;
        currentRailLine = null;
    }

    public void CreateRailLines()
    {
        RailLine railLineRed = new RailLine(new List<Station>(), RailColor.Red, Color.Red, new Color(92, 16, 22, 255), 30, 90, 14);
        RailLineList.Add(railLineRed);
        RailLine railLineGreen = new RailLine(new List<Station>(), RailColor.Green, Color.Green, new Color(0, 91, 19, 255), 65, 90, 14);
        RailLineList.Add(railLineGreen);
        RailLine railLineYellow = new RailLine(new List<Station>(), RailColor.Yellow, Color.Yellow, new Color(101, 100, 0, 255), 100, 90, 14);
        RailLineList.Add(railLineYellow);
        RailLine railLineBlue = new RailLine(new List<Station>(), RailColor.Blue, Color.Blue, new Color(0, 48, 96, 255), 135, 90, 14);
        RailLineList.Add(railLineBlue);

        currentRailLine = railLineRed;
    }
}