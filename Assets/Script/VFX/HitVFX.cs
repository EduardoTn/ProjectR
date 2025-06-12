using UnityEngine;

public class HitVFX : MonoBehaviour
{
    [Header("Random Spawn")]
    [SerializeField] private float xMinOffset = -.3f;
    [SerializeField] private float xMaxOffset = .3f;
    [SerializeField] private float yMinOffset = -.3f;
    [SerializeField] private float yMaxOffset = .3f;

    private void Start()
    {
        ApplyRandomOffSet();
    }
    private void ApplyRandomOffSet()
    {
        float xOffSet = Random.Range(xMinOffset, xMaxOffset);
        float yOffSet = Random.Range(yMinOffset, yMaxOffset);

        this.gameObject.transform.position = transform.position + new Vector3(xOffSet, yOffSet, 0);
    }
    public void AnimationTrigger()
    {
        Destroy(gameObject);
    }
}
