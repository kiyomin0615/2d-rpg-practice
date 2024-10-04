using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [SerializeField] GameObject clonePrefab;
    public float cloneDuration = 1.5f;

    public void createClone(Vector2 position) {
        GameObject clone = Instantiate(clonePrefab);
        clone.transform.position = position;
        Destroy(clone, cloneDuration);
    }
}
