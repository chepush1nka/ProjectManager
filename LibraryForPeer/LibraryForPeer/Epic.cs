using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForPeer
{
    // Отмечаем класс для сериализации.
    [Serializable]
    public class Epic : Theme
    {
        /// <summary>
        /// Список с задачами типа Story, наследуемых от проекта.
        /// </summary>
        public List<Story> StoryList { get; set; }
        public Epic(string name, DateTime date, string status) : base(name, date, status)
        {
            Name = name;
            Date = date;
            Status = status;
        }
        /// <summary>
        /// Метод добавляет задачу типа Story в соответствующий список.
        /// </summary>
        /// <param name="">задача типа Story</param>
        public void AddStory(Story story)
        {
            if (StoryList == null)
                StoryList = new List<Story>();
            if (StoryList.Count <= 20)
                StoryList.Add(story);
            else
                Console.WriteLine("Вы не можете создавать больше 20ти задач");
        }
    }
}
