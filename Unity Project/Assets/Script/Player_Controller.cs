using UnityEditor;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float movespeed = 10;

    private Interest _interest;

    [SerializeField]
    private Vector3 _destTransform;

    [SerializeField] 
    private Vector3 _moveVector;

    private Vector3 _distVector, resultant;

    [SerializeField] 
    private Vector3 _conflictVector;

    private GameObject interestObject;

    private float remainingDistance;
    // Start is called before the first frame update
    void Start()
    {
        _destTransform = transform.position;
        remainingDistance = Vector3.Distance(transform.position, _destTransform);
    }

    // Update is called once per frame
    void Update()
    {
        _distVector.y = 0.5f;
        transform.position = Vector3.Lerp(transform.position, _distVector, Time.deltaTime * movespeed);
        GetVectors();
    }

    void GetVectors()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;

            interestObject = null;
            if (playerPlane.Raycast(ray, out hitdist))
            {
                Collider[] hitColliders = Physics.OverlapSphere(ray.GetPoint(hitdist), 1);

                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].CompareTag("Interest"))
                    {
                        interestObject = hitColliders[i].gameObject;
                        break;
                    }
                }

                if (interestObject == null)
                    return;

                _interest = interestObject.GetComponent<Interest>();
                _destTransform = interestObject.transform.position;
                _moveVector = _destTransform - transform.position;
                
                Debug.DrawLine(transform.position, _destTransform,Color.black, 10.0f);
                
                float tempAngle = Vector3.SignedAngle(transform.right, _moveVector,transform.up);
                float tempVar = _interest.interestRatio / 100.0f;
                
                _conflictVector = Quaternion.AngleAxis(-2 * tempAngle, Vector3.up) * (_moveVector);
                _moveVector = _moveVector * tempVar;
                _conflictVector = _conflictVector * (1 - tempVar);
                
                Debug.DrawLine(transform.position, _conflictVector+transform.position,Color.red,10.0f);
                Debug.DrawLine(transform.position, transform.position+transform.right*5f, Color.blue, 10.0f);
                resultant = ((_moveVector) + (_conflictVector));
                Debug.DrawLine(transform.position, resultant + transform.position, Color.green, 10.0f);
                _distVector = resultant + transform.position;
            }
        }
    }
}