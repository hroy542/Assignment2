Assignment 2 Group 37;
Hritom Roy hroy542
Leighton Jensen ljen819

Task1.cs
Assignment 2 - Task 1: This task contains the class definition for the FiniteStateTable class. The class consists
of a 2D-array repersenting the table, where each cell contains an index to the next state and the associated action.
There are also 4 main methods included in the class: SetNextState(), GetNextState(), SetActions(), and GetActions().

Task2.cs
Assignment 2 - Task 2: This task creates a console application that implements a certain finite state machine. The
user will interface to the machine using the keyboard with 'a', 'b' and 'c' used to trigger events. Actions are then
displayed in the console as well as the state transition. The application can also be quit by pressing 'q' on the 
keyboard. Furthermore, this task ensures all data regarding actions and trigger events are written and timestampled
to a text file which the user specifies.

Task3.cs
Assignment 2 - Task 3: This task further develops the console application of task 2 such that 2 concurrent, dependent,
synchoronous finite state machines are created. The task uses multi-threading in order to achieve this whereby, the
finite state machine created by task 2 is the main thread and a new finite state machine operates as another thread.

