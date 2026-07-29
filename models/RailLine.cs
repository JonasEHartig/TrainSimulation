using System.Numerics;
using Raylib_cs;

namespace challenge;

public class RailLine
{
    public List<Station> Stations = new List<Station>();
    public Color Color;
    public Color DimmedColor;
    public RailColor RailColor;

    public int OffsetAmount;

    public int CircleX;
    public int CircleY;
    public int CircleRadius;
        
    public int InteractXCoverArea1;
    public int InteractXCoverArea2;
    public int InteractYCoverArea1;
    public int InteractYCoverArea2;

    public RailLine(){}

    public RailLine(List<Station> stations ,RailColor railColor, Color color, Color dimmedColor, int circleX, int circleY, int circleRadius, int interactXCoverArea1, int interactXCoverArea2, int interactYCoverArea1, int interactYCoverArea2, int offsetAmount)
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

        OffsetAmount = offsetAmount;
    }

    public Station? StartPointStation => Stations.Count > 0 ? Stations[0] : null;

    public Station? EndPointStation => Stations.Count > 0 ? Stations[^1] : null;

    public bool IsActive => Stations.Count > 0;

    public bool IsLoop => Stations.Count > 2 && Stations[0] == Stations[^1];

    //ai offset kode. laver relativ højre venstre logik angående hvordan vi skal offset rail hvis der mere end 1.
    public Vector2 OffsetFor(Station s1, Station s2)
    {
        Vector2 a = s1.StationPlacement.Position;
        Vector2 b = s2.StationPlacement.Position;

        // canonical direction: always compute from the "lower" station
        bool flip = s1.Name > s2.Name;          // or compare coordinates
        Vector2 dir = flip ? a - b : b - a;

        if (dir.LengthSquared() < 0.0001f) return Vector2.Zero;

        Vector2 perp = Vector2.Normalize(new Vector2(-dir.Y, dir.X));
        return perp * OffsetAmount;
    }
}

public enum RailColor
{
    Red = 1,
    Green = 2,
    Yellow = 3,
    Blue = 4
}