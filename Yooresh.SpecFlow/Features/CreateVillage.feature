Feature: Create Village

Handles the Village Scenarios

    Background:
        Given 
          | ID                                   | Name | Email        | Password | Role         | Confirmed |
          | b410ec1f-0783-46c6-bdb6-dcfecb0efe8b | John | john@doe.com | Aa123456 | SimplePlayer | true      |
        Given the following faction is already created:
          | ID                                   | Name        |
          | 0006ccc9-ef2d-46e1-a2a9-407ff50dcce3 | FactionName |

    @tag1
    Scenario: Create Village
        When the player creates a village by following data
          | Faction                              | Name      |
          | 0006ccc9-ef2d-46e1-a2a9-407ff50dcce3 | MyVillage |
        Then then user can lookup his village and get following data
          | Name      | PlayerId                             | FactionId                            | ResourceFood | ResourceLumber | ResourceGold | ResourceMetal | ResourceStone | AvailableBuilders | DateOfBirth | BankAccountNumber          |
          | MyVillage | b410ec1f-0783-46c6-bdb6-dcfecb0efe8b | 0006ccc9-ef2d-46e1-a2a9-407ff50dcce3 | 0            | 0              | 0            | 0             | 0             | 2                 | 01-JAN-2000 | IR710570029971601460641001 |