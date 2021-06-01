using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3 // EXTENSION OF TASK 2 - RIGHT NOW JUST IMPLEMENTATION OF TASK 3 FSM
{
	public class Program
	{
		public class FiniteStateMachine
		{
			public enum States { SA, SB }
			public States State { get; set; }

			public enum Actions { J,K,L }

			public delegate void delActions();
			private delActions[,] FSM;

			public FiniteStateMachine() // Maybe use finite state table (task 1)
			{
				delActions actions_JKL;
				actions_JKL = (delActions)J + (delActions)K + (delActions)L; // Perform actions J, K, L 

				this.FSM = new delActions[2, 2] { 
                //a,				in S1,		
                {actions_JKL,       this.do_nothing},			   //SA
                {this.do_nothing,	actions_JKL}};     //SB  
			}

			public void J()
			{
				this.State = States.SB; // maybe use flags to ensure all three done to move states (in L()?)
			}
			public void K()
			{
				this.State = States.SB;
			}
			public void L()
			{
				this.State = States.SB;
			}
			private void do_nothing() { return; }
			public static void Main(string[] args)
			{
				var FSM = new FiniteStateMachine();

				Console.WriteLine("Beginning in SB");
				Console.WriteLine("Press 'a' to start:");

				if (Console.ReadKey() == "a")
				{

				}
			}
		}
	}
}
