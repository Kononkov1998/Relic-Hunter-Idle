using UnityEngine;

namespace Utilities.Extensions
{
    public static class TransformExtensions
    {
        public static void RotateTo(this Transform objectToRotate, Vector3 target, float maxDegrees)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target - objectToRotate.position, Vector3.up);
            objectToRotate.rotation =
                Quaternion.RotateTowards(objectToRotate.rotation, targetRotation, maxDegrees);
        }
    }
}