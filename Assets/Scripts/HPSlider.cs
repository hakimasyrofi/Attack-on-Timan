using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    public Slider hpSlider;
    public int life;

    void Start(){
        initHP(1000);
        life = 1000;
    }

    void Update(){
        setHP(life);
    }
    // Start is called before the first frame update
    public void initHP(int life){
        hpSlider.maxValue = 1000;
        hpSlider.value = life;
    }

    public void setHP(int life){
        hpSlider.value = life;
        // Debug.Log(life);
    }
}
