using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBox : MonoBehaviour
{
    public bool isSelectable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isSelectable && (other.name == "Collider" || other.name == "Rigidbody" || other.name == "LeftHand" || other.name == "RightHand"))
        {
            Debug.Log("성공 다음 오브벡트 활성화");
            IslandGameManager.Instance.ResetTimer();
            IslandGameManager.Instance.ActivateNextBox();
            IslandGameManager.Instance.SuccessTouchBox();
            isSelectable = false;
        }
    }
}
