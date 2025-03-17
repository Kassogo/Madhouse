using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    [CreateAssetMenu(fileName = "TasksData", menuName = "Madhouse/TasksData")]
    public class TasksData : ScriptableObject
    {
        public List<TaskModel> Tasks => _tasks;

        [SerializeField] private List<TaskModel> _tasks;
    }
}
