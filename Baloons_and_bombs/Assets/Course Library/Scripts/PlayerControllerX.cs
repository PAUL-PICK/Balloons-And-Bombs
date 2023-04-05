using UnityEngine;

public class PlayerControllerX : MonoBehaviour {

   public bool gameOver;

   public float floatForce;
   private float gravityModifier = 1.5f;
   private Rigidbody playerRb;

   //Bounds
   private float topBoundsY = 12.0f;
   private float bottomBoundsY = 2.08f;
   private bool isInBounds = true;

   //Effects 
   public ParticleSystem explosionParticle;
   public ParticleSystem fireworksParticle;
   // Sound
   private AudioSource playerAudio;
   public AudioClip moneySound;
   public AudioClip explodeSound;


   // Start is called before the first frame update
   void Start() {
      Physics.gravity *= gravityModifier;
      playerAudio = GetComponent<AudioSource>();
      playerRb = GetComponent<Rigidbody>();

      // Apply a small upward force at the start of the game
      playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

      }

   // Update is called once per frame
   void Update() {
      // if above topBounds
      if (transform.position.y >= topBoundsY) {

         isInBounds = false;
         floatForce = 0.0f;

         }
      else {
         isInBounds = true;
         floatForce = 50f;

         }

      // While space is pressed and player is low enough, float up
      if (Input.GetKey(KeyCode.Space) && !gameOver && isInBounds) {
         playerRb.AddForce(Vector3.up * floatForce);

         }



      }



   private void OnCollisionEnter(Collision other) {
      // if player collides with bomb, explode and set gameOver to true
      if (other.gameObject.CompareTag("Bomb")) {
         explosionParticle.Play();
         playerAudio.PlayOneShot(explodeSound, 1.0f);
         gameOver = true;
         Debug.Log("Game Over!");
         Destroy(other.gameObject);
         }
      // if player collides with money, fireworks
      else if (other.gameObject.CompareTag("Money")) {
         fireworksParticle.Play();
         gameOver = false;
         playerAudio.PlayOneShot(moneySound, 1.0f);
         Destroy(other.gameObject);
         }
      }
   }

