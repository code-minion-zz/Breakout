using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MetalBlock : BaseBlock
{

    void Start()
    {
        base.Start();

        sprite.color = Color.grey;
    }

    // we've been hit!
    protected override void Damage()
    {
        // do nothing, for we are the mighty MetalBlock
    }

}