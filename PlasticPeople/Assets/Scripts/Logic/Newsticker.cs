using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

namespace Logic
{

    public class Newsticker : MonoBehaviour
    {

        public LinkedList<News> news = new LinkedList<News>();
        public Image background;
        public TextMeshProUGUI infoText;
        public TextMeshProUGUI newsFeed;
        public GameObject openTicker;

        void Awake()
        {
            
        }

        public void AddNews(News news)
        {
            this.news.AddFirst(news);
            if(this.news.Count > 5)
            {
                this.news.RemoveLast();
            }

            this.PrintLastNews();
        }

        public string PrintNews()
        {
            string o = "";
            foreach(News n in news)
            {
                o += this.MakeNewsString(n) + "\r\n";
            }

            return o;
        }

        public void PrintLastNews()
        {
            infoText.text = this.MakeNewsString(this.news.First.Value);
        }

        public void PrintNewsFeed()
        {
            newsFeed.text = this.PrintNews();
        }

        private string MakeNewsString(News news)
        {
            string o = "";
            switch (news.GetType())
            {
                case NewsType.News:
                    o += "<b>News: </b>" + news.GetDescr();
                    break;

                case NewsType.Emergency:
                    o += "<b><color=#b51818>Emergency: </color></b>" + news.GetDescr();
                    break;

                case NewsType.Action:
                    o += "<b><color=#1c3759>Action: </color></b>" + news.GetDescr();
                    break;
            }

            return o;
        }

        public void Open()
        {
            openTicker.SetActive(true);
            newsFeed.text = this.PrintNews();
        }

        public void Close()
        {
            openTicker.SetActive(false);
        }

    }

}
