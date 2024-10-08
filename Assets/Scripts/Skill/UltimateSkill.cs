using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill : Skill
{
    [SerializeField] GameObject blackholePrefab;

    [SerializeField] float maxSize;
    [SerializeField] float growingSpeed;
    [SerializeField] float shrinkingSpeed;

    protected override void Start() {

    }
    
    protected override void Update() {
        base.Update();
    }

    public override bool TrySkill() {
        return base.TrySkill();
    }

    public override void ExecuteSkill() {
        base.ExecuteSkill();

        GameObject blackhole = Instantiate(blackholePrefab, PlayerManager.instance.player.transform.position, Quaternion.identity);
        Blackhole blackholeComponent = blackhole.GetComponent<Blackhole>();

        blackholeComponent.Setup(maxSize, growingSpeed, shrinkingSpeed);
    }
}
