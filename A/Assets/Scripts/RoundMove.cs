using UnityEngine;

public class RoundMove : MonoBehaviour
{
    public float speed;

    int dir;
    Rigidbody m_Rigidbody;
    Rigidbody player;
    Vector3 movePos;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        dir = -1;
    }


    void FixedUpdate()
    {
        movePos = transform.right * dir * speed * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(transform.position + movePos);
        if (player != null)
            player.MovePosition(player.position + movePos);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Side"))
        {
            dir *= -1;
        }

        if (collision.transform.CompareTag("Player"))
        {
            player = collision.rigidbody;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player = null;
        }
    }
}
