using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    [SerializeField] private List<KeyCode> keyCodeList;
    [SerializeField] private GameObject hotKeyPrefab;
    private List<GameObject> hotKeys = new List<GameObject>();
    private int keycodeCount;
    private float maxSize;
    private float growSpeed;
    private bool canGrow = true;
    private bool canShrink = false;
    private bool canAttack = false;
    private float blackHoleDuration;
    private float blackHoleTimer;
    private float cloneAttackCooldown = .3f;
    private int amountOfAttacks = 3;
    private float cloneAttackTimer;
    private List<Transform> targets = new List<Transform>();


    public void SetupBlackHole(float _maxSize, float _growSpeed, int _amountOfAttacks, float _cloneAttackCooldown, float _blackHoleDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;
        blackHoleDuration = _blackHoleDuration;
        blackHoleTimer = _blackHoleDuration;
        keycodeCount = keyCodeList.Count;
    }
    void Update()
    {
        blackHoleTimer -= Time.deltaTime;
        AttackClone();
        if (canGrow && !canShrink) 
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), (growSpeed*2) * Time.deltaTime);
            if (transform.localScale.x <= 0)
                Destroy(this.gameObject);
        }
        if (blackHoleTimer < 0 && !canAttack)
        {
            FinishBlackHole();
        }

    }
    private void AttackClone()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            canAttack = true;
            DestroyHotKeys();
            PlayerManager.instance.player.MakeTransparent(true);
        }
        cloneAttackTimer -= Time.deltaTime;
        if (cloneAttackTimer <= 0 && canAttack)
        {
            cloneAttackTimer = cloneAttackCooldown;
            int randomIndex = Random.Range(0, targets.Count);
            int front = Random.Range(1, 100) > 50 ? -1 : 1;
            Vector3 randomPosition = new Vector3(Random.Range(1, 2) * front, 0, 0);
            SkillManager.instance.clone.SkillCast(targets[randomIndex].position + randomPosition, front == 1 ? false : true);
            amountOfAttacks--;
            if (amountOfAttacks <= 0)
            {
                FinishBlackHole();
            }
        }
    }

    private void FinishBlackHole()
    {
        canAttack = false;
        canShrink = true;
        PlayerManager.instance.player.ExitBlackHole();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotKey(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(false);
            DestroyHotKeys();
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodeList.Count <= 0 || (canAttack || amountOfAttacks <= 0))
            return;
        GameObject newHotkey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        hotKeys.Add(newHotkey);
        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);
        BlackHoleHotkey newHotkeyScript = newHotkey.GetComponent<BlackHoleHotkey>();
        newHotkeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }
    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);

    private void DestroyHotKeys()
    {
        if(hotKeys.Count <= 0)
            return;

        foreach(var key in hotKeys)
        {
            Destroy(key);
        }
    }
}
