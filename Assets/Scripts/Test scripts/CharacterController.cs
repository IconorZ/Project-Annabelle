using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float maxBrzina = 5f;
	private bool facingRight = true;

	Animator anima;

	bool naZemlji = false;
	public Transform _naZemlji;
	float radiusDoZemlje = 0.2f;
	public LayerMask staJeZemlja;
	public float skokF = 500f;

	bool dupliSkok = false;

	void Start()
	{
		anima = GetComponent<Animator> ();
	}


	void FixedUpdate()
	{
		naZemlji = Physics2D.OverlapCircle (_naZemlji.position, radiusDoZemlje, staJeZemlja);
		anima.SetBool ("Ground", naZemlji);

		if (naZemlji) 
		{
			dupliSkok = false;
		}

		anima.SetFloat ("verticalSpeed", rigidbody2D.velocity.y);


		float move = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * maxBrzina, rigidbody2D.velocity.y);

		anima.SetFloat ("Speed", Mathf.Abs(move));

		if (move > 0 && !facingRight) {
			FlipX();
		} 
		else if (move < 0 && facingRight) 
		{
			FlipX();
		}
						
	}

	void Update()
	{
		if ((naZemlji || !dupliSkok) && Input.GetKeyDown (KeyCode.Space)) 
		{
			anima.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, skokF));

			if(!dupliSkok && !naZemlji)
			{
				dupliSkok = true;
			}
		}
					
	}

	void FlipX()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
