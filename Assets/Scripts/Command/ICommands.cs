using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class ICommands { }
public class CameraNavigation : ICommand
{
    FlyCamera MyFlycam;
    GameObject Owner;
    CameraNavigation(FlyCamera script, GameObject _Owner)
    {
        MyFlycam = script;
        Owner = _Owner;
    }
    public void Execute()
    {
        MyFlycam.enabled = true;
    }

    public void Undo()
    {
        MyFlycam.enabled = false;
    }
}

