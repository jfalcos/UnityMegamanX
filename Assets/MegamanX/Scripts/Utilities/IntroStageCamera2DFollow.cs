using UnityEngine;
using System.Collections;

public class IntroStageCamera2DFollow : MonoBehaviour
{
	public Transform target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
	public float yBottomPositionLimit = 0f;
	public float deathRogumerCameraSize = 1.1f;
	public CameraBoundaries defaultCameraLimit = null;
	public CameraBoundaries beeBlader1CameraLimit = null;
	public CameraBoundaries beeBlader2CameraLimit = null;
	public CameraBoundaries vileModeCameraLimit = null;

	private Camera myCamera = null;
	private float m_OffsetZ;
	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	private Vector3 m_LookAheadPos;
	
	void Awake()
	{
		myCamera = GetComponent<Camera> ();
	}
	
	// Use this for initialization
	private void Start()
	{
		EnableDefaultMode ();

		m_LastTargetPosition = target.position;
		m_OffsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}
	
	
	// Update is called once per frame
	private void Update()
	{
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - m_LastTargetPosition).x;
		
		bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
		
		if (updateLookAheadTarget)
		{
			m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
		}
		else
		{
			m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
		}
		
		Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
		
		transform.position = newPos;
		
		m_LastTargetPosition = target.position;
	}

	public void EnableDefaultMode()
	{
		SetCameraLimitsEnabled (true, false, false, false);
	}

	public void EnableBeeBlader1Mode()
	{
		SetCameraLimitsEnabled (false, true, false, false);
	}

	public void EnableBeeBlader2Mode()
	{
		SetCameraLimitsEnabled (false, false, true, false);
	}

	public void EnableDeathRogumerMode()
	{
		SetCameraLimitsEnabled (false, false, false, true);
	}
	
	void SetCameraLimitsEnabled(bool defaultEnabled, bool beeBlader1Enabled, bool beeBlader2Enabled, bool vileModeEnabled)
	{
		defaultCameraLimit.enabled = defaultEnabled;
		beeBlader1CameraLimit.enabled = beeBlader1Enabled;
		beeBlader2CameraLimit.enabled = beeBlader2Enabled;
		vileModeCameraLimit.enabled = vileModeEnabled;
	}
}
