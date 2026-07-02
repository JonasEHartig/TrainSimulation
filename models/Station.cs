using System;
using Raylib_cs;

namespace challenge; 

public class Station
{

    public StationName StationName;
    public int StationXPosition;
    public int StationYPosition;
    public Color StationColor;
    

    public Station(int stationXPosition, int stationYPosition)
    {
        StationXPosition = stationXPosition;
        StationYPosition = stationYPosition;
    }
}

public enum StationName
{
    København,
    Roskilde,
    Odense,
    Kolding,
    Esbjerg,
    Vejle,
    Horsens,
    Aarhus,
    Randers,
    Skjern,
    Aalborg
}