﻿using UnityEngine;
using System.Collections;

public class BaseAnimController : MonoBehaviour
{

	[SerializeField]
	public string
		_runTr, _atack_0_Tr, _dieTr, _gd_Tr;
    
    public Animator _anim;

	private void Awake ()
	{
		_anim = GetComponent <Animator> ();
	}

    public void SetAnimator(Animator anim)
    {
        _anim = anim;
    }


	private void OnGUI ()
	{
		//BaseButtons ();
	}

	protected virtual void BaseButtons ()
	{
		if (GUI.Button (new Rect (10, 10, 100, 50), "Run")) {
            print("run");
			_anim.SetTrigger (Animator.StringToHash(_runTr));
		}
		
		if (GUI.Button (new Rect (10, 80, 100, 50), "Atack_0")) {
            print("Attack");
			_anim.SetTrigger (_atack_0_Tr);
		}

		if (GUI.Button (new Rect (10, 380, 100, 50), "GetDamage")) {
			_anim.SetTrigger (_gd_Tr);
		}
		
		if (GUI.Button (new Rect (10, 500, 100, 50), "Die")) {
			_anim.SetTrigger (_dieTr);
		}

		if (GUI.Button (new Rect (Screen.width - 110, 10, 100, 50), "Idle")) {
			_anim.SetTrigger ("Idle");
		}


	}
}
