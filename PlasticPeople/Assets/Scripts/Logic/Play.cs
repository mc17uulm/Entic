using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    public class Play
    {

        private float amount;
        private float production;
        private LinkedList<Country> countries;
        private LinkedList<Action> actions;

        public Play(LinkedList<Country> countries)
        {
            this.countries = countries;
            this.actions = new LinkedList<Action>();
        }

        public void Tick()
        {
            Debug.Log("Tick!");

            // Ab hier auskommentieren um Logik nicht laufen zu lassen
            foreach(Action action in this.actions)
            {
                action.Tick();
            }

            float newAmount = 0.0f;
            float newProduction = 0.0f;

            foreach(Country country in this.countries)
            {
                foreach(Action action in this.actions)
                {
                    if (action.IsFinished())
                    {
                        country.ExecuteAction(action);
                    }
                }
                country.Tick();
                newAmount += country.GetAmount();
            }

            newProduction = 1 - (this.amount / newAmount);

            if(newAmount < this.amount)
            {
                // weniger Plastik
            }
            else if(newAmount == this.amount)
            {
                // gleich viel Plastik
            }
            else
            {
               // mehr Plastik
            }

            this.amount = newAmount;
            this.production = newProduction;

        }

        public void AddAction(Action action)
        {
            this.actions.AddLast(action);
        }

        public void RemoveAction(Action action)
        {
            this.actions.Remove(action);
        }

        public Country GetCountry(int id)
        {
            foreach(Country country in this.countries)
            {
                if (country.GetId() == id)
                {
                    return country;
                }
            }
            throw new System.Exception("Id nicht in json");
        }

    }
}
