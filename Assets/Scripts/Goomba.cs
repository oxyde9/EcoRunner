using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
     public int pointsOnKill = 100;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
            } else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }

     private void Flatten()
    {
      Collider2D collider = GetComponent<Collider2D>();
    if (collider != null) collider.enabled = false;

    EntityMovement movement = GetComponent<EntityMovement>();
    if (movement != null) movement.enabled = false;

    AnimatedSprite sprite = GetComponent<AnimatedSprite>();
    if (sprite != null) sprite.enabled = false;

    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
    if (renderer != null) renderer.sprite = flatSprite;

      // Add points to player score
        Player_Score.Instance.AddPoints(pointsOnKill);

        Destroy(gameObject, 0.5f);
    }

    public void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

}
