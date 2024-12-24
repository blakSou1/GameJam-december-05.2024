using Unity.Burst.CompilerServices;
using UnityEngine;

public class DetectionStene : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1.0f;
    private bool movingRight = false;

    EnemyMove mov => GetComponent<EnemyMove>();
    private void Start() => movingRight = mov.MovDos();

    private void Update() => DetectWalls();

    private void DetectWalls()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movingRight ? Vector3.right : Vector3.left, out hit, rayDistance))
        {
            if (!hit.collider.CompareTag("Player")) // Убедитесь, что ваши стены имеют тег "Wall"
            {
                movingRight = !movingRight; // Меняем направление
                mov.MovPol(movingRight);
            }
        }
    }
}
