using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Creature : MonoBehaviour
{
    public Brain brain;
    private float lifetime = 10f;
    private Transform foodsTransform;

    private void Awake() {
        if (this.brain == null) {
            this.brain = new Brain(this);
        }
    }

    void Start()
    {
        this.brain.Mutate();
        foodsTransform = this.transform.parent.parent.GetChild(1);
        this.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));
    }

    void Update()
    {
        this.UpdateInputs();
        this.brain.Act();

        if (this.transform.position.x > 10f || this.transform.position.x < -10f) {
            this.transform.position = new Vector2(-this.transform.position.x + this.transform.position.x/Mathf.Abs(this.transform.position.x), this.transform.position.y);
        }
        if (this.transform.position.y > 5f || this.transform.position.y < -5f) {
            this.transform.position = new Vector2(this.transform.position.x, -this.transform.position.y + this.transform.position.y/Mathf.Abs(this.transform.position.y));
        }

        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            this.Die();
        }

        if(Input.GetKey("left")) {
            this.Turn(200);
        } 
        if (Input.GetKey("right")) {
            this.Turn(-200);
        } 
        if (Input.GetKey("up")) {
            this.MoveForward();
        } 
        if (Input.GetKey("down")) {
            this.MoveBackwards();
        }
        if (Input.GetKeyDown("m")) {
            this.brain.Mutate();
        }
    }

    public void UpdateInputs() {
        Transform closestFood = GetClosestFood();
        if (closestFood != null) {
            this.brain.inputs[0].UpdateInput(ClosestFoodDistance(closestFood));
            this.brain.inputs[1].UpdateInput(ClosestFoodAngle(closestFood));
        }
    }

    private List<Transform> GetAllFoods() {
        List<Transform> foods = new List<Transform>();
        foreach (Transform foodTransform in foodsTransform) foods.Add(foodTransform);
        return foods;
    }

    private List<Transform> GetAllFoodsInSight() {
        List<Transform> res = new List<Transform>();
        float sightLength = 20f;
        float sightWidth = 10f;
        Vector2 originPos = transform.position + transform.right * sightLength/2;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(originPos, new Vector2(sightLength, sightWidth), Vector2.SignedAngle(Vector2.right, transform.right), transform.right, 0f, LayerMask.GetMask("Food"));
        //ExtDebug.DrawBoxCastBox(originPos, new Vector2(sightWidth, sightLength), Quaternion.LookRotation(forward: Vector3.forward, upwards :transform.right), transform.right, 5f, Color.red);
        foreach (RaycastHit2D hit in hits) {
            res.Add(hit.transform);
        }
        return res;
    }

    private Transform GetClosestFood() {
        List<Transform> foods = this.GetAllFoodsInSight();
        Transform closestFood = null;
        float closestDistance = Mathf.Infinity;
        foreach (Transform potentialFood in foods) {
            Vector2 directionToFood = potentialFood.position - this.transform.position;
            float dSqrToFood = directionToFood.sqrMagnitude;
            if (dSqrToFood < closestDistance) {
                closestFood = potentialFood;
                closestDistance = dSqrToFood;
            }
        }
        return closestFood;
    }

    private float ClosestFoodAngle(Transform closestFood) {
        Vector2 dirToFood = closestFood.position - this.transform.position;
        Vector2 creatureDir = transform.right;
        return Vector2.SignedAngle(creatureDir, dirToFood);
    }

    private float ClosestFoodDistance(Transform closestFood) {
        return Vector2.Distance(this.transform.position, closestFood.position);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.collider.gameObject.tag == "food") {
            this.Eat(collisionInfo.collider.gameObject);
        }
    }

    public void Eat(GameObject food) {
        this.lifetime += food.GetComponent<Food>().nutritionValue;
        Destroy(food);
        this.GiveBirth();
    }

    public void Die() {
        Destroy(this.gameObject); 
    }

    private void GiveBirth() {
        Creature child = Instantiate(this, new Vector3(0f, 0f, 0f), Quaternion.identity);
        child.brain = new Brain(child, this.brain);
        child.transform.parent = this.transform.parent;
        child.transform.position = this.transform.position;
    }

    public void MoveForward() {
        this.transform.Translate(Vector3.right * Time.deltaTime);
    }

    public void MoveBackwards() {
        this.transform.Translate(-Vector3.right * Time.deltaTime);
    }

    public void TurnLeft() {
        this.transform.Rotate(0, 0, 20 * Time.deltaTime);
    }

    public void TurnRight() {
        this.transform.Rotate(0, 0, -20 * Time.deltaTime);
    }

    public void Turn(float turnValue) {
        this.transform.Rotate(0, 0, turnValue * Time.deltaTime);
    }

    public void OnDrawGizmos() {
        if (!Application.isPlaying) return;

        Vector2 pos = (Vector2)this.transform.position;

        for (int i = 0; i<this.brain.inputs.Count; i++) {
            InputNeuron fromNeuron = this.brain.inputs[i];

            for(int j = 0; j<this.brain.hiddenNeurons.Count; j++) {
                Neuron toNeuron = this.brain.hiddenNeurons[j];
                if (fromNeuron.IsConnected(toNeuron)) {
                    Gizmos.color = new Color(Math.Abs(Math.Min((float)fromNeuron.GetSynapse(toNeuron).weight, 0)), Math.Max((float)fromNeuron.GetSynapse(toNeuron).weight, 0), 0);
                    Gizmos.DrawLine(pos + new Vector2(-1, i), pos + new Vector2(0, 0.5f + j));
                }
            }
            for(int j = 0; j<this.brain.outputs.Count; j++) {
                Neuron toNeuron = this.brain.outputs[j];
                if (fromNeuron.IsConnected(this.brain.outputs[j])) {
                    Gizmos.color = new Color(Math.Abs(Math.Min((float)fromNeuron.GetSynapse(toNeuron).weight, 0)), Math.Max((float)fromNeuron.GetSynapse(toNeuron).weight, 0), 0);
                    Gizmos.DrawLine(pos + new Vector2(-1, i), pos + new Vector2(1, j));
                }
            }
        }

        for (int i = 0; i<this.brain.hiddenNeurons.Count; i++) {
            Neuron fromNeuron = this.brain.hiddenNeurons[i];
            for(int j = 0; j<this.brain.hiddenNeurons.Count; j++) {
                Neuron toNeuron = this.brain.hiddenNeurons[j];
                if (fromNeuron.IsConnected(toNeuron)) {
                    Gizmos.color = new Color(Math.Abs(Math.Min((float)fromNeuron.GetSynapse(toNeuron).weight, 0)), Math.Max((float)fromNeuron.GetSynapse(toNeuron).weight, 0), 0);
                    Gizmos.DrawLine(pos + new Vector2(0, 0.5f + i), pos + new Vector2(0, 0.5f + j));
                }
            }

            for(int j = 0; j<this.brain.outputs.Count; j++) {
                Neuron toNeuron = this.brain.outputs[j];
                if (fromNeuron.IsConnected(toNeuron)) {
                    Gizmos.color = new Color(Math.Abs(Math.Min((float)fromNeuron.GetSynapse(toNeuron).weight, 0)), Math.Max((float)fromNeuron.GetSynapse(toNeuron).weight, 0), 0);
                    Gizmos.DrawLine(pos + new Vector2(0, 0.5f + i), pos + new Vector2(1, j));
                }
            }
        }
        
        Gizmos.color = Color.gray;
        for (int i = 0; i<this.brain.inputs.Count; i++) {
            Gizmos.DrawSphere((Vector2)this.transform.position + new Vector2(-1, i), 0.1f);
        }
        for (int i = 0; i<this.brain.hiddenNeurons.Count; i++) {
            Gizmos.DrawSphere((Vector2)this.transform.position + new Vector2(0, 0.5f + i), 0.1f);
        }
        for (int i = 0; i<this.brain.outputs.Count; i++) {
            Gizmos.DrawSphere((Vector2)this.transform.position + new Vector2(1, i), 0.1f);
        }

        GUI.color = Color.black;
        Handles.Label(transform.position, "" + Math.Round(this.lifetime));
    }
}
