using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsList : MonoBehaviour
{

    public GameObject SkinVisual;

    private RectTransform __contentTransform;
    private RectTransform _contentTransform
    {
        get
        {
            if (!__contentTransform)
            {
                __contentTransform = GetComponent<ScrollRect>().content;
            }
            return __contentTransform;
        }
    }

    public void UpdateList(List<SnakeSkin> skins)
    {

        foreach (Transform t in _contentTransform)
        {
            Destroy(t.gameObject);
        }

        foreach (SnakeSkin lm in skins)
        {
            GameObject newModule = Instantiate(SkinVisual, Vector3.zero, Quaternion.identity, _contentTransform);
            newModule.transform.localScale = Vector3.one;
            newModule.transform.localPosition = Vector3.zero;
            newModule.GetComponent<SkinVisual>().Init(lm);
        }
    }
}
