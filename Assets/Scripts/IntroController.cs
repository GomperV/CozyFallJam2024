using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public GameObject[] pages;
    public SceneSwitcher sceneSwitcher;

    private int _page = 0;
    private void Start()
    {
        for(int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == 0);
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(_page < pages.Length - 1)
            {
                pages[_page].SetActive(false);
                _page++;
                pages[_page].SetActive(true);
            }
            else
            {
                sceneSwitcher.SwitchScene("SampleScene");
            }
        }
    }
}
