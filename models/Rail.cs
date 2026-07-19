using System.Drawing;

namespace challenge;

public class Rail
{
    public Station Destination1;
    public Station Destination2;
    public RailColors? RailColorsEnum;
    public Color? RailColor;

    public Rail(Station destination1, Station destination2, RailColors? railColorsEnum, Color? railColor)
    {
        Destination1 = destination1;
        Destination2 = destination2;   
        RailColorsEnum = railColorsEnum;
        RailColor = railColor;
    }
}

public enum RailColors
{
    Red,
    Green,
    Yellow,
    Blue
}