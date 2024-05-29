using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Person
{
    public void SayHello()
    {
        Debug.Log("Hello");
    }
}

public class GameInstaller : MonoBehaviour
{
    private string? possibleInt = null;
    
    
    private void XD(Person? possibleI)
    {
        possibleI.SayHello();
    }
}