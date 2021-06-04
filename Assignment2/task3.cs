using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Task3 // EXTENSION OF TASK 2 - RIGHT NOW ITS JUST IMPLEMENTATION OF TASK 3 FSM (NEED TO ADD THREADING)
{
	public class Program
	{
		public class FiniteStateMachine1
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

			public static String GetTimestamp(DateTime value) // method that gets time stamp 
			{
				string timeStamp = value.ToString("yyyyMMddHHmmssffff");
				return timeStamp.Substring(4, 2) + "/" + timeStamp.Substring(6, 2) + " " + timeStamp.Substring(8, 2) + ":" + timeStamp.Substring(10, 2) + ":" + timeStamp.Substring(12, 2) + " ";
			}
		}

		public class FiniteStateMachine2
		{
			public enum States { SB, SA } // Set-up states for task 3
			public States State { get; set; } // getter and setter for task 3 states


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

		public static String GetTimestamp(DateTime value) // method that gets time stamp 
		{
			string timeStamp = value.ToString("yyyyMMddHHmmssffff");
			return timeStamp.Substring(4, 2) + "/" + timeStamp.Substring(6, 2) + " " + timeStamp.Substring(8, 2) + ":" + timeStamp.Substring(10, 2) + ":" + timeStamp.Substring(12, 2) + " ";
		}

		public static void Main(string[] args) // Entry point to program - begin main thread
		{
			// THREADING HERE
			var fsm1 = new FiniteStateMachine1();
			var fsm2 = new FiniteStateMachine2();

			var currentState1 = fsm1.State;
			var currentState2 = fsm2.State;

			bool SB_flag = true;
			bool SA_flag = false;

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

			ConsoleKeyInfo cki;

			// Console interface with user
			Console.WriteLine("\nTask 2 FSM beginning in state " + fsm1.State);
			Console.WriteLine("Task 3 FSM beginning in state " + fsm2.State);
			Console.WriteLine("Press any key to start:\n");
			Console.WriteLine("Press 'q' to quit appllication\n");

			File.WriteAllText(path, "Loggging Data:\n"); // being logging data

			do
			{
				cki = Console.ReadKey(true);

				Thread thrJ = new Thread(fsm2.J);
				Thread thrK = new Thread(fsm2.K);
				Thread thrL = new Thread(fsm2.L);
				Thread thrTransition = new Thread(fsm2.transition);

				currentState1 = fsm1.State;
				currentState2 = fsm2.State;

				if (cki.Key == ConsoleKey.A) // if user inputs 'a'
				{
					string timeStamp = GetTimestamp(DateTime.Now);
					File.AppendAllText(path, timeStamp + " 'a' key pressed\n");

					fsm1.Process(FiniteStateMachine1.Events.a); // Process method ran for event a for FSM

					if (SA_flag)
					{
						thrTransition.Start();
						Thread.Sleep(1);

						SA_flag = false;
						SB_flag = true;
					}

				}
				else if (cki.Key == ConsoleKey.B)
				{
					string timeStamp = GetTimestamp(DateTime.Now);
					File.AppendAllText(path, timeStamp + " 'b' key pressed\n");

					fsm1.Process(FiniteStateMachine1.Events.b); // Process method ran for event b for FSM
				}
				else if (cki.Key == ConsoleKey.C)
				{
					string timeStamp = GetTimestamp(DateTime.Now);
					File.AppendAllText(path, timeStamp + " 'c' key pressed\n");

					fsm1.Process(FiniteStateMachine1.Events.c); // Process method ran for event c for FSM
				}

				if (cki.Key != ConsoleKey.Q)
				{
					if (currentState1 != fsm1.State) { Console.WriteLine("Task 2 FSM now in state " + fsm1.State + "\n"); } // Write which state currently in

					if ((fsm1.State == FiniteStateMachine1.States.S1) && (SB_flag))
					{
						thrJ.Start();
						thrK.Start();
						thrL.Start();
						Thread.Sleep(1);

						SB_flag = false;
						SA_flag = true;
					}

					if (currentState2 != fsm2.State) { Console.WriteLine("Task 3 FSM now in state " + fsm2.State + "\n"); }
				}
				else
				{
					string timeStamp = GetTimestamp(DateTime.Now);
					File.AppendAllText(path, timeStamp + " 'q' key pressed - Closing Application\n");
					Console.WriteLine("Now quitting application...");
				}

			} while (cki.Key != ConsoleKey.Q); // continue loop until user presses 'q'

		}
	}
}
