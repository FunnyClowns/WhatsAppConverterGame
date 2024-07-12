using UnityEngine;

public class MoveLoop : MonoBehaviour
{   
    private float frequency;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    void Start(){
        posOffset = this.gameObject.transform.position;

        float randomFreq = Random.Range(3f,5f);

        frequency = randomFreq;
        
   }

    void FixedUpdate(){
        tempPos = posOffset;
        tempPos.y += System.MathF.Sin(Time.fixedTime * Mathf.PI * frequency) * 1f;

        this.gameObject.transform.position = tempPos;

   }
}
