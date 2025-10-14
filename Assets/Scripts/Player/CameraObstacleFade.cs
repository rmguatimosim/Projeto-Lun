using System.Collections.Generic;
using UnityEngine;

public class CameraObstacleFade : MonoBehaviour
{

    private Transform player;
    public LayerMask obstacleLayer;
    public Material transparentMaterial;
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private List<Renderer> fadedRenderers = new List<Renderer>();

    void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Transform>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Restore previous materials
        foreach (Renderer r in fadedRenderers)
        {
            if (originalMaterials.ContainsKey(r))
                r.materials = originalMaterials[r];
        }
        fadedRenderers.Clear();

        // Raycast from camera to player
        Vector3 direction = player.position - transform.position;
        Ray ray = new(transform.position, direction);
        RaycastHit[] hits = Physics.RaycastAll(ray, direction.magnitude, obstacleLayer);

        foreach (RaycastHit hit in hits)
        {
            Renderer r = hit.collider.GetComponent<Renderer>();
            if (r != null && !fadedRenderers.Contains(r))
            {
                if (!originalMaterials.ContainsKey(r))
                {
                    originalMaterials[r] = r.materials;
                }

                Material[] transparentMats = new Material[r.materials.Length];
                for (int i = 0; i < transparentMats.Length; i++)
                {
                    transparentMats[i] = transparentMaterial;
                }

                r.materials = transparentMats;
                fadedRenderers.Add(r);
            }
        }

    }

}
