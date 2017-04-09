using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Bird : MonoBehaviour {
  private Vector3 mousePosition;
  private float moveSpeed = 0.3f;
  public Ball ball;
  public SkeletonAnimation skeletonAnimation;
  private Vector2 ballPositionOffset = Vector2.zero;

	// Use this for initialization
	void Start () {
		skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
    mousePosition = Input.mousePosition;
    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    if(ball) {
      if(!Input.GetMouseButton(0) || ball.IsSold) { //Drop the ball
        Vector2 nextFrame = Vector2.Lerp(ball.transform.position, mousePosition, moveSpeed);
        nextFrame -= Util.Make2D(ball.transform.position);
        ball.DroppedByBird(nextFrame);
        ball = null;
         skeletonAnimation.state.SetAnimation(0, "flap", true);
      } else {
        ball.transform.position = ballPositionOffset + Vector2.Lerp(transform.position, mousePosition, moveSpeed);
      }
    }
	}

  void OnTriggerStay2D(Collider2D collider) {
    if(Game.Instance.GameOver) {
      return;
    }

    if(Input.GetMouseButton(0) && !ball) { //Left click, check for a ball if you don't have one already
      ball = collider.gameObject.GetComponent<Ball>();
      if(ball && !ball.IsSold) {
        ballPositionOffset = Util.Make2D(ball.transform.position - transform.position);
        ball.PickedUpByBird();
        skeletonAnimation.state.SetAnimation(0, "flap-withgem", true);
      }
    }
  }
}
