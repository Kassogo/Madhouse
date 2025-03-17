using UnityEngine;
using TMPro;

namespace Madhouse.ADHD
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TaskController _taskController;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private int _score;

        private void Awake()
        {
            _spawner.OnDestroyObject += CheckDestroyObject;
            _score = 0;
            _scoreText.text = _score.ToString();
        }

        private void OnDestroy()
        {
            _spawner.OnDestroyObject -= CheckDestroyObject;
        }

        private void CheckDestroyObject(ShapeTypes form, ShapeColors color, InteractionEndTypes interactionEnd)
        {
            if (CheckCompledTask(_taskController.TaskChoosen.WrongTask, form, color, interactionEnd))
                _score -= 2;
            if (CheckCompledTask(_taskController.TaskChoosen.FirstTask, form, color, interactionEnd))
                _score += 2;
            if (CheckCompledTask(_taskController.TaskChoosen.SecondTask, form, color, interactionEnd))
                _score += 1;

            _scoreText.text = _score.ToString();
        }

        private bool CheckCompledTask(Task task, ShapeTypes form, ShapeColors color, InteractionEndTypes interactionEnd)
        {
            switch (task.Type)
            {
                case TaskType.Color:
                    return task.ShapeColor == color;
                case TaskType.Form:
                    return task.Form == form;
                case TaskType.Interaction:
                    return task.Interaction == interactionEnd;
                case TaskType.InteractionAndColor:
                    return task.Interaction == interactionEnd && task.ShapeColor == color;
                case TaskType.InteractionAndForm:
                    return task.Interaction == interactionEnd && task.Form == form;
                case TaskType.ColorAndForm:
                    return task.Form == form && task.ShapeColor == color;
                default:
                    return false;
            }
        }
    }
}
