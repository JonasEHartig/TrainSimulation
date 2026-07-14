namespace challenge;

public class StationRepo
{

    public List<Station> StationList = new List<Station>();
    public bool mapFull { get; private set; }= false;

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
            stationPlacement.X, 
            stationPlacement.Y, 

            stationPlacement.XCoverArea1, 
            stationPlacement.XCoverArea2, 
            stationPlacement.YCoverArea1, 
            stationPlacement.YCoverArea2, 
            
            stationName, 

            stationPlacement.InteractXCoverArea1, 
            stationPlacement.InteractXCoverArea2, 
            stationPlacement.InteractYCoverArea1, 
            stationPlacement.InteractYCoverArea2
        );

        StationList.Add(station);
    }

    public Tuple<StationPlacement, bool> CalculateStationPlacement()
    {
        Random rng = new Random();

        int X = rng.Next(100,700);
        int Y = rng.Next(100,380);

        int XCoverArea1 = X - 100;
        int XCoverArea2 = X + 100;
        int YCoverArea1 = Y - 100;
        int YCoverArea2 = Y + 100;
        
        int InteractXCoverArea1 = X - 15;
        int InteractXCoverArea2 = X + 15;
        int InteractYCoverArea1 = Y - 5;
        int InteractYCoverArea2 = Y + 5;

        bool stationLocationValidBool = true;

        foreach (Station currentStation in StationList)
        {
            if (X >= currentStation.XCoverArea1 & 
            X <= currentStation.XCoverArea2 & 
            Y >= currentStation.YCoverArea1 & 
            Y <= currentStation.YCoverArea2)
            {
                stationLocationValidBool = false;
                break;
            }
        }

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
        Random rng = new Random();
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
}