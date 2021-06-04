/* Assignment 2: Task 3 - This task implements 2 concurrent, dependent, synchonous finite state machines. The
 code extended from task 2 whereby another FSM is implemented using multi-threading. Similar to task 2 a console
 application is created which interfaces to the user through the keyboard. A file is similarly created which again
 prints all the trigger events and actions.

 Authors: Leighton Jensen (ljen819), Hritom Roy (hroy542)
 */

// System set-up
using System;
using System.Threading;
using System.IO;

namespace Task3 
{
	public class Program
	{
		public class FiniteStateMachine1 // FSM for task 2
		{
			public enum States { S0, S1, S2, } // Set-up states for task 2 
			public States State { get; set; } // Implement getter and setter for states

			public enum Events { a, b, c } // Set-up different keyboard events

			public bool X_flag = false; // flags to ensure all actions occur for state transition

			// Create delegate for FSM action implementation
			public delegate void delActions();
			private delActions[,] FSM;

			public FiniteStateMachine1() // Maybe use finite state table (task 1)
			{
				delActions actions_XY, actions_XZ;
				actions_XY = (delActions)X + (delActions)Y; // Perform actions X and Y 
				actions_XZ = (delActions)X + (delActions)Z; // Perform actions X and Z

				this.FSM = new delActions[3, 3] { 
                //a,			b,					c,			
                {actions_XY,    this.do_nothing,    this.do_nothing},     //S0
                {this.W,        actions_XZ,         this.do_nothing},     //S1
                {this.W,        this.do_nothing,    actions_XY} };        //S2
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

		public class FiniteStateMachine2 // fsm for task 3
		{
			public enum States { SB, SA } // Set-up states for task 3
			public States State { get; set; } // getter and setter for task 3 states

			// flags for actions J and K set up
			public bool J_flag = false;
			public bool K_flag = false;

			public void J() // method for Action J
			{
				J_flag = true;
				Console.WriteLine("Action J");
			}
			public void K() // method for Action K
			{
				K_flag = true;
				Console.WriteLine("Action K");
			}
			public void L() // method for Action L
			{
				if ((J_flag) && (K_flag)) // if all three methods entered
				{
					this.State = States.SA; // state transition
					Console.WriteLine("Action L");
					J_flag = false;
					K_flag = false;
				}
			}

			public void transition() // transition from SA to SB (no action)
            {
				this.State = States.SB;
            }

		}

		public static String GetTimestamp(DateTime value) // method that gets time stamp in DD/MM hh:mm:ss form
		{
			string timeStamp = value.ToString("yyyyMMddHHmmssffff");
			return timeStamp.Substring(6, 2) + "/" + timeStamp.Substring(4, 2) + " " + timeStamp.Substring(8, 2) + ":" + timeStamp.Substring(10, 2) + ":" + timeStamp.Substring(12, 2) + " ";
		}

		public static void Main(string[] args) // Entry point to program - begin main thread
		{
			// create new instances for both finite state machines
			var fsm1 = new FiniteStateMachine1();
			var fsm2 = new FiniteStateMachine2();

			// current state variables
			var currentState1 = fsm1.State;
			var currentState2 = fsm2.State;

			// flags for task 2
			bool S0_flag = true;
			bool S1_flag = false;
			bool S2_flag = false;

			// flags for task 3
			bool SB_flag = true;
			bool SA_flag = false;

			// strings for data logging
			string timeStamp;
			string allText = "";

			ConsoleKeyInfo cki; // for reading key inputs

			// Console interface with user
			Console.WriteLine("\nTask 2 FSM beginning in state " + fsm1.State);
			Console.WriteLine("Task 3 FSM beginning in state " + fsm2.State);
			Console.WriteLine("\nPress 'a' to start:\n");
			Console.WriteLine("Press 'q' to quit appllication\n");

			do
			{
				cki = Console.ReadKey(true); // read key input

				// Create new threads for actions J, K, and L
				Thread thrJ = new Thread(fsm2.J);
				Thread thrK = new Thread(fsm2.K);
				Thread thrL = new Thread(fsm2.L);
				Thread thrTransition = new Thread(fsm2.transition);

				// Update the current state
				currentState1 = fsm1.State;
				currentState2 = fsm2.State;

				if (cki.Key == ConsoleKey.A) // if user inputs 'a'
				{
					timeStamp = GetTimestamp(DateTime.Now);
					allText = string.Concat(allText, timeStamp + "	'a' key pressed\n");
					
					fsm1.Process(FiniteStateMachine1.Events.a); // Process method ran for event a for FSM

					// change flags to indicate current state and insert into logging string
					if (S0_flag) 
					{ 
						S1_flag = true; 
						S0_flag = false;
						S2_flag = false;
						allText = string.Concat(allText, timeStamp + "	Action X\n");
						allText = string.Concat(allText, timeStamp + "	Action Y\n");
					}
					else if (S1_flag || S2_flag) // if in S1 or S2
					{ 
						S0_flag = true; 
						S1_flag = false; 
						S2_flag = false;
						allText = string.Concat(allText, timeStamp + "	Action W\n");
					}

					if (SA_flag) // if in SA state, transition to SB
					{
						thrTransition.Start();
						Thread.Sleep(1); // 1ms pause to ensure order maintained

						// invert flags
						SA_flag = false;
						SB_flag = true;
					}

				}
				else if (cki.Key == ConsoleKey.B) // if user inputs 'b'
				{
					timeStamp = GetTimestamp(DateTime.Now);

					if (S1_flag) // if current state is S1, trigger process
					{
						fsm1.Process(FiniteStateMachine1.Events.b); // Process method ran for event b for FSM

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
				else if (cki.Key == ConsoleKey.C) // if user inputs 'c'
				{
					timeStamp = GetTimestamp(DateTime.Now);

					if (S2_flag) // if current state is S2, trigger process
					{
						fsm1.Process(FiniteStateMachine1.Events.c); // Process method ran for event c for FSM

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

				if (cki.Key != ConsoleKey.Q) // if application is running
				{
					if (currentState1 != fsm1.State) { Console.WriteLine("Task 2 FSM now in state " + fsm1.State + "\n"); } // Display task 2 state

					if ((fsm1.State == FiniteStateMachine1.States.S1) && (SB_flag)) // if we are in state S1 for task 2 and SB
					{
						thrJ.Start(); // start action J thread
						timeStamp = GetTimestamp(DateTime.Now);
						allText = string.Concat(allText, timeStamp + "	Action J\n");

						thrK.Start(); // start action K thread
						timeStamp = GetTimestamp(DateTime.Now);
						allText = string.Concat(allText, timeStamp + "	Action K\n");

						thrL.Start(); // start action L thread
						timeStamp = GetTimestamp(DateTime.Now);
						allText = string.Concat(allText, timeStamp + "	Action L\n");

						Thread.Sleep(1); // 1ms pause to ensure order maintained

						// invert flags
						SB_flag = false;
						SA_flag = true;
					}

					if (currentState2 != fsm2.State) { Console.WriteLine("Task 3 FSM now in state " + fsm2.State + "\n"); } // Display task 3 state
				}
				else if (cki.Key == ConsoleKey.Q) // if 'q' has been pressed - closing application
				{
					timeStamp = GetTimestamp(DateTime.Now);
					allText = string.Concat(allText, timeStamp + "	'q' key pressed - Closing Application\n");
					Console.WriteLine("Now quitting application...");

					// Ask user for file name input for .txt
					Console.WriteLine("Please input a fully qualified text file name: ");
					string textInput = Console.ReadLine();

					// while loop that ensures user inputs .txt file 
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
