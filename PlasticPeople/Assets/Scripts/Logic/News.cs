using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    public enum NewsType
    {
        News,
        Emergency,
        Action
    }

    public class News
    {

        private string name;
        private string descr;
        private NewsType type;

        public News(string name, string descr, NewsType type)
        {
            this.name = name;
            this.descr = descr;
            this.type = type;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetDescr()
        {
            return this.descr;
        }

        public NewsType GetType()
        {
            return this.type;
        }

    }

}