using System.Collections.Generic;

namespace Lab1
{
    sealed class DataLogic
    {
        private readonly DataBaseFile _dataBaseFile = new DataBaseFile();

        public void Create(Share share)
        {
            _dataBaseFile.MainDataBase.Push(share);
        }

        public Share Read(int id)
        {
            return _dataBaseFile.MainDataBase[id];
        }

        List<Share> db = new List<Share>();
        public List<Share> ReadAll()
        {
            for (int i = 0; i < _dataBaseFile.MainDataBase.Length; i++)
            {
                db.Add(_dataBaseFile.MainDataBase[i]);
            }
            return db;
        }

        public bool Update(int id, Share share)
        {
            _dataBaseFile.MainDataBase[id] = share;
            return true;
        }

        public void Delete(int id)
        {
            _dataBaseFile.MainDataBase.Delete(id);
        }

        public int DBLength => _dataBaseFile.MainDataBase.Length;   
        
        public bool LoadFile(string file)
        {
            return _dataBaseFile.Load(file);
        }

        public bool SaveFile(string file)
        {
            return _dataBaseFile.Save(file);
        }
    }
}