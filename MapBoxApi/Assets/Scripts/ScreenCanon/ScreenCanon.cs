using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class ScreenCanon : MonoBehaviour
{
    [SerializeField] private GameObject projetile;
    [SerializeField] private int maxAmmo = 5;
    [SerializeField] private float replenishTime = 1.0f;
    [SerializeField] private float shotForce = 100.0f;


    private int currentAmmo = 0;
    private static ScreenCanon instance;

    public int MaxAmmo { get { return maxAmmo; } }
    public int CurrentAmmo { get { return currentAmmo; } }
    public static ScreenCanon Instannce { get { return instance; } }



    // Start is called before the first frame update
    private void Awake()
    {
        Assert.IsNotNull(projetile);
        if(instance == null)
        {
            instance = this;
        }
        else if(instance !=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireShot(); 
        }
    }
    void Start()
    {
        StartCoroutine(ReplenishAmmo());
    }

    private IEnumerator ReplenishAmmo()
    {
        while(true)
        {
            currentAmmo = Mathf.Min(currentAmmo + 1, maxAmmo);
            yield return new WaitForSeconds(replenishTime);
        }
    }

    private void FireShot()
    {
        if(currentAmmo > 0  &&  !EventSystem.current.IsPointerOverGameObject())
        {
            GameObject newProjectile = Instantiate(projetile, transform.position, transform.rotation);
            Rigidbody projBody = newProjectile.GetComponent<Rigidbody>();
            projBody.AddForce(transform.forward * shotForce);
            currentAmmo--;
        }
    }
}
