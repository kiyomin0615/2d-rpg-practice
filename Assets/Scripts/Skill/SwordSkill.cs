using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : Skill
{
    [SerializeField] GameObject swordPrefab;
    [SerializeField] float fireForce;
    [SerializeField] float swordGravity;
    Vector2 swordVelocity;

    [SerializeField] GameObject aimDotPrefab;
    [SerializeField] Transform aimDotParentTransform;
    [SerializeField] int count;
    [SerializeField] float spaceBetween;
    GameObject[] aimDots;

    protected override void Start()
    {
        base.Start();

        GenerateAimDots();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Mouse2)) {
            swordVelocity = CalculateFireDirection() * fireForce;
             for (int i = 0; i < aimDots.Length; i++) {
                aimDots[i].transform.position = CalculateAimDotPosition(i * spaceBetween);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse2))
            swordVelocity = CalculateFireDirection() * fireForce;
    }

    public void CreateSword()
    {
        GameObject sword = Instantiate(swordPrefab);
        PlayerSword playerSword = sword.GetComponent<PlayerSword>();

        sword.transform.position = PlayerManager.instance.player.transform.position;
        playerSword.SetupSword(swordVelocity, swordGravity);
        ToggleAimDots(false);
    }

    public Vector2 CalculateFireDirection()
    {
        Vector2 playerPosition = PlayerManager.instance.player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - playerPosition).normalized;
    }

    void GenerateAimDots() {
        aimDots = new GameObject[count];
        for (int i = 0; i < count; i++) {
            aimDots[i] = Instantiate(aimDotPrefab, PlayerManager.instance.player.transform.position, Quaternion.identity, aimDotParentTransform);
            aimDots[i].SetActive(false);
        }
    }

    public void ToggleAimDots(bool isActive) {
        for (int i = 0; i < aimDots.Length; i++) {
            aimDots[i].SetActive(isActive);
        }
    }

    Vector2 CalculateAimDotPosition(float t) {
        Vector2 position = (Vector2) PlayerManager.instance.player.transform.position + new Vector2(
            swordVelocity.x * t,
            swordVelocity.y * t - (0.5f * 9.8f * swordGravity * t * t)
        );

        return position;
    }
}
