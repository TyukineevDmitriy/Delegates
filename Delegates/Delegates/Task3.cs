﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public interface IObserver
    {
        void Update(string methodName, Dictionary<string, int> paramsDict);
    }

    class DataModel
    {
        private List<IObserver> Observers;
        private int[,] Table;

        public DataModel()
        {
            Observers = new List<IObserver>();
            Table = new int[0,0];
        }

        public void AddObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        private void NotifyObservers(string methodName, Dictionary<string, int> paramsDict)
        {
            foreach (var observer in Observers)
            {
                observer.Update(methodName, paramsDict);
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Put(int row, int column, int value)
        {
            if (row < 0 || row >= Table.GetLength(0))
                throw new ArgumentException("Wrog row");
            if (column < 0 || column >= Table.GetLength(1))
                throw new ArgumentException("Wrog column");
            Table[row, column] = value;
            //DataModel.Put
            string methodName = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
            NotifyObservers(methodName, new Dictionary<string, int>
            {
                { "row", row },
                { "column", column },
                { "value", value }
            });
        }
        public void InsertRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex > Table.GetLength(0))
                throw new ArgumentException("Wrog row");
            int[,] newTable = new int[Table.GetLength(0) + 1, Table.GetLength(1)];
            UpdateTable(newTable);
            string methodName = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
            NotifyObservers(methodName, new Dictionary<string, int>
            {
                { "rowIndex", rowIndex }
            });
        }
        public void InsertColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex > Table.GetLength(1))
                throw new ArgumentException("Wrog column");
            int[,] newTable = new int[Table.GetLength(0), Table.GetLength(1) + 1];
            UpdateTable(newTable);
            string methodName = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
            NotifyObservers(methodName, new Dictionary<string, int>
            {
                { "columnIndex", columnIndex }
            });
        }
        private void UpdateTable(int[,] newTable)
        {
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    newTable[i, j] = Table[i, j];
                }
            }
            Table = newTable;
        }
        public int Get(int row, int column)
        {
            if (row < 0 || row >= Table.GetLength(0))
                throw new ArgumentException("Wrog row");
            if (column < 0 || column >= Table.GetLength(1))
                throw new ArgumentException("Wrog column");
            string methodName = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
            NotifyObservers(methodName, new Dictionary<string, int>
            {
                { "row", row },
                { "column", column }
            });
            return Table[row, column];
        }
    }

    class Logger : IObserver
    {
        DataModel Observable;

        public Logger(DataModel observable)
        {
            Observable = observable;
            Observable.AddObserver(this);
        }

        public void Update(string methodName, Dictionary<string, int> paramsDict)
        {
            throw new NotImplementedException();
        }
    }
    class View : IObserver
    {
        DataModel Observable;

        public View(DataModel observable)
        {
            Observable = observable;
            Observable.AddObserver(this);
        }

        public void Update(string methodName, Dictionary<string, int> paramsDict)
        {
            throw new NotImplementedException();
        }
    }
}
