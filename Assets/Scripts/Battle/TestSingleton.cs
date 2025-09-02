using UnityEngine;

public class TestSingleton : Singleton<TestSingleton>
{

    public void Shoot()
    { 
        Debug.Log("»ç°Ý");
    }
    
}
