using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop1.Data
{
    internal class Description
    {
        // Здесь переменная descript приватна и не доступна извне класса
        private string descript = "gbgbgbgb";

        // Метод, который возвращает значение переменной descript
        public string GetDescription()
        {
            return descript;
        }
    }
}