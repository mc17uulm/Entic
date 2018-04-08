using System.Collections;
using System;
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
        private double amountBefore;
        private double productionBefore;

        public Country(int id, string code, string name, int population, string description, double amount, double production, System.Random random)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.population = population;
            this.description = description;
            this.amount = amount;
            this.production = production;
            this.influence = (double) random.NextDouble();
            this.actions = new LinkedList<Action>();
            this.amountBefore = amount;
            this.productionBefore = production;
        }

        public void Tick()
        {
            this.productionBefore = this.production;
            this.amountBefore = this.amount;
            this.production = this.production + ((this.production/12) * (1.01f - this.influence));
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

        public string PrintWaste()
        {
            Debug.Log("Influence: " + this.influence);
            string o;

            double rate = Math.Round((this.amount / this.amountBefore) - 1.0, 4);
            string exponential = string.Format("{0:E2}", this.amount);
            string[] parts = exponential.Split('E');
            string sum = Math.Round((Double.Parse(parts[0]) * 100), 0) + " * 10<sup>" + (Int32.Parse(parts[1].Substring(1)) - 2) + "</sup>";

            if (rate > 0)
            {
                o = "Total: <b>" + sum + "</b> tons <color=#b51818>+" + rate + "%</color>";
            } else if(rate == 0)
            {
                o = "Total: <b>" + sum + "</b> tons +/-" + rate + "%";
            }
            else
            {
                o = "Total: <b>" + sum + "</b> tons <color=#18b518>-" + rate + "%</color>";
            }

            return o;
        }

        public string PrintProduction()
        {
            string o;

            double rate = Math.Round((this.production / this.productionBefore) - 1.0, 4);
            string exponential = string.Format("{0:E2}", this.production);
            string[] parts = exponential.Split('E');
            string prod = Math.Round((Double.Parse(parts[0]) * 100), 0) + " * 10<sup>" + (Int32.Parse(parts[1].Substring(1)) - 2) + "</sup>";

            if (rate > 0)
            {
                o = "monthly: <b>" + prod + "</b> tons <color=#b51818>+" + rate + "%</color>";
            }
            else if (rate == 0)
            {
                o = "monthly: <b>" + prod + "</b> tons +/-" + rate + "%";
            }
            else
            {
                o = "monthly: <b>" + prod + "</b> tons <color=#18b518>-" + rate + "%</color>";
            }

            return o;
        }

        public LinkedList<Action> GetActions()
        {
            return this.actions;
        }

    }

}
