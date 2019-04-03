using System.Collections;
using System.Collections.Generic;
using ModTool.Interface;
using UnityEngine;
using UnityEngine.UI;

public class DropItHookVerifier : ModBehaviour
{
    public Button OkButton;
    public Dropdown Call1, Call2, Call3, Call4, Number1, Number2, Number3, Number4;
    public MissionHook Hook;
    
    private void Start()
    {
        OkButton.onClick.AddListener(Verify);
    }

    private void Verify()
    {
        bool success = true;

        if (Call1.value != 3)
            success = false;
        
        if (Call2.value != 1)
            success = false;
        
        if (Call3.value != 0)
            success = false;
        
        if (Call4.value != 2)
            success = false;
        
        if (Number1.value != 1)
            success = false;
        
        if (Number2.value != 1)
            success = false;
        
        if (Number3.value != 3)
            success = false;
        
        if (Number4.value != 1)
            success = false;
        
        if (success)
            Hook.RaiseSuccess();
        else
            Hook.RaiseFailed();
    }
    
    private void OnDestroy()
    {
        OkButton.onClick.RemoveListener(Verify);
    }
}
