using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls objects going out of bounds
/// </summary>
public class BoundaryHandler : MonoBehaviour
{
    public float fadeSpeed;

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);

        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(FadeOutObj(other.gameObject));
        }
    }

    private IEnumerator FadeOutObj(GameObject go)
    {
        MeshRenderer objMesh = go.GetComponent<MeshRenderer>();

        if (objMesh == null)
            objMesh = go.GetComponentInChildren<MeshRenderer>();

        if (objMesh == null)
            yield return null;

        //Changes the material of the object to be able to fade
        //https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html?_ga=2.8123764.771745098.1581571457-857422823.1533679413
        objMesh.material.SetFloat("_Mode", 2f);
        objMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        objMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        objMesh.material.SetInt("_ZWrite", 0);
        objMesh.material.DisableKeyword("_ALPHATEST_ON");
        objMesh.material.EnableKeyword("_ALPHABLEND_ON");
        objMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        objMesh.material.renderQueue = 3000;

        //Decreases material color alpha
        while (objMesh.material.color.a > 0)
        {
            Color newColor = objMesh.material.color;
            newColor.a -= fadeSpeed * Time.deltaTime;
            objMesh.material.color = newColor;

            yield return new WaitForEndOfFrame();
        }

        //Destroys obj once fully faded
        Destroy(go);

        yield return null;
    }
}
