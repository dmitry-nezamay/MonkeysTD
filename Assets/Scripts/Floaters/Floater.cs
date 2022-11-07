using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Floater : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _reward;

    private Path _path;
    private PathPoint _nextPathPoint;

    public int Reward => _reward;

    public static event UnityAction<Floater> DestroyedByMonkey;
    public static event UnityAction<Floater> DestroyedByFinishPoint;

    private void Update()
    {
        if (_nextPathPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPathPoint.transform.position, _speed * Time.deltaTime);

            if (transform.position == _nextPathPoint.transform.position)
            {
                int nextPathPointIndex = _path.PathPoints.IndexOf(_nextPathPoint);

                if (nextPathPointIndex >=0 && _path.PathPoints.Count > nextPathPointIndex + 1)
                    _nextPathPoint = _path.PathPoints[nextPathPointIndex + 1];
                else
                    _nextPathPoint = _path.FinishPoint.GetComponent<PathPoint>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out FinishPoint finishPoint))
        {
            DestroyedByFinishPoint?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Monkey monkey = GetComponentInParent<Monkey>();

        if (monkey != null)
            DestroyedByMonkey?.Invoke(this);
    }

    public void Init(Path path)
    {
        _path = path;

        if (_path.PathPoints.Count > 0)
            _nextPathPoint = _path.PathPoints[0];
        else
            _nextPathPoint = _path.FinishPoint.GetComponent<PathPoint>();
    }
}
