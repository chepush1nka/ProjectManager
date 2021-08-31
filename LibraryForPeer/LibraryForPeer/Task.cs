using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForPeer
{
    // Отмечаем класс для сериализации.
    [Serializable]
    public class Task : Theme, IAssignable
    {
        public Task(string name, DateTime date, string status) : base(name, date, status)
        {
            Name = name;
            Date = date;
            Status = status;
            IAssignable.TaskExecutor = 1;
        }
        /// <summary>
        /// Снижаем рождаемость среди исполнителей таска до одного.
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
