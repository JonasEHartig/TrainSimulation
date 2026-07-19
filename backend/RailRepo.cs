using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace challenge;

public class RailRepo
{
    public List<RailColors?> TakenColors = new List<RailColors?>();
    public List<Rail> RailList = new List<Rail>();

    public bool newRail = true;
    public RailColors? currentRailColor = null;
    public bool newRailsAvalible = true;
    private readonly Random rng = new();

    public void AddRail(Station currentInteractedStation, Station lastInteractedStation)
    {
        RailColors? railColorsEnum;
        Color railColor = Color.White;
        int RailLocationTries = 0;
        bool railColorVaild = false;

        if (newRail)
        {
            do
            {
                lastInteractedStation.StartPoint = true;
                
                RailLocationTries++;
                if (RailLocationTries < 50)
                {
                    (railColorsEnum, railColorVaild) = GetUnusedColor();
                    currentRailColor = railColorsEnum;
                }
                else
                {
                    newRailsAvalible = false;
                    return;
                }
            } while (!railColorVaild);  
        }
        else
        {
            railColorsEnum = currentRailColor;
        }


        if(!TakenColors.Contains(railColorsEnum))
        {
            TakenColors.Add(railColorsEnum);
        }
            
        if (railColorsEnum == RailColors.Blue)
        {
            railColor = Color.Blue;
        }
        else if (railColorsEnum == RailColors.Green)
        {
            railColor = Color.Green;
        }
        else if (railColorsEnum == RailColors.Red)
        {
            railColor = Color.Red;
        }
        else if (railColorsEnum == RailColors.Yellow)
        {
            railColor = Color.Yellow;
        }

        Rail rail = new Rail(currentInteractedStation, lastInteractedStation, railColorsEnum, railColor);
        RailList.Add(rail);

        int takenColorsAmount = TakenColors.Count();
        if (takenColorsAmount == 4)
        {
            newRailsAvalible = false;
        }
    }

    public Tuple<RailColors?, bool> GetUnusedColor()
    {
        int railColorEnum = rng.Next(1,5);
        bool railColorValid = true;

        foreach(Rail currentRail in RailList)
        {
            if (currentRail.RailColorsEnum == (RailColors)railColorEnum)
            {
                railColorValid = false;
                break;
            }
        }

        return Tuple.Create((RailColors?)railColorEnum, railColorValid);
    }
}