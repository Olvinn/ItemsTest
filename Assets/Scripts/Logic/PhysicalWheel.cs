using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalWheel : MonoBehaviour
{
    public float spinningTime = 10;
    public string segment;
    public float angle { get { return _rb.transform.rotation.eulerAngles.z; } }
    public UnityEvent onEnd;

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _rayStart;
    [SerializeField] Transform visuals;
    float _spinningRest;

    private void Awake()
    {
        onEnd = new UnityEvent();
    }

    void Update()
    {
        visuals.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Spin()
    {
        _rb.AddTorque(Random.Range(500, 1500));
        StopAllCoroutines();
        StartCoroutine(Stopping());
    }

    IEnumerator Stopping()
    {
        yield return new WaitForSeconds(1);
        float startAngV = _rb.angularVelocity;
        _spinningRest = spinningTime;
        while (_rb.angularVelocity > 0.01f)
        {
            _spinningRest -= Time.deltaTime;
            _spinningRest = Mathf.Clamp(_spinningRest, 0, spinningTime);
            _rb.angularVelocity = Mathf.Lerp(0, startAngV, _spinningRest / spinningTime);
            yield return new WaitForEndOfFrame();
        }
        _rb.angularVelocity = 0;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        if (Physics2D.Raycast(_rayStart.position, Vector2.up, new ContactFilter2D(), hits) > 0)
        {
            segment = hits[0].collider.name;
        }
        onEnd?.Invoke();
    }
}
