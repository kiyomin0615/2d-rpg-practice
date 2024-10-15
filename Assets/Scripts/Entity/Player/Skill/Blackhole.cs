using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    [SerializeField] GameObject hotkeyPrefab;
    [SerializeField] List<KeyCode> keyList;
    [SerializeField] List<GameObject> hotkeyList;

    float maxSize;
    float growingSpeed;
    float shrinkingSpeed;
    public bool canGrow;
    public bool canShrink;

    public int maxAttackCount = 5;
    float timer;
    float cooldown = 0.3f;

    List<Transform> enemyTransforms = new List<Transform>();

    void Update()
    {
        timer -= Time.deltaTime;

        Grow();
        AttackWithinBlackhole();
        Shrink();
    }

    public void Setup(float maxSize, float growingSpeed, float shrinkingSpeed)
    {
        this.maxSize = maxSize;
        this.growingSpeed = growingSpeed;
        this.shrinkingSpeed = shrinkingSpeed;

        canGrow = true;
    }

    public void AddEnemyToList(Transform enemyTransform)
    {
        enemyTransforms.Add(enemyTransform);
    }

    void AttackWithinBlackhole()
    {
        if (maxAttackCount <= 0)
        {
            canGrow = false;
            canShrink = true;
            return;
        }

        if (timer < 0 && Input.GetKeyDown(KeyCode.R))
        {
            timer = cooldown;

            PlayerManager.instance.player.Disappear();

            int randomIndex = Random.Range(0, enemyTransforms.Count);
            float xOffset = Random.Range(0, 2) == 0 ? 1.5f : -1.5f;
            SkillManager.instance.cloneSkill.CreateClone(enemyTransforms[randomIndex].position, new Vector2(xOffset, 0));

            maxAttackCount--;
        }
    }

    void Grow()
    {
        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growingSpeed * Time.deltaTime);
        }
    }

    void Shrink()
    {
        if (canShrink && !canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0, 0), shrinkingSpeed * Time.deltaTime);
            if (transform.localScale.magnitude <= 0.1f)
            {
                PlayerManager.instance.player.ExitUltimateState();
                DestroyAllHotkey();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemyComponent = other.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.Freeze();
            GenerateHotkey(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemyComponent = other.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.Unfreeze();
        }
    }

    void GenerateHotkey(Collider2D other)
    {
        if (keyList.Count <= 0)
        {
            return;
        }

        GameObject hotkey = Instantiate(hotkeyPrefab, other.transform.position + Vector3.up * 2f, Quaternion.identity);
        hotkeyList.Add(hotkey);

        KeyCode randomKey = keyList[Random.Range(0, keyList.Count)];
        keyList.Remove(randomKey);

        Hotkey hotkeyComponent = hotkey.GetComponent<Hotkey>();
        hotkeyComponent.Setup(randomKey, other.transform, this);
    }

    void DestroyAllHotkey()
    {
        if (hotkeyList.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < hotkeyList.Count; i++)
        {
            Destroy(hotkeyList[i]);
        }
    }
}
