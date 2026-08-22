using UnityEngine;

public class MultiplayerCameraFollow : MonoBehaviour
{
    [Header("Jogadores")]
    [SerializeField] private Transform[] targets;

    [Header("Movimento da câmera")]
    [SerializeField] private float moveSmoothTime = 0.2f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, -10f);

    [Header("Zoom")]
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 12f;
    [SerializeField] private float zoomPadding = 2f;
    [SerializeField] private float zoomSmoothTime = 0.2f;

    private Camera cam;

    private Vector3 moveVelocity;
    private float zoomVelocity;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets == null || targets.Length == 0)
            return;

        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        Bounds bounds = GetTargetsBounds();

        Vector3 targetPosition = bounds.center + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref moveVelocity,
            moveSmoothTime
        );
    }

    private void ZoomCamera()
    {
        Bounds bounds = GetTargetsBounds();

        float requiredHeight =
            bounds.size.y / 2f + zoomPadding;

        float requiredWidth =
            bounds.size.x / 2f + zoomPadding;

        float heightFromWidth =
            requiredWidth / cam.aspect;

        float targetZoom =
            Mathf.Max(requiredHeight, heightFromWidth);

        targetZoom = Mathf.Clamp(
            targetZoom,
            minZoom,
            maxZoom
        );

        cam.orthographicSize = Mathf.SmoothDamp(
            cam.orthographicSize,
            targetZoom,
            ref zoomVelocity,
            zoomSmoothTime
        );
    }

    private Bounds GetTargetsBounds()
    {
        bool foundTarget = false;
        Bounds bounds = new Bounds();

        foreach (Transform target in targets)
        {
            if (target == null)
                continue;

            if (!foundTarget)
            {
                bounds = new Bounds(
                    target.position,
                    Vector3.zero
                );

                foundTarget = true;
            }
            else
            {
                bounds.Encapsulate(target.position);
            }
        }

        return bounds;
    }
}