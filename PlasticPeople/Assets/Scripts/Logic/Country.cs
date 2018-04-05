using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    [System.Serializable]
    public class Country
    {

        private int id;
        private string code;
        private string name;
        private int population;
        private string description;
        private double amount;
        private double production;
        private double influence;
        private LinkedList<Action> actions;
        private bool rateBetter;
        private bool sumBetter;

        public Country(int id, string code, string name, int population, string description, double amount, double production)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.population = population;
            this.description = description;
            this.amount = amount;
            this.production = production;
            System.Random random = new System.Random();
            this.influence = (double) random.NextDouble();
            this.actions = new LinkedList<Action>();
            this.rateBetter = true;
            this.sumBetter = true;
        }

        public void Tick()
        {
            double prod = this.production;
            double am = this.amount;
            this.production = this.production + ((this.production/12) * 0.01f * (1.01f - this.influence));
            foreach (Action action in this.actions)
            {
                Debug.Log("Execute Actions");
                action.Tick();
                if (action.IsFinished())
                {
                    this.ExecuteAction(action);
                }
            }
            this.amount = this.amount + (this.production/12);
            if(prod > this.production)
            {
                this.rateBetter = false;
            }
            else
            {
                this.rateBetter = true;
            }
            if (am > this.amount)
            {
                this.sumBetter = false;
            }
            else
            {
                this.sumBetter = true;
            }
        }

        public void AddAction(Action action)
        {
            this.actions.AddLast(action);
        }

        public void RemoveAction(Action action)
        {
            this.actions.Remove(action);
        }

        public double AddToAmount(double factor)
        {
            this.amount = this.amount + factor;
            return this.amount;
        }

        public double RemoveAmount(double factor)
        {
            this.amount = this.amount - (this.amount * this.influence * factor);
            return this.amount;
        }

        public double RaiseProduction(double factor)
        {
            this.production = this.production + (this.production * this.influence * factor);
            return this.production;
        }

        public double ReduceProduction(double factor)
        {
            this.production = this.production - (this.production * this.influence * factor);
            return this.production;
        }

        public void ExecuteAction(Action action)
        {
            switch (action.GetType())
            {
                case Type.Once:
                    if (action.GetAdd())
                    {
                        AddToAmount(action.GetFactor());
                    }
                    else
                    {
                        RemoveAmount(action.GetFactor());
                    }
                    break;

                case Type.Unlimited:
                    if (action.GetAdd())
                    {
                        RaiseProduction(action.GetFactor());
                    }
                    else
                    {
                        ReduceProduction(action.GetFactor());
                    }
                    break;
                case Type.Influence:
                    this.influence = this.influence * action.GetFactor();
                    break;
            }
        }

        public int GetId()
        {
            return this.id;
        }

        public string GetCode()
        {
            return this.code;
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetPopulation()
        {
            return this.population;
        }

        public string PrintPopulation()
        {
            return this.population.ToString("N");
        }

        public string GetDescription()
        {
            return this.description;
        }

        public double GetAmount()
        {
            return this.amount;
        }

        public double GetProduction()
        {
            return this.production;
        }

        public string PrintValues()
        {
            Debug.Log("Amount: " + this.amount);
            string o = "";
            if (sumBetter)
            {
                o += "" + this.amount.ToString("N") + " t";
            }
            else
            {
                o += "" + this.amount.ToString("N") + " t";
            }
            if (rateBetter)
            {
                o += " | " + this.production.ToString("N") + " t";
            }
            else
            {
                o += " | " + this.production.ToString("N") + " t";
            }
            return o;
        }

        public LinkedList<Action> GetActions()
        {
            return this.actions;
        }

    }

}
