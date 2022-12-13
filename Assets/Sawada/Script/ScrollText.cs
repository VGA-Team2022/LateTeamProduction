using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{
    [SerializeField] Text _targetText = null;

    public void TextActivate(PlayerController player)
    {
        if(player)
        {
            _targetText.enabled = true;
        }
        else
        {
            _targetText.enabled = false;
        }
    }
}
