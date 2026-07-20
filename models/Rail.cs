using Raylib_cs;

namespace challenge;

public class Rail
{
    public Station Destination1;
    public Station Destination2;

    public Rail(Station destination1, Station destination2)
    {
        Destination1 = destination1;
        Destination2 = destination2;   
    }
}