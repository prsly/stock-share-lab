using System.Collections.Generic;

namespace Lab1
{
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
}
