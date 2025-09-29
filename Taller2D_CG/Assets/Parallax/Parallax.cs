using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Parallax Settings")]
    [SerializeField] private float parallaxMultiplier = 0.1f;

    private Material parallaxMaterial;
    private Transform player;
    private float lastPlayerX;


    void Start()
    {
        // Obtener el material del renderer
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            parallaxMaterial = rend.material;
        }
        else
        {
            Debug.LogWarning(" {gameObject.name} no tiene Renderer, el Parallax no funcionará.");
        }

        // Buscar el Player por tag
        BuscarPlayer();

    }


    void Update()
    {

        // Si el player todavía no está asignado, intentar encontrarlo
        if (player == null)
        {
            BuscarPlayer();
            return; // esperar hasta que exista
        }

        if (parallaxMaterial == null) return;

        // Calcular desplazamiento en X del player
        float deltaX = player.position.x - lastPlayerX;

        // Mover la textura
        parallaxMaterial.mainTextureOffset += new Vector2(deltaX * parallaxMultiplier, 0);

        // Actualizar última posición
        lastPlayerX = player.position.x;
    }

    private void BuscarPlayer()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            player = obj.transform;
            lastPlayerX = player.position.x;
            Debug.Log(" Parallax enlazado a {player.name}");
        }
    }
}
