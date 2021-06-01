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
			interface IAction { }

			public enum States { S0, S1, S2, }
			public States State { get; set; }

			public enum Events { a, b, c }

			// flags to ensure both actions for state transition (not sure if needed)
			public bool X_flag = false;
			public bool Y_flag = false;
			public bool Z_flag = false;

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
                {this.W,        this.do_nothing,    actions_XY} };		  //S2
			}
			public void Process(Events Event)
            {
				this.FSM[(int)this.State, (int)Event].Invoke();
            }

			public void W()
			{
				this.State = States.S0;
			}
			public void X()
			{
				this.State = States.S1;
			}
			public void Y()
			{
				this.State = States.S1;
			}
			public void Z()
			{
				this.State = States.S2;
			}
			private void do_nothing() { return; }

			public static void Main(string[] args)
			{
				var FSM = new FiniteStateMachine();

				Console.WriteLine("Beginning in S0");
				Console.WriteLine("Press 'a' to start:");
				Console.ReadKey();

			}
		}
	}
}
