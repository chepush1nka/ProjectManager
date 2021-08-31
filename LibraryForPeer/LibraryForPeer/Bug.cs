using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForPeer
{
    // Отмечаем класс для сериализации.
    [Serializable]
    public class Bug : Theme, IAssignable
    {
        public Bug(string name, DateTime date, string status) : base(name, date, status)
        {
            Name = name;
            Date = date;
            Status = status;
            IAssignable.BugExecutor = 1;
        }
        /// <summary>
        /// Снижаем рождаемость среди исполнителей бага до одного.
        /// </summary>
        new public int MaxExecutor
        {
            get
            {
                return 1;
            }
            set
            {
                MaxExecutor = value;
            }
        }
    }
}
