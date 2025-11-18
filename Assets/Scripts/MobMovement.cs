using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 2f;
    private string myTag;
    public bool debugRay = true;

    private bool isStopped = false;
    private Transform detectedTarget;

    private float frontOffset;

    void Awake()
    {
        myTag = this.gameObject.tag;
        frontOffset = GetComponent<Collider2D>().bounds.extents.x;
    }

    void Update()
    {
        Vector2 dir = transform.right.normalized;
        Vector2 origin = (Vector2)transform.position + dir * (frontOffset + 0.05f);

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, detectionRange);

        Transform closestValidTarget = null;
        float closestDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.collider == null) 
                continue;
            if (hit.collider.gameObject == this.gameObject) 
                continue;

            float dist = Vector2.Distance(origin, hit.point);

            // ignorowanie swojego tagu
            if (hit.collider.tag == myTag) 
                continue;

            // Najbliższy obiekt o innym tagu w zasięgu
            if (dist < closestDist)
            {
                closestDist = dist;
                closestValidTarget = hit.collider.transform;
            }
        }

        // znaleziono przeciwnika
        if (closestValidTarget != null)
        {
            isStopped = true;
            detectedTarget = closestValidTarget;

            // rozpocznij walkę
            if(!GetComponent<MobCombat>().fighting)
                StartCoroutine(GetComponent<MobCombat>().Fight(closestValidTarget.gameObject));
        }
        else
        {
            // po pokonaniu przeciwnika
            if (isStopped && detectedTarget == null)
            {
                isStopped = false;
                GetComponent<MobCombat>().fighting = false;
            }
        }

        // gdy nie wykryto zadnego przeciwnika idz dalej
        if (!isStopped)
        {
            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        }

        // debugowe wyswietlanie raycastu
        if (debugRay)
        {
            Color color = closestValidTarget ? Color.red : Color.yellow;
            Debug.DrawRay(origin, dir * detectionRange, color);
        }
    }

}
