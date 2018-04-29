using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

namespace Logic
{

    public class Play : MonoBehaviour
    {

        private double amount;
        private double production;
        private LinkedList<Country> countries;
        private LinkedList<RandomEvent> events;
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

        private System.Random random;
        private double extra;
        

        public Play(LinkedList<Country> countries, LinkedList<RandomEvent> events, Newsticker news)
        {
            this.countries = countries;
            this.events = events;
            this.news = news;
            this.actions = new LinkedList<Action>();
            this.capital = new Capital(30000000, 2500000);
            this.lobby = new Lobby(10, 1);
            this.panel = FindObjectOfType<InfoPanel>();
            this.tree = FindObjectOfType<Skilltree>();
            this.display = FindObjectOfType<CountryDisplay>();
            this.population = 0;
            this.prog = FindObjectOfType<progresstest>();

            this.random = new System.Random();
            this.extra = 0;
        }

        public void Tick(DateTime print)
        {

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

            // Statistisch 1 random event in 3 Jahren
            if (this.random.Next(1, 1077) == 365)
            {
                Debug.Log("Random Event");
                int f = this.GetRandomEvent(this.random.Next(1, 55));
                LinkedList<RandomEvent> selected = new LinkedList<RandomEvent>();
                foreach (RandomEvent ev in this.events)
                {
                    if (ev.GetFactor() == f)
                    {
                        selected.AddLast(ev);
                    }
                }

                this.ExecuteRandomEvent(selected.ToArray()[this.random.Next(0, selected.Count)]);
            }
            this.prog.Tick();

            //if (print.Day == 1)
            //{
                int days = DateTime.DaysInMonth(print.Year, print.Month);
                double newAmount = 0.0f;
                double newProduction = 0.0f;

                int now = 0;
                foreach (Country country in this.countries)
                {
                    country.Tick(days);
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

                this.capital.Tick(days);
                this.lobby.Tick(days);

                this.panel.changeWaste(newAmount, newProduction);
                this.panel.changeCapital(this.capital.GetAmount(), this.capital.GetRate());

                if (this.display.IsActive())
                {
                    this.display.ShowClickedCountry(this.display.GetLastClicked());
                }

                this.amount = newAmount + this.extra;
                this.production = newProduction;

                if ((this.amount > 1500000000) || (this.production > 50000000))
                {
                    Lose();
                }
                else if ((this.amount < 7000000) && (this.production < 100000))
                {
                    Win();
                }
                if((print.Year - 2019) > 60)
                {
                    Lose();
                }
            //}

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
                            this.news.AddNews(new News("Support", country.GetName() + " gets supported in the fight against plastic waste", NewsType.News));
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
                    //Debug.Log("Capital before: " + this.capital.GetAmount());
                    this.capital.Buy(action.GetPrice());
                    this.lobby.Buy(action.GetPoints());
                    foreach (Action a in this.actions)
                    {
                        if (a.GetId() == action.GetId())
                        {
                            a.Develope();
                            this.news.AddNews(new News("Action in development", "Action \"" + action.GetName() + "\" is in development", NewsType.Action));
                        }
                    }
                    //Debug.Log("Capital after: " + this.capital.GetAmount());

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

        public void ExecuteRandomEvent(RandomEvent e)
        {
            Debug.Log("Random Event: " + e.GetMsg());
            string o = "";
            switch (e.GetChange())
            {
                case Value.Amount:
                    this.extra += e.GetAmount();
                    o = "+ " + e.GetAmount() + " tons of plastic";
                    break;
                case Value.Capital:
                    this.capital.Change(e.GetAmount());
                    o = "+ " + e.GetAmount() + " €";
                    break;
            }
            news.AddNews(new News("RandomEvent", e.GetMsg() + ": " + o, NewsType.Emergency));
        }

        public void ExecuteAction(Action action)
        {
            LinkedList<Effect> effects = action.GetEffects();
            foreach (Effect effect in effects)
            {
                this.ExecuteEffect(effect);
            }
            action.ChangeState(ActionClick.State.Executed);
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

        public int GetRandomEvent(int f)
        {
            if (f <= 10)
            {
                return 1;
            }
            else if ((f > 10) && (f <= 19))
            {
                return 2;
            }
            else if ((f > 19) && (f <= 27))
            {
                return 3;
            }
            else if ((f > 27) && (f <= 34))
            {
                return 4;
            }
            else if ((f > 34) && (f <= 40))
            {
                return 5;
            }
            else if ((f > 40) && (f <= 45))
            {
                return 6;
            }
            else if ((f > 45) && (f <= 49))
            {
                return 7;
            }
            else if ((f > 49) && (f <= 52))
            {
                return 8;
            }
            else if ((f > 52) && (f <= 54))
            {
                return 9;
            }
            else
            {
                return 10;
            }
        }

    }
}
