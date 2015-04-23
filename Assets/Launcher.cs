using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {


    public GameObject missle;
    public GameObject launchTube1;
    public GameObject launchTube2;
    public GameObject axis;
    public GameObject baseAxis;


    /// <summary>
    /// Gets the 3D position of where the mouse cursor is pointing on a 3D plane that is
    /// on the axis of front/back and up/down of this transform.
    /// Throws an UnityException when the mouse is not pointing towards the plane.
    /// </summary>
    /// <returns>The 3d mouse position</returns>
	Vector3 GetMousePositionInPlaneOfLauncher () {
        Plane p = new Plane(transform.right, transform.position);
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        float d;
        if(p.Raycast(r, out d)) {
            Vector3 v = r.GetPoint(d);
            return v;
        }

        throw new UnityException("Mouse position ray not intersecting launcher plane");
	}


    static void DrawPlane(Vector3 up, Vector3 point, int s = 10)
    {
        var forward = Quaternion.AngleAxis(-90, Vector3.up)*up;
        var right = Quaternion.AngleAxis(-90, Vector3.forward)*up;

        //Gizmos.color = Color.green; Gizmos.DrawLine(point, point + up * s);
        //Gizmos.color = Color.blue; Gizmos.DrawLine(point, point + forward * s);
        //Gizmos.color = Color.red; Gizmos.DrawLine(point, point + right * s);

        Gizmos.DrawLine(point, point + up*s);
        for (int i = 1 - s; i < s; i++)
        {
            Gizmos.DrawLine(point - s * forward + right * i, point + s * forward + right * i);
            Gizmos.DrawLine(point - s * right + forward * i, point + s * right + forward * i);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        DrawPlane(transform.right, transform.position);

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(r.GetPoint(0), r.GetPoint(100));

        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, GetMousePositionInPlaneOfLauncher());
            Gizmos.DrawCube(GetMousePositionInPlaneOfLauncher(), Vector3.one);
        }

        Gizmos.color = Color.white;
        var tube = (shot % 1 == 0) ? launchTube1 : launchTube2;
        Gizmos.DrawLine(tube.transform.position, tube.transform.position + 10 * tube.transform.forward);
    }


    int shot = 0;
    public float cooldown = .5f;
    float currentCooldown = 0;
    void Shoot()
    {
        var tube = ((shot++ & (int)1) == 0) ? launchTube1 : launchTube2;
        GameObject.Instantiate(missle, tube.transform.position, tube.transform.rotation);
        currentCooldown = cooldown;
    }


    void Update()
    {
        var target = GetMousePositionInPlaneOfLauncher();

        axis.transform.LookAt(target, transform.up);

        currentCooldown -= Time.deltaTime;
        if (Input.GetMouseButton(0) && currentCooldown <= 0)
            Shoot();
    }

}
