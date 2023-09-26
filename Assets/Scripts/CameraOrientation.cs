using UnityEngine;

public class CameraOrientation : MonoBehaviour
{
    [SerializeField] private float _cameraDistanceStep = 5f;

    private Grid _grid;
    private Transform _gridTransform;
    private Camera _mainCamera;
    private float _cameraDistance;

    private void Awake() => _grid = FindObjectOfType<Grid>();

    private void Start()
    {
        _gridTransform = _grid.transform;
        _grid.GridCreated += OnGridCreated;

        _mainCamera = Camera.main;

        if (_gridTransform != null && _mainCamera != null)
        {
            _cameraDistance = _grid.GridSize * _cameraDistanceStep;
            Vector3 lookAtPosition = _gridTransform.position;

            Vector3 cameraPosition = lookAtPosition - _mainCamera.transform.forward * _cameraDistance + _mainCamera.transform.up * _cameraDistance / 2;
            _mainCamera.transform.position = cameraPosition;

            _mainCamera.transform.LookAt(lookAtPosition);
        }

    }

    private void OnDestroy() => _grid.GridCreated -= OnGridCreated;

    private void OnGridCreated() => AlineCamera();

    private void AlineCamera()
    {
        Vector3 lookAtPosition = _gridTransform.position;
        _mainCamera.transform.LookAt(lookAtPosition);

        Vector3 cameraPosition = lookAtPosition - _mainCamera.transform.forward * _cameraDistance;
        _mainCamera.transform.position = cameraPosition;
    }
}
