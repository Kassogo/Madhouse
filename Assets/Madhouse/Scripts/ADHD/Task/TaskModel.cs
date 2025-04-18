using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Модель задач.
    /// </summary>
    [Serializable]
    public class TaskModel
    {
        [SerializeField] private Task _firstTask;
        [SerializeField] private Task _secondTask;
        [SerializeField] private Task _wrongTask;

        /// <summary>
        /// Первое задание.
        /// </summary>
        public Task FirstTask => _firstTask;

        /// <summary>
        /// Второе задание.
        /// </summary>
        public Task SecondTask => _secondTask;

        /// <summary>
        /// Задание, которое нужно игнорировать.
        /// </summary>
        public Task WrongTask => _wrongTask;
    }
}
