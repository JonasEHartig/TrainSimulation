using System.Numerics;

namespace challenge;

public class StationRepo
{
    public List<Station> StationList = new List<Station>();
    public bool mapFull { get; private set; }= false;
    private readonly Random rng = new();
    public void AddStation()
    {

        StationPlacement stationPlacement;
        int stationLocationTries = 0;
        bool stationLocationValidBool = false;

        StationName? stationName;
        int stationNameTries = 0;
        bool stationNameValid = false;

        do
        {
            stationLocationTries++;
            if (stationLocationTries < 50)
            {
                (stationPlacement, stationLocationValidBool) = CalculateStationPlacement();
            }
            else
            {
                mapFull = true;
                return;
            }

        } while (!stationLocationValidBool);

        do
        {
            stationNameTries++;
            if (stationNameTries < 50)
            {
                (stationName, stationNameValid) = GetSationName();
            }
            else
            {
                mapFull = true;
                return;
            }
        } while (!stationNameValid);


        Station station = new Station
        (
            stationPlacement,
            stationName
        );

        StationList.Add(station);
    }

    public Tuple<StationPlacement, bool> CalculateStationPlacement()
    {
        int X = rng.Next(100,700);
        int Y = rng.Next(100,380);



        bool stationLocationValidBool = true;

        foreach (Station currentStation in StationList)
        {
            if (X >= currentStation.StationPlacement.XCoverArea1 && 
            X <= currentStation.StationPlacement.XCoverArea2 && 
            Y >= currentStation.StationPlacement.YCoverArea1 && 
            Y <= currentStation.StationPlacement.YCoverArea2)
            {
                stationLocationValidBool = false;
                break;
            }
        }

        int XCoverArea1 = X - 100;
        int XCoverArea2 = X + 100;
        int YCoverArea1 = Y - 100;
        int YCoverArea2 = Y + 100;
        
        int InteractXCoverArea1 = X - 12;
        int InteractXCoverArea2 = X + 12;
        int InteractYCoverArea1 = Y - 10;
        int InteractYCoverArea2 = Y + 10;

        StationPlacement stationPlacement = new StationPlacement
        (
            X,
            Y,

            XCoverArea1, 
            XCoverArea2, 
            YCoverArea1, 
            YCoverArea2, 
            
            InteractXCoverArea1, 
            InteractXCoverArea2, 
            InteractYCoverArea1, 
            InteractYCoverArea2
        ); 

        return Tuple.Create(stationPlacement, stationLocationValidBool);
    }

    public Tuple<StationName?, bool> GetSationName()
    {   
        int stationNameEnum = rng.Next(1,10);
        bool stationNameValid = true;

        foreach(Station currentStation in StationList)
        {
            if (currentStation.Name == (StationName)stationNameEnum)
            {
                stationNameValid = false;
                break;
            }
        }
        return Tuple.Create((StationName?)stationNameEnum, stationNameValid);
    }

    public bool CollisionCheck(Station currentStation, Vector2 mousePosition)
    {
        if (mousePosition.X >= currentStation.StationPlacement.InteractXCoverArea1 &&
            mousePosition.X <= currentStation.StationPlacement.InteractXCoverArea2 && 
            mousePosition.Y >= currentStation.StationPlacement.InteractYCoverArea1 && 
            mousePosition.Y <= currentStation.StationPlacement.InteractYCoverArea2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}