using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForPeer
{
    // Отмечаем класс для сериализации.
    [Serializable]
    public class Project
    {
        public string Name { get;  set; }
        // Ниже списки задач всех типов.
        public List<Epic> EpicList { get; set; }
        public List<Story> StoryList { get; set; }
        public List<Task> TasksList { get; set; }
        public List<Bug> BugList { get; set; }
        public Project(string name)
        {
            Name = name;
        }
        /// <summary>
        /// Ниже методы, добавляющие задачу определенного типа в определенный список.
        /// </summary>
        /// <param name="task"></param>
        public void AddEpic(Epic task)
        {
            if (EpicList == null)
                EpicList = new List<Epic>();
            if (EpicList.Count <= 10)
                EpicList.Add(task);
            else
                Console.WriteLine("Вы не можете создавать больше 10ти задач");
        }
        public void AddStory(Story task)
        {
            if (StoryList == null)
                StoryList = new List<Story>();
            if (StoryList.Count <= 10)
                StoryList.Add(task);
            else
                Console.WriteLine("Вы не можете создавать больше 10ти задач");
        }
        public void AddTask(Task task)
        {
            if (TasksList == null)
                TasksList = new List<Task>();
            if (TasksList.Count <= 10)
                TasksList.Add(task);
            else
                Console.WriteLine("Вы не можете создавать больше 10ти задач");
        }
        public void AddBug(Bug task)
        {
            if (BugList == null)
                BugList = new List<Bug>();
            if (BugList.Count <= 10)
                BugList.Add(task);
            else
                Console.WriteLine("Вы не можете создавать больше 10ти задач");
        }
    }
}
