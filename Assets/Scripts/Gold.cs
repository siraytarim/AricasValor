using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] Animator _coinanimator;
    [SerializeField] GameObject gold;
    private void OnMouseDown()
    {
        _coinanimator.SetBool("Gold", true);
    }
}
