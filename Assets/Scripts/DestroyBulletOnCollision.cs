using UnityEngine;

public class DestroyBulletOnCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<FireBulletsAtTarget>()) return;
        Destroy(gameObject);
    }
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
