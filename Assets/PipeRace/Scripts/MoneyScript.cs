using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public AudioClip coin;
    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, hazardMove.speed);
        if (transform.position.z < -30)
            Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(coin);
            PlayerPrefs.SetInt("WITgameO", PlayerPrefs.GetInt("WITgameO") + 3);
        }
    }
}
