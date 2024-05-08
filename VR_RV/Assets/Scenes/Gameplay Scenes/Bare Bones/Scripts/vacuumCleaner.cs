using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class vacuumCleaner : MonoBehaviour
{

    public float AttractionRange;
    public float AttractionStrength;
    public LayerMask AttractedTo;
    private Rigidbody RB;
    public float AttractionStartSpeed;
    public float AttractionMaxSpeed;
    public AnimationCurve VelocityCurve;
    public float AccelerationSpeed;

    float s;
    float varSpeed;
    GameObject target;
    RaycastHit[] hits;

    void Start()
    {
        RB = GetComponent<Rigidbody>();

    }

    void Update()
    {
        hits = Physics.SphereCastAll(transform.position, AttractionRange, transform.forward, 0.01f, AttractedTo);
        if (hits.Length < 1)
        {
            s = 0;
            return;
        }

        List<float> ranges = new List<float>();
        foreach (var item in hits)
        {
            ranges.Add(Vector3.Distance(item.transform.position, transform.position));
        }
        if (ranges.Count < 1)
        {
            s = 0;
            return;
        }

        target = hits[ranges.IndexOf(ranges.Min())].collider.gameObject;
        Vector3 dir = target.transform.position - transform.position;
        s += Time.deltaTime * AccelerationSpeed;
        varSpeed = Mathf.Lerp(AttractionStartSpeed, AttractionMaxSpeed, VelocityCurve.Evaluate(s / 1f));
        RB.velocity = dir.normalized * varSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vacuum"))
        {
            Destroy(gameObject);
        }
    }
}
