using UnityEngine;

public class RightClick : MonoBehaviour
{

    private Camera cam;
    public LayerMask layerMask;

        private LeftClick leftClick;
    public static RightClick instance;

   void Awake()
    {
        leftClick = GetComponent<LeftClick>();
    }

    void Start()
    {
        instance = this;
        cam = Camera.main;
        layerMask = LayerMask.GetMask("Ground", "Character", "Building");
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            TryCommand(Input.mousePosition);
        }
    }

    private void CommandToWalk(RaycastHit hit, Character c)
    {
        if (c != null)
            c.WalkToPosition(hit.point);

        CreateVFX(hit.point, VFXManager.instance.DoubleRingMarker);
    }

    private void TryCommand(Vector2 ScreenPos)
    {
        Ray ray = cam.ScreenPointToRay(ScreenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        { 
          switch (hit.collider.tag)
            {
                case "Ground":
                    CommandToWalk(hit, leftClick.CurChar);
                    break;
            }
        }
    }

    private void CreateVFX(Vector3 pos, GameObject vfxPrefad)
    {
        if (vfxPrefad == null)
            return;
        Instantiate(vfxPrefad,
            pos + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
    }


}
