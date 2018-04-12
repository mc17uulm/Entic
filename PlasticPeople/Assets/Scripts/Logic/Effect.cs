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

        public Effect(Type type, Value on, Occurence occur, double factor)
        {
            this.type = type;
            this.value = on;
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

        public string PrintEffect()
        {
            // 0 = plus
            // 1 = minus
            // rot = #b51818
            // grün = #18b51
            string[] bad = { "#b51818", "#18b518" };
            string[] good = { "#18b518", "#b51818" };
            string[] color = good;
            string unity = "";
            Debug.Log("Effect: " + this.value);
            switch (this.value)
            {
                case Value.Capital:
                    color = good;
                    unity = "Euro to capital";
                    break;

                case Value.Lobby:
                    color = good;
                    if (factor > 1)
                    {
                        unity = "Lobbypoints";
                    }
                    else
                    {
                        unity = "Lobbypoint";
                    }
                    break;

                case Value.Amount:
                    color = bad;
                    unity = "tons of waste";
                    break;

                case Value.Production:
                    color = bad;
                    unity = "tons to production";
                    break;

            }
            string o = "";
            string prefix = "<color=" + color[0] + ">+</color>";
            if (this.type.Equals(type == Type.reduce))
            {
                prefix = "<color=" + color[1] + ">-</color>";
            }

            Debug.Log("Prefix: " + prefix);
            o = prefix + factor.ToString("N") + " " + unity + "<br>";
            Debug.Log("Out: " + o);
            return o;
        }

    }

}
