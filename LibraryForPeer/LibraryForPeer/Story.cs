using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForPeer
{
    // Отмечаем класс для сериализации.
    [Serializable]
    public class Story : Theme, IAssignable
    {
        /// <summary>
        /// Список с задачами типа Task, наследуемых от проекта.
        /// </summary>
        public List<Task> TasksList { get; set; }

        public Story(string name, DateTime date, string status) : base(name, date, status)
        {
            Name = name;
            Date = date;
            Status = status;
            IAssignable.StoryExecutor = 10;
        }
        /// <summary>
        /// Метод добавляет к списку задач переданную задачу типа Task.
        /// </summary>
        /// <param name="task">Задача типа Task.</param>
        public void AddTask(Task task)
        {
            if (TasksList == null)
                TasksList = new List<Task>();
            if (TasksList.Count <= 20)
                TasksList.Add(task);
            else
                Console.WriteLine("Вы не можете создавать больше 20ти задач");
        }
        /// <summary>
        ///  Снижаем рождаемость среди исполнителей стори до одного.
        /// </summary>
        new public int MaxExecutor
        {
            get
            {
                return 10;
            }
            set
            {
                MaxExecutor = value;
            }
        }
    }
}
