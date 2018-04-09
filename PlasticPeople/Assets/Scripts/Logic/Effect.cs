using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    public enum Type
    {
        add,
        reduce
    }

    public enum Value
    {
        Amount,
        Production,
        Capital,
        Lobby
    }

    public enum Occurence
    {
        once,
        unlimited
    }

    public class Effect
    {

        private Type type;
        private Value value;
        private Occurence occur;
        private double factor;

        public Effect(Type type, Value value, Occurence occur, double factor)
        {
            this.type = type;
            this.value = value;
            this.occur = occur;
            this.factor = factor;
        }

        public Type GetType()
        {
            return this.type;
        }

        public Value GetValue()
        {
            return this.value;
        }

        public Occurence GetOccurence()
        {
            return this.occur;
        }

        public double GetFactor()
        {
            return this.factor;
        }

    }

}
