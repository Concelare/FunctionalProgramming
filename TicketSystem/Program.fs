// Import of System.Threading so we can lock the thread
open System.Threading

// Object used for locking Threads Easily
let object = obj()
// The Type for tickets with no constructor
type Ticket = {seat:int; customer:string}

// List that generates ten tickets from 1 - 10
let mutable tickets = [for n in 1..10 -> {Ticket.seat = n; Ticket.customer = ""}]

// The Display Tickets Function Iterates through each ticket printing them one by one
let DisplayTickets() =
    // Iterates through using the Console.WriteLine to print the ticket details out
    tickets |> List.iter(fun ticket -> System.Console.WriteLine($"Ticket Info\nSeat Number: {ticket.seat}\nCustomer: {ticket.customer}"))
    
let BookSeats() =
    // Check if the object is locked if not then it can proceed forward if it is locked it will not run the code
    if Monitor.TryEnter(object) then
        // Runs a try loop
        try
            // Uses the Console.WriteLine function to output
            System.Console.WriteLine("What Seat Would You Like?")
            // Reads an int input from the user
            let input = System.Console.ReadLine() |> int
            
            // Checks if the input is a ticket and greater than 0
            if tickets.Length < input || 0 > input then
                // Uses the Console.WriteLine function to output
                System.Console.WriteLine("Invalid Input Provided...")
            else
                // Uses the Console.WriteLine function to output
                System.Console.WriteLine("What is your name?")
                // Takes the name of the user from the command line
                let name = System.Console.ReadLine()    
               
                // Defines the update ticket function 
                let updateTicket (ticket:Ticket) =
                    // Checks the ticket seat is the same as the input one
                    if ticket.seat = input then
                        // Creates new ticket with customer name
                        { ticket with customer = name }
                    else
                        // Otherwise returns ticket the same
                        ticket
                        
                // Runs a map through the tickets updating them
                tickets <- List.map updateTicket tickets
           finally
                // Unlocks the object so it can be used by another threat
                Monitor.Exit(object)

// Main Function Using an EntryPoint Allow it to be easily ran     
[<EntryPoint>]
let main _main =
    // Loops the program until the user selects the exit option or press ctrl + c
    while true do
        // Uses the Console.WriteLine function to output
        System.Console.WriteLine("Welcome to the Ticket System\nPlease Select an Option:\n1) Display Tickets\n2) Book a Ticket\n3) Exit")
        
        // Reads an int input from the user 
        let input = System.Console.ReadLine() |> int
    
        // Match statment to match the input with the corresponding attribute
        match input with
            | 1 ->
                // Runs the display tickets function showing all ticket details
                DisplayTickets()            
            | 2 ->
                // Runs the book seats function allowing the user to books seats
                BookSeats()
            | 3 ->
                // Exits the program
                exit(0)
            // Default Option
            | _ ->
                // Uses the Console.WriteLine function to output
                System.Console.WriteLine("Invalid Option Selected")
    // Return Type For a main function is an int so must return 0           
    0