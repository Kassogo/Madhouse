using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контроллер системы заданий.
    /// </summary>
    public class TaskController : MonoBehaviour
    {
        public event Action OnChangeTask = delegate { };

        public float TimeToNextTask => _timeToNextTask;

        [SerializeField] private ShapesDispatcher _spawner;
        [SerializeField] private TasksData _tasksData;
        [SerializeField] private TaskView _taskView;
        [Space]
        [SerializeField] private TaskSetting _setting;

        private float _timeToNextTask;
        private int _indexChooseTask;
        private List<int> _correctSpecialTypes;
        private SpecialShapeTypes _chooseSpecialShape;
        private InteractionEndTypes _chooseInteractionFoeSpecialShape;

        /// <summary>
        /// Актуальное задание.
        /// </summary>
        public TaskModel TaskChoosen => _tasksData.Tasks[_indexChooseTask];

        /// <summary>
        /// Выбранный тип специальной фигуры.
        /// </summary>
        public SpecialShapeTypes ChooseSpecialShape => _chooseSpecialShape;

        /// <summary>
        /// Выбранный тип взаимодействия со специальной фигурой.
        /// </summary>
        public InteractionEndTypes ChooseInteractionForSpecialShape => _chooseInteractionFoeSpecialShape;

        private void Awake()
        {
            _indexChooseTask = Random.Range(0, _tasksData.Tasks.Count);
            _taskView.ShowTasks(TaskChoosen);
            ChangeTask();

            _correctSpecialTypes = new();
            for (int i = 0; i < System.Enum.GetValues(typeof(SpecialShapeTypes)).Length; i += 2)
                _correctSpecialTypes.Add(i);
            StartCoroutine(StartSpecialTask());
            _spawner.OnDestroySpecialObject += SpecialShapeInteract;
        }

        private void Update()
        {
            if (_timeToNextTask <= 0)
                ChangeTask();
            _timeToNextTask -= Time.deltaTime;
        }

        private void OnDestroy()
        {
            _spawner.OnDestroySpecialObject -= SpecialShapeInteract;
        }

        private void ChangeTask()
        {
            if (_indexChooseTask > _tasksData.Tasks.Count / 2)
                _indexChooseTask = Random.Range(0, _tasksData.Tasks.Count / 2);
            else
                _indexChooseTask = Random.Range(_tasksData.Tasks.Count / 2, _tasksData.Tasks.Count);
            _taskView.ShowTasks(TaskChoosen);

            OnChangeTask.Invoke();

            _timeToNextTask = Random.Range(_setting.MinTimeChangeTask, _setting.MaxTimeChangeTask);
        }

        private IEnumerator StartSpecialTask()
        {
            yield return new WaitForSeconds(Random.Range(_setting.MinTimeChangeSpecialTask, _setting.MaxTimeChangeSpecialTask));

            _chooseInteractionFoeSpecialShape = (InteractionEndTypes)Random.Range(0, System.Enum.GetValues(typeof(InteractionEndTypes)).Length - 1);
            _chooseSpecialShape = (SpecialShapeTypes)_correctSpecialTypes[Random.Range(0, _correctSpecialTypes.Count)];

            _taskView.ShowSpecialTask(_chooseInteractionFoeSpecialShape, _chooseSpecialShape);
            _spawner.CreateSpecialShape(_chooseSpecialShape);

            StartCoroutine(StartSpecialTask());
        }

        private void SpecialShapeInteract(SpecialShapeTypes specialShape, InteractionEndTypes interactionEnd)
        {
            _taskView.RemoveSpecialTask();
        }
    }
}
