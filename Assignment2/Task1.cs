using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class FiniteStateTable
    {
        //constructor
        public FiniteStateTable() {
            Console.WriteLine("Default constructor.");
        }
        
        //Variables
        public cell_FST[,] FST = new cell_FST[3, 3];

        struct cell_FST
        {
            public int next_state;
            public void (*action) (void);
        }

        //Methods
        void SetNextState(int i, int j, int next_state)
        {
            FST[i, j].next_state = this.next_state;
        }
        void SetActions(int i, int j, int action)
        {
            FST[i, j].action = this.action;
        }

        int GetNextState(int i, int j)
        {
            return FST[i, j].next_state;
        }
        int GetActions(int i, int j)
        {
            return 0;
        }

        //Main function
        public static void Main(string[] args)
        {
            
        }
    }
}
