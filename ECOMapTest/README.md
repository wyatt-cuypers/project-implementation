# Testing

Project contains tests as they pertain to the ECO Map logic.

## Code Review Comments

**** First lets put a freeze on the unit tests until more progress on other parts of the map.  So don't read this and think you have to fix and change anything. Information feedback here. I will only caveat this with if you find benefit in a new unit test as it helps verify a unit of code is working please feel free to write that.  Let me know if you have questions on this.

- Unit Tests in general would not use the same data source that application code uses.  You could very easily create a smaller subset of data or mock data to test logic.
- Having separate constructors just for unit tests bypasses logic that then makes the unit test on code never used in the production application not giving a solid representation of a test.
- I broke out some logic/loading code (not completely) then tied the tests to the project in name and code and was able to remove the dependency on the service application which is how you see these in more real world examples.
- Take note on some of the tests written as there were a few tests around formatting methods but they now have been replaced by Microsoft's formatting which not just leads to the unit test testing Microsoft's code which becomes unnecessary. A hard thing to judge up front granted but speaks to really thinking of what tests are bringing you value.
- (Cont) Some of these tests feel like they fall into integration testing taking the CSV (real data) and verifying results. Integration Testing will help to verify the connection between two or more modules (CSV/Oracle and code) while Unit Testing is a testing method by which individual units of code are tested.
- Breaking apart and grouping units of code into smaller objects can help write unit tests by simplifying and making things more readable. Example here is how the Service.cs was doing everything and some of the minor breakout I did here.