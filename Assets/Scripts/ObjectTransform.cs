using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Renderer)),RequireComponent(typeof(NavMeshAgent))]
public class ObjectTransform : MonoBehaviour
{

    ParticleSystem _particleSystem;
    Color beforeColor;
    RaycastHit hit;
    Vector3 newPosition;
    Quaternion newRotation;
    NavMeshAgent _navMeshAgent;
    public float speed=2;
    public int quantityMilitary = 15;
    public bool selected = false;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        newPosition = transform.position;
    }

    void OnMouseDown()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.yellow);
    }

    private void OnMouseEnter()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend.material.GetColor("_Color") != Color.red && rend.material.GetColor("_Color") != Color.yellow)
        {
            beforeColor = rend.material.GetColor("_Color");
            rend.material.SetColor("_Color", Color.red);

        }
        else if (rend.material.GetColor("_Color") != Color.yellow)
        {
            rend.material.SetColor("_Color", Color.red);
        }
    }

    private void OnMouseExit()
    {
       
        Renderer rend = GetComponent<Renderer>();
        if (rend.material.GetColor("_Color") == Color.red)
        {
            rend.material.SetColor("_Color", beforeColor);
        }
    }

    private void OnMouseUp()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", beforeColor);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            if (hit.transform.name == gameObject.transform.name)
            {
                Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 100);
            }

            newRotation = Quaternion.LookRotation(hit.point - transform.position);

            newPosition = hit.point+ new Vector3(0,1,0);
        }
    }
    private void Update()
    {
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (selected)
                {
                    this.OnMouseUp();
                    selected = false;
                }
            }
            _navMeshAgent.SetDestination(newPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 10 * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 canPos = Camera.main.WorldToScreenPoint(transform.position);
                canPos.y = BoxSelection.InvertMouseY(canPos.y);
                selected = BoxSelection.selection.Contains(canPos);
            }
            if (selected)
            {
                this.OnMouseEnter();
            }else
            {
                this.OnMouseExit();
            }
        }
    }
}
