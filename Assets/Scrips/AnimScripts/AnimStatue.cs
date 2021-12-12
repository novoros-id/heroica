using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStatue : MonoBehaviour
{
    Animator anim;


    [SerializeField] private float time1;

    private float _timeLeft = 0f;
    private float _timeLeft1 = 0f;
    private bool _timerOn = false;
    private bool _timerOn1 = false;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Idle");    
        _timeLeft1 = time1;
        _timeLeft = Random.Range(15, 30);
        _timerOn = true;

    }


    // Update is called once per frame
    void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
            }
            else
            {
                _timerOn1 = true;
                _timeLeft = Random.Range(15, 30);
                anim.Play("Statue's Animation");
            }
        }


        if (_timerOn1)
        {
            if (_timeLeft1 > 0)
            {
                _timeLeft1 -= Time.deltaTime;
            }
            else
            {
                _timeLeft1 = time1;
                _timerOn1 = false;
                anim.Play("Idle");
            }
        }
    }
    
}
