using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void switchToFieldSettings()
    {
        anim.SetBool("SwitchToFieldSettings", true);
    }

    public void switchToMenuFromFieldSettings()
    {
        anim.SetBool("SwitchToFieldSettings", false);
    }
    public void switchToSettings()
    {
        anim.SetBool("SwitchToSettings", true);
    }

    public void switchToMenuFromSettings()
    {
        anim.SetBool("SwitchToSettings", false);
    }

    public void switchToHelp()
    {
        anim.SetBool("SwitchToHelp", true);
    }

    public void switchFromHelp()
    {
        anim.SetBool("SwitchToHelp", false);
    }

    public void switchToCustom()
    {
        anim.SetBool("SwitchToCustom", true);
    }

    public void switchFromCustom()
    {
        anim.SetBool("SwitchToCustom", false);
    }
    public void switchToRecords()
    {
        anim.SetBool("SwitchToRecords", true);
    }

    public void switchFromRecords()
    {
        anim.SetBool("SwitchToRecords", false);
    }

    public void quit()
    {
        Application.Quit();
    }
}

