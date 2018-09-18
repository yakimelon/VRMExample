using UnityEngine;

public class CharacterController : Photon.MonoBehaviour {
	public float Speed = 2f;
	public float Thrust = 100;

	private Rigidbody _rigidBody;
	private Animator _animator;
	private PhotonTransformView _photonTransformView;

	private bool _onGround;

	void Start() {
		_rigidBody = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
		_photonTransformView = GetComponent<PhotonTransformView>();
	}

	void FixedUpdate() {
		if (!photonView.isMine) return;
		if (!_onGround) return;
		
		CalcMove();
		CalcJump();
	}
    
	void OnCollisionStay(Collision other) {
		_onGround = true;
	}

	private void CalcMove() {
		if (Input.GetKey(KeyCode.RightArrow)) {
			_rigidBody.velocity = new Vector3(Speed, 0, 0);
			transform.rotation = Quaternion.Euler(0, 90, 0);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			_rigidBody.velocity = new Vector3(-Speed, 0, 0);
			transform.rotation = Quaternion.Euler(0, 270, 0);
		} else if (Input.GetKey(KeyCode.UpArrow)) {
			_rigidBody.velocity = new Vector3(0, 0, Speed);
			transform.rotation = Quaternion.Euler(0, 0, 0);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			_rigidBody.velocity = new Vector3(0, 0, -Speed);
			transform.rotation = Quaternion.Euler(0, 180, 0);
		} else {
			_animator.SetBool("Running", false);
			return;
		}
		
		// Photonの位置情報同期処理
		_photonTransformView.SetSynchronizedValues(_rigidBody.velocity, 0);
        
		_animator.SetBool("Running", true);
	}

	private void CalcJump() {
		if (Input.GetKey(KeyCode.Space)) {
			_rigidBody.AddForce(new Vector3(0, Thrust, 0));
			_animator.SetBool("Jumping", true);
			_onGround = false;
		} else {
			_animator.SetBool("Jumping", false);
		}
	}
}