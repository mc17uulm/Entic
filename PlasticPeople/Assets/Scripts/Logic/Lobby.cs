using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    public class Lobby : MonoBehaviour
    {

        private int amount;
        private int rate;
        private double change;
        private int i;

        public Lobby(int amount, int rate)
        {
            this.amount = amount;
            this.rate = rate;
            this.change = 0.0;
            this.i = 0;
        }

        public void Tick(int days)
        {
            if(this.i == days)
            {
                this.i = 0;
                this.amount += this.rate;
                int before = this.amount;
                if (before == 0)
                {
                    this.change = 0;
                }
                else
                {
                    this.change = 1 - (this.amount / before);
                }
            }
        }

        public void ExecuteEffect(Effect effect, int factor)
        {
            switch (effect.GetOccurence())
            {
                case Occurence.once:
                    this.amount = this.amount + (factor * (int) effect.GetFactor());
                    break;
                case Occurence.unlimited:
                    this.rate = this.rate + (factor * (int) effect.GetFactor());
                    break;
            }
        }

        public int GetAmount()
        {
            return this.amount;
        }

        public int GetRate()
        {
            return this.rate;
        }

        public double GetChange()
        {
            return this.change;
        }

        public int Buy(int value)
        {
            this.amount -= value;
            return this.amount;

        }

        public double Change(int value)
        {
            this.amount += value;
            return this.amount;
        }

        public string PrintStatus()
        {
            string o = "<b>Lobbypunkte: </b>" + this.amount;
            if(rate < 0)
            {
                o += " (<color=#b51818>-</color> " + this.rate + ")";
            }
            else if(rate == 0)
            {
                o += " (+ " + this.rate + ")";
            }
            else
            {
                o += " (<color=#18b518>+</color> " + this.rate + ")";
            }

            return o;
        }
        
    }
}
