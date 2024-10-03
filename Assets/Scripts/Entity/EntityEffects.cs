using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEffects : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material original;
    [SerializeField] private Material hitEffect;

    void Start() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        original = spriteRenderer.material;
    }

    IEnumerator ApplyHitEffect() {
        spriteRenderer.material = hitEffect;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.material = original;
    }
}
