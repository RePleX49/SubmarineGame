using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletButton : MonoBehaviour
{
    [HideInInspector]
    public Symbols activeSymbol;
    public MeshRenderer symbolImage;

    TabletManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<TabletManager>();
    }

    public void ChangeImage()
    {
        symbolImage.material.SetTexture("_MainTex", manager.tabletData.symbolTexs[(int)activeSymbol]);
    }

    public void TryButton()
    {
        // Check if button matches the current correct symbol
        if(activeSymbol == manager.answerSymbol)
        {
            manager.UpdatePuzzle();
        }
    }
}
