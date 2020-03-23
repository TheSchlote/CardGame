using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings 
{
    public static ResourceManager _resourcesManager;

    public static ResourceManager GetResourceManager()
    {
        if (_resourcesManager == null)
        {
            _resourcesManager = Resources.Load("ResourceManager") as ResourceManager;
        }
        return _resourcesManager;
    }

}
