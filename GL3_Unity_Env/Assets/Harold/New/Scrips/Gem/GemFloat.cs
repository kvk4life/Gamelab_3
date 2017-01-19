using UnityEngine;
using System.Collections;

public class GemFloat : MonoBehaviour {

    private Vector3 startPosition;
    public float speed, maxMoveAmountHorizontal, maxMoveAmountVertical;
    public float rotateSpeed, minWaitTime, maxWaitTime, rotateTime; //is for rotating the wall
    private Quaternion beforeRotate, afterRotate;
    private float waitTime, time;
    private bool turned, check, right, up;
    public bool rotate, move;

    void Start() {
        startPosition = transform.position;
        right = true;
        up = true;
    }

    void Update() {
        if (rotate) {
            Rotate();
        }

        if (move){
            MoveAround();
        }
    }

    void MoveAround() {
        if (right) {
            Vector3 temp;
            float maxMove = startPosition.x;
            maxMove += maxMoveAmountHorizontal;
            temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
            if(transform.position.x >= maxMove) {
                right = false;

            }
        }
        else {
            Vector3 temp;
            float maxMove = startPosition.x;
            maxMove -= maxMoveAmountHorizontal;
            temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;

            if (transform.position.x <= maxMove) {
                right = true;

            }
        }

        if (up) {
            Vector3 temp;
            float maxMove = startPosition.y;
            maxMove += maxMoveAmountVertical;
            temp = transform.position;
            temp.y += speed * Time.deltaTime;
            transform.position = temp;

            if (transform.position.y >= maxMove) {
                up = false;

            }
        }
        else {
            Vector3 temp;
            float maxMove = startPosition.y;
            maxMove -= maxMoveAmountVertical;
            temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            transform.position = temp;

            if (transform.position.y <= maxMove) {
                up = true;

            }
        }
    }

    void Rotate() {
        if (!check) { //does this at the start to save the rotations into variables.
            beforeRotate = transform.rotation;
            afterRotate = transform.rotation;
            afterRotate.y += 90;
            check = true;
        }

        if (waitTime >= 0) { //your basic timer
            waitTime -= Time.deltaTime;
        }
        else {
            if (turned) {// here the wall will be turned
                transform.rotation = Quaternion.Lerp(transform.rotation, beforeRotate, Time.deltaTime * rotateSpeed);
                time -= Time.deltaTime;

                if (time <= 0) {//at the end of a lerp it slows down alot, so i made a maximum timer it can spent turning
                    transform.rotation = beforeRotate;
                }

                if (transform.rotation == beforeRotate) {//wait time bewteen rotates
                    turned = false;
                    waitTime = Random.Range(minWaitTime, maxWaitTime);
                }
            }
            else {//sets it to the original turn position so it can turn again
                transform.rotation = afterRotate;
                turned = true;
                time = rotateTime;
            }
        }
    }
    
}
