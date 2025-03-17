using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    [Serializable]
    public class TaskModel
    {
        public Task FirstTask => _firstTask;
        public Task SecondTask => _secondTask;
        public Task WrongTask => _wrongTask;

        [SerializeField] private Task _firstTask;
        [SerializeField] private Task _secondTask;
        [SerializeField] private Task _wrongTask;
    }
}
