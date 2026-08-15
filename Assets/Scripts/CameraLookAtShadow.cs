using UnityEngine;
using DG.Tweening;

public class CameraLookAtShadow : MonoBehaviour
{
    [SerializeField] private Transform shadow;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float lookStrength = 0.3f;

    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    public void LookAtShadow()
    {
        Vector3 direction = shadow.position - transform.position;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.DORotateQuaternion(
            Quaternion.Slerp(
                originalRotation,
                targetRotation,
                lookStrength
            ),
            duration
        ).SetEase(Ease.InOutSine);
    }

    public void ResetCamera()
    {
        transform.DORotateQuaternion(
            originalRotation,
            duration
        ).SetEase(Ease.InOutSine);
    }
}