using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialHandler : MonoBehaviour
{
    [Header("Assign all plane objects here")]
    public List<GameObject> planes = new List<GameObject>();
    public Camera OVRigCamera;
    public GameObject Void;
    public GameObject TeleportL;
    public GameObject TeleportR;

    [Header("Fade Settings")]
    public float fadeDuration = 2f;
    public bool destroyAfterFade = false;

    private Dictionary<string, FadePlaneData> _planeData = new Dictionary<string, FadePlaneData>();

    private enum FadeType { None, In, Out }

    private class FadePlaneData
    {
        public GameObject plane;
        public Renderer renderer;
        public Material material;
        public Color originalColor;
        public float timer;
        public FadeType fadeType;
    }

    void Start()
    {
        foreach (var plane in planes)
        {
            if (plane == null) continue;

            Renderer renderer = plane.GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.LogWarning($"TutorialHandler: {plane.name} has no Renderer.");
                continue;
            }

            Material mat = renderer.material;
            SetMaterialTransparent(mat);

            // Initialize fade data
            bool startInactive = plane.name == "tutorial1" || plane.name == "tutorial2";

            _planeData[plane.name] = new FadePlaneData
            {
                plane = plane,
                renderer = renderer,
                material = mat,
                originalColor = mat.color,
                timer = 0f,
                fadeType = FadeType.None
            };

            // Set initial alpha and active state
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, startInactive ? 0f : mat.color.a);
            plane.SetActive(!startInactive ? true : false);
        }

        StartCoroutine(Tutorial());
    }

    void Update()
    {
        foreach (var data in _planeData.Values)
        {
            if (data.fadeType == FadeType.None) continue;

            data.timer += Time.deltaTime;
            float t = Mathf.Clamp01(data.timer / fadeDuration);
            float alpha = data.fadeType == FadeType.Out
                ? Mathf.Lerp(data.originalColor.a, 0f, t)
                : Mathf.Lerp(0f, data.originalColor.a, t);

            data.material.color = new Color(data.originalColor.r, data.originalColor.g, data.originalColor.b, alpha);

            if (t >= 1f)
            {
                if (data.fadeType == FadeType.Out)
                {
                    if (destroyAfterFade)
                        Destroy(data.plane);
                    else
                        data.plane.SetActive(false);
                }

                data.fadeType = FadeType.None;
            }
        }
    }

    // ---- Public fade controls ----
    public void FadeOut(string planeName)
    {
        if (_planeData.TryGetValue(planeName, out var data))
        {
            data.timer = 0f;
            data.fadeType = FadeType.Out;
        }
    }

    public void FadeIn(string planeName)
    {
        if (_planeData.TryGetValue(planeName, out var data))
        {
            data.plane.SetActive(true);
            data.timer = 0f;
            data.fadeType = FadeType.In;
        }
    }

    public void FadeOutAll()
    {
        foreach (var data in _planeData.Values)
        {
            data.timer = 0f;
            data.fadeType = FadeType.Out;
        }
    }

    public void FadeInAll()
    {
        foreach (var data in _planeData.Values)
        {
            data.plane.SetActive(true);
            data.timer = 0f;
            data.fadeType = FadeType.In;
        }
    }

    private void SetMaterialTransparent(Material mat)
    {
        if (mat.HasProperty("_Mode"))
            mat.SetFloat("_Mode", 2);

        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }

    private IEnumerator Tutorial()
    {
        TeleportL.SetActive(false);
        TeleportR.SetActive(false);
        // Fade in tutorial1
        FadeIn("tutorial1");
        yield return new WaitForSeconds(fadeDuration + 5f);

        // Fade out tutorial1 and wait for fade duration to finish
        FadeOut("tutorial1");
        yield return new WaitForSeconds(fadeDuration);

        // Now fade in tutorial2
        FadeIn("tutorial2");
        yield return new WaitForSeconds(fadeDuration + 5f);

        // Fade out all planes
        FadeOutAll();
        yield return new WaitForSeconds(2f);

        // Set camera culling mask to everything
        if (OVRigCamera != null)
            OVRigCamera.cullingMask = ~0;

        yield return new WaitForSeconds(fadeDuration);

        TeleportL.SetActive(true);
        TeleportR.SetActive(true);

        if (Void != null)
            Void.SetActive(false);
    }
}