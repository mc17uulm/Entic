using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    public enum Type
    {
        Once,
        Unlimited,
        Influence
    }

    public class Action
    {

        private string name;
        private int start;
        private int developed;
        private bool finished;
        private Type type;
        private string descr;
        private float factor;
        private bool add;

        public Action(string name, int developed, Type type, string descr, float factor, bool add)
        {
            this.name = name;
            this.start = 0;
            this.developed = developed;
            this.finished = false;
            this.descr = descr;
            this.factor = factor;
            this.add = add;
        }

        public void Tick()
        {
            if (!this.finished)
            {
                this.start += 1;
                this.finished = (this.start == this.developed);
            }
        }

        public string GetName()
        {
            return this.name;
        }

        public bool IsFinished()
        {
            return this.finished;
        }

        public Type GetType()
        {
            return this.type;
        }

        public string GetDescr()
        {
            return this.descr;
        }

        public float GetFactor()
        {
            return this.factor;
        }

        public bool GetAdd()
        {
            return this.add;
        }

    }
}
