using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GlobalSettings : ScriptableObject
{
    public string levelSelectScene = "LevelSelect";
    [Header("Movement Related")]

    public float BaseMoveSpeedMultiplier = 1f;
    public float BaseRunSpeedMultiplier = 1.5f;
    public float MovingTurnSpeed = 600;
    public float StationaryTurnSpeed = 400;
    public float JumpPower = 6.9f;
    public float MaxSprintTime = 2f;
    [Tooltip("recharge per second in seconds ")]
    public float SprintRechargeRate = 0.5f;

    [Header("SnowBall Throw Related")]
    public float DefaultShotSpeed = 600;
    public float DefaultShotAngle = 8;
    public float BallWeight = 50;
    public Vector3 BallScale = Vector3.one * 0.2f;
    [Space]
    public int MaxAmmo = 5;
    public float ReloadTime = 2f;
    [Header("Flag Related")]
    public float FlagPickUpRadius = 1;

    [Header("ShovelWar Related")]
    public string test;

    //[Header("Key Bindings")]
    //public PierInputManager.ButtonName HorizontalAxis = PierInputManager.ButtonName.Left_Horizontal;
    //public PierInputManager.ButtonName VerticalAxis = PierInputManager.ButtonName.Left_Vertical;
    //public PierInputManager.ButtonName CrouchKey = PierInputManager.ButtonName.Left_Bumper;
    //public PierInputManager.ButtonName JumpKey = PierInputManager.ButtonName.A;
    //public PierInputManager.ButtonName RunKey = PierInputManager.ButtonName.Right_Trigger;
    //public PierInputManager.ButtonName AltRunKey = PierInputManager.ButtonName.Left_Trigger;


}
