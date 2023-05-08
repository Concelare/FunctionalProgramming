// Account Type Object, With A Construct Containing a String Identifer & Float Deposit Amount
type Account(identifier: string, deposit: float) =
    // The currentBalance is to making the Balance Mutable so that it can be edited later on
    let mutable currentBalance = deposit
    
    // Sets the identifer for  the Account from the one provided in the constructor
    member this.Identifier = identifier
    
    // Sets the mutable currentBalance to the Account's Balance so that it can be editted as this.Balance cannot be set as mutable
    member this.Balance = currentBalance
    
    // The Print Function for outputting all the values held in the type on one line
    member this.Print() =
        // Uses the Console.WriteLine function to output
        System.Console.WriteLine($"Account ID: {this.Identifier}, The Current Balance is £{currentBalance}.")

    // The Deposit Function Allows Users to deposit money into an account
    member this.Deposit() =
        // Uses the Console.WriteLine function to output
        System.Console.WriteLine("How much would you like to deposit?")
        // Takes the amount to be deposited
        let amount = System.Console.ReadLine() |> float
        // Makes sure the amount is not negative
        if amount < 0 then
            // Uses the Console.WriteLine function to output
            System.Console.WriteLine("You cannot deposit a negative amount.")
        else
            // Updates the CurrentBalance to the new amount
            currentBalance <- currentBalance + amount
            // Uses the Console.WriteLine function to output
            System.Console.WriteLine($"Successfully Deposited £{amount}.\nNew Balance: {currentBalance}")
    
    // The Withdraw Function Allows Users to withdraw money from an account
    member this.Withdraw() =
        // Uses the Console.WriteLine function to output
        System.Console.WriteLine("How much would you like to withdraw?")
        // Takes the amount to be withdrawn
        let amount = System.Console.ReadLine() |> float
        // Makes sure the amount is not negative
        if amount < 0 then
            // Uses the Console.WriteLine function to output
            System.Console.WriteLine("You cannot withdraw a negative amount.")
        // Checks if the amount is greater than the currentBalance if it is it cancels the transaction and display error message 
        else if amount > currentBalance then
            // Uses the Console.WriteLine function to output
            System.Console.WriteLine("Transaction Cancelled, You cannot withdraw an amount greater than the balance.")
        // if it passes the two if statements it means it is allowed to go through
        else
            // Updates the currentBalance Amount
            currentBalance <- currentBalance - amount
            // Uses the Console.WriteLine function to output
            System.Console.WriteLine($"Successfully Withdrew £{amount}.\nNew Balance: {currentBalance}")
    
    // CheckAccount Function checks if the amount is less 10, between 10 - 100 or greater than 100 and outputs an appropriate message
    member this.CheckAccount() =
        // Uses a match statement to use pattern matching to select an object
        match this.Balance with
            // If the balance is less than 10 it runs this
            | balance when balance < 10.0 ->
                // Uses the Console.WriteLine function to output
                System.Console.WriteLine("Balance is low")
            // if the balance is between 10 - 100 it runs this
            | balance when balance >= 10.0 && balance <= 100.0 ->
                // Uses the Console.WriteLine function to output
                System.Console.WriteLine("Balance is OK")
            // if the balance is greater than 100 it runs this
            | balance when balance >= 100.0 ->
                // Uses the Console.WriteLine function to output
                System.Console.WriteLine("Balance is high")
// Creation of the accounts list
let accounts = [Account("001", 40); Account("002", 60); Account("003", 22); Account("004", 44); Account("005", 88); Account("006", 92)]

// The PickAccount Function Allows the user to pick an account from the list, can return null as it is an option
let PickAccount(): Account option =
    // Prints all the accounts out
    accounts |> List.iter (fun acc -> acc.Print())
    // Uses the Console.WriteLine function to output
    System.Console.WriteLine("Which account do you want to select:")
    // Reads choice from user
    let choice = System.Console.ReadLine()
    
    // Trys to find the account the user has chosen
    accounts |> List.tryFind(fun acc -> acc.Identifier = choice)

// The LowAccounts Function Prints out all accounts between a 0 - 50 Balance
let LowAccounts() =
    // Uses the Console.WriteLine function to output
    System.Console.WriteLine("Low Accounts:")
    // Uses an Iter to print all accounts with a balance between 0 - 50
    accounts |> List.iter (fun acc -> if acc.Balance >= 0.0 && acc.Balance < 50.0 then acc.Print())

// The HighAccounts Function Prints out all accounts higher than 50 balance
let HighAccounts() =
    // Uses the Console.WriteLine function to output
    System.Console.WriteLine("High Accounts:")
    // Uses an Iter to print all accounts with a balance greater than 50
    accounts |> List.iter (fun acc -> if acc.Balance > 50.0 then acc.Print())

// Main Function using an EntryPoint
[<EntryPoint>]
let main _main =
    // Loops the program till the program uses the exit option or presses ctrl + c
    while true do
        // Uses the Console.WriteLine function to output
        System.Console.WriteLine("Welcome To The Banking App\n1) Print Account Info\n2) Deposit Money\n3) Withdraw Money\n4) Check Acccount\n5) Low Accounts\n6) High Accounts\n7) Exit")
    
        // Reads an int input from the user
        let input = System.Console.ReadLine() |> int
    
        // Runs the PickAccount function
        let acc = PickAccount()
        // Checks if account is null
        if acc.IsNone then
            // Uses the Console.WriteLine function to output
            // Shows that account is not found
            System.Console.WriteLine("Account Not Found.")
        // If Account is not null
        else
            // Checks Users Input with the options
            match input with
                | 1 ->
                    // Prints Selected Account Info
                    acc.Value.Print()
                    
                | 2 ->
                    // Deposit Money Into Selected Account
                    acc.Value.Deposit()
                    
                | 3 ->
                    // Withdraw Money From Selected Account
                    acc.Value.Withdraw()
                    
                | 4 ->
                    // Checks the Select Account
                    acc.Value.CheckAccount()
                    
                | 5 ->
                    // Prints All Low Accounts
                    LowAccounts()
                    
                | 6 ->
                    // Prints All High Acounts
                    HighAccounts()
                | 7 ->
                    // Exits The Program
                    exit(0)
                // Default Option if no others match
                | _ ->
                    // Uses the Console.WriteLine function to output, Shows that the option selected was invalid
                    System.Console.WriteLine("Invalid Option Selected")
    // Return Type For a main function is an int so must return 0
    0      