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
        
    public int InteractXCoverArea1;
    public int InteractXCoverArea2;
    public int InteractYCoverArea1;
    public int InteractYCoverArea2;

    public RailLine(){}

    public RailLine(List<Station> stations ,RailColor railColor, Color color, Color dimmedColor, int circleX, int circleY, int circleRadius, int interactXCoverArea1, int interactXCoverArea2, int interactYCoverArea1, int interactYCoverArea2)
    {
        Stations = stations;
        RailColor = railColor;
        Color = color;
        DimmedColor = dimmedColor;

        CircleX = circleX;
        CircleY = circleY;
        CircleRadius = circleRadius;

        InteractXCoverArea1 = interactXCoverArea1;
        InteractXCoverArea2 = interactXCoverArea2;

        InteractYCoverArea1 = interactYCoverArea1;
        InteractYCoverArea2 = interactYCoverArea2;
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