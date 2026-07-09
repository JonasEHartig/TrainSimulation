using System;
using Raylib_cs;

namespace challenge; 

public class Station
{

    public StationName StationName;
    public int StationXPosition;
    public int StationYPosition;
    public int StationXCoverArea1;
    public int StationXCoverArea2;
    public int StationYCoverArea1;
    public int StationYCoverArea2;


    public Color StationColor;
    
    public Station(){}

    public Station(int stationXPosition, int stationYPosition, int stationXCoverArea1, int stationXCoverArea2, int stationYCoverArea1, int stationYCoverArea2)
    {
        StationXPosition = stationXPosition;
        StationYPosition = stationYPosition;
        StationXCoverArea1 = stationXCoverArea1;
        StationXCoverArea2 = stationXCoverArea2;
        StationYCoverArea1 = stationYCoverArea1;
        StationYCoverArea2 = stationYCoverArea2;
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