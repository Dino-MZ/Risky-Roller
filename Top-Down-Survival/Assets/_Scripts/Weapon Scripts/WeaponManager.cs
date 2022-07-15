using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region PlayerObjectFlipping
    [SerializeField] private Transform ParentObject;
    private Camera cam;
    private Vector2 mousePos;
    #endregion

    #region gunUI
    #endregion

    #region unlocking

    public PlayerSO playerSO;

    #endregion

    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        #region PlayerObjectFlipping
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x <= transform.position.x)
        {
            ParentObject.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            ParentObject.eulerAngles = new Vector3(0, 0, 0);
        }
        #endregion
    }
}
