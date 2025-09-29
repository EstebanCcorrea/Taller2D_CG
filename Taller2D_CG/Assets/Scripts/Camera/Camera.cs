using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string targetTag = "Player"; // El tag del objeto a seguir
    [SerializeField] private float smoothSpeed = 0.125f; // La velocidad de suavizado
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f); // Desplazamiento de la cámara

    private Transform target;

    private void Start()
    {
        BuscarTarget();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            BuscarTarget(); // Intentar encontrar el target si aun no existe
            return;
        }

        // Movimiento suavizado
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Si quieres que la cámara mire al target (para 3D), mantenlo.
        // Para 2D normalmente no hace falta.
        // transform.LookAt(target);
    }

    private void BuscarTarget()
    {
        GameObject obj = GameObject.FindGameObjectWithTag(targetTag);
        if (obj != null) target = obj.transform;
    }
}
