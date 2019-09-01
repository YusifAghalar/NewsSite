using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsBLL.Abstract
{
    public interface INewsModal
    {
        void AddToModal(News newsm ,ModalNews modalNews);
        void RemoveFromModal(News news, ModalNews modalNews);
        List<News> List(ModalNews modalNews);
    }
}
