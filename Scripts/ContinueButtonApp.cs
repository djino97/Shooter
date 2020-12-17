using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButtonApp : MonoBehaviour
{
    public void ButtonClickedContinue()
    {
        this.gameObject.SetActive(false);
    }
}