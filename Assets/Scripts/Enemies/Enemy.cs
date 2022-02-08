using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Localization))]
public class Enemy : MonoBehaviour
{
    /*[SerializeField]
    protected Stat health;
    [SerializeField]
    protected Stat range;
    [SerializeField]
    protected Stat attackSpeed;*/
    [SerializeField]
    protected Stat moveSpeed;

    protected float elapsed;

    [Header("Components")]
    [SerializeField]
    protected Rigidbody2D _body;
    [SerializeField]
    protected Localization localization;
    protected CharacterController2D player;
    public CombatConfiguration combatConfiguration;

    


    //Attack related

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;

    }


}
