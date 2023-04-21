using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using static UnityEngine.GraphicsBuffer;

public class AgentControls : Agent
{
    private Rigidbody rb;
    private bool isGrounded;
    public float speedMultiplier = 0.1f;
    public float jumpForce = 7.0f;
    public float jumpTime = 0.1f;
    private float jumpTimer;
    private bool collides = false;
    public Transform coin;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // reset de positie en orientatie als de agent gevallen is of een collision meemaak
        if (this.transform.localPosition.y < 0 || collides)
        {
            this.transform.localPosition = new Vector3(-9.2f, 0.2f, 32.5f);
            this.transform.localRotation = Quaternion.identity;
        }

        // Verwijder alle obstacles met de tag "obstacle"
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        collides = false;
        isGrounded = true;

        // Geef de coin een nieuwe willekeurige positie
        coin.localPosition = new Vector3(-9.2f + Random.Range(-4f, 4f),
                                     0.5f, 32.5f + Random.Range(-4f, 4f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Voeg de positie van de agent toe aan de observaties
        sensor.AddObservation(this.transform.localPosition);
    }

    
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        this.transform.Translate(controlSignal * speedMultiplier);
        // Voeg een kracht toe aan de rigidbody om de agent te laten bewegen
        rb.AddForce(controlSignal * speedMultiplier, ForceMode.VelocityChange);

        RaycastHit hit;
        Vector3[] directions = { transform.forward, -transform.forward, transform.right, -transform.right, transform.up, -transform.up };
        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(transform.position, direction, out hit, 10f))
            {
                if (hit.collider.tag == "obstacle")
                {
                    // Spring alleen als de agent op de grond staat en de jump timer is verlopen
                    if (Mathf.Abs(this.transform.localPosition.y - 0.2f) < 0.1f && isGrounded && jumpTimer <= 0f)
                    {
                        AddReward(1.0f);
                        print("Jumped");
                        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        isGrounded = false;
                        jumpTimer = jumpTime;
                    }
                }
            }
        }


        // Verminder de jump timer
        if (!isGrounded)
        {
            jumpTimer -= Time.deltaTime;
        }

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, coin.localPosition);

        if (distanceToTarget < 1.42f)
        {
            AddReward(3.0f);
            print(GetCumulativeReward());
            coin.localPosition = new Vector3(-9.2f + Random.Range(-4f, 4f),
                                     0.5f, 32.5f + Random.Range(-4f, 4f));
        }

        // Verminder reward en begin nieuwe episode wanneer van platform gevallen of een collision
        if (this.transform.localPosition.y < 0 || collides)
        {
            AddReward(-1.0f);
            print(GetCumulativeReward());
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && isGrounded && jumpTimer <= 0f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpTimer = jumpTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
            jumpTimer = 0f;
        }
        if (collision.gameObject.tag == "obstacle" || collision.gameObject.tag == "floor")
        {
            collides = true;
        }
    }

}
