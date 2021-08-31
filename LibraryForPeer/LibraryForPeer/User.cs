using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForPeer
{
    // Отмечаем класс для сериализации.
    [Serializable]
    public class User
    {
        public string Name { get; set; }
        public User(string name)
        {
            Name = name;
        }
    }
}
