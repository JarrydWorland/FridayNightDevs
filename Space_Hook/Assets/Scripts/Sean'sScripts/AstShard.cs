using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstShard : MonoBehaviour {

    public SpriteRenderer m_SpriteR;
    public Sprite s;
    public Rigidbody2D m_rigidbody2D;
    public Collider2D m_collider2D;
	// Use this for initialization
	void Start ()
    {
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_collider2D = gameObject.AddComponent<BoxCollider2D>();
        m_SpriteR = gameObject.AddComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this);
    }
    // Update is called once per frame
    void Update () {
        if(m_SpriteR.sprite == null )
        {
            m_SpriteR.sprite = s;
        }
	}
}
