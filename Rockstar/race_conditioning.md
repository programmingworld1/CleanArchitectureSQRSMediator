1. State Mutation Races (Data Correctness)
Read–Modify–Write Race
Increment / Decrement Race
Lost Update
Last Write Wins
Read–Write Race

2. Decision & Timing Races
Check-Then-Act Race
Time-of-Check to Time-of-Use (TOCTOU)
Concurrent Reads with Conditional Write

3. Memory Visibility & Ordering Races
Stale Read
Visibility Race
Instruction Reordering Race
Unsafe Publication
Partial Construction Exposure

4. Async / Task Interleaving Races
Async Reentrancy Race
Async Check-Then-Await Race
Continuation Interleaving Race
Cancellation–Completion Race

5. Initialization & Lifecycle Races
Lazy Initialization Race
Double Initialization Race
Dispose–Use Race
Resource Lifetime Race

6. Shared Collections & Resources
Collection Modification Race

7. Synchronization & Locking Failures
Deadlock
Lock Order Inversion
Lost Wake-Up
Missed Signal






Last Write Wins Race:

sequenceDiagram
    participant User1
    participant User2
    participant DB
    
    User1->>DB: GET Artist (Name: "Metallica")
    User2->>DB: GET Artist (Name: "Metallica")
    User1->>DB: PUT Name = "Metallica Updated"
    User2->>DB: PUT Name = "Metallica Live"
    
    Note over DB: User1's update LOST!


Write read race:
sequenceDiagram
    participant User1
    participant User2
    participant DB

    User1->>DB: PUT Artist Name = "Metallica Updated"
    User2->>DB: GET Artist Name

    Note over DB: User2 may read OLD or NEW value depending on timing