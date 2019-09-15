using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 枪械控制器(热武器)
/// </summary>
public abstract class LikeGunControllerBase : GunControllerBase {
    public override void MouseButtonDown0()
    {
        base.MouseButtonDown0();
        PlayEffect();
    }
    /// <summary>
    /// 枪械特效
    /// </summary>
    protected abstract void PlayEffect();
}
