using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Logic
{

    public class Newsticker : MonoBehaviour
    {

        private LinkedList<News> news;
        public bool open;
        public Image background;
        public TextMeshProUGUI infoText;

        void Awake()
        {
            Debug.Log("Awake Newsticker");
            open = false;
            news = new LinkedList<News>();
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

        public void PrintNews()
        {
            string o = "";
            foreach(News n in news)
            {
                o += this.MakeNewsString(n) + "\r\n";
            }

            infoText.text = o;
        }

        public void PrintLastNews()
        {
            infoText.text = this.MakeNewsString(this.news.First.Value);
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

        public void Click()
        {
            Debug.Log("Newsticker Btn clicked");
            if (open)
            {

            }
            else
            {

            }
            open = !open;
        }

    }

}
