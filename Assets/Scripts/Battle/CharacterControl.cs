using UnityEngine;
using UnityEngine.Splines;

public class CharacterControl : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject WeaponSlot;
    public GameObject Target;

    private SplineContainer throwPath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(Weapon != null)
        {
            Weapon.transform.parent = WeaponSlot.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowWeapon()
    {
        Weapon.transform.SetParent(null);
        Spline spline = throwPath.Spline;


        BezierKnot knot = new BezierKnot();
        knot.Position = new Vector3(10, 10, 0);
        spline.SetKnot(0, knot);
        spline.SetTangentMode(0, TangentMode.AutoSmooth);
//        Weapon.AddComponent<>


    }

    public void WeaponGrap(GameObject newWeapon)
    {
        Weapon = newWeapon;
        Weapon.transform.parent = WeaponSlot.transform;


    }

}
