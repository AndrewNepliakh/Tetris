using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManagerMonoBehaviour : MonoBehaviour
{

    private UpdateManager _updateManager;

    public void SetUp(UpdateManager updateManager)
    {
        _updateManager = updateManager;
    }

    public void Update()
    {
        _updateManager.Update();
    }

    public void FixedUpdate()
    {
        _updateManager.FixedUpdate();
    }
}
