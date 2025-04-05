using System.Collections;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Класс включающий эффекты после взаимодействия с специальными объектами.
    /// </summary>
    public class SpecialEffectsActivator : MonoBehaviour
    {
        [SerializeField] private ShapesDispatcher _spawner;
        [SerializeField] private TaskController _taskController;
        [SerializeField] private ScoreController _scoreController;
        [Space]
        [SerializeField] private int _scoreChange = 5;
        [SerializeField] private float _timeEffect = 6;
        [SerializeField] private float _timeCoefficientChange = 0.2f;

        private WaitForSeconds _waitForChangeTime;

        private void Awake()
        {
            _spawner.OnDestroySpecialObject += CheckDestroySpecialShape;
            _waitForChangeTime = new WaitForSeconds(_timeEffect);
        }

        private void OnDestroy()
        {
            _spawner.OnDestroySpecialObject -= CheckDestroySpecialShape;
            Time.timeScale = 1;
        }

        private void CheckDestroySpecialShape(SpecialShapeTypes specialShapeType, InteractionEndTypes interactionEnd)
        {
            if (_taskController.ChooseSpecialShape == specialShapeType && _taskController.ChooseInteractionForSpecialShape == interactionEnd)
                ActivateEffect(specialShapeType);
            else if (_taskController.ChooseSpecialShape + 1 == specialShapeType)
                ActivateEffect(specialShapeType);
        }

        private void ActivateEffect(SpecialShapeTypes specialShapeType)
        {
            switch (specialShapeType)
            {
                case SpecialShapeTypes.CorrectBomb:
                    DeleteShape(_taskController.TaskChoosen.WrongTask);
                    break;
                case SpecialShapeTypes.WrongBomb:
                    DeleteShape(_taskController.TaskChoosen.FirstTask);
                    DeleteShape(_taskController.TaskChoosen.SecondTask);
                    break;
                case SpecialShapeTypes.IncreaseScore:
                    _scoreController.ChangeScore(_scoreChange);
                    break;
                case SpecialShapeTypes.DecreaseScore:
                    _scoreController.ChangeScore(-_scoreChange);
                    break;
                case SpecialShapeTypes.SlowerTime:
                    StartCoroutine(ChangeTime(true));
                    break;
                case SpecialShapeTypes.FastTime:
                    StartCoroutine(ChangeTime(false));
                    break;
            }
        }

        private void DeleteShape(Task task)
        {
            switch (task.Type)
            {
                case TaskType.Color:
                case TaskType.InteractionAndColor:
                    _spawner.DeleteShape(task.ShapeColor);
                    break;
                case TaskType.Form:
                case TaskType.InteractionAndForm:
                    _spawner.DeleteShape(task.Form);
                    break;
                case TaskType.ColorAndForm:
                    _spawner.DeleteShape(task.Form, task.ShapeColor);
                    break;
            }
        }

        private IEnumerator ChangeTime(bool isSlower)
        {
            Time.timeScale += isSlower ? -_timeCoefficientChange : _timeCoefficientChange;
            yield return _waitForChangeTime;
            Time.timeScale = 1;
        }
    }
}
