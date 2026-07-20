using Raylib_cs;

namespace challenge;

public class RailLine
{
    public List<Rail> Rails = new List<Rail>();
    public Color Color;
    public RailColor RailColor;

    public Station StartPointStation;
    public bool StartPoint;

    public Station EndPointStation;
    public bool EndPoint;

    public bool IsLoop;

    public bool IsActive;

    public RailLine(){}

    public RailLine(List<Rail> rails ,RailColor railColor, Color color)
    {
        Rails = rails;
        RailColor = railColor;
        Color = color;
    }
}

public enum RailColor
{
    Red = 1,
    Green = 2,
    Yellow = 3,
    Blue = 4
}