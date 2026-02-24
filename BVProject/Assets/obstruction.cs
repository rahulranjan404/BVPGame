using UnityEngine;
using System.Collections.Generic;

public class CameraObstructionFade : MonoBehaviour
{
    public Transform player;
    public LayerMask obstructionMask;
    public float fadeAlpha = 0.5f;
    public float fadeSpeed = 10f;

    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private List<Renderer> currentlyFading = new List<Renderer>();

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction.normalized, distance, obstructionMask);

        List<Renderer> hitRenderers = new List<Renderer>();

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                hitRenderers.Add(rend);

                if (!originalMaterials.ContainsKey(rend))
                {
                    originalMaterials.Add(rend, rend.materials);
                }

                FadeRenderer(rend, fadeAlpha);
            }
        }

        // Restore materials that are no longer blocking
        for (int i = currentlyFading.Count - 1; i >= 0; i--)
        {
            Renderer rend = currentlyFading[i];
            if (!hitRenderers.Contains(rend))
            {
                RestoreRenderer(rend);
                currentlyFading.RemoveAt(i);
            }
        }

        currentlyFading = hitRenderers;
    }

    void FadeRenderer(Renderer rend, float targetAlpha)
    {
        foreach (Material mat in rend.materials)
        {
            if (mat.HasProperty("_Color"))
            {
                Color color = mat.color;
                color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * fadeSpeed);
                mat.color = color;
            }
        }
    }

    void RestoreRenderer(Renderer rend)
    {
        foreach (Material mat in rend.materials)
        {
            if (mat.HasProperty("_Color"))
            {
                Color color = mat.color;
                color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime * fadeSpeed);
                mat.color = color;
            }
        }
    }
}