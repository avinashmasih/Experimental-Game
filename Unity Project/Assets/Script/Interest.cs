using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interest : MonoBehaviour
{
    [Tooltip("Interest Ratio Green to Red")]
    [Range(0,100)]
    public int interestRatio;

    public Slider UiSlider;

    //private float _greenIntrst;
    //private float _redIntrst;

    //private Vector3 _posVector;

    // Start is called before the first frame update
    void Start()
    {
        UiSlider.value = interestRatio / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
