using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("FX")]
    private SpriteRenderer sr;
    [SerializeField] private Material hitMat;
    private Material originalMat;
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.15f);
        sr.material = originalMat;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        } else
        {
            sr.color = Color.red;
        }
    }

    public void CancelInvokes()
    {
        sr.color = Color.white;
        CancelInvoke();
    }
}
