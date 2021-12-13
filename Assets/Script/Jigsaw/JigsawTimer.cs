using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JigsawTimer : MonoBehaviour
{

    public Image timerBar;
    public TextMeshProUGUI timerText;
    public Image timerBody;
    [Space]
    public Sprite redBar;
    public Sprite greenBar;
    public Sprite redBody;
    public Sprite greenBody;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float maxTimer;
    public bool timeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerBar.sprite = greenBar;
            timerBody.sprite = greenBody;
            timerText.color = Color.white;

            if (timer < 5)
            {
                timerBar.sprite = redBar;
                timerText.color = Color.red;
                timerBody.sprite = redBody;
            }
        }
        else if(!timeOut && timer <= 0)
        {
            timeOut = true;
        }

        timerBar.fillAmount = timer / maxTimer;
        timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    public void StartTimer(float t)
    {
        timer = t;
        maxTimer = t;
    }

}
