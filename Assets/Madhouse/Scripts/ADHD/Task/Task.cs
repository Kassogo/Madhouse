using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    [Serializable]
    public class Task
    {
        public TaskType Type => _taskType;

        public InteractionEndTypes Interaction => _interactionEnd;

        public ShapeColors ShapeColor => _shapeColor;

        public ShapeTypes Form => _shapeForm;

        [SerializeField] private TaskType _taskType;
        [SerializeField] private InteractionEndTypes _interactionEnd;
        [SerializeField] private ShapeColors _shapeColor;
        [SerializeField] private ShapeTypes _shapeForm;
    }
}