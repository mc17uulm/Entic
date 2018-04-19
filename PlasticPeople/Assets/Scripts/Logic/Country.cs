using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        private float density;

        public Country(int id, string code, string name, int population, string description, double amount, double production, System.Random random)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.population = population;
            this.description = description;
            this.amount = amount;
            this.production = production;
            this.influence = (double) random.NextDouble() + 0.2f;
            this.actions = new LinkedList<Action>();
            this.amountBefore = amount;
            this.productionBefore = production;
            this.density = (float)(1.0f - (this.amount / this.population / 2.9f));
        }

        public void Tick()
        {
            this.productionBefore = this.production;
            this.amountBefore = this.amount;
            double tmp = ((this.production / 12) * (1.0f - this.influence)*0.7);
            //Debug.Log(this.name + ": " + tmp);
            this.production = this.production + tmp;
            this.population += (int) (this.population * 0.001f);

            float density = (float) (1.0f - (this.amount / this.population / 2.9f));
            
            Image img = GameObject.Find("/UI/Map/" + this.code).GetComponent<Image>();
            Color c = img.color;
            c.a = density;
            img.color = c;
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

        public float GetDensity()
        {
            return this.density;
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

        public void ExecuteEffect(Effect effect, int factor, float ffp)
        {
            switch (effect.GetOccurence())
            {
                case Occurence.once:
                    this.amount = this.amount + (factor * (this.population * ffp) * this.influence);
                    break;

                case Occurence.unlimited:
                    this.production = this.production + (factor * (this.population * ffp) * this.influence);
                    break;
            }
        }

        public void ChangeInfluence()
        {
            double f = Game.random.NextDouble() * 2.01;
            double n = this.influence * f;
            if(n > 1.2)
            {
                n = 1.2;
            }
            Debug.Log("New Influence: " + n);
            this.influence = n;
        }

    }

}
