using DVT_Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {

        var building = new Building(numFloors: 10, new ElevatorFactory());

        // Simulate user interaction asynchronously
        await building.RequestElevatorAsync(fromFloor: 3, toFloor: 5, numPassengers: 2);
        await building.RequestElevatorAsync(fromFloor: 7, toFloor: 2, numPassengers: 1);

        Console.WriteLine("Welcome to the Elevator System!");

        while (true)
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1. Request Elevator");
            Console.WriteLine("2. Display Elevator Status");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice (1/2/3): ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RequestElevator(building);
                    break;
                case "2":
                    building.DisplayElevatorStatus();
                    break;
                case "3":
                    Console.WriteLine("Exiting the Elevator System. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                    break;
            }
        }
    }

    public static void RequestElevator(Building building)
    {
        try
        {
            Console.Write("Enter current floor: ");
            int fromFloor = int.Parse(Console.ReadLine()!); 

            Console.Write("Enter destination floor: ");
            int toFloor = int.Parse(Console.ReadLine()!);

            Console.Write("Enter the number of passengers: ");
            int numPassengers = int.Parse(Console.ReadLine()!);

            building.RequestElevatorAsync(fromFloor, toFloor, numPassengers).Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}