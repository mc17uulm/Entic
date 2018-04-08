using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    public class Play : MonoBehaviour
    {

        private double amount;
        private double production;
        private LinkedList<Country> countries;
        private LinkedList<Action> actions;
        private Capital capital;
        private InfoPanel panel;

        public Play(LinkedList<Country> countries)
        {
            this.countries = countries;
            this.actions = new LinkedList<Action>();
            this.capital = new Capital(30000000, 2500000);
            this.panel = FindObjectOfType<InfoPanel>();
        }

        public void Tick()
        {
            //Debug.Log("Tick!");

            // Ab hier auskommentieren um Logik nicht laufen zu lassen
            foreach(Action action in this.actions)
            {
                action.Tick();
            }

            double newAmount = 0.0f;
            double newProduction = 0.0f;

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
                newProduction += country.GetProduction();
            }

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
            
            this.capital.Tick();

            this.panel.changeWaste(newAmount, newProduction);
            this.panel.changeCapital(this.capital.GetAmount(), this.capital.GetRate());

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
