using UnityEngine;

namespace MelenitasDev
{
	public class AdventurerController : MonoBehaviour
	{
		// ----- Serialized Fields

		[Header("Settings")]
		[SerializeField] private float speed;
		[SerializeField] private float jumpHeith;
		
		// ----- Fields
		private Rigidbody2D rb;
		private float xInput;
		public Transform groundcheck;
		public bool isGrounded;
		public float groundcheckRadius;
		public LayerMask whatIsGround;
		Animator anim;

		// ----- Unity Callbacks
		void Start ()
		{
			// Referenciamos el RigidBody2D adjunto al personaje.
			rb = GetComponent<Rigidbody2D>();
			anim = GetComponent<Animator>();
		}

		void Update ()
		{
			isGrounded = Physics2D.OverlapCircle(groundcheck.position,groundcheckRadius,whatIsGround);
			// Llamamos a la función que gira al personaje.
			if (isGrounded)
			{
				anim.SetBool("Jump",false);
			}
			else
			{
				anim.SetBool("Jump",true);
			}

			Flip();
			Attack_1();
			Attack_2();


		}
		
		void FixedUpdate ()
		{
			// Llamamos al control de movimiento
			HandleMovement();
			Jump();
		}

		public void Attack_1()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				anim.SetBool("Attack_1",true);
			}
			else
			{
				anim.SetBool("Attack_1",false);
			}

		}

		public void Attack_2()
		{
			if (Input.GetButtonDown("Fire2"))
			{
				anim.SetBool("Attack_2",true);
			}
			else
			{
				anim.SetBool("Attack_2",false);
			}

		}


		// ----- Private Methods
		private void HandleMovement ()
		{
			// Capturamos las teclas ("A" y "D") y ("←" y "→") para conocer la dirección.
			xInput = Input.GetAxisRaw("Horizontal");

			// Creamos un vector para el movimiento horizontal multiplicando la dirección por la velocidad.
			// En el eje vertical mantenemos la velocidad del Rigidbody.
			Vector2 move = new Vector2(xInput * speed, rb.velocity.y);

			// Le pasamos el movimiento a la velocidad del Rigidbody.
			rb.velocity = move;

			if (rb.velocity.x != 0)
			{
				anim.SetBool("Walk",true);
			}
			else
			{
				anim.SetBool("Walk",false);
			}
		}

		private void Flip ()
		{
			// Si el personaje se mueve a la derecha...
			if (xInput > 0 && transform.localScale.x < 0)
			{
				// Ponemos la escala en X en positivo (Recuerda cambiar los unos por la escala de tu personaje).
				transform.localScale = new Vector3(1, 1, 1);
			}
			// Si el personaje se mueve a la izquierda...
			else if (xInput < 0 && transform.localScale.x > 0)
			{
				// Ponemos la escala en X en negativo (Recuerda cambiar los unos por la escala de tu personaje).
				transform.localScale = new Vector3(-1, 1, 1);
			}
		}

		private void Jump ()
		{
			if (Input.GetButton("Jump")&& isGrounded )
			{
				rb.velocity= new Vector2 (rb.velocity.x,jumpHeith);
			}
		}

	}
}
