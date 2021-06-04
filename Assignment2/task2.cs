/* Assignment 2: Task 2 - This task creates a specified finite state machine and develops a console appliaction that interfaces
with the user through the keyboard. State transitions and actions are triggered using 'a', 'b', or 'c' and the application is 
quit using 'q'. A user inputted text file containing the triggered events and actions with timestamps is also created
Authors: Leighton Jensen (ljen819), Hritom Roy (hroy542)
*/

// System set-up
using System;
using System.IO;

namespace task2
{
	public class Program
	{
		public class FiniteStateMachine // Class for task 2 finite state machine
		{
			public enum States { S0, S1, S2, } // Set-up different states
			public States State { get; set; } // Implement getter and setter for states

			public enum Events { a, b, c } // Set-up different keyboard events

			public bool X_flag = false; // flags to ensure all actions occur for state transition

			// Create delegate for FSM action implementation
			public delegate void delActions();
			private delActions[,] FSM;

			public FiniteStateMachine() // contructor that creates table
			{
				delActions actions_XY, actions_XZ;
				actions_XY = (delActions)X + (delActions)Y; // Perform actions X and Y 
				actions_XZ = (delActions)X + (delActions)Z; // Perform actions X and Z

				this.FSM = new delActions[3, 3] { 
                			//a,			b,		c,			
                			{actions_XY,    this.do_nothing,    this.do_nothing},     //S0
                			{this.W,        actions_XZ,         this.do_nothing},     //S1
                			{this.W,        this.do_nothing,    actions_XY} 	  //S2
				};
			}

			public void Process(Events Event) // Method that invokes the finite state machine
			{
				this.FSM[(int)this.State, (int)Event].Invoke();
			}

			public void W() // Action W Method
			{
				this.State = States.S0; // Change state to S0
				Console.WriteLine("Action W");
			}
			public void X() // Action X Method
			{
				X_flag = true; // set flag to true
				Console.WriteLine("Action X");
			}
			public void Y() // Action Y Method
			{
				if (X_flag) // if Action X has occurred
				{
					this.State = States.S1; // Change state to S1
					Console.WriteLine("Action Y");
					X_flag = false; // reset Action X flag
				}
			}
			public void Z() // Action Y Method
			{
				if (X_flag) // if Action X has occurred
				{
					this.State = States.S2; // Change state to S2
					Console.WriteLine("Action Z");
					X_flag = false; // reset Action X flag
				}
			}
			private void do_nothing() { return; } // method that returns nothing - for events that don't trigger transitions

		}

		public static String GetTimestamp(DateTime value) // method that gets time stamp in DD/MM hh:mm:ss form
		{
			string timeStamp = value.ToString("yyyyMMddHHmmssffff");
			// returns time stamp string in DD/MM hh:mm:ss form
			return timeStamp.Substring(6, 2) + "/" + timeStamp.Substring(4, 2) + " " + timeStamp.Substring(8, 2) + ":" + timeStamp.Substring(10, 2) + ":" + timeStamp.Substring(12, 2) + " ";
		}

		public static void Main(string[] args) // Main Function
		{

			var FSM = new FiniteStateMachine(); // Creates instance of FiniteStateMachine()

			var currentState = FSM.State; // get the current state

			// flags for states
			bool S0_flag = true;
			bool S1_flag = false;
			bool S2_flag = false;

			// strings for data logging
			string timeStamp;
			string allText = "";

			ConsoleKeyInfo cki;

			// Console interface with user
			Console.WriteLine("\nBeginning in state " + FSM.State);
			Console.WriteLine("Press any key to start:\n");
			Console.WriteLine("Press 'q' to quit appllication\n");
			do
			{
				currentState = FSM.State; // update current state

				cki = Console.ReadKey(true);

				if (cki.Key == ConsoleKey.A) // if user inputs 'a'
				{
					timeStamp = GetTimestamp(DateTime.Now);
					allText = string.Concat(allText, timeStamp + "	'a' key pressed\n");
					FSM.Process(FiniteStateMachine.Events.a); // Process method ran for event a for FSM

					if (S0_flag) // if in S0
					{
						// change state flags and append to logging string
						S1_flag = true;
						S0_flag = false;
						S2_flag = false;
						allText = string.Concat(allText, timeStamp + "	Action X\n");
						allText = string.Concat(allText, timeStamp + "	Action Y\n");
					}
					else if (S1_flag || S2_flag) // if in S1 or S2
					{
						// change state flags and append to logging string
						S0_flag = true;
						S1_flag = false;
						S2_flag = false;
						allText = string.Concat(allText, timeStamp + "	Action W\n");
					}
				}
				else if (cki.Key == ConsoleKey.B)
				{
					timeStamp = GetTimestamp(DateTime.Now);
					if (S1_flag) // if current state is S1, trigger process
					{
						FSM.Process(FiniteStateMachine.Events.b); // Process method ran for event b for FSM

						// change state flags
						S2_flag = true;
						S0_flag = false;
						S1_flag = false;

						// Add actions and events to logging string
						allText = string.Concat(allText, timeStamp + "	'b' key pressed\n");
						allText = string.Concat(allText, timeStamp + "	Action X\n");
						allText = string.Concat(allText, timeStamp + "	Action Z\n");
					}
				}
				else if (cki.Key == ConsoleKey.C)
				{
					timeStamp = GetTimestamp(DateTime.Now);
					if (S2_flag) // if current state is S2, trigger process
					{
						FSM.Process(FiniteStateMachine.Events.c); // Process method ran for event c for FSM

						// change state flags
						S1_flag = true;
						S0_flag = false;
						S2_flag = false;

						// Add actions and events to logging string
						allText = string.Concat(allText, timeStamp + "	'c' key pressed\n");
						allText = string.Concat(allText, timeStamp + "	Action X\n");
						allText = string.Concat(allText, timeStamp + "	Action Y\n");
					}
				}

				if ((cki.Key != ConsoleKey.Q) && (currentState != FSM.State)) // if not quitting and a state transition has occurred
				{
					Console.WriteLine("Now in state " + FSM.State + "\n"); // Write which state currently in
				}
				else if (cki.Key == ConsoleKey.Q) // Console interface for quitting application
				{
					timeStamp = GetTimestamp(DateTime.Now);
					allText = string.Concat(allText, timeStamp + "	'q' key pressed - Closing Application\n");
					Console.WriteLine("Now quitting application...");

					// Ask user for file name input for .txt
					Console.WriteLine("Please input a fully qualified text file name: ");
					string textInput = Console.ReadLine();

					// while loop that ensures user inputs .txt file is created
					while (textInput.Substring(textInput.Length - 4) != ".txt")
					{
						Console.WriteLine("\nError: Re-enter text file name:");
						textInput = Console.ReadLine();
					}

					var path = @textInput; // sets the file path to input

					File.WriteAllText(path, allText); // writes log to text file

					Console.WriteLine("\nData successfully logged to file");
				}

			} while (cki.Key != ConsoleKey.Q); // continue loop until user presses 'q'
		}
	}
}
