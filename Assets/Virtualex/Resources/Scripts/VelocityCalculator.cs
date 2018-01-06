using UnityEngine;
using System.Collections;

public class VelocityCalculator : MonoBehaviour 
{
	public SettingsVelocity Settings { get;	set; }

	public Vector3 estimatedVelocity { get; private set; }
	public Vector3 estimatedAngularVelocity { get; private set; }

	private Vector3 _lastPosition;
	public Vector3 _lastVelocity;
	public Vector3 _lastAngularVelocity;
	private Quaternion _lastRotation;
	private int _curSmoothingFrameIndex=0;
	private HistoryFrame[] smoothingFrames;
	Vector3 previousPosition;
	Quaternion previousOrientation;

	void Start() {
		_lastPosition = transform.position;
		_lastVelocity = new Vector3(0.0f,0.0f,0.0f);
		_lastRotation = transform.rotation;
		_curSmoothingFrameIndex = 0;
        Settings = new SettingsVelocity();
		smoothingFrames=new HistoryFrame[Settings.NumSmoothingFrames];
	}

	void Update() {
			float invDeltaTime = 1.0f / Time.deltaTime;
			estimatedVelocity = ( transform.position - previousPosition ) * invDeltaTime;

			float angle;
			Vector3 axis;
			Quaternion deltaRotation = transform.rotation * Quaternion.Inverse( previousOrientation );
			deltaRotation.ToAngleAxis( out angle, out axis );
			estimatedAngularVelocity = Mathf.Deg2Rad * angle * axis * invDeltaTime;

			previousPosition = transform.position;
			previousOrientation = transform.rotation;
		}

	void FixedUpdate() {
		Vector3 curVelocity = (transform.position - _lastPosition) / Time.fixedDeltaTime;
		_lastPosition = transform.position;
		_lastVelocity = curVelocity;
		
		float rotAngle;
		Vector3 rotAxis;
		
		Quaternion rotQuat = Quaternion.Inverse(_lastRotation) * transform.rotation;
		rotQuat.ToAngleAxis(out rotAngle, out rotAxis);
		Vector3 angularVelocity = (rotAxis * rotAngle * Mathf.Deg2Rad) / Time.fixedDeltaTime;
		
		_lastRotation = transform.rotation;
		_lastAngularVelocity = angularVelocity;
		
		smoothingFrames[_curSmoothingFrameIndex % smoothingFrames.Length] = new HistoryFrame(_lastVelocity, _lastAngularVelocity);
		++_curSmoothingFrameIndex;
	}
	
	public void Apply(Rigidbody rigidbody) {
		if (rigidbody){
			if (Settings.NumSmoothingFrames > 0) {
				Vector3 throwVel, throwAngularVel;
				throwVel = throwAngularVel = Vector3.zero;
				int numUsedSmoothingFrames = Mathf.Min(_curSmoothingFrameIndex, smoothingFrames.Length);
				for (int i=0; i<numUsedSmoothingFrames; ++i)
				{
					throwVel += smoothingFrames[i].Velocity;
					throwAngularVel += smoothingFrames[i].AngularVelocity;
				}
				throwVel /= numUsedSmoothingFrames;
				throwAngularVel /= numUsedSmoothingFrames;
				
				rigidbody.angularVelocity = throwAngularVel;
                //rigidbody.velocity = new Vector3(throwVel.x * Settings.ThrowScale.x, throwVel.y < 0 ? 0 : (throwVel.y * Settings.ThrowScale.y), throwVel.z * Settings.ThrowScale.z);
                ImpartVelocity(rigidbody, throwVel, throwAngularVel);
			}else{
				if(Settings.UseAngularVelocity) rigidbody.angularVelocity =  _lastAngularVelocity;
				rigidbody.velocity = new Vector3(_lastVelocity.x*Settings.ThrowScale.x,_lastVelocity.y < 0 ? 0 : (_lastVelocity.y*Settings.ThrowScale.y),_lastVelocity.z*Settings.ThrowScale.z) ;
			}
		}
	}

    void ImpartVelocity(Rigidbody rigidbody, Vector3 throwVel, Vector3 throwAngularVel)
    {
        //find the gravity direction

        if(rigidbody.GetComponent<AffectedByGravityFields>()){
            AffectedByGravityFields grav = rigidbody.GetComponent<AffectedByGravityFields>();
            if (grav.isInGravityField)
            {
                Vector3 finalVel = Vector3.zero;
                if (grav.gravityDirection == new Vector3(1, 0, 0))
                {
                    finalVel = new Vector3(throwVel.x > 0 ? 0 : (throwVel.x * Settings.ThrowScale.x), throwVel.y * Settings.ThrowScale.y, throwVel.z * Settings.ThrowScale.z);
                }
                else if (grav.gravityDirection == new Vector3(-1, 0, 0))
                {
                    finalVel = new Vector3(throwVel.x < 0 ? 0 : (throwVel.x * Settings.ThrowScale.x), throwVel.y * Settings.ThrowScale.y, throwVel.z * Settings.ThrowScale.z);
                }
                else if (grav.gravityDirection == new Vector3(0, 1, 0))
                {
                    finalVel = new Vector3(throwVel.x * Settings.ThrowScale.x, throwVel.y > 0 ? 0 : (throwVel.y * Settings.ThrowScale.y), throwVel.z * Settings.ThrowScale.z);
                }
                else if (grav.gravityDirection == new Vector3(0, -1, 0))
                {
                    finalVel = new Vector3(throwVel.x * Settings.ThrowScale.x, throwVel.y < 0 ? 0 : (throwVel.y * Settings.ThrowScale.y), throwVel.z * Settings.ThrowScale.z);
                }
                else if (grav.gravityDirection == new Vector3(0, 0, 1))
                {
                    finalVel = new Vector3(throwVel.x * Settings.ThrowScale.x, throwVel.y * Settings.ThrowScale.y, throwVel.z > 0 ? 0 : (throwVel.z * Settings.ThrowScale.z));
                }
                else if (grav.gravityDirection == new Vector3(0, 0, -1))
                {
                    finalVel = new Vector3(throwVel.x * Settings.ThrowScale.x, throwVel.y * Settings.ThrowScale.y, throwVel.z < 0 ? 0 : (throwVel.z * Settings.ThrowScale.z));
                }
                else
                {
                    finalVel = new Vector3(throwVel.x * Settings.ThrowScale.x, throwVel.y * Settings.ThrowScale.y, throwVel.z * Settings.ThrowScale.z);
                }

                rigidbody.angularVelocity = throwAngularVel;
                rigidbody.velocity = finalVel;
            }
            else
            {
                rigidbody.angularVelocity = throwAngularVel;
                rigidbody.velocity = new Vector3(throwVel.x * Settings.ThrowScale.x, throwVel.y < 0 ? 0 : (throwVel.y * Settings.ThrowScale.y), throwVel.z * Settings.ThrowScale.z) * VarsTracker.clubHitModifier;
            }
        }
        else
        {
            rigidbody.angularVelocity = throwAngularVel;
            rigidbody.velocity = new Vector3(throwVel.x * Settings.ThrowScale.x, throwVel.y < 0 ? 0 : (throwVel.y * Settings.ThrowScale.y), throwVel.z * Settings.ThrowScale.z) * VarsTracker.clubHitModifier;
        }
    }

    public void ResetVelocity()
    {
        for (int i = 0; i < smoothingFrames.Length; i++)
        {
            smoothingFrames[i] = new HistoryFrame(Vector3.zero, Vector3.zero);
        }
        _lastVelocity = Vector3.zero;
        _lastRotation = Quaternion.identity;
    }

	public Vector3 AngularMovementVelocity{get { return _lastAngularVelocity; } }
	public Vector3 MovementVelocity{get { return _lastVelocity;  } }

}




public class HistoryFrame 
{
	public Vector3 Velocity { get; private set; }
	public Vector3 AngularVelocity { get; private set; }

	public HistoryFrame (Vector3 _lastVelocity,Vector3 _lastAngularVelocity){
		Velocity=_lastVelocity;
		AngularVelocity=_lastAngularVelocity;
	}
}