using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [SerializeField] GameObject clonePrefab;
    public float cloneDuration = 1.5f;

    public void CreateClone(Vector2 position, Vector2 offset) {
        GameObject clone = Instantiate(clonePrefab);
        clone.transform.position = position + offset;
        Destroy(clone, cloneDuration);
    }
}
