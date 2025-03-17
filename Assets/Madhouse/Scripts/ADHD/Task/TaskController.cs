using System.Collections;
using UnityEngine;

namespace Madhouse.ADHD
{
    public class TaskController : MonoBehaviour
    {
        public TaskModel TaskChoosen => _tasksData.Tasks[_indexChooseTask];

        [SerializeField] private TasksData _tasksData;
        [SerializeField] private TaskView _taskView;
        [SerializeField] private float _minTimeChangeTask = 8;
        [SerializeField] private float _maxTimeChangeTask = 15;

        [SerializeField] private int _indexChooseTask;

        private void Awake()
        {
            _indexChooseTask = Random.Range(0, _tasksData.Tasks.Count);
            _taskView.ShowTasks(TaskChoosen);
            StartCoroutine(ChangeTask());
        }

        private IEnumerator ChangeTask()
        {
            if(_indexChooseTask > _tasksData.Tasks.Count / 2)
                _indexChooseTask = Random.Range(0, _tasksData.Tasks.Count / 2);
            else
                _indexChooseTask = Random.Range(_tasksData.Tasks.Count / 2, _tasksData.Tasks.Count);
            _taskView.ShowTasks(TaskChoosen);
            yield return new WaitForSeconds(Random.Range(_minTimeChangeTask, _maxTimeChangeTask));
            StartCoroutine(ChangeTask());
        }
    }
}
