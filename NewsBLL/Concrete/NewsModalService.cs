using NewsBLL.Abstract;
using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsBLL.Concrete
{
    public class NewsModalService : INewsModal
    {
        public void AddToModal(News news,ModalNews modalNews)
        {
            modalNews.News.Add(news);   
        }

        public List<News> List(ModalNews modalNews)
        {
            return modalNews.News;
        }

        public void RemoveFromModal(News news, ModalNews modalNews)
        {
            modalNews.News.Remove(news);
        }
    }
}
