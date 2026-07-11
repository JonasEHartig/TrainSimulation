namespace challenge;

public class StationRepo
{

    public List<Station> StationList = new List<Station>();
    public bool mapFull { get; private set; }= false;

    public void AddStation()
    {
        int stationLocationTries = 0;

        int stationXPosition;
        int stationYPosition;
        int stationXCoverArea1; 
        int stationXCoverArea2; 
        int stationYCoverArea1; 
        int stationYCoverArea2;

        int stationNameTries = 0;

        StationName? stationName;

        bool stationLocationValidBool = false;
        bool stationNameValid = false;

        do
        {
            stationLocationTries++;
            if (stationLocationTries < 50)
            {
                (stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2, stationLocationValidBool) = CalculateStationPlacement();
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
        (stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2, stationName);

        if (stationLocationValidBool)
        {
            StationList.Add(station);
        }
    }

    public Tuple<int, int, int, int, int, int, bool> CalculateStationPlacement()
    {
        Random rng = new Random();

        int stationXPosition = rng.Next(100,700);
        int stationYPosition = rng.Next(100,380);
        int stationXCoverArea1 = stationXPosition - 100;
        int stationXCoverArea2 = stationXPosition + 100;
        int stationYCoverArea1 = stationYPosition - 100;
        int stationYCoverArea2 = stationYPosition + 100;

        bool stationLocationValidBool = true;

        foreach (Station currentStation in StationList)
        {
            if (stationXPosition >= currentStation.StationXCoverArea1 & 
            stationXPosition <= currentStation.StationXCoverArea2 & 
            stationYPosition >= currentStation.StationYCoverArea1 & 
            stationYPosition <= currentStation.StationYCoverArea2)
            {
                stationLocationValidBool = false;
                break;
            }
        }

        return Tuple.Create(stationXPosition, stationYPosition, stationXCoverArea1, stationXCoverArea2, stationYCoverArea1, stationYCoverArea2, stationLocationValidBool);
    }

    public Tuple<StationName?, bool> GetSationName()
    {   
        Random rng = new Random();
        int stationNameEnum = rng.Next(1,10);
        bool stationNameValid = true;

        foreach(Station currentStation in StationList)
        {
            if (currentStation.StationName == (StationName)stationNameEnum)
            {
                stationNameValid = false;
                break;
            }
        }
        return Tuple.Create((StationName?)stationNameEnum, stationNameValid);
    }
}