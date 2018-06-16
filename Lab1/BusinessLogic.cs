using System.Collections.Generic;

namespace Lab1
{
    /*-Слой данных
     * -Все действия с БД производятся через класс слоя данных, который обязательно имеет четыре метода,
     * манипулирующих одной строкой (create, read, update, delete), а также индексатор для быстрого получения
     * данных по первичному ключу. Дополнительные методы, если необходимы, реализуются самостоятельно;
     * -Слой дата-логики имеет право взаимодействовать только с физическим файлом, хранящим базу данных с одной стороны и слоем бизнес-логики – с другой;
     * • Между слоем данных и слоем бизнес-логики данные передаются через объекты, инкапсулирующие сущности данных; 
     * • Ни слой бизнес-логики, ни слой данных не имеют возможности интерактивно общаться с пользователем;
     *
     * -Слой бизнес-логики
     * -Решение по условию задачи осуществляются классом бизнес-логики, имеющим методы для работы с данными
     * и решения поставленной задачи;
     * -Слой бизнес-логики имеет право взаимодействовать только со слоем юзер-логики с одной стороны и слоем дата-логики – с другой; */
    sealed class BusinessLogic
    {
        readonly DataLogic _dataLogic = new DataLogic() {};

        public bool[] Selection(string companyName)
        {
            int length = _dataLogic.DBLength;
            bool[] selected = new bool[length];
            for (int i = 0; i < length; i++)
                if (_dataLogic.Read(i).CompanyName == companyName)
                    selected[i] = true;
            return selected;
        }

        internal void Add(Share share)
        {
            _dataLogic.Create(share);
        }

        public void Edit(int id, Share share)
        {
            _dataLogic.Update(id, share);
        }

        public void Delete(int id)
        {
            _dataLogic.Delete(id);
        }

        public int DBLength => _dataLogic.DBLength;

        public Share Read(int id) => _dataLogic.Read(id);

        public List<Share> ReadAll()
        {
            return _dataLogic.ReadAll();
        }

        public bool LoadFile(string file)
        {
            return _dataLogic.LoadFile(file);
        }

        public bool SaveFile(string file)
        {
            return _dataLogic.SaveFile(file);
        }

    }

    /* -Слой юзер-логики
     * -Слой юзер-логики имеет право взаимодействовать только с пользователем с одной стороны и слоем бизнес-логики – с другой;
     * +Интерфейс пользователя должен предлагать следующую функциональность: добавить, удалить, отредактировать запись; просмотреть конкретную запись; просмотреть все записи; показать решение задачи;
     */
}
