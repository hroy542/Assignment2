using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment2
{
    class FiniteStateTable
    {
        //constructor
        public FiniteStateTable() {
            Console.WriteLine("Default constructor.");
        }

        public int numStates;
        public int numEvents;

        public FiniteStateTable(int numStates, int numEvents)
        {
            this.numStates = numStates;
            this.numEvents = numEvents;
        }

        public delegate void delAction(); // unsure how to implement when passing into a method


        struct cell_FST
        {
            public int next_state;
            public delegate void actions();
            public actions FSM_action;

        }

        cell_FST[,] FST;

        //Methods
        void SetNextState(int i, int j, int next_state)
        {
            FST[i, j].next_state = next_state;
        }
        void SetActions(int i, int j, delAction action)
        {
            //delAction d1 = new delAction(action);
            FST[i, j].FSM_action = action;
        }

         public int GetNextState(int i, int j)
        {
            return FST[i, j].next_state;
        }
         public int GetActions(int i, int j)
        {
            return FST[i, j].Action();
        }

        //Main function
        public static void Main(string[] args)
        {
            Console.WriteLine("Input Number of States:");
            int numStates = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Input Number of Events:");
            int numEvents = Convert.ToInt32(Console.ReadLine());

            var FST = new FiniteStateTable(numStates, numEvents);
        }
    }
}
