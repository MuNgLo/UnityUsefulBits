using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public Transform _playerPivvot = null;
    public Transform _lineEnd = null;
    public Rigidbody _ball = null;
    public Slider _powerSlider = null;

    public float _sensitivity = 1.0f;

    public float _chargeRate = 80.0f;
    public float _charge = 0.0f;
    public float _maxCharge = 100.0f;

    public float _sideOffset = 0.0f;
    public float _angleOffset = 0.0f;

    public float _maxSideOffset = .5f;
    public float _maxAngleOffset = 15.0f;

    private Vector3 _startLocation = Vector3.zero;
    private LineRenderer _line = null;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _startLocation = _playerPivvot.position;
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        _angleOffset += Input.GetAxisRaw("Mouse X") * _sensitivity;
        _angleOffset = Mathf.Clamp(_angleOffset, -_maxAngleOffset, _maxAngleOffset);
        _playerPivvot.localEulerAngles = new Vector3(0.0f, _angleOffset, 0.0f);
        _playerPivvot.position = _startLocation + Vector3.left * _sideOffset;
        _line.SetPosition(0, _playerPivvot.position);
        _line.SetPosition(1, _lineEnd.position);

        if(Input.GetButton("Fire1")){ _charge += _chargeRate * Time.deltaTime; _charge = Mathf.Clamp(_charge, 0.0f, 100.0f); }
        if(Input.GetButtonUp("Fire1")){ TossBall(_charge); _charge = 0.0f; }
        if (Input.GetKeyDown(KeyCode.R)) { ResetBall(); }
        _powerSlider.value = _charge;
    }

    void TossBall(float strength)
    {
        _ball.maxAngularVelocity = 500.0f;
        _ball.isKinematic = false;
        _ball.AddForce(_playerPivvot.transform.forward * strength * 0.8f, ForceMode.Impulse);
    }

    internal void ResetBall()
    {
        _ball.velocity = Vector3.zero;
        _ball.angularVelocity = Vector3.zero;
        _ball.isKinematic = true;
        _ball.transform.position = _playerPivvot.position + Vector3.up *0.245f;
    }
}
