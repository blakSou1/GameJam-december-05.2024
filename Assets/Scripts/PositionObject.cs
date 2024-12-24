using UnityEngine;

public class PositionObject : MonoBehaviour
{
    public Camera mainCamera; // Основная камера
    public RectTransform canvasRectTransform; // RectTransform Canvas
    public Vector3 anchorPosition; // Позиция внутри Canvas (нормализованные координаты от 0 до 1)

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // Получаем размеры Canvas
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        // Рассчитываем позицию в пикселях
        Vector2 targetPosition = new Vector2(canvasSize.x * anchorPosition.x, canvasSize.y * anchorPosition.y);

        // Преобразуем из экранных координат в мировые
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(targetPosition.x, targetPosition.y, anchorPosition.z));

        // Учитываем смещение по оси Y
        worldPosition.y = transform.position.y; // оставляем текущее значение по высоте

        // Обновляем позицию объекта
        transform.position = worldPosition;
    }
}
