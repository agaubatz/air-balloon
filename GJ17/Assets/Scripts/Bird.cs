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
  public ContactFilter2D PickupFilter;

  private Collider2D[] _collisions = new Collider2D[25];

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

    if (Game.Instance.IsGameGoing())
    {
      if(Input.GetMouseButton(0) && !ball) { //Left click, check for a ball if you don't have one already
        int numCollisions = GetComponent<Collider2D>().OverlapCollider(PickupFilter, _collisions);
        float minDistance = 999999f;
        for (int i = 0; i < numCollisions; i++)
        {
          var curBall = _collisions[i].gameObject.GetComponent<Ball>();
          if(curBall && !curBall.IsSold) {
            float dist = Vector3.Distance(transform.position, curBall.transform.position);
            if (dist < minDistance)
            {
              ball = curBall;
            }
          }
        }

        if (ball)
        {
          ballPositionOffset = Util.Make2D(ball.transform.position - transform.position);
          ball.PickedUpByBird();
          skeletonAnimation.state.SetAnimation(0, "flap-withgem", true);
        }
      }
    }
	}
}
