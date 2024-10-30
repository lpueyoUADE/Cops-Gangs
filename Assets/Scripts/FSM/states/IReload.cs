using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReload
{
    public void Reload();
    bool CanReload();
    bool NeedsToReload();
    bool IsReloading { get; set; }

}

