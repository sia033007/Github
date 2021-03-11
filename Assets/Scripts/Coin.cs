using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinPrefab;
    public static int point;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstanciateCoin());

        Character character1 = new Character("Amin","Male","Iran",27);
        Character character2 = new Character("Jack","Male","America",30);
        Character character3 = new Character("Sylvia","Female","Russia",25);

        character1.person();
        character2.person();
        character3.person();

        Weapon weapon1 = new Weapon("Klash",100,50);
        Weapon weapon2 = new Weapon("Kolt",50,20);

        Debug.Log(weapon1.name);
        Debug.Log(weapon2.name);

        weapon1.myWeapon();
        weapon2.myWeapon();
        
    }
    private void OnGUI() {
        GUI.Box(new Rect(430,20,100,25),"Score : " + point);
        GUI.color = Color.yellow;
    }
    IEnumerator InstanciateCoin()
    {
        GameObject myCoin = Instantiate(coinPrefab,new Vector3(Random.Range(-4,4.37f),0.41f,Random.Range(-3.85f,4)),Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(2f);
        Destroy(myCoin.gameObject);
        StartCoroutine(InstanciateCoin());
    }
}
public class Weapon
{
    public string name;
    public int bullet;
    public int price;

    public Weapon(string myName, int myBullet, int myPrice)
    {
        name = myName;
        bullet = myBullet;
        price = myPrice;
    }
    public void myWeapon()
    {
        Debug.Log($"This weapon is {name} , With {bullet} bullets and {price} dollars");
    }
}
public class Character
{
    public string name;
    public string gender;
    public string nationality;
    public int age;

    public Character(string myName, string myGender, string myNationality, int myAge)
    {
        name = myName;
        gender = myGender;
        nationality = myNationality;
        age = myAge;
    }
    public void person()
    {
        Debug.Log($"My name is {name}, I'm a {gender}, {age} years old from {nationality}");
    }
}
