using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    [System.Serializable]
    public class RandomEvent
    {

        private string message;
        private Value change;
        private int amount;
        private int factor;

        public RandomEvent(string message, Value change, int amount, int factor)
        {
            this.message = message;
            this.change = change;
            this.amount = amount;
            this.factor = factor;
        }

        public string GetMsg()
        {
            return this.message;
        }

        public Value GetChange()
        {
            return this.change;
        }

        public int GetAmount()
        {
            return this.amount;
        }

        public int GetFactor()
        {
            return this.factor;
        }

    }

}
