using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab1
{
    [Serializable]
    sealed class Share//строка бд
    {
        public string CompanyName { get; set; }
        public DateTime DateOfBuy { get; set; }
        /* public int DayOfBuy { get; set; }
         * public int MonthOfBuy { get; set; }
         * public int YearOfBuy { get; set; } */    
        public int AmountOfBuy { get; set; }
        public double PriceOneOfBuy { get; set; }
        // int dayOfBuy, int monthOfBuy, int yearOfBuy
        public Share(string companyName, DateTime dateOfBuy, int amountOfBuy, double priceOneOfBuy)
        {
            CompanyName = companyName;
            /* DayOfBuy = dayOfBuy;
             * MonthOfBuy = monthOfBuy;
             * YearOfBuy = yearOfBuy; */
            DateOfBuy = dateOfBuy;
            AmountOfBuy = amountOfBuy;
            PriceOneOfBuy = priceOneOfBuy;
        }
    }

    //-База данных – список записей БД;
    [Serializable]
    class DataBase//односвязный список строк бд
    {
        [Serializable]
        private class ShareList 
        {
            private ShareList _next;//ссылка на следующий элемент в списке
            private Share _element;
            
            public Share Element
            {
                get { return _element; }
                set { _element = value; }
            }

            public ShareList Next
            {
                get { return _next; }
                set { _next = value; }
            }

        }

        public int Length { get; private set; }
        private ShareList _head;
        private ShareList _tail;

        public DataBase()
        {
            // создание пустого списка
            _head = null;
            _tail = _head;
            Length = 0;
        }

        public void Push(Share element)
        {
            if (_head == null)
            {
                // создать узел, сделать его головным
                _head = new ShareList {Element = element};
                // этот же узел и является хвостовым
                _tail = _head;
                // следующего узла нет
                _head.Next = null;
            }
            else
            {
                // создать временный узел
                ShareList tempBL = new ShareList();
                // следующий за предыдущим хвостовым узлом - это наш временный новый узел
                _tail.Next = tempBL;
                // сделать его же новым хвостовым
                _tail = tempBL;
                _tail.Element = element;
                // следующего узла пока нет
                _tail.Next = null;
            }
            ++Length;
        }

        public void Delete(int position)
        {
            if (position == 0)
            {
                _head = _head.Next;
            }
            else
            {
                ShareList tempBL = _head;
                ShareList prev = _head;
                for (int i = 0; i < position + 1; ++i)
                {
                    if (position > 0 && i == position - 1)
                        prev = tempBL;
                    tempBL = tempBL.Next;
                }
                prev.Next = tempBL;
            }
            Length--;
            GC.Collect();
        }

        public Share this[int position]
        {
            get
            {
                if (position >= 0)
                {
                    ShareList tempBL = _head;
                    for (int i = 0; i < position; ++i)
                    {
                        // переходим к следующему узлу списка
                        tempBL = tempBL.Next;
                    }
                    return tempBL.Element;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (position >= 0)
                {
                    ShareList tempBL = _head;
                    for (int i = 0; i < position; ++i)
                        tempBL = tempBL.Next;
                    tempBL.Element = value;
                }
            }
        }

    }

    //-Манипуляция с файлом, хранилищем БД
    //-Непосредственная манипуляция с файлом-хранилищем БД осуществляется специальным классом
    sealed class DataBaseFile
    {
        public DataBase MainDataBase = new DataBase() {};

        public bool Save(string file)
        {
            try
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = new FileStream(file + ".dat", FileMode.Create, FileAccess.Write))
                    binFormat.Serialize(fStream, MainDataBase);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Load(string file)
        {
            try
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                using (FileStream fStream = new FileStream(file + ".dat", FileMode.Open, FileAccess.Read))
                    MainDataBase = (DataBase) binFormat.Deserialize(fStream);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
