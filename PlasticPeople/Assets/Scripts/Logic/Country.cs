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
        private float amount;
        private float production;
        private float influence;
        private LinkedList<Action> actions;

        public Country(int id, string code, string name, int population, string description, float amount, float production)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.population = population;
            this.description = description;
            this.amount = amount;
            this.production = production;
            this.influence = 1.0f;
            this.actions = new LinkedList<Action>();
        }

        public void Tick()
        {
            foreach(Action action in this.actions)
            {
                action.Tick();
                if (action.IsFinished())
                {
                    this.ExecuteAction(action);
                }
            }
            this.amount = this.amount + (this.amount * this.production);
        }

        public void AddAction(Action action)
        {
            this.actions.AddLast(action);
        }

        public void RemoveAction(Action action)
        {
            this.actions.Remove(action);
        }

        public float AddToAmount(float factor)
        {
            this.amount = this.amount + factor;
            return this.amount;
        }

        public float RemoveAmount(float factor)
        {
            this.amount = this.amount - (this.amount * this.influence * factor);
            return this.amount;
        }

        public float RaiseProduction(float factor)
        {
            this.production = this.production + (this.production * this.influence * factor);
            return this.production;
        }

        public float ReduceProduction(float factor)
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

        public string GetDescription()
        {
            return this.description;
        }

        public float GetAmount()
        {
            return this.amount;
        }

        public float GetProduction()
        {
            return this.production;
        }

        public LinkedList<Action> GetActions()
        {
            return this.actions;
        }

    }

}
