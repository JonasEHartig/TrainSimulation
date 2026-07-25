using Raylib_cs;

namespace challenge;

public class RailLine
{
    public List<Station> Stations = new List<Station>();
    public Color Color;
    public Color DimmedColor;
    public RailColor RailColor;
    public int CircleX;
    public int CircleY;
    public int CircleRadius;

    public RailLine(){}

    public RailLine(List<Station> stations ,RailColor railColor, Color color, Color dimmedColor, int circleX, int circleY, int circleRadius)
    {
        Stations = stations;
        RailColor = railColor;
        Color = color;
        DimmedColor = dimmedColor;
        CircleX = circleX;
        CircleY = circleY;
        CircleRadius = circleRadius;

    }

    public Station? StartPointStation => Stations.Count > 0 ? Stations[0] : null;

    public Station? EndPointStation => Stations.Count > 0 ? Stations[^1] : null;

    public bool IsActive => Stations.Count > 0;

    public bool IsLoop => Stations.Count > 2 && Stations[0] == Stations[^1];
}

public enum RailColor
{
    Red = 1,
    Green = 2,
    Yellow = 3,
    Blue = 4
}