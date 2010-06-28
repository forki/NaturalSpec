module AccountSpec

open NaturalSpec
open Machine.Specifications.Example

let transferring_money_to money toAccount (fromAccount:Account) =
    toSpec <| (sprintf "transferring %A to %A" money toAccount)
    fromAccount.Transfer(money,toAccount)
    fromAccount

let balance amount (account:Account) =
    printMethod amount
    account.Balance = amount



[<Scenario>]
let ``When transferring between two account``() =
    let fromAccount = new Account(AccountNo=1,Balance=1m)
    let toAccount = new Account(AccountNo=2,Balance=1m)
    
    Given fromAccount                                // Arrange
      |> When transferring_money_to 1m toAccount     // Act
      |> It should have (balance 0m)                 // Assert
      |> Whereas toAccount
      |> It should have (balance 2m)
      |> Verify