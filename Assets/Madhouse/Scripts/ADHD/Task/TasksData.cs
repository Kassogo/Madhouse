using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контейнер задач.
    /// </summary>
    [CreateAssetMenu(fileName = "TasksData", menuName = "Madhouse/ADHD/TasksData")]
    public class TasksData : ScriptableObject
    {
        [SerializeField] private List<TaskModel> _tasks;

        /// <summary>
        /// Задачи.
        /// </summary>
        public List<TaskModel> Tasks => _tasks;
    }
}
