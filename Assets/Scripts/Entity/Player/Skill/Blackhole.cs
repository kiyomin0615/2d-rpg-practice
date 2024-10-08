using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    [SerializeField] GameObject hotkeyPrefab;
    [SerializeField] List<KeyCode> keyList;

    public float maxSize;
    public float growingSpeed;
    public bool canGrow;

    private List<Transform> enemyTransforms = new List<Transform>();

    void Update()
    {
        if (canGrow) {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growingSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemyComponent = other.GetComponent<Enemy>();
        if (enemyComponent != null) {
            enemyComponent.Freeze();
            GenerateHotkey(other);
        }
    }

    void GenerateHotkey(Collider2D other) {
        if (keyList.Count <= 0) {
            return;
        }

        GameObject hotkey = Instantiate(hotkeyPrefab, other.transform.position + Vector3.up * 2f, Quaternion.identity);
        
        KeyCode randomKey = keyList[Random.Range(0, keyList.Count)];
        keyList.Remove(randomKey);
        
        Hotkey hotkeyComponent = hotkey.GetComponent<Hotkey>();
        hotkeyComponent.Setup(randomKey, other.transform, this);
    }

    public void AddEnemyToList(Transform enemyTransform) {
        enemyTransforms.Add(enemyTransform);
    }
}
