using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
	public class Program
	{
		public class FiniteStateMachine
		{
			public enum States { S0, S1, S2, }
			public States State { get; set; }

			public enum Events { W, X, Y, Z, }

			private Action[,] FSM;

			public FiniteStateMachine() // Maybe use finite state table (task 1)
            {
				this.FSM = new Action[3, 3] { 
                //a,			b,					c,			
                {actionX,		null,               null},     //S0
                {actionY,       actionZ,			null},     //S1
                {null,          actionX,            null} };   //S2
			}

			public static void Main(string[] args)
			{
				var FSM = new FiniteStateMachine();

				Console.WriteLine("Beginning in S0");
				Console.WriteLine("Press 'a' to start:");

				if (Console.ReadKey() == "a")
                {

                }
			}
		}
	}
}
