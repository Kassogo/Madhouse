using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контроллер системы заданий.
    /// </summary>
    public class TaskController : MonoBehaviour
    {
        [SerializeField] private ShapesController _spawner;
        [SerializeField] private TasksData _tasksData;
        [SerializeField] private TaskView _taskView;
        [SerializeField] private float _minTimeChangeTask = 8;
        [SerializeField] private float _maxTimeChangeTask = 15;
        [Space]
        [SerializeField] private float _minTimeSpecialTask = 20;
        [SerializeField] private float _maxTimeSpecialTask = 30;

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
            StartCoroutine(ChangeTask());

            _correctSpecialTypes = new();
            for (int i = 0; i < System.Enum.GetValues(typeof(SpecialShapeTypes)).Length; i += 2)
                _correctSpecialTypes.Add(i);
            StartCoroutine(StartSpecialTask());
            _spawner.OnDestroySpecialObject += SpecialShapeInteract;
        }

        private void OnDestroy()
        {
            _spawner.OnDestroySpecialObject -= SpecialShapeInteract;
        }

        private IEnumerator ChangeTask()
        {
            if (_indexChooseTask > _tasksData.Tasks.Count / 2)
                _indexChooseTask = Random.Range(0, _tasksData.Tasks.Count / 2);
            else
                _indexChooseTask = Random.Range(_tasksData.Tasks.Count / 2, _tasksData.Tasks.Count);
            _taskView.ShowTasks(TaskChoosen);
            yield return new WaitForSeconds(Random.Range(_minTimeChangeTask, _maxTimeChangeTask));
            StartCoroutine(ChangeTask());
        }

        private IEnumerator StartSpecialTask()
        {
            yield return new WaitForSeconds(Random.Range(_minTimeSpecialTask, _maxTimeSpecialTask));

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
