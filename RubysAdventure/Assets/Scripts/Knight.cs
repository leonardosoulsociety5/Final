using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;
public class Knight : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public TextMeshProUGUI dialog;
    public OrcBehavior orc;
    
    float timerDisplay;

    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }

        if(orc == null){
            dialog.SetText("Thank you for defeating the orc! Your reward is unlimited ammo!");
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        if(orc == null){
            FindObjectOfType<RubyController>().cogs += 100;  
            FindObjectOfType<RubyController>().uptadecogstext();
        }
    }
}

