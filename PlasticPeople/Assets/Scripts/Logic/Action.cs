using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    public enum Category
    {
        Politics,
        Society,
        Sience
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
        private int[] needed;

        private int start;
        private bool activated;
        private bool executed;

        public Action(int id, string name, int development, Category category, double price, int points, string descr, string img, LinkedList<Effect> effects, int[] needed)
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
            this.needed = needed;

            this.start = 0;
            this.activated = false;
            this.executed = false;
        }

        public void Activate()
        {
            this.activated = true;
        }

        public void DevelopTick()
        {
            if (start != this.development)
            {
                this.start += 1;
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
            return this.needed;
        }

        public bool Unlocked(int[] developed)
        {
            bool o = true;
            foreach (int n in this.needed)
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
        }

        public bool IsExecuted()
        {
            return this.executed;
        }

    }
}
