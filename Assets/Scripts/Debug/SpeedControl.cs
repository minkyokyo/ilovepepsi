using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControl : MonoBehaviour
{

    public Button fasterBtn, slowerBtn;
    float defaultTime;
    // Start is called before the first frame update
    void Start()
    {
        fasterBtn.onClick.AddListener(FasterTime);
        slowerBtn.onClick.AddListener(SlowerTime);
    }

    void FasterTime()
    {
        defaultTime += 1f;
        Time.timeScale = defaultTime;
    }
    void SlowerTime()
    {
        defaultTime -= 1f;
        if (defaultTime == 0)
        {
            defaultTime = 1f;
        }
        Time.timeScale = defaultTime;
    }

       
}
