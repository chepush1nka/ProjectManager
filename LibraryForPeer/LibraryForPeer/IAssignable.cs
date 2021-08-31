using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryForPeer
{
    interface IAssignable
    {
        /// <summary>
        /// Здесь храним максимальное значение исполнителей.
        /// </summary>
        int MaxExecutor { get; set; }
        /// <summary>
        /// Счетчик исполнителей задачи типа Task.
        /// </summary>
        public static int TaskExecutor { get; set; }
        /// <summary>
        /// Счетчик исполнителей задачи типа Story.
        /// </summary>
        public static int StoryExecutor { get; set ; }
        /// <summary>
        /// Счетчик исполнителей задачи типа Bug.
        /// </summary>
        public static int BugExecutor { get; set; }
    }
}
