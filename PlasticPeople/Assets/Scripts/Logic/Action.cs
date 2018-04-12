using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Logic
{

    public enum Category
    {
        Politics,
        Society,
        Science
    }

    public class Action
    {

        private int id;
        private string name;
        private int development;
        private Category category;
        private double price;
        private int points;
        private string descr;
        private string img;
        private LinkedList<Effect> effects;
        private int[] needs;

        private ActionClick.State state;
        private int start;
        private bool activated;
        private bool executed;

        public Action(int id, string name, int development, Category category, double price, int points, string descr, string img, LinkedList<Effect> effects, int[] needs)
        {
            this.id = id;
            this.name = name;
            this.development = development;
            this.category = category;
            this.price = price;
            this.points = points;
            this.descr = descr;
            this.img = img;
            this.effects = effects;
            this.needs = needs;

            this.state = ActionClick.State.Deactivated;
            this.start = 0;
            this.activated = false;
            this.executed = false;
        }

        public void Activate()
        {
            this.activated = true;
            this.state = ActionClick.State.InDevelopement;
        }

        public void DevelopTick()
        {
            if (start != this.development)
            {
                this.start += 1;
            }
            else
            {
                Game.play.AddNews(new News("Action developed", "The development of " + this.name + " is finished!", NewsType.Action));
                this.state = ActionClick.State.Developed;
            }
        }

        public int GetId()
        {
            return this.id;
        }

        public string GetName()
        {
            return this.name;
        }

        public float GetProgress()
        {
            return (this.start / this.development);
        }
        
        public Category GetCategory()
        {
            return this.category;
        }

        public double GetPrice()
        {
            return this.price;
        }

        public int GetPoints()
        {
            return this.points;
        }

        public int GetDevelopment()
        {
            return this.development;
        }

        public string GetDescr()
        {
            return this.descr;
        }

        public string GetImg()
        {
            return this.img;
        }

        public LinkedList<Effect> GetEffects()
        {
            return this.effects;
        }

        public int[] GetNeeded()
        {
            return this.needs;
        }

        public ActionClick.State GetState()
        {
            return this.state;
        }

        public bool Unlocked(int[] developed)
        {
            bool o = true;
            foreach (int n in this.needs)
            {
                bool i = false;
                foreach(int a in developed)
                {
                    if(!i)
                    {
                        i = (n == a);
                    }
                }
                o = (o && i);
            }
            return o;
        }

        public bool IsInDevelopment()
        {
            return (this.start != 0);
        }

        public bool IsActivated()
        {
            return this.activated;
        }

        public bool IsFinished()
        {
            return (this.start == this.development);
        }

        public void Executed()
        {
            this.executed = true;
            this.state = ActionClick.State.Developed;
            LinkedList<Action> actions = Game.play.GetActions();

            foreach (int i in this.needs)
            {
                foreach (Action action in actions)
                {
                    if (action.GetId() == i)
                    {
                        action.ChangeState(ActionClick.State.Clickable);
                    }
                }
            }
        }

        public void ChangeState(ActionClick.State state)
        {
            this.state = state;
        }

        public bool IsExecuted()
        {
            return this.executed;
        }

        public string PrintCosts()
        {
            return this.price.ToString("N") + " Euro<br>" + this.points + " points<br>" + this.development + " months";
        }

        public string PrintEffects()
        {
            string o = "", w = "None<br>";

            // [0] = Capital
            // [1] = Lobby
            // [2] = Amount
            // [3] = Production
            string[] t = { w, w, w, w };
            foreach (Effect effect in this.effects)
            {
                int y = 0;
                switch (effect.GetValue())
                {
                    case Value.Capital: y = 0;
                        break;
                    case Value.Lobby:
                        y = 1;
                        break;
                    case Value.Amount:
                        y = 2;
                        break;
                    case Value.Production:
                        y = 3;
                        break;
                }

                string tmp = effect.PrintEffect();
                Debug.Log("TMP: " + tmp);
                t[y] = tmp;
            }

            Debug.Log("Array: " + string.Join(",", t));
            foreach (string n in t)
            {
                o += n;
            }

            return o;
        }

        public string PrintRequirements(LinkedList<Action> actions)
        {
            string o = "None";
            if (this.needs.Length == 0)
            {
                o = "None";
            }
            else
            {
                foreach (int i in this.needs)
                {
                    foreach (Action action in actions)
                    {
                        if (i == action.GetId())
                        {
                            o += action.GetName() + "<br>";
                        }
                    }
                }
            }

            return o;
        }

    }
}
