using UnityEngine;
using Random = UnityEngine.Random;

public class DropManager : MonoBehaviour
{
    public GameObject Blaster, Minigun, X, Ammo, DrogaX, DrogaY;
    public static GameObject _Blaster, _Minigun, _X, _Ammo, _DrogaX, _DrogaY;
    static int whatToSpawn;

    public void Start()
    {
        _Blaster = Blaster;
        _Minigun = Minigun;
        _X = X;
        _Ammo = Ammo;
        _DrogaX = DrogaX;
        _DrogaY = DrogaY;
    }

    public static void Drop(GameObject caller)
    {
        /*Armas y Items 
         * 
         * Basic Blaster [Arma 1
         * Minigun [Arma 2
         * X [Arma 3
         * Ammo [Item 4
         * Droga X [Item 5 // Sube HP
         * Droga Y [Item 6 // Sube Daño temporalmente
         */



        whatToSpawn = Random.Range(1,5); //Valor Maximo no se incluye (1,3 = [1 - 2])

        switch (whatToSpawn)
        {
            case 1: /* Spawnear Basic Blaster [Arma 1 */
                Instantiate(_Blaster, caller.transform.position, Quaternion.identity);
                Debug.Log("Spawneando: '" + _Blaster + "' En pos X,Y,Z " + caller.transform.position);
                break;
            case 2: /* Spawnear Minigun [Arma 2 */
                Instantiate(_Minigun, caller.transform.position, Quaternion.identity);
                Debug.Log("Spawneando: '" + _Minigun + "' En pos X,Y,Z " + caller.transform.position);
                break;
            case 3: /* Spawnear Arma X [Arma 3 */
                Instantiate(_X, caller.transform.position, Quaternion.identity);
                Debug.Log("Spawneando: '" + _X + "' En pos X,Y,Z " + caller.transform.position);
                break;
            case 4: /* Spawnear Ammo [ Item ID 4 */
                Instantiate(_Ammo, caller.transform.position, Quaternion.identity);
                Debug.Log("Spawneando: '" + _Ammo + "' En pos X,Y,Z " + caller.transform.position);
                break;
        }
    }

}
