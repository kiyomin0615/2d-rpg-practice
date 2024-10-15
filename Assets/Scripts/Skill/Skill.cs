using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
  [SerializeField] protected float cooldown;
  [SerializeField] protected float timer;

  protected virtual void Start()
  {

  }

  protected virtual void Update()
  {
    timer -= Time.deltaTime;
  }

  public virtual bool TrySkill()
  {
    if (timer < 0)
    {
      ExecuteSkill();
      timer = cooldown;
      return true;
    }

    Debug.Log("You can not use skill right now.");
    return false;
  }

  public virtual void ExecuteSkill()
  {

  }
}
