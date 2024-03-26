using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class ICommands 
{
    [System.Serializable]

    public class IMove : ICommand
    {
        MoveableBehaviour playerMover;
        Vector3 movement;
        public IMove(MoveableBehaviour player, Vector3 moveVector)
        {
            this.playerMover = player;
            this.movement = moveVector;
        }
        public void Execute()
        {
            playerMover.Move(movement);
            Debug.Log(playerMover.transform.position);
        }
        public void Undo()
        {
            playerMover.Move(-movement);
        }
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
    }
}


