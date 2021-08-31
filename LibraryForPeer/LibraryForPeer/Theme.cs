using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForPeer
{
    // Отмечаем класс для сериализации.
    [Serializable]
    //Родительский класс для всех задач.
    public class Theme
    {
        /// <summary>
        /// Список пользователей, работающих над задачей.
        /// </summary>
        public List<User> Users { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public Theme(string name, DateTime date, string status)
        {
            Name = name;
            Date = date;
            Status = status;
        }
        /// <summary>
        /// Метод для переопределения в наследниках.
        /// </summary>
        public int MaxExecutor { get; set; }
        /// <summary>
        /// Метод, добавляющий в список пользователей.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public void AddUser(User user)
        {
            if (Users == null)
                Users = new List<User>();
            if (Users.Contains(user))
                Console.WriteLine("Такой пользователь уже работает над этой задачей");
            else
            {
                if (Users.Count < MaxExecutor)
                    Users.Add(user);
                else
                    Console.WriteLine("Вы не можете назначать более {0} исполнителя/исполнителей для такого типа задачи", MaxExecutor);
            }
        }
        /// <summary>
        /// Метод, удаляющий из списка пользователей.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public void DeleteUser()
        {
            if (Users == null)
                Users = new List<User>();
            else
            {
                foreach (User u in Users)
                    Console.WriteLine(u.Name);
                Console.WriteLine("|\nВведите имя пользователя для удаления");
                string userName = Console.ReadLine();
                User user = new User("d");
                bool flag = false;
                for (int i = 0; i<Users.Count;i++)
                    if (Users[i].Name == userName)
                    {
                        flag = true;
                        user = Users[i];
                        i += Users.Count;
                    }
                if (flag)
                {
                    Users.Remove(user);
                    Console.WriteLine("Пользователь успешно удален");
                }
                else
                    Console.WriteLine("Такого исполнителя тут и в помине не было");
            }
        }
    }
}
