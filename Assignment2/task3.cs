using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sytem.IO;

namespace Task3 // EXTENSION OF TASK 2 - RIGHT NOW ITS JUST IMPLEMENTATION OF TASK 3 FSM (NEED TO ADD THREADING)
{
	public class Program
	{
		public class FiniteStateMachine
		{
			public enum States { SA, SB }
			public States State { get; set; }

			public enum Actions { a, S1 }

			public delegate void delActions();
			private delActions[,] FSM;
			
			public bool J_flag = false;
			public bool K_flag = false;

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
				J_flag = true;
				Console.WriteLine("Action J");
			}
			public void K()
			{
				K_flag = true;
				Console.WriteLine("Action K");
			}
			public void L()
			{
				if ((J_flag) && (K_flag)) {
					this.State = States.SB;
					Console.WriteLine("Action L");
					J_flag = false;
					K_flag = false;
				}
			}
			private void do_nothing() { return; }
			public static void Main(string[] args)
			{
				// THREADING HERE
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
