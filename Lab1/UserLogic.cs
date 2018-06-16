using System;
using System.Collections.Generic;
// using System.Text;

namespace Lab1
{
    sealed class UserLogic
    {

        private const string Format = "|{0,5}|{1,20}|{2,10}|{3,5}|{4,5}|";

        BusinessLogic businessLogic = new BusinessLogic();

        public void ErrorRead() => Console.WriteLine("Неверный формат ввода.");
        public void ErrorIDNotFound() => Console.WriteLine("Такого id не существует.");

        public void WriteAllDB()
        {
            List<Share> DBList = businessLogic.ReadAll();
            foreach (Share share in DBList)
            {
                if (share == null) continue;
                string dateBuy = share.DateOfBuy.ToString().Remove(10);
                string companyName = share.CompanyName.Length > 20 ? share.CompanyName.Remove(20) : share.CompanyName;
               Console.WriteLine(Format, DBList.IndexOf(share)+1,
                    share.CompanyName.Length > 20 ? share.CompanyName.Remove(20) : share.CompanyName,
                    dateBuy, share.AmountOfBuy, share.PriceOneOfBuy); 
            }
            DBList.Clear();
            /* for (int i = 0; i < businessLogic.DBLength; i++)
            {
                share = businessLogic.Read(i);
                dateBuy = Convert.ToString(share.DateOfBuy);
                Console.WriteLine(Format,
                    i+1,
                    share.CompanyName.Length > 20 ? share.CompanyName.Remove(20) : share.CompanyName,
                    dateBuy.Remove(10), share.AmountOfBuy, share.PriceOneOfBuy);
            }*/
        }

        public void Menu()
        {
            int k;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1.Добавить акции");
                Console.WriteLine("2.Удалить акции");
                Console.WriteLine("3.Отредактировать запись");
                Console.WriteLine("4.Просмотреть конкретную запись");
                Console.WriteLine("5.Просмотреть все записи");
                Console.WriteLine("6.Показать решение задачи");
                Console.WriteLine("7.Сохранить");
                Console.WriteLine("8.Загрузить");
                Console.WriteLine("0.Выход из любого меню");
                Console.WriteLine(businessLogic.DBLength);
                try
                {
                    k = Convert.ToInt32(Console.ReadLine());
                    if ((k == 2 || k == 3 || k == 4 || k == 5 || k == 6) && businessLogic.DBLength == 0)
                    {
                        Console.WriteLine("База пуста.\nНажмите any key для продолжения");
                        Console.ReadKey();
                        continue;
                    }
                    if (k < 0 && k > 9)
                    {
                        Console.WriteLine("Неправильный ввод.\nНажмите any key для продолжения");
                        Console.ReadKey();
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
                
                switch (k)
                {
                    case 1:
                        Console.WriteLine("Введите наименование фирмы: ");
                        string name = Console.ReadLine();
                        int day, month, year;
                        DateTime dt;
                        while (true)
                        {
                            try
                            { 
                                Console.WriteLine("Введите дату покупки в формате dd-mm-yyyy: ");
                                char[] delim = {'-'};
                                string[] date = Console.ReadLine().Split(delim); // --> "1, 1" -> ["1", "2"]
                                day = int.Parse(date[0]);
                                month = int.Parse(date[1]);
                                year = int.Parse(date[2]);
                                dt = new DateTime(year, month, day);
                                if (dt > DateTime.Now)
                                {
                                    Console.WriteLine("Дата не наступила.");
                                    throw new Exception();
                                }
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        int amount;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите количество акций: ");
                                amount = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        double price;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите цену за 1 акцию: ");
                                price = Convert.ToDouble(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        Share share = new Share(name, dt, amount, price);
                        businessLogic.Add(share);
                        break;
                    case 2:
                        WriteAllDB();
                        int id;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите id записи для удаления: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (id >= 0 && id < businessLogic.DBLength+1)
                                {
                                    break;
                                }
                                ErrorIDNotFound();
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        if (id == 0) break;
                        businessLogic.Delete(id-1);
                        break;
                    case 3:
                        WriteAllDB();
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите id записи для редактирования записи: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (id >= 0 && id < businessLogic.DBLength+1)
                                {
                                    break;
                                }
                                ErrorIDNotFound();
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        if (id == 0) break;
                        Console.WriteLine("Введите наименование фирмы: ");
                        name = Console.ReadLine();
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите дату купли в формате dd-mm-yyyy: ");
                                char[] delim = { '-' };
                                string[] date = Console.ReadLine().Split(delim); // --> "1, 1" -> ["1", "2"]
                                day = int.Parse(date[0]);
                                month = int.Parse(date[1]);
                                year = int.Parse(date[2]);
                                dt = new DateTime(year, month, day);
                                if (dt > DateTime.Now)
                                {
                                    Console.WriteLine("Дата не наступила.");
                                    throw new Exception();
                                }
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите количество акций: ");
                                amount = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите цену за 1 акцию: ");
                                price = Convert.ToDouble(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        share = new Share(name, dt, amount, price);
                        businessLogic.Edit(id-1, share);
                        break;
                    case 4:
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите id записи для просмотра: ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (id >= 0 && id < businessLogic.DBLength+1)
                                {
                                    break;
                                }
                                ErrorIDNotFound();
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        if (id == 0) break;
                        share = businessLogic.Read(id-1);
                        String dateBuy = Convert.ToString(share.DateOfBuy);
                        Console.WriteLine(Format,
                            id,
                            share.CompanyName.Length > 20 ? share.CompanyName.Remove(20) : share.CompanyName,
                            dateBuy.Remove(10), share.AmountOfBuy, share.PriceOneOfBuy);
                        break;
                    case 5:
                        WriteAllDB();
                        break;
                    case 6:
                        Console.WriteLine("Вычислить цену всех акций указанной фирмы на текущий день,\n" +
                            "если имеются сведения об изменении стоимости акции в процентном отношении\n" +
                            "от текущей стоимости акции за каждый месяц.\n" +
                            "Для сравнения указать исходную стоимость всех акций и процент прибыли или убытка.");
                        WriteAllDB();
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите id компании: ");
                                id = Convert.ToInt32(Console.ReadLine())-1;
                                if (id >= 0 && id < businessLogic.DBLength)
                                {
                                    break;
                                }
                                if (id == -1) break;
                                ErrorIDNotFound();
                            }
                            catch
                            {
                                ErrorRead();
                            }
                        }
                        if (id == -1) break;
                        share = businessLogic.Read(id);
                        bool[] sel = businessLogic.Selection(share.CompanyName);
                        DateTime localTime = DateTime.Now;
                        double allPriceChanges = 0;
                        double allPrice = 0;
                        int countBuy = 0;
                        for (int i = 0; i < businessLogic.DBLength; i++)
                        {
                            if (sel[i])
                            {
                                share = businessLogic.Read(i);
                                int MonthCount = 0;
                                countBuy++;
                                if (share.DateOfBuy.Year == localTime.Year) MonthCount = localTime.Month - share.DateOfBuy.Month;
                                else
                                {
                                    int YearCount = localTime.Year - share.DateOfBuy.Year - 1;
                                    MonthCount = 12 * YearCount + (12 - share.DateOfBuy.Month) + localTime.Month;
                                }
                                double allPricePart = share.PriceOneOfBuy * share.AmountOfBuy;
                                allPrice = allPrice + allPricePart;
                                for (int j = 0; j < MonthCount; j++)
                                {
                                    Console.WriteLine("Введите изменение в процентах за {0} месяц по {1} покупке: ", j+1, countBuy);
                                    double procent = Convert.ToDouble(Console.ReadLine());
                                    allPricePart = allPricePart * (1.00 + procent * 0.01);
                                }
                                allPriceChanges = allPriceChanges + allPricePart;
                            }
                        }
                        Console.WriteLine("Цена всех акций на время покупки: {0}\nЦена всех акций с учетом изменений на текущий момент: {1}", Math.Round(allPrice, 2), Math.Round(allPriceChanges, 2));
                        double change = (allPriceChanges / allPrice) * 100 - 100;
                        Console.WriteLine("Изменения за период времени в процентах: {0}", Math.Round(change, 2));
                        break;
                    case 7:
                        Console.WriteLine("Введите имя файла: ");
                        string file = Console.ReadLine();
                        if (file == "")
                        {
                            Console.WriteLine("Имя файла введено неверно.");
                            break;
                        }
                        if (businessLogic.SaveFile(file))
                            Console.WriteLine("Файл сохранен.");
                        else
                            Console.WriteLine("Ошибка загрузки.");
                        break;
                    case 8:
                        Console.WriteLine("Введите имя файла:");
                        file = Console.ReadLine();
                        if (file == "")
                        {
                            Console.WriteLine("Имя файла введено неверно.");
                            break;
                        }
                        if (businessLogic.LoadFile(file))
                            Console.WriteLine("Файл загружен.");
                        else
                            Console.WriteLine("Ошибка загрузки.");
                        break;
                    case 0:
                        return;
                }
                Console.WriteLine("Нажмите any key для продолжения");
                Console.ReadKey();
            }
        }
    }

    class Program
    {
        private static void Main()
        {
            UserLogic userLogic = new UserLogic();
            userLogic.Menu();
        }
    }
}