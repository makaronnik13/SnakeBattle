using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEditor : MonoBehaviour
{

    public void OpenEditor()
    {
        gameObject.SetActive(true);
    }

    public void CloseEditor()
    {
        gameObject.SetActive(false);
    }
   
}
