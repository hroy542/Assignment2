/* Assignment 2: Task 2 - This task */

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
			interface IAction { } // Looking at Mario implementation

			public enum States { S0, S1, S2, }
			public States State { get; set; }

			public enum Events { a, b, c }

			// flags to ensure both actions for state transition
			public bool X_flag = false;

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
			public void Process(Events Event)
			{
				this.FSM[(int)this.State, (int)Event].Invoke();
			}

			public void W()
			{
				this.State = States.S0;
				Console.WriteLine("Action W");
			}
			public void X()
			{
				X_flag = true;
				Console.WriteLine("Action X");
			}
			public void Y()
			{
				if (X_flag)
				{
					this.State = States.S1;
					Console.WriteLine("Action Y");
					X_flag = false;
				}
			}
			public void Z()
			{
				if (X_flag)
				{
					this.State = States.S2;
					Console.WriteLine("Action Z");
					X_flag = false;
				}
			}
			private void do_nothing() { return; }

			public static String GetTimestamp(DateTime value)
			{
				string timeStamp = value.ToString("yyyyMMddHHmmssffff");
				return timeStamp.Substring(4, 2) + "/" + timeStamp.Substring(6, 2) + " " + timeStamp.Substring(8, 2) + ":" + timeStamp.Substring(10, 2) + ":" + timeStamp.Substring(12, 2) + "	";
			}

			public static void Main(string[] args)
			{
				
				var FSM = new FiniteStateMachine(); // Creates instance of FiniteStateMachine()

				Console.WriteLine("Please input a fully qualified text file name: ");
				string textInput = Console.ReadLine();

				while(textInput.Substring(textInput.Length - 4) != ".txt")
                {
					Console.WriteLine("\nError: Re-enter text file name:");
					textInput = Console.ReadLine();
				}

				var path = @textInput;

				ConsoleKeyInfo cki;

				Console.WriteLine("\nBeginning in state " + FSM.State);
				Console.WriteLine("Press any key to start:\n");
				Console.WriteLine("Press 'q' to quit appllication\n");

				File.WriteAllText(path, "Loggging Data:\n");

				do
				{
					cki = Console.ReadKey(true);

					if(cki.Key == ConsoleKey.A)
                    {
						string timeStamp = GetTimestamp(DateTime.Now);
						File.AppendAllText(path, timeStamp + " 'a' key pressed\n");

						FSM.Process(FiniteStateMachine.Events.a);
					}
					else if (cki.Key == ConsoleKey.B)
					{
						string timeStamp = GetTimestamp(DateTime.Now);
						File.AppendAllText(path, timeStamp + " 'b' key pressed\n");

						FSM.Process(FiniteStateMachine.Events.b);
					}
					else if (cki.Key == ConsoleKey.C)
					{
						string timeStamp = GetTimestamp(DateTime.Now);
						File.AppendAllText(path, timeStamp + " 'c' key pressed\n");

						FSM.Process(FiniteStateMachine.Events.c);
					}

					if (cki.Key != ConsoleKey.Q)
					{
						Console.WriteLine("Now in state " + FSM.State + "\n");
					}
					else
                    {
						string timeStamp = GetTimestamp(DateTime.Now);
						File.AppendAllText(path, timeStamp + " 'q' key pressed - Closing Application\n");
						Console.WriteLine("Now quitting application...");
                    }

				} while (cki.Key != ConsoleKey.Q);

			}
		}
	}
}
