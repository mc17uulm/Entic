using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class Capital
    {

        private double amount;
        private double rate;
        private double change;
        private string currency;

        public Capital(double start, double rate)
        {
            this.amount = start;
            this.rate = rate;
            this.change = 0.0;
            this.currency = "EUR";
        }

        public void Tick()
        {
            double before = this.amount;

            this.amount += this.rate;
            this.change = 1 - (this.amount / before);
        }

        public void ExecuteEffect(Effect effect, int factor)
        {

            switch (effect.GetOccurence())
            {
                case Occurence.once:
                    this.amount = this.amount + (factor * effect.GetFactor());
                    break;
                case Occurence.unlimited:
                    this.rate = this.rate + (factor * effect.GetFactor());
                    break;
            }

        }

        public double GetAmount()
        {
            return this.amount;
        }

        public double GetRate()
        {
            return this.rate;
        }

        public double GetChange()
        {
            return this.change;
        }

        public double Buy(double value)
        {
            this.amount -= amount;
            return this.amount;
        }

        public double Change(double value)
        {
            this.amount += value;
            return this.amount;
        }
    }
}
