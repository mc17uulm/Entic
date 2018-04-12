using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Logic
{

    public class Play : MonoBehaviour
    {

        private double amount;
        private double production;
        private LinkedList<Country> countries;
        private LinkedList<Action> actions;
        private Capital capital;
        private Lobby lobby;
        private InfoPanel panel;
        private int population;

        public Play(LinkedList<Country> countries)
        {
            this.countries = countries;
            this.actions = new LinkedList<Action>();
            this.capital = new Capital(30000000, 2500000);
            this.lobby = new Lobby(10, 1);
            this.panel = FindObjectOfType<InfoPanel>();
            this.population = 0;
        }

        public void Tick()
        {
            //Debug.Log("Tick!");

            // Ab hier auskommentieren um Logik nicht laufen zu lassen
            foreach(Action action in this.actions)
            {
                if (action.IsActivated())
                {
                    action.DevelopTick();
                }
                if (action.IsFinished())
                {
                    this.ExecuteAction(action);
                }
            }

            double newAmount = 0.0f;
            double newProduction = 0.0f;

            int now = 0;
            foreach(Country country in this.countries)
            {
                country.Tick();
                newAmount += country.GetAmount();
                newProduction += country.GetProduction();
                now += country.GetPopulation();
            }
            this.population = now;

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
            this.lobby.Tick();

            this.panel.changeWaste(newAmount, newProduction);
            this.panel.changeCapital(this.capital.GetAmount(), this.capital.GetRate());

            this.amount = newAmount;
            this.production = newProduction;

        }

        public void SetActions(LinkedList<Action> actions)
        {
            this.actions = actions;
        }

        public void AddAction(Action action)
        {
            this.actions.AddLast(action);
        }

        public void RemoveAction(Action action)
        {
            this.actions.Remove(action);
        }

        public LinkedList<Action> GetActions()
        {
            return this.actions;
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

        public void ExecuteAction(Action action)
        {
            LinkedList<Effect> effects = action.GetEffects();
            foreach (Effect effect in effects)
            {
                this.ExecuteEffect(effect);
            }
            action.Executed();
        }

        public void ExecuteEffect(Effect effect)
        {
            int f = 1;
            switch (effect.GetType())
            {
                case Type.reduce:
                    f = -1;
                    break;
                case Type.add:
                    f = 1;
                    break;
            }

            Value value = effect.GetValue();
            if (value.Equals(Value.Capital))
            {
                this.capital.ExecuteEffect(effect, f);
            }
            else if (value.Equals(Value.Lobby))
            {
                this.lobby.ExecuteEffect(effect, f);
            }
            else
            {
                float ffp = (float) effect.GetFactor() / this.population;

                foreach(Country country in this.countries)
                {
                    country.ExecuteEffect(effect, f, ffp);
                }
            }
        }

    }
}
