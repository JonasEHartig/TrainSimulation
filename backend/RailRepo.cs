using System.Drawing;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace challenge;

public class RailRepo
{
    public List<Rail> RailList = new List<Rail>();
    public bool newRailsAvalible = true;
    private readonly Random rng = new();

    public void AddRail(Station currentInteractedStation, Station lastInteractedStation)
    {
        RailColors? railColorsEnum;
        Color? railColor = null;
        int RailLocationTries = 0;
        bool railColorVaild = false;

        do
        {
            RailLocationTries++;
            if (RailLocationTries < 50)
            {
                (railColorsEnum, railColorVaild) = GetUnusedColor();
            }
            else
            {
                newRailsAvalible = false;
                return;
            }
        } while (!railColorVaild);

        GetUnusedColor();

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
    }

    public Tuple<RailColors?, bool> GetUnusedColor()
    {
        int railColorEnum = rng.Next(1,4);
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