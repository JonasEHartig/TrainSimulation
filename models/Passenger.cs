using System;
using Raylib_cs;

namespace challenge; 

public class Passenger
{
    Station PassengerDestination;
    Station CurrentStation;
    Train? CurrentTrain;

    public Passenger(){}

    public Passenger(Station passengerDestination, Station currentStation)
    {
        PassengerDestination = passengerDestination;
        CurrentStation = currentStation;
    } 
}