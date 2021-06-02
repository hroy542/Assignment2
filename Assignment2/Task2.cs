/* Assignment 2: Task 2 - This task */

// System set-up
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task2
{
	public class Program
	{
		public class FiniteStateMachine
		{
			public enum States { S0, S1, S2, } // Set-up different states
			public States State { get; set; } // Implement getter and setter for states

			public enum Events { a, b, c } // Set-up different keyboard events

			public bool X_flag = false; // flags to ensure all actions occur for state transition

			// Create delegate for FSM action implementation
			public delegate void delActions();
			private delActions[,] FSM;

			public FiniteStateMachine() // Maybe use finite state table (task 1)
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

			public static void Main(string[] args) // Main Function
			{
				
				var FSM = new FiniteStateMachine(); // Creates instance of FiniteStateMachine()

				// Ask user for file name input for .txt
				Console.WriteLine("Please input a fully qualified text file name: ");
				string textInput = Console.ReadLine();

				// while loop that ensures user inputs .txt file is created
				while(textInput.Substring(textInput.Length - 4) != ".txt")
                {
					Console.WriteLine("\nError: Re-enter text file name:");
					textInput = Console.ReadLine();
				}

				var path = @textInput; // sets the file path to input

				ConsoleKeyInfo cki;

				// Console interface with user
				Console.WriteLine("\nBeginning in state " + FSM.State);
				Console.WriteLine("Press any key to start:\n");
				Console.WriteLine("Press 'q' to quit appllication\n");

				File.WriteAllText(path, "Loggging Data:\n"); // being logging data

				do
				{
					cki = Console.ReadKey(true);

					if(cki.Key == ConsoleKey.A) // if user inputs 'a'
                    {
						string timeStamp = GetTimestamp(DateTime.Now);
						File.AppendAllText(path, timeStamp + " 'a' key pressed\n");

						FSM.Process(FiniteStateMachine.Events.a); // Process method ran for event a for FSM
					}
					else if (cki.Key == ConsoleKey.B)
					{
						string timeStamp = GetTimestamp(DateTime.Now);
						File.AppendAllText(path, timeStamp + " 'b' key pressed\n");

						FSM.Process(FiniteStateMachine.Events.b); // Process method ran for event b for FSM
					}
					else if (cki.Key == ConsoleKey.C)
					{
						string timeStamp = GetTimestamp(DateTime.Now);
						File.AppendAllText(path, timeStamp + " 'c' key pressed\n");

						FSM.Process(FiniteStateMachine.Events.c); // Process method ran for event c for FSM
					}

					if (cki.Key != ConsoleKey.Q)
					{
						Console.WriteLine("Now in state " + FSM.State + "\n"); // Write which state currently in
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
}
