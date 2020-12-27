using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerArcher : MonoBehaviour
{
    [SerializeField] private Arrow arrow;
    [SerializeField] private Arrow flameArrow;
    [SerializeField] Transform bowArrowStartPoint;
    [SerializeField] GameObject bow;
    [SerializeField] float speed;
    [SerializeField] float shootingAngleClamp;
    [SerializeField] int damage;
    [SerializeField] AudioClip drawSound;
    [SerializeField] AudioClip looseSound;

    

    private Transform bodyTR;
    private Animator bodyAnim;
    private PlayerMover playerMover;
    private Vector3 shootingPoint;
    public bool isFlame = false;
    private bool isEnable;
    public bool IsEnable
    {
        get { return isEnable; }
        set
        {
            isEnable = value;
            BowEquip(isEnable);
        }
    }

    private void Start()
    {
        bodyAnim = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0];
        playerMover = GetComponent<PlayerMover>();
        bodyTR = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0].transform;
    }


    #region PUBLIC METHODS
    // Called by PlayerMover
    public float CalculateCharacterAimingAngle(PlayerMover.PlayerMoveDirection Direction)
    {
        // Calculation For Character Aiming 

        // 카메라 to 마우스포인터 Ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 카메라 to 캐릭터 Vector
        Vector3 cameraToCharacter = transform.position - Camera.main.transform.position;

        // 해당 Vector와 Ray 사이의 각도를 구합니다.
        float angleBtwnRayToCharacterNRayToMouse = Vector3.SignedAngle(ray.direction, cameraToCharacter, Vector3.Cross(cameraToCharacter, ray.direction).normalized);

        // 카메라와 캐릭터사이의 거리 계산
        float cameraToCharacterDistance = Mathf.Abs(
                Vector3.Distance(Camera.main.transform.position, transform.position) / Mathf.Cos(Mathf.Deg2Rad * angleBtwnRayToCharacterNRayToMouse)
            );

        // 캐릭터와 같은 x 직선상의 Ray Point
        Vector3 aimingPoint = ray.GetPoint(cameraToCharacterDistance);

        // 카메라 - 마우스포인트 Vector
        Vector3 cameraToMousePoint = aimingPoint - Camera.main.transform.position;

        // z축을 기준으로 캐릭터의 앞방향과 Aiming Point사이의 각도
        float angleForCharacterAimingAngle = Vector3.SignedAngle(bodyTR.forward, aimingPoint - transform.position, Vector3.forward);

        // Left 일경우 각도가 반전되는 경우 예외처리
        if (Direction == PlayerMover.PlayerMoveDirection.LEFT) angleForCharacterAimingAngle *= -1f;

        // Set Shooting Point by Clamped Angle
        shootingPoint = ShootingPointClamp(Direction, angleForCharacterAimingAngle, aimingPoint);

        //Debug.DrawLine(Camera.main.transform.position, aimingPoint, Color.red); // Camera -> MousePoint (== Character Z)
        //Debug.DrawLine(transform.position, transform.position + transform.forward * 10f, Color.blue); // Character -> Character Forward
        //Debug.DrawLine(transform.position, aimingPoint, Color.yellow); // Character -> AimPoint
        //Debug.DrawLine(transform.position, shootingPoint, Color.yellow); // Character -> Shooting Point

        return angleForCharacterAimingAngle;
    }

    // Called by PlayerController
    public void Aim()
    {
        bodyAnim.ResetTrigger("ShootArrow");
        bodyAnim.SetBool("isAiming", true);
    }

    // Called by PlayerController
    public void Shoot()
    {
        bodyAnim.SetTrigger("ShootArrow");
    }

    // Called by ArcherHelper
    public void ShootArrow_AnimationEvent()
    {
        GameObject arrowGO;
        if (!isFlame)
        {
            arrowGO = Instantiate(arrow.gameObject, bowArrowStartPoint.position, Quaternion.identity);
        }
        else
        {
            arrowGO = Instantiate(flameArrow.gameObject, bowArrowStartPoint.position, Quaternion.identity);
        }
        arrowGO.GetComponent<Arrow>().damage = damage;
        arrowGO.GetComponent<Rigidbody>().velocity = (shootingPoint - transform.position).normalized * speed;

    }

    // Called by ArcherHelper
    public void PlayDrawSoundOnce()
    {
        GetComponent<AudioSource>().PlayOneShot(drawSound);
    }

    // Called by ArcherHelper
    public void PlayLooseSoundOnce()
    {
        GetComponent<AudioSource>().PlayOneShot(looseSound);
    }
    #endregion

    #region PRIVATE METHODS

    private void BowEquip(bool equip)
    {
        bow.SetActive(equip);
    }

    // Shooting Angle Clamp by [SerializeField] shootingAngleClamp
    private Vector3 ShootingPointClamp(PlayerMover.PlayerMoveDirection Direction, float angle, Vector3 originalPoint)
    {
        float clampAngle = Mathf.Clamp(angle, -shootingAngleClamp, shootingAngleClamp);

        if (Direction == PlayerMover.PlayerMoveDirection.RIGHT)
            return RotatePointAroundPivot(originalPoint, transform.position, new Vector3(0f, 0f, clampAngle - angle));
        else
            return RotatePointAroundPivot(originalPoint, transform.position, new Vector3(0f, 0f, angle - clampAngle));
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
    #endregion



}


