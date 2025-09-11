using UnityEngine;
using UnityEngine.Splines;

public class TestSplineInit : MonoBehaviour
{
    public GameObject GameObject1;
    public GameObject GameObject2;  
    public GameObject GameObject3;


    private Spline spline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spline = GetComponent<SplineContainer>().Spline;
        SplineData<Object> data;
        data = new SplineData<Object>();


            
//        data.Add(GameObject1);
        spline.SetObjectData("Knot", data);
        /*
        spline.SetObjectData("2", GameObject2);
        spline.SetObjectData("3", GameObject3);
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
}
