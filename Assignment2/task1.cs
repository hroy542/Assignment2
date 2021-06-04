/* Assignment 2: Task 1 - This task asks us to create a definition for a 
Finite State Table class including; A 2D named FST, A struct named cell_FST,
SetNextState() and SetActions() which set the contents of any cell in the
FST and GetNextState() and GetActions() which returns the contents of that
cell.
Authors: Leighton Jensen (ljen819), Hritom Roy (hroy542)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    // Action class defines an action object, it contains a variable 
    // that stores the actions name(s) and a method that executes the
    // action when the trigger action calls it.
    class Action
    {
        // constructors
        public Action()
        {
            string[] Act = new string[] { "do_nothing" };
            this.name = Act;
        }
        public Action(string[] ActionName)
        {
            this.name = ActionName;
        }
        
        //variables
        public string[] name;
        
        //methods
        public void ExecuteAction()
        {
            if (this.name[0] != "do_nothing")
            {
                for (int i = 0; i < this.name.Length; i++)
                {
                    Console.WriteLine("Action " + this.name[i] + "\n");
                }
            }
        }
    }
    class FiniteStateTable
    {
        //constructor
        public FiniteStateTable(int StateCount, int EventCount)
        {
            this.FST = new cell_FST[EventCount, StateCount];
        }

        //Variables
        private struct cell_FST
        {
            public int nextState;
            public Action FSMactions;
        }
        private cell_FST[,] FST;
        public int state;


        //Methods
        public void ExecuteAction(FiniteStateTable FSM, int state, int input)
        {
            Action Act = FSM.GetActions(state, input);
            int nextState = FSM.GetNextState(state, input);
            Act.ExecuteAction();
            FSM.state = nextState;
        }
        public void SetNextState(int currentState, int Event, int nextState)
        {
            this.FST[Event, currentState].nextState = nextState;
        }
        public int GetNextState(int currentState, int Event)
        {
            return this.FST[Event, currentState].nextState;
        }
        public void SetActions(int currentState, int Event, Action action)
        {
            this.FST[Event, currentState].FSMactions = action;
        }
        public Action GetActions(int currentState, int Event)
        {
            return this.FST[Event, currentState].FSMactions;
        }
        static void Main(string[] args)
        {
        }
    }
}
