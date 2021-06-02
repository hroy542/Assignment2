using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task3 // NEED TO ADD THREADING IDK HOW
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// THREADING HERE
			var fsm1 = new FiniteStateMachine1();
			var fsm2 = new FiniteStateMachine2();


			Console.WriteLine("Beginning in SB");
			Console.WriteLine("Press 'a' to start:");
		}

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
			public enum States { SA, SB } // Set-up states for task 3
			public States State { get; set; } // getter and setter for task 3 states

			public enum Actions { a, S1 } // actions associated with task 3

			public delegate void delActions();
			private delActions[,] FSM;

			public bool J_flag = false;
			public bool K_flag = false;

			public FiniteStateMachine2() // Implement task 3 finite state machine
			{
				delActions actions_JKL;
				actions_JKL = (delActions)J + (delActions)K + (delActions)L; // Perform actions J, K, L 

				this.FSM = new delActions[2, 2] { 
                //a,				in S1,		
                {this.transition,   this.do_nothing},	//SA
                {this.do_nothing,   actions_JKL}};      //SB  
			}

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

			private void do_nothing() { return; }

		}
	}
}
