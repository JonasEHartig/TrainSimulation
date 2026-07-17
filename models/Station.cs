using System;
using Raylib_cs;

namespace challenge; 

public class Station
{
    public StationName? Name;
    public List<Passenger> Passengers;
   public StationPlacement StationPlacement;
    public Color StationColor;
    
    public Station (){}

    public Station (StationPlacement stationPlacement, StationName? name)
    {
        Name = name;
        StationPlacement = stationPlacement;
    }
}

public class StationPlacement
{
    public int X;
    public int Y;

    public int XCoverArea1;
    public int XCoverArea2;
    public int YCoverArea1;
    public int YCoverArea2;
    
    public int InteractXCoverArea1;
    public int InteractXCoverArea2;
    public int InteractYCoverArea1;
    public int InteractYCoverArea2;

        public StationPlacement
        (
        int x, 
        int y, 

        int xCoverArea1, 
        int xCoverArea2, 
        int yCoverArea1, 
        int yCoverArea2, 

        int interactXCoverArea1, 
        int interactXCoverArea2, 
        int interactYCoverArea1, 
        int interactYCoverArea2 
        )
        {
            X = x;
            Y = y;

            XCoverArea1 = xCoverArea1;
            XCoverArea2 = xCoverArea2;
            YCoverArea1 = yCoverArea1;
            YCoverArea2 = yCoverArea2;

            InteractXCoverArea1 = interactXCoverArea1;
            InteractXCoverArea2 = interactXCoverArea2;
            InteractYCoverArea1 = interactYCoverArea1;
            InteractYCoverArea2 = interactYCoverArea2;
        }
}

public enum StationName
{
    København = 1,
    Roskilde = 2,
    Odense = 3,
    Kolding = 4,
    Esbjerg = 5,
    Vejle = 6,
    Aalborg = 7,
    Aarhus = 8,
    Randers = 9,
    Skjern = 10,
}