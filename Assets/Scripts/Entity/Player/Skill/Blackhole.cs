using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    [SerializeField] GameObject hotkeyPrefab;
    [SerializeField] List<KeyCode> keyList;
    [SerializeField] List<GameObject> hotkeyList;

    public float maxSize;
    public float growingSpeed;
    public float shrinkingSpeed;
    public bool canGrow;
    public bool canShrink;

    public int maxAttackCount = 5;
    float timer;
    float cooldown = 0.3f;

    public List<Transform> enemyTransforms = new List<Transform>();

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && Input.GetKeyDown(KeyCode.R) && maxAttackCount > 0) {
            timer = cooldown;

            int randomIndex = Random.Range(0, enemyTransforms.Count);
            float xOffset = Random.Range(0, 2) == 0 ? 1.5f : -1.5f;
            SkillManager.instance.cloneSkill.CreateClone(enemyTransforms[randomIndex].position, new Vector2(xOffset, 0));

            maxAttackCount--;
        }

        if (canGrow && !canShrink) {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growingSpeed * Time.deltaTime);
        }

        if (canShrink && !canGrow) {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0, 0), shrinkingSpeed * Time.deltaTime);
            if (transform.localScale.magnitude <= 0.1f) {
                Destroy(gameObject);
                DestroyAllHotkey();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemyComponent = other.GetComponent<Enemy>();
        if (enemyComponent != null) {
            enemyComponent.Freeze();
            GenerateHotkey(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Enemy enemyComponent = other.GetComponent<Enemy>();
        if (enemyComponent != null) {
            enemyComponent.Unfreeze();
        }
    }

    void GenerateHotkey(Collider2D other) {
        if (keyList.Count <= 0) {
            return;
        }

        GameObject hotkey = Instantiate(hotkeyPrefab, other.transform.position + Vector3.up * 2f, Quaternion.identity);
        hotkeyList.Add(hotkey);

        KeyCode randomKey = keyList[Random.Range(0, keyList.Count)];
        keyList.Remove(randomKey);
        
        Hotkey hotkeyComponent = hotkey.GetComponent<Hotkey>();
        hotkeyComponent.Setup(randomKey, other.transform, this);
    }

    void DestroyAllHotkey() {
        if (hotkeyList.Count <= 0) {
            return;
        }

        for (int i = 0; i < hotkeyList.Count; i++) {
            Destroy(hotkeyList[i]);
        }
    }

    public void AddEnemyToList(Transform enemyTransform) {
        enemyTransforms.Add(enemyTransform);
    }
}
