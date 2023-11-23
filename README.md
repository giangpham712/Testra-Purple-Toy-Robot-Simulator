# README #

This is Giang's implementation for the Toy Robot Simulator coding challenge

### Assumptions ###

* The program ignores and doesn't throw any error on any invalid command

### Considerations ###

* The simulator issues PLACE, MOVE, REPORT commands to the robot no matter if they are valid actions to be performed, the robot makes its decision where it should perform the action 

### How to run ###

The solution includes a very simple console app which requires a list of string arguments in this format

`
5,5 "PLACE 4,4,EAST" MOVE MOVE MOVE MOVE REPORT
`

where the first argument is the dimension for the table, followed by the commands for the robot

The app can be run via Visual Studio or command prompt as follows

`
dotnet run --project TelstraPurple.ToyRobotSimulator\TelstraPurple.ToyRobotSimulator.csproj 5,5 "PLACE 4,4,EAST" MOVE MOVE MOVE MOVE REPORT
`

The tests can be run via Visual Studio or command prompt as follows

`
dotnet test
`