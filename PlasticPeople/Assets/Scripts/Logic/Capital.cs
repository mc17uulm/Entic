using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class Capital
    {

        private double amount;
        private double rate;
        private double change;
        private string currency;
        private LinkedList<Action> actions;

        public Capital(double start, double rate)
        {
            this.amount = start;
            this.rate = rate;
            this.change = 0.0;
            this.currency = "EUR";
            this.actions = new LinkedList<Action>();
        }

        public void Tick()
        {
            double before = this.amount;
            
            foreach (Action action in this.actions)
            {
                Debug.Log("Execute Actions");
                action.Tick();
                if (action.IsFinished())
                {
                    this.ExecuteAction(action);
                }
            }

            this.amount += this.rate;
            this.change = 1 - (this.amount / before);
        }

        public void AddAction(Action action)
        {
            this.actions.AddLast(action);
        }

        public void RemoveAction(Action action)
        {
            this.actions.Remove(action);
        }

        public void ExecuteAction(Action action)
        {
            switch (action.GetType())
            {
                case Type.Once:
                    this.amount += action.GetFactor();
                    break;

                case Type.Unlimited:
                    this.rate += action.GetFactor();
                    break;

                default:
                    break;
            }
        }

        public double GetAmount()
        {
            return this.amount;
        }

        public double GetRate()
        {
            return this.rate;
        }

        public double GetChange()
        {
            return this.change;
        }

    }
}
