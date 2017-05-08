using UnityEngine;
using System.Collections;
using System;

public class InputController : MonoBehaviour {

    protected class Repeater
    {
        const float threshold = 0.5f;
        const float rate = 0.25f;
        float _next;
        bool _hold;
        string _axis; 

        public Repeater (string axisName)
        {
            _axis = axisName;
        }

        public int Update()
        {
            int retValue = 0;
            int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));

            if (value != 0)
            {
                if (Time.time > _next)
                {
                    retValue = value;
                    _next = Time.time + (_hold ? rate : threshold);
                    _hold = true;
                }
            }
            else
            {
                _hold = false;
                _next = 0;
            }
            return retValue;
        }
    }

    Repeater _hor = new Repeater("Horizontal");
    Repeater _ver = new Repeater("Vertical");
    Repeater _piece = new Repeater("Move Piece");

    #region Notifications
    public const string MoveNotification = "InputController.MoveNotification";
    public const string FireNotification = "InputController.FireNotification";
    public const string MovePieceNotification = "InputController.MovePieceNotification";
    #endregion

    string[] _buttons = new string[] { "Fire1", "Fire2", "Fire3" };
	
	// Update is called once per frame
	public virtual void Update () {
        int x = _hor.Update();
        int y = _ver.Update();
         
        if (x != 0 || y != 0)
        {
            this.PostNotification(MoveNotification, new Vector3(x, y, 0));
        }

        int pX = _piece.Update();
        if (pX != 0)
        {
            this.PostNotification(MovePieceNotification, pX);
        }

        for (int i = 0; i < 3; ++i)
        {
            if (Input.GetButtonUp(_buttons[i]))
            {
                this.PostNotification(FireNotification, i);
            }
        }

        
	}
}
