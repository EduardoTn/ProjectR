using TMPro;
using UnityEngine;

public class BlackHoleHotkey : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;
    private Transform myEnemy;
    private BlackHoleSkillController blackHole;
    
    public void SetupHotKey(KeyCode _myHotKey, Transform _myEnemy, BlackHoleSkillController _blackHole)
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        sr = GetComponent<SpriteRenderer>();
        myEnemy = _myEnemy;
        blackHole = _blackHole;
        myHotKey = _myHotKey;
        myText.text = _myHotKey.ToString();

    }

    private void Update()
    {
        if (Input.GetKeyUp(myHotKey))
        {
            blackHole.AddEnemyToList(myEnemy);
            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
