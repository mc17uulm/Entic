using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        private Newsticker news;
        public Skilltree tree;
        public CountryDisplay display;
        public progresstest prog;

        public static LinkedList<Action> endActions;
        public static Capital endCapital;
        public static DateTime endTime;

        public Play(LinkedList<Country> countries, Newsticker news)
        {
            this.countries = countries;
            this.news = news;
            this.actions = new LinkedList<Action>();
            this.capital = new Capital(30000000, 2500000);
            this.lobby = new Lobby(10, 1);
            this.panel = FindObjectOfType<InfoPanel>();
            this.tree = FindObjectOfType<Skilltree>();
            this.display = FindObjectOfType<CountryDisplay>();
            this.prog = FindObjectOfType<progresstest>();
            this.population = 0;
        }

        public void Tick()
        {
            //Debug.Log("Tick!");

            // Ab hier auskommentieren um Logik nicht laufen zu lassen
            foreach (Action action in this.actions)
            {
                if (action.GetState() == ActionClick.State.InDevelopement)
                {
                    action.DevelopTick();
                }
                if (action.GetState().Equals(ActionClick.State.Developed))
                {
                    this.ExecuteAction(action);
                }
            }

            this.prog.Tick();

            double newAmount = 0.0f;
            double newProduction = 0.0f;

            int now = 0;
            foreach (Country country in this.countries)
            {
                country.Tick();
                newAmount += country.GetAmount();
                newProduction += country.GetProduction();
                now += country.GetPopulation();
            }
            this.population = now;

            if (newAmount < this.amount)
            {
                // weniger Plastik
            }
            else if (newAmount == this.amount)
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

            if (this.display.IsActive())
            {
                this.display.ShowClickedCountry(this.display.GetLastClicked());
            }

            this.amount = newAmount;
            this.production = newProduction;

            if ((this.amount > 20000000000) || (this.production > 1000000000))
            {
                Lose();
            }
            else if ((this.amount < 7000000) && (this.production < 100000))
            {
                Win();
            }



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

        public void Support()
        {
            if (this.display.IsActive())
            {
                int id = this.display.GetLastClicked();
                foreach (Country country in this.countries)
                {
                    if (country.GetId() == id)
                    {
                        if (capital.GetAmount() > 10000000)
                        {
                            capital.Buy(10000000);
                            country.ChangeInfluence();
                            this.display.ShowClickedCountry(id);
                        }
                    }
                }
            }
        }

        public bool Buy(Action action)
        {
            if (this.capital.GetAmount() >= action.GetPrice())
            {
                if (this.lobby.GetAmount() >= action.GetPoints())
                {
                    Debug.Log("Capital before: " + this.capital.GetAmount());
                    this.capital.Buy(action.GetPrice());
                    this.lobby.Buy(action.GetPoints());
                    foreach (Action a in this.actions)
                    {
                        if (a.GetId() == action.GetId())
                        {
                            a.Activate();
                            this.news.AddNews(new News("Action in development", "Action \"" + action.GetName() + "\" is in development", NewsType.Action));
                        }
                    }
                    Debug.Log("Capital after: " + this.capital.GetAmount());

                    return true;
                }
            }

            return false;
        }

        public void AddNews(News news)
        {
            this.news.AddNews(news);
        }

        public Country GetCountry(int id)
        {
            foreach (Country country in this.countries)
            {
                if (country.GetId() == id)
                {
                    return country;
                }
            }
            throw new System.Exception("Id nicht in json");
        }

        public CountryDisplay GetCountryDisplay()
        {
            return this.display;
        }

        public Capital GetCapital()
        {
            return this.capital;
        }

        public Lobby GetLobby()
        {
            return this.lobby;
        }

        public void RefreshSkilltree()
        {
            this.tree.Pause();
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
                float ffp = (float)effect.GetFactor() / this.population;

                foreach (Country country in this.countries)
                {
                    country.ExecuteEffect(effect, f, ffp);
                }
            }
        }

        public void Lose()
        {
            endCapital = this.capital;
            endActions = this.actions;
            endTime = Game.print;
            SceneManager.LoadScene(3);
        }

        public void Win()
        {
            endCapital = this.capital;
            endActions = this.actions;
            endTime = Game.print;
            SceneManager.LoadScene(2);
        }

        public LinkedList<Action> GetEndActions()
        {
            return endActions;
        }

        public Capital GetEndCapital()
        {
            return endCapital;
        }

        public DateTime GetEndTime()
        {
            return endTime;
        }

    }
}
