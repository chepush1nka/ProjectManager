using System;
using LibraryForPeer;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JustDoItApp
{
    class Program
    {
        /// <summary>
        /// Список пользователей.
        /// </summary>
        public static List<User> Users { get; set; }
        /// <summary>
        /// Список проектов.
        /// </summary>
        public static List<Project> Projects { get; set; }
        static void Main(string[] args)
        {
            // Приветственное сообщение.
            Hello();
            do
            {
                // Трайкэтч от сглаза.
                try
                {
                    // Вызываем метод десериализации, чтобы восстановить сохраненные данные.
                    Deserialization();
                }
                catch
                {
                    Console.WriteLine("Ошибка при десериализации");
                }
                // Событие закрытия приложения.
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(Serialization);
                // Вызываем главное меню.
                Menu();
                Console.WriteLine("нажмите \"Enter\", если хочешь повторить и любую другую клавишу, чтобы выйти");
            } while (Console.ReadKey(true).Key == ConsoleKey.Enter); 
        }
        /// <summary>
        /// Главное меню программы. В зависимости от выбора пользователя вызывает соответствующий метод.
        /// </summary>
        public static void Menu()
        {
            // Трайкэтч от сглаза.
            try
            {
                bool flag = true;
                while (flag)
                {
                    flag = false;
                    Console.WriteLine("Введите номер операции из списка\n");
                    Console.WriteLine("1. Работа с пользователями");
                    Console.WriteLine("2. Работа с проектами");
                    Console.WriteLine("3. Работа с задачами в проекте\n\n");
                    Console.WriteLine("4. Выход");
                    string ans = Console.ReadLine();
                    ClearConsole();
                    if (ans == "1")
                        WorkWithUsers();
                    else if (ans == "2")
                        WorkWithProject();
                    else if (ans == "3")
                        WhichProject();
                    else if (ans == "4")
                        Environment.Exit(0);
                    else
                    {
                        Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                        flag = true;
                    }
                }
            }
            catch
            {
                Console.WriteLine("ПИЗДЕЦ");
                // Гениальный кэтч, пользователь даже не поймет, если что-то не дай бог сломается, хотя не должно.
                // Из-за сериализации даже никакая информация не потеряется, так что считаю этот кэтч крутым парнем.
                Menu();
            }
        }
        /// <summary>
        /// Метод-меню, который обеспечиваетработу с пользователями(назначение, удаление, просмотр).
        /// </summary>
        public static void WorkWithUsers()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Создать пользователя");
                Console.WriteLine("2. Удалить пользователя");
                Console.WriteLine("3. Просмотреть список пользователей\n\n");
                Console.WriteLine("4. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    CreateUser();
                else if (ans == "2")
                    DeleteUser();
                else if (ans == "3")
                    CheckUsers();
                else if (ans == "4")
                    Menu();
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод создает нового пользователя, проверяя, что такого пользователя еще нет.
        /// </summary>
        public static void CreateUser()
        {
            if (Users == null)
                Users = new List<User>();
            Console.WriteLine("Введите имя пользователя");
            bool flag = true;
            string userName = Console.ReadLine();
            // Проверка, что такого пользователя еще не создано.
            foreach (User u in Users)
                if (u.Name == userName)
                    flag = false;
            if (!flag)
                Console.WriteLine("Такой пользователь уже создан, а другого нам и не надо");
            else
            {
                Console.WriteLine("");
                Users.Add(new User(userName));
                Console.WriteLine("Вы создали пользователя \"{0}\"", userName);
            }
            // Возвращаем пользователя в меню для работы с исполнителями.
            WorkWithUsers();
        }
        /// <summary>
        /// Метод удаляет пользователя из соответствующего списка.
        /// </summary>
        public static void DeleteUser()
        {
            if ((Users == null) || Users.Count == 0)
                Console.WriteLine("У вас нет ни одного пользователя");
            // Не мучаем пользователя выбором удаляемого объекта, так как он всего один.
            else if (Users.Count == 1)
            {
                Console.WriteLine("Пользователь \"{0}\" убит", Users[0].Name);
                Users.Remove(Users[0]);
            }
            else
            {
                ClearConsole();
                foreach (User a in Users)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите имя пользователя, которого вы хотите убить\n");
                string userName = Console.ReadLine();
                bool flag = false;
                // Удаляем пользователя из соответствующего списка.
                for (int i = 0; i < Users.Count; i++)
                    if (Users[i].Name == userName)
                    {
                        flag = true;
                        Users.Remove(Users[i]);
                        i += Users.Count;
                    }

                if (flag)
                    Console.WriteLine("Пользователь \"{0}\" смог убежать, но работать с вам и он больше не будет", userName);
                else
                {
                    Console.WriteLine("Пользователя с именем \"{0}\" не существует, нажмите любую клавишу, чтобы смириться", userName);
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            // Возвращаем пользователя в менб для работы с исполнителями.
            WorkWithUsers();
        }
        /// <summary>
        /// Метод выводит в консоль имена всех пользователей.
        /// </summary>
        public static void CheckUsers()
        {
            if ((Users == null) || Users.Count == 0)
                Console.WriteLine("У вас нет ни одного пользователя");
            else
            {
                foreach (User a in Users)
                {
                    Console.WriteLine(a.Name);
                }
            }
            // Возвращаем пользователя в менб для работы с исполнителями.
            WorkWithUsers();
        }
        /// <summary>
        /// Метод-меню для работы с проектами.
        /// </summary>
        public static void WorkWithProject()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Создать проект");
                Console.WriteLine("2. Удалить проект");
                Console.WriteLine("3. Просмотреть/Отредактировать список проектов\n\n");
                Console.WriteLine("4. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    CreateProject();
                else if (ans == "2")
                    DeleteProject();
                else if (ans == "3")
                    CheckProject();
                else if (ans == "4")
                    Menu();
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод создает новый проект, если такого еще не существует.
        /// </summary>
        public static void CreateProject()
        {
            if (Projects == null)
                Projects = new List<Project>();
            Console.WriteLine("Введите название проекта");
            string projectName = Console.ReadLine();
            bool flag = true;
            // Проверка, чтобы названия проектов не повторялись.
            foreach (Project p in Projects)
                if (p.Name == projectName)
                    flag = false;
            if (!flag)
                Console.WriteLine("Такой проект уже создан, а другого нам и не надо");
            else
            {
                if (Projects.Count <= 15)
                {
                    Console.WriteLine("");
                    Projects.Add(new Project(projectName));
                    Console.WriteLine("Вы создали проект \"{0}\"", projectName);
                }
                else
                    Console.WriteLine("Вы создали уже достаточно пректов, успокойтесь, пожалуйста или удалите какой-нибудь");
            }
            // Возвращаем пользователя в меню для работы с проектами.
            WorkWithProject();
        }
        /// <summary>
        /// Метод удаляет проект из соответствующего списка.
        /// </summary>
        public static void DeleteProject()
        {
            if ((Projects == null)|| Projects.Count==0)
                Console.WriteLine("У вас нет ни одного проекта");
            // Если проект всего один, то сразу удаляем его.
            else if (Projects.Count == 1)
            {
                Console.WriteLine("Проект \"{0}\" помер", Projects[0].Name);
                Projects.Remove(Projects[0]);
            }
            else
            {
                ClearConsole();
                // Выводим список имен проектов.
                foreach (Project a in Projects)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название проекта, который вы хотите удалить\n");
                string projectName = Console.ReadLine();
                bool flag = false;
                // Удаляем выбранный проект.
                for (int i = 0; i < Projects.Count; i++)
                {
                    if (Projects[i].Name == projectName)
                    {
                        flag = true;
                        Projects.Remove(Projects[i]);
                        i += Projects.Count;
                    }

                }
                if (flag)
                    Console.WriteLine("Проект \"{0}\" успешно удален", projectName);
                else
                {
                    Console.WriteLine("Проекта с именем \"{0}\" не существует, нажмите любую клавишу, чтобы смириться", projectName);
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            // Возвращаем пользователя в меню для работы с проектами.
            WorkWithProject();
        }
        /// <summary>
        /// Метод выводит на консоль список проектов и предлагает изменить какой-то из них.
        /// </summary>
        public static void CheckProject()
        {
            if ((Projects == null) || Projects.Count == 0)
                Console.WriteLine("У вас нет ни одного проекта\n");
            else
            {
                // Подсчитываем и выводим число задач в проекте вместе с его именем.
                foreach (Project a in Projects)
                {
                    Console.WriteLine("\"{0}\"  (Число задач: {1})", a.Name, TasksCounter(a));
                }
                string inputAns;
                // Предлагаем пользователю изменить имя какого-либо проекта.
                do
                {
                    Console.WriteLine("Хотите ли вы изменить название одного из проектов?\n1. Да\n2. Нет");
                    inputAns = Console.ReadLine();
                    if (inputAns == "1")
                    {
                        ChangeName();
                    }
                    else if (inputAns != "2")
                    {
                        Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    }
                } while ((inputAns != "2")&&(inputAns != "1"));
            }
            // Возвращаем пользователя в меню для работы с проектами.
            WorkWithProject();
        }
        /// <summary>
        /// Метод подсчитывает число связанных с проектом задач.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Метод возвращает число связанных с проектом задач.</returns>
        public static int TasksCounter(Project project)
        {
            int count = 0;
            if (project.EpicList != null)
            {
                count += project.EpicList.Count;
                foreach (Epic e in project.EpicList)
                    if (e.StoryList != null)
                    {
                        count += e.StoryList.Count;
                        foreach (Story s in e.StoryList)
                            if (s.TasksList != null)
                                count += project.TasksList.Count;
                    }
            }
            if (project.StoryList != null)
            {
                count += project.StoryList.Count;
                foreach (Story s in project.StoryList)
                    if (s.TasksList != null)
                        count += project.TasksList.Count;
            }
            if (project.TasksList != null)
                count += project.TasksList.Count;
            if (project.BugList != null)
                count += project.BugList.Count;
            return count;
        }
        /// <summary>
        /// Метод для смены имени у проекта.
        /// </summary>
        public static void ChangeName()
        {
            if ((Projects == null) || Projects.Count == 0)
                Console.WriteLine("У вас нет ни одного проекта");
            // Если проект всего один, то сразу удаляем его.
            else if (Projects.Count == 1)
            {
                Console.WriteLine("\nВведите новое название проекта\n");
                string newProject = Console.ReadLine();
                Projects[0].Name = newProject;
            }
            else
            {
                ClearConsole();
                foreach (Project a in Projects)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название проекта, которое вы хотите изменить\n");
                string projectName = Console.ReadLine(), newProjectName = "";
                bool flag = false;
                for (int i = 0; i < Projects.Count; i++)
                    if (Projects[i].Name == projectName)
                    {
                        flag = true;
                        Console.WriteLine("\nВведите новое название проекта\n");
                        newProjectName = Console.ReadLine();
                        Projects[i].Name = newProjectName;
                        i += Projects.Count;
                    }
                if (flag)
                    Console.WriteLine("Проект c именем " + projectName + " переименован в " + newProjectName + "\n");
                else
                {
                    Console.WriteLine("Проекта с именем " + projectName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                    ClearConsole();
                }
                // Возвращаем пользователя в меню для работы с проектами.
                WorkWithProject();
            }
        }
        /// <summary>
        /// Метод, с помощью которого пользователь определяет проект, в котором он хочет продолжить работу.
        /// </summary>
        public static void WhichProject()
        {
            if ((Projects == null) || Projects.Count == 0)
                Console.WriteLine("У вас нет ни одного проекта\n");
            else if (Projects.Count == 1)
                // Отправляемпользователя в метод-меню, где он выберет с каким типом задач продолжить работу.
                WhichTipeMenu(Projects[0]);
            else
            {
                ClearConsole();
                foreach (Project a in Projects)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название проекта в котором вы хотите продолжить работу\n");
                string projectName = Console.ReadLine();
                bool flag = false;
                Project project = new Project("d");
                for (int i = 0; i < Projects.Count; i++)
                    if (Projects[i].Name == projectName)
                    {
                        flag = true;
                        project = Projects[i];
                        i += Projects.Count;
                    }

                if (flag)
                {
                    // Отправляемпользователя в метод-меню, где он выберет с каким типом задач продолжить работу.
                    WhichTipeMenu(project);
                }
                else
                {
                    Console.WriteLine("Проекта с именем " + projectName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            Menu();
        }
        /// <summary>
        /// Метод-меню, где пользователь выберает с каким типом задач продолжить работу.
        /// Также он может просмотреть список всех задач или сгруппировать их по статусу.
        /// </summary>
        /// <param name="project"></param>
        public static void WhichTipeMenu(Project project)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВыберите тип из предложенного списка\n");
                Console.WriteLine("1. Работа с Epic(или с ее подзадачами, т.е Epic-Story-Task)");
                Console.WriteLine("2. Работа со Story(или с ее подзадачами, т.е Story-Task)");
                Console.WriteLine("3. Работа с Task");
                Console.WriteLine("4. Работа с Bug\n\n");
                Console.WriteLine("5. Просмотр всех задач проекта\n\n");
                Console.WriteLine("6. Группировка задач по статусу\n\n");
                Console.WriteLine("7. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                // Далее отправляем пользователя в соответствующий его выбору метод.
                if (ans == "1")
                    EpicMenu(project);
                else if (ans == "2")
                    StoryMenu(project);
                else if (ans == "3")
                    TaskMenu(project);
                else if (ans == "4")
                    BugMenu(project);
                else if (ans == "5")
                    CheckAllTasks(project);
                else if (ans == "6")
                    GroupByStatus(project);
                else if (ans == "7")
                    Menu();
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод отправляет в соответствующие методы для просмотра списка задач в проекте.
        /// </summary>
        /// <param name="project"></param>
        public static void CheckAllTasks(Project project)
        {
            //Трайкэтч от сглаза.
            try
            {
                PrintEpics(project);
                PrintAllExeptEpics(project);
                WhichTipeMenu(project);
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так");
            }
        }
        /// <summary>
        /// Метод-меню для работы с задачами типа Epic.
        /// </summary>
        /// <param name="project"></param>
        public static void EpicMenu(Project project)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление задачи Epic");
                Console.WriteLine("2. Удаление задачи Epic");
                Console.WriteLine("3. Работа с подзадачами задачи Epic");
                Console.WriteLine("4. Установка статуса");
                Console.WriteLine("5. Просмотреть список задач\n\n");
                Console.WriteLine("6. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    CreateEpic(project);
                else if (ans == "2")
                    DeleteEpic(project);
                else if (ans == "3"){
                    Epic epic = WhichEpic(project);
                    if (epic!=null)
                        WorkWithStoris(epic);
                }
                else if (ans == "4"){
                    Epic epic =  WhichEpic(project);
                    if (epic != null)
                        epic.Status = Status();
                }
                else if (ans == "5")
                    CheckAllTasks(project);
                else if (ans == "6")
                    WhichTipeMenu(project);
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод осуществляет выбор задачи типа Epic.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Метод возвращает выбранную пользователем задачу типа Epic и null, если выбрать задачу не получилось.</returns>
        public static Epic WhichEpic(Project project)
        {
            if ((project.EpicList == null) || project.EpicList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи типа Epic\n");
            else if (project.EpicList.Count == 1)
                return project.EpicList[0];
            else
            {
                ClearConsole();
                foreach (Epic a in project.EpicList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи типа Epic в которой вы хотите продолжить работу\n");
                string epicName = Console.ReadLine();
                bool flag = false;
                Epic epic = new Epic("d", DateTime.Now, "s");
                for (int i = 0; i < project.EpicList.Count; i++)
                    if (project.EpicList[i].Name == epicName)
                    {
                        flag = true;
                        epic = project.EpicList[i];
                        i += project.EpicList.Count;
                    }
                if (flag)
                    return epic;
                else
                {
                    Console.WriteLine("Задачи типа Story  с именем " + epicName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                }
            }
            return null;
        }
        /// <summary>
        /// Метод создает задачу типа Epic и добавляет ее в соответствующий список.
        /// </summary>
        /// <param name="project"></param>
        public static void CreateEpic(Project project)
        {
            Console.WriteLine("Введите название задачи");
            string taskName = Console.ReadLine();
            project.AddEpic(new Epic(taskName, DateTime.Now, "Открытая задача"));
            Console.WriteLine("Вы создали задачу " + taskName+" типа Epic\n");
            // Возвращаем пользователя в меню для работы с задачами типа Epic.
            EpicMenu(project);
        }
        /// <summary>
        /// Метод удаляет задачу типа Epic из соответствующего списка.
        /// </summary>
        /// <param name="project"></param>
        public static void DeleteEpic(Project project)
        {
            if ((project.EpicList == null) || project.EpicList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи в этом проекте");
            else if (project.EpicList.Count == 1)
            {
                Console.WriteLine("Epic \"{0}\" помер", project.EpicList[0].Name);
                project.EpicList.Remove(project.EpicList[0]);
            }
            else
            {
                ClearConsole();
                foreach (var a in project.EpicList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи, который вы хотите удалить\n");
                string epicName = Console.ReadLine();
                bool flag = false;
                for (int i = 0; i < project.EpicList.Count; i++)
                {
                    if (project.EpicList[i].Name == epicName)
                    {
                        flag = true;
                        project.EpicList.Remove(project.EpicList[i]);
                        i += project.EpicList.Count;
                    }

                }
                if (flag)
                    Console.WriteLine("Задача " + epicName + " успешно удалена");
                else
                {
                    Console.WriteLine("Задачи с именем " + epicName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            // Возвращаем пользователя в меню для работы с задачами типа Epic.
            EpicMenu(project);
        }
        /// <summary>
        /// Метод-меню для работы с задачами типа Story.
        /// </summary>
        /// <param name="project"></param>
        public static void StoryMenu(Project project)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление задачи Story\n2. Удаление задачи Story\n3. Работа с подзадачами задачи Story");
                Console.WriteLine("4. Назначение исполнителей\n5. Установка статуса\n6. Просмотреть список задач\n\n7. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    CreateStory(project);
                else if (ans == "2")
                    DeleteStory(project);
                else if (ans == "4"){
                    Story story = WhichStory(project);
                    if (story != null)
                        StoryUsers(story);
                }
                else if (ans == "3"){
                    Story story = WhichStory(project);
                    if (story != null)
                        WorkWithTasks(story);
                }
                else if (ans == "5"){
                    Story story = WhichStory(project);
                    if (story != null)
                        story.Status = Status();
                }
                else if (ans == "6")
                    CheckAllTasks(project);
                else if (ans == "7")
                    WhichTipeMenu(project);
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод создает задачу типа Story и добавляет ее в соответствующий список.
        /// </summary>
        /// <param name="project"></param>
        public static void CreateStory(Project project)
        {
            Console.WriteLine("Введите название задачи");
            string taskName = Console.ReadLine();
            project.AddStory(new Story(taskName, DateTime.Now, "Открытая задача"));
            Console.WriteLine("Вы создали задачу " + taskName + " типа Story\n");
            StoryMenu(project);
        }
        /// <summary>
        /// Метод удаляет задачу типа Story из соответствующего списка.
        /// </summary>
        /// <param name="project"></param>
        public static void DeleteStory(Project project)
        {
            if ((project.StoryList == null) || project.StoryList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи в этом проекте");
            else if (project.StoryList.Count == 1)
            {
                Console.WriteLine("Story \"{0}\" помер", project.StoryList[0].Name);
                project.StoryList.Remove(project.StoryList[0]);
            }
            else
            {
                ClearConsole();
                foreach (var a in project.StoryList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи, который вы хотите удалить\n");
                string epicName = Console.ReadLine();
                bool flag = false;
                for (int i = 0; i < project.StoryList.Count; i++)
                {
                    if (project.StoryList[i].Name == epicName)
                    {
                        flag = true;
                        project.StoryList.Remove(project.StoryList[i]);
                        i += project.StoryList.Count;
                    }

                }
                if (flag)
                    Console.WriteLine("Задача " + epicName + " успешно удалена");
                else
                {
                    Console.WriteLine("Задачи с именем " + epicName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            StoryMenu(project);
        }
        /// <summary>
        /// Метод для работы с пользователями задачи типа Story.
        /// </summary>
        /// <param name="story"></param>
        public static void StoryUsers(Story story)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление пользователя к Story");
                Console.WriteLine("2. Удаление пользователя из Story");
                Console.WriteLine("3. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                {
                    User user = WhichUser();
                    if (user!=null)
                        story.AddUser(user);
                }
                else if (ans == "2")
                    story.DeleteUser();
                else if (ans == "3")
                    Menu();
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
            Menu();
        }
        /// <summary>
        /// Метод осуществляет  выбор пользователя.
        /// </summary>
        /// <returns>Метод возвращает выбранного пользователем исполнителя  и null, если выбрать пользователя не получилось.</returns>
        public static User WhichUser()
        {
            if ((Users == null) || Users.Count == 0)
                Console.WriteLine("У вас нет ни одного пользователя\n");
            else if (Users.Count == 1)
                return Users[0];
            else
            {
                ClearConsole();
                foreach (User a in Users)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите имя исполнителя\n");
                string userName = Console.ReadLine();
                bool flag = false;
                User user = new User("d");
                for (int i = 0; i < Users.Count; i++)
                    if (Users[i].Name == userName)
                    {
                        flag = true;
                        user = Users[i];
                        i += Users.Count;
                    }
                if (flag)
                    return user;
                else
                {
                    Console.WriteLine("Пользователя  с именем " + userName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                }
            }
            return null;
        }
        /// <summary>
        /// Метод-меню для работы с задачами типа Task.
        /// </summary>
        /// <param name="project"></param>
        public static void TaskMenu(Project project)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление задачи Task\n2. Удаление задачи Task\n3. Назначение исполнителей");
                Console.WriteLine("4. Установка статуса\n5. Просмотреть список задач\n\n6. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    CreateTask(project);
                else if (ans == "2")
                    DeleteTask(project);
                else if (ans == "3"){
                    Task task = WhichTask(project);
                    if (task != null)
                        TaskUsers(task);
                }
                else if (ans == "4"){
                    Task task = WhichTask(project);
                    if (task != null)
                        task.Status = Status();
                }
                else if (ans == "5")
                    CheckAllTasks(project);
                else if (ans == "6")
                    WhichTipeMenu(project);
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод осуществляет выбор задачи типа Task.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Метод возвращает задачу типа Task и null, если выбрать задачу не получилось.</returns>
        public static Task WhichTask(Project project)
        {
            if ((project.TasksList == null) || project.TasksList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи типа Task\n");
            else if (project.TasksList.Count == 1)
                return project.TasksList[0];
            else
            {
                ClearConsole();
                foreach (Task a in project.TasksList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи типа Story в которой вы хотите продолжить работу\n");
                string storyName = Console.ReadLine();
                bool flag = false;
                Task task = new Task("d", DateTime.Now, "s");
                for (int i = 0; i < project.TasksList.Count; i++)
                    if (project.TasksList[i].Name == storyName)
                    {
                        flag = true;
                        task = project.TasksList[i];
                        i += project.TasksList.Count;
                    }
                if (flag)
                    return task;
                else
                {
                    Console.WriteLine("Задачи типа Story  с именем " + storyName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                }
            }
            return null;
        }
        /// <summary>
        /// Метод осуществляет выбор задачи типа Task.
        /// </summary>
        /// <param name="story"></param>
        /// <returns>Метод возвращает задачу типа Task и null, если выбрать задачу не получилось.</returns>
        public static Task WhichTask(Story story)
        {
            if ((story.TasksList == null) || story.TasksList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи типа Task\n");
            else if (story.TasksList.Count == 1)
                return story.TasksList[0];
            else
            {
                ClearConsole();
                foreach (Task a in story.TasksList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи типа Story в которой вы хотите продолжить работу\n");
                string storyName = Console.ReadLine();
                bool flag = false;
                Task task = new Task("d", DateTime.Now, "s");
                for (int i = 0; i < story.TasksList.Count; i++)
                    if (story.TasksList[i].Name == storyName)
                    {
                        flag = true;
                        task = story.TasksList[i];
                        i += story.TasksList.Count;
                    }
                if (flag)
                    return task;
                else
                {
                    Console.WriteLine("Задачи типа Story  с именем " + storyName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                }
            }
            return null;
        }
        /// <summary>
        /// Метод осуществляет работу с исполнителями задачи типа Task.
        /// </summary>
        /// <param name="task"></param>
        public static void TaskUsers(Task task)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление пользователя к Task");
                Console.WriteLine("2. Удаление пользователя из Task");
                Console.WriteLine("3. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                {
                    User user = WhichUser();
                    if (user != null)
                        task.AddUser(user);
                }
                else if (ans == "2")
                {
                    task.DeleteUser();
                }
                else if (ans == "3")
                    Menu();
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
            Menu();
        }
        /// <summary>
        /// Метод создает новую задачу типа Task и добавляет ее в соответствующий список.
        /// </summary>
        /// <param name="project"></param>
        public static void CreateTask(Project project)
        {
            Console.WriteLine("Введите название задачи");
            string taskName = Console.ReadLine();
            project.AddTask(new Task(taskName, DateTime.Now, "Открытая задача"));
            Console.WriteLine("Вы создали задачу " + taskName + " типа Task\n");
            WhichTipeMenu(project);
        }
        /// <summary>
        /// Метод удаляет задачу типа Task из соответствующего списка.
        /// </summary>
        /// <param name="project"></param>
        public static void DeleteTask(Project project)
        {
            if ((project.TasksList == null) || project.TasksList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи в этом проекте");
            else if (project.TasksList.Count == 1)
            {
                Console.WriteLine("Task \"{0}\" помер", project.TasksList[0].Name);
                project.TasksList.Remove(project.TasksList[0]);
            }
            else
            {
                ClearConsole();
                foreach (var a in project.TasksList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи, который вы хотите удалить\n");
                string taskName = Console.ReadLine();
                bool flag = false;
                // Проверяем наличие такой задачи в списке.
                for (int i = 0; i < project.TasksList.Count; i++)
                {
                    if (project.TasksList[i].Name == taskName)
                    {
                        flag = true;
                        project.TasksList.Remove(project.TasksList[i]);
                        i += project.TasksList.Count;
                    }

                }
                if (flag)
                    Console.WriteLine("Задача " + taskName + " успешно удалена");
                else
                {
                    Console.WriteLine("Задачи с именем " + taskName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            WhichTipeMenu(project);
        }
        /// <summary>
        /// Метод-меню для работы с задачами типа Bug.
        /// </summary>
        /// <param name="project"></param>
        public static void BugMenu(Project project)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление ошибки Bug\n2. Удаление ошибки Bug\n3. Назначение исполнителей");
                Console.WriteLine("4. Установка статуса\n5. Просмотреть список задач\n\n");
                Console.WriteLine("6. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    CreateBug(project);
                else if (ans == "2")
                    DeleteBug(project);
                else if (ans == "3")
                {
                    Bug bug = WhichBug(project);
                    if (bug != null)
                        BugUsers(bug);
                }
                else if (ans == "4")
                {
                    Bug bug = WhichBug(project);
                    if (bug != null)
                        bug.Status = Status();
                }
                else if (ans == "5")
                    CheckAllTasks(project);
                else if (ans == "6")
                    WhichTipeMenu(project);
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод для работы с исполнителями задачи типа Bug.
        /// </summary>
        /// <param name="bug"></param>
        public static void BugUsers(Bug bug)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление пользователя к Bug");
                Console.WriteLine("2. Удаление пользователя из Bug");
                Console.WriteLine("3. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                {
                    User user = WhichUser();
                    if (user != null)
                        bug.AddUser(user);
                }
                else if (ans == "2")
                {
                    bug.DeleteUser();
                }
                else if (ans == "3")
                    return;
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод осуществляет выбор задачи типа Bug.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Метод возвращает задачу типа Bug и null, если выбрать задачу не получилось.</returns>
        public static Bug WhichBug(Project project)
        {
            if ((project.BugList == null) || project.BugList.Count == 0)
                Console.WriteLine("У вас нет ни одного бага\n");
            else if (project.BugList.Count == 1)
                return project.BugList[0];
            else
            {
                ClearConsole();
                foreach (Bug a in project.BugList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название бага в которой вы хотите продолжить работу\n");
                string bugName = Console.ReadLine();
                bool flag = false;
                Bug task = new Bug("d", DateTime.Now, "s");
                /// Проверяем на наличие введенное имя задачи.
                for (int i = 0; i < project.BugList.Count; i++)
                    if (project.BugList[i].Name == bugName)
                    {
                        flag = true;
                        task = project.BugList[i];
                        i += project.BugList.Count;
                    }
                if (flag)
                    return task;
                else
                {
                    Console.WriteLine("Бага  с именем " + bugName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                }
            }
            return null;
        }
        /// <summary>
        /// Метод создает новую задачу типа Bug и добавляет ее в соответствующий список.
        /// </summary>
        /// <param name="project"></param>
        public static void CreateBug(Project project)
        {
            Console.WriteLine("Введите название задачи");
            string bugName = Console.ReadLine();
            project.AddBug(new Bug(bugName, DateTime.Now, "Открытая задача"));
            Console.WriteLine("Вы создали Bug " + bugName + "\n");
            Menu();
        }
        /// <summary>
        /// Метод удаляет задачу типа Bug из соответствующего списка.
        /// </summary>
        /// <param name="project"></param>
        public static void DeleteBug(Project project)
        {
            if ((project.BugList == null) || project.BugList.Count == 0)
                Console.WriteLine("У вас нет ни одной ошибки в этом проекте");
            else if (project.BugList.Count == 1)
                project.BugList.Remove(project.BugList[0]);
            else
            {
                ClearConsole();
                foreach (var a in project.BugList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи, который вы хотите удалить\n");
                string bugName = Console.ReadLine();
                bool flag = false;
                // Проверяем введенное имя на правильность.
                for (int i = 0; i < project.BugList.Count; i++)
                {
                    if (project.BugList[i].Name == bugName)
                    {
                        flag = true;
                        project.BugList.Remove(project.BugList[i]);
                        i += project.BugList.Count;
                    }

                }
                if (flag)
                    Console.WriteLine("Ошибка " + bugName + " успешно удалена");
                else
                {
                    Console.WriteLine("Ошибки с именем " + bugName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            Menu();
        }
        /// <summary>
        /// Метод-меню для работы с задачами типа Story, наследующихся от Epic.
        /// </summary>
        /// <param name="epic"></param>
        public static void WorkWithStoris(Epic epic)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление задачи Story\n2. Удаление задачи Story\n3. Работа с подзадачами задачи Story");
                Console.WriteLine("4. Назначение исполнителей\n5. Установка статуса\n\n6. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    CreateStory(epic);
                else if (ans == "2")
                    DeleteStory(epic);
                else if (ans == "4")
                {
                    Story story = WhichStory(epic);
                    if (story != null)
                        StoryUsers(story);
                }
                else if (ans == "3")
                {
                    Story story = WhichStory(epic);
                    if (story != null)
                        WorkWithTasks(story);
                }
                else if (ans == "5")
                {
                    Story story = WhichStory(epic);
                    if (story != null)
                        story.Status = Status();
                }
                else if (ans == "6")
                    return;
                else{
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод создает новую задачу типа Story и добавляет ее в соответствующий список.
        /// </summary>
        /// <param name="epic"></param>
        public static void CreateStory(Epic epic)
        {
            Console.WriteLine("Введите название задачи");
            string taskName = Console.ReadLine();
            epic.AddStory(new Story(taskName, DateTime.Now, "Открытая задача"));
            Console.WriteLine("Вы создали задачу " + taskName + " типа Story\n");
            Menu();
        }
        /// <summary>
        /// Метод удаляет задачу типа Story из соответсвующего списка.
        /// </summary>
        /// <param name="epic"></param>
        public static void DeleteStory(Epic epic)
        {
            if ((epic.StoryList == null) || epic.StoryList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи в этом проекте");
            else if (epic.StoryList.Count == 1)
                epic.StoryList.Remove(epic.StoryList[0]);
            else
            {
                ClearConsole();
                foreach (var a in epic.StoryList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи, который вы хотите удалить\n");
                string epicName = Console.ReadLine();
                bool flag = false;
                // Проверяем введенное имя на корректность.
                for (int i = 0; i < epic.StoryList.Count; i++)
                {
                    if (epic.StoryList[i].Name == epicName)
                    {
                        flag = true;
                        epic.StoryList.Remove(epic.StoryList[i]);
                        i += epic.StoryList.Count;
                    }

                }
                if (flag)
                    Console.WriteLine("Задача " + epicName + " успешно удалена");
                else
                {
                    Console.WriteLine("Задачи с именем " + epicName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            Menu();
        }
        /// <summary>
        /// Метод осуществляет вобор пользователем определенной задачи типа Story.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Метод возвращает выбранную задачу типа Story и null, если выбрать задачу не получилось.</returns>
        public static Story WhichStory(Project project)
        {
            if ((project.StoryList == null) || project.StoryList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи типа Story\n");
            else if (project.StoryList.Count == 1)
                return project.StoryList[0];
            else
            {
                ClearConsole();
                foreach (Story a in project.StoryList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи типа Story в которой вы хотите продолжить работу\n");
                string storyName = Console.ReadLine();
                bool flag = false;
                Story story = new Story("d", DateTime.Now, "s");
                for (int i = 0; i < project.StoryList.Count; i++)
                    if (project.StoryList[i].Name == storyName)
                    {
                        flag = true;
                        story = project.StoryList[i];
                        i += project.StoryList.Count;
                    }
                if (flag)
                    return story;
                else
                {
                    Console.WriteLine("Задачи типа Story  с именем " + storyName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                }
            }
            return null;
        }
        /// <summary>
        /// Метод осуществляет вобор пользователем определенной задачи типа Story.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Метод возвращает выбранную задачу типа Story и null, если выбрать задачу не получилось.</returns>
        public static Story WhichStory(Epic epic)
        {
            if ((epic.StoryList == null) || epic.StoryList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи типа Story\n");
            else if (epic.StoryList.Count == 1)
                return epic.StoryList[0];
            else
            {
                ClearConsole();
                foreach (Story a in epic.StoryList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи типа Story в которой вы хотите продолжить работу\n");
                string storyName = Console.ReadLine();
                bool flag = false;
                Story story = new Story("d", DateTime.Now, "s");
                for (int i = 0; i < epic.StoryList.Count; i++)
                    if (epic.StoryList[i].Name == storyName)
                    {
                        flag = true;
                        story = epic.StoryList[i];
                        i += epic.StoryList.Count;
                    }
                if (flag)
                    return story;
                else
                {
                    Console.WriteLine("Задачи типа Story  с именем " + storyName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                }
            }
            return null;
        }
        /// <summary>
        /// Метод осуществляет работу с задачами типа Task, наследуемых от задачи типа Story.
        /// </summary>
        /// <param name="story"></param>
        public static void WorkWithTasks(Story story)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Добавление задачи Task");
                Console.WriteLine("2. Удаление задачи Task");
                Console.WriteLine("3. Назначение исполнителей");
                Console.WriteLine("4. Установка статуса\n\n");
                Console.WriteLine("5. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    CreateTask(story);
                else if (ans == "2")
                    DeleteTask(story);
                else if (ans == "3")
                {
                    Task task = WhichTask(story);
                    if (task != null)
                        TaskUsers(task);
                }
                else if (ans == "4")
                {
                    Task task = WhichTask(story);
                    if (task != null)
                        task.Status = Status();
                }
                else if (ans == "5")
                    return;
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// С помощью этого метода пользователь создает новое задание типа Task.
        /// </summary>
        /// <param name="story"></param>
        public static void CreateTask(Story story)
        {
            Console.WriteLine("Введите название задачи");
            string taskName = Console.ReadLine();
            story.AddTask(new Task(taskName, DateTime.Now, "Открытая задача"));
            Console.WriteLine("Вы создали задачу " + taskName + " типа Task\n");
            Menu();
        }
        /// <summary>
        /// С помощью этого метода пользователь удаляет задание типа Task.
        /// </summary>
        /// <param name="story"></param>
        public static void DeleteTask(Story story)
        {
            if ((story.TasksList == null) || story.TasksList.Count == 0)
                Console.WriteLine("У вас нет ни одной задачи в этом проекте");
            else if (story.TasksList.Count == 1)
                story.TasksList.Remove(story.TasksList[0]);
            else
            {
                ClearConsole();
                foreach (var a in story.TasksList)
                    Console.WriteLine(a.Name);
                Console.WriteLine("\nВведите название задачи, который вы хотите удалить\n");
                string taskName = Console.ReadLine();
                bool flag = false;
                // Проверка введенных данных на корректность.
                for (int i = 0; i < story.TasksList.Count; i++)
                {
                    if (story.TasksList[i].Name == taskName)
                    {
                        flag = true;
                        story.TasksList.Remove(story.TasksList[i]);
                        i += story.TasksList.Count;
                    }

                }
                if (flag)
                    Console.WriteLine("Задача " + taskName + " успешно удалена");
                else
                {
                    Console.WriteLine("Задачи с именем " + taskName + " не существует, нажмите любую клавишу, чтобы смириться");
                    Console.ReadKey();
                    ClearConsole();
                }
            }
            Menu();
        }
        /// <summary>
        /// С помощью этого метода определяется статус для задач проекта.
        /// </summary>
        /// <returns>Строку, содержащую статус задачи.</returns>
        public static string Status()
        {
            Console.WriteLine("Обозначьте статус задачи");
            string status = "";
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("Введите номер операции из списка\n");
                Console.WriteLine("1. Открытая задача");
                Console.WriteLine("2. Задача в работе");
                Console.WriteLine("3. Завершенная задача\n\n");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    status = "Открытая задача";
                else if (ans == "2")
                    status = "Задача в работе";
                else if (ans == "3")
                    status = "Завершенная задача";
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
            return status;
        }
        /// <summary>
        /// Метод для выбора типа группировки задач по статусу.
        /// </summary>
        /// <param name="project"></param>
        public static void GroupByStatus(Project project)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.WriteLine("\nВведите номер операции из списка\n");
                Console.WriteLine("1. Открытые задачи");
                Console.WriteLine("2. Задачи к работе");
                Console.WriteLine("3. Завершенные задачи\n\n");
                Console.WriteLine("4. Назад");
                string ans = Console.ReadLine();
                ClearConsole();
                if (ans == "1")
                    EpicsStatus(project, "Открытая задача");
                else if (ans == "2")
                    EpicsStatus(project, "Задача в работе");
                else if (ans == "3")
                    EpicsStatus(project, "Завершенная задача");
                else if (ans == "4")
                    return;
                else
                {
                    Console.WriteLine("Не балуйтесь и введите что-то адекватное");
                    flag = true;
                }
            }
        }
        /// <summary>
        /// Метод, выводящий в консоль задачи типа Epic(и его подзадачи) с определенным статусом.
        /// </summary>
        /// <param name="project">Проект, в котором ведется работа.</param>
        /// <param name="status">Статус задачи.</param>
        public static void EpicsStatus(Project project, string status)
        {
            if (project.EpicList != null)
                foreach (Epic e in project.EpicList)
                {
                    if (e.Status == status)
                        Console.WriteLine("- - Epic:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", e.Name, e.Status, e.Date);
                    if (e.StoryList != null)
                        foreach (Story s in e.StoryList)
                        {
                            if (s.Status == status)
                            {
                                Console.WriteLine("- -Story:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", s.Name, s.Status, s.Date);
                                if (s.Users != null)
                                {
                                    Console.WriteLine("\t\tИсполнители:");
                                    foreach (User u in s.Users)
                                        Console.WriteLine("\t\t{0}", u.Name);
                                }
                            }
                            if (s.TasksList != null)
                                foreach (Task t in s.TasksList)
                                    if (t.Status == status)
                                    {
                                        Console.WriteLine("\t- -Task:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", t.Name, t.Status, t.Date);
                                        if (t.Users != null)
                                            Console.WriteLine("\t\tИсполнитель:\n\t\t{0}", t.Users[0].Name);
                                    }
                        }
                }
            TasksStatus(project, status);
        }
        /// <summary>
        /// Метод, выводящий в консоль все задачи проекта, кроме Epic(и его подзадачи) с определенным статусом.
        /// </summary>
        /// <param name="project">Проект, в котором ведется работа.</param>
        /// <param name="status">Статус задачи.</param>
        public static void TasksStatus(Project project, string status)
        {
            if (project.StoryList != null)
                foreach (Story s in project.StoryList)
                {
                    if (s.Status== status){
                        Console.WriteLine("- -Story:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", s.Name, s.Status, s.Date);
                        if (s.Users != null){
                            Console.WriteLine("\t\tИсполнители:");
                            foreach (User u in s.Users)
                                Console.WriteLine("\t\t{0}", u.Name);
                        }
                    }
                    if (s.TasksList != null)
                        foreach (Task t in s.TasksList)
                            if (t.Status == status) {
                                Console.WriteLine("\t- -Task:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", t.Name, t.Status, t.Date);
                                if (t.Users != null)
                                    Console.WriteLine("\t\tИсполнитель:\n\t\t{0}", t.Users[0].Name);
                            }
                }
            if (project.TasksList != null)
                foreach (Task t in project.TasksList)
                    if (t.Status == status) {
                        Console.WriteLine("\t\t- -Task:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", t.Name, t.Status, t.Date);
                        if (t.Users != null)
                            Console.WriteLine("\t\tИсполнитель:\n\t\t{0}", t.Users[0].Name);
                    }
            if (project.BugList != null)
                foreach (Bug b in project.BugList)
                    if (b.Status == status)
                    {
                        Console.WriteLine("~ ~ Bug:   Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", b.Name, b.Status, b.Date);
                        if (b.Users != null)
                            Console.WriteLine("\t\tИсполнитель:\n\t\t{0}", b.Users[0].Name);
                    }
            Menu();
        }
        /// <summary>
        /// Метод, выводящий в консоль задачи типа Epic(и его подзадачи).
        /// </summary>
        /// <param name="project">Проект, в котором ведется работа.</param>
        public static void PrintEpics(Project project)
        {
            if (project.EpicList != null && project.EpicList.Count > 0)
                foreach (Epic e in project.EpicList)
                {
                    Console.WriteLine("- - Epic:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", e.Name, e.Status, e.Date);
                    if (e.StoryList != null && e.StoryList.Count > 0)
                        foreach (Story s in e.StoryList)
                        {
                            Console.WriteLine("\t- - Story:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", s.Name, s.Status, s.Date);
                            if (s.Users != null && s.Users.Count > 0)
                            {
                                Console.WriteLine("\t\tИсполнители:");
                                foreach (User u in s.Users)
                                    if (Users.Contains(u))
                                        Console.WriteLine("\t\t{0}",u.Name);
                            }  
                            if (s.TasksList != null && s.TasksList.Count > 0)
                                foreach (Task t in s.TasksList)
                                {
                                    Console.WriteLine("\t\t- - Task:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", t.Name, t.Status, t.Date);
                                    if (t.Users != null && t.Users.Count > 0)
                                        if (Users.Contains(t.Users[0]))
                                            Console.WriteLine("\t\tИсполнитель:\n\t\t{0}", t.Users[0].Name);
                                }
                        }
                }
        }
        /// <summary>
        /// Метод, выводящий в консоль все задачи проекта, кроме Epic(и его подзадачи).
        /// </summary>
        /// <param name="project">Проект, в котором ведется работа.</param>
        public static void PrintAllExeptEpics(Project project)
        {
            if (project.StoryList != null && project.StoryList.Count > 0)
                foreach (Story s in project.StoryList)
                {
                    Console.WriteLine("- - Story:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", s.Name, s.Status, s.Date);
                    if (s.Users != null && s.Users.Count > 0)
                    {
                        Console.WriteLine("\t\tИсполнители:");
                        foreach (User u in s.Users)
                            if (Users.Contains(u))
                                Console.WriteLine("\t\t{0}", u.Name);
                    }
                    if (s.TasksList != null && s.TasksList.Count > 0)
                        foreach (Task t in s.TasksList)
                        {
                            Console.WriteLine("\t- - Task:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", t.Name, t.Status, t.Date);
                            if (t.Users != null && t.Users.Count > 0)
                                if (Users.Contains(t.Users[0]))
                                    Console.WriteLine("\t\tИсполнитель:\n\t\t{0}", t.Users[0].Name);
                        }
                }
            if (project.TasksList != null && project.TasksList.Count > 0)
                foreach (Task t in project.TasksList)
                {
                    Console.WriteLine("- - Task:    Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", t.Name, t.Status, t.Date);
                    if (t.Users != null&&t.Users.Count>0)
                        if (Users.Contains(t.Users[0]))
                            Console.WriteLine("\t\tИсполнитель:\n\t\t{0}", t.Users[0].Name);
                }
            if (project.BugList != null && project.BugList.Count > 0)
                foreach (Bug b in project.BugList)
                {
                    Console.WriteLine("~ ~ Bug:   Имя: \"{0}\", Статус: \"{1}\"\n\t\tДата создания: \"{2}\"", b.Name, b.Status, b.Date);
                    if (b.Users != null && b.Users.Count > 0)
                        if (Users.Contains(b.Users[0]))
                            Console.WriteLine("\t\tИсполнитель:\n\t\t{0}", b.Users[0].Name);
                }
            Console.WriteLine("");
        }
        /// <summary>
        /// Метод для сериализации данных при закрытии прриложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Serialization(object sender, EventArgs e)
        {
            // Бинарный форматор для сериализации.
            var binFormatter = new BinaryFormatter();
            // Создаем или открываем файл для сериализации и сохраняем туда информацию о списке пользователей.
            using (FileStream fs = new FileStream("users.txt", FileMode.OpenOrCreate))
            {
                // Пустой трайкэтч для первого запуска программы, потому что выдается ошибка, но пользователю о ней знать незачем.
                try
                {
                    binFormatter.Serialize(fs, Users);
                }
                catch
                {

                }
            }
            // Создаем или открываем файл для сериализации и сохраняем туда информацию о списке проектов.
            using (FileStream fs = new FileStream("projects.txt", FileMode.OpenOrCreate))
            {
                // Пустой трайкэтч для первого запуска программы, потому что выдается ошибка, но пользователю о ней знать незачем.
                try
                {
                    binFormatter.Serialize(fs, Projects);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Метод для десериализации данных при открытии прриложения.
        /// </summary>
        public static void Deserialization()
        {
            // Бинарный форматор для десериализации.
            var binFormatter = new BinaryFormatter();
            // Если файл существует, то пытаемся провести десериализацию списка пользователей.
            if (File.Exists("users.txt"))
            {
                // Поток для считывания файла.
                using (FileStream fs = new FileStream("users.txt", FileMode.OpenOrCreate))
                {
                    // Пустой трайкэтч для первого запуска программы, потому что выдается ошибка, но пользователю о ней знать незачем.
                    try
                    {
                        Users = binFormatter.Deserialize(fs) as List<User>;
                    }
                    catch
                    {

                    }
                }
            }
            // Если файл существует, то пытаемся провести десериализацию списка проектов.
            if (File.Exists("projects.txt"))
            {
                // Поток для считывания файла.
                using (FileStream fs = new FileStream("projects.txt", FileMode.OpenOrCreate))
                {
                    // Пустой трайкэтч для первого запуска программы, потому что выдается ошибка, но пользователю о ней знать незачем.
                    try
                    {
                        Projects = binFormatter.Deserialize(fs) as List<Project>;
                    }
                    catch
                    {

                    }
                }
            }
        }
        /// <summary>
        /// Метод на всякий случай, Console.Clear() опасная вещь.
        /// </summary>
        public static void ClearConsole()
        {
            try
            {
                Console.Clear();
            }
            catch
            {
                Console.WriteLine("У вас такая грязная консоль, что она не чистится");
                return;
            }
        }
        /// <summary>
        /// Здороваемся с пользователем.
        /// </summary>
        public static void Hello()
        {
            Console.WriteLine("  П Р И В Е Т С Т В У Ю !\n  Всем привет дарова\n");
            Console.WriteLine("  Это моя программа(лично), надеюсь вы(лично) останетесь довольны.\n\n");
        }
    }
}
