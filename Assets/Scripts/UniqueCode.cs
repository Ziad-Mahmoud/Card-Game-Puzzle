using TMPro;
using UnityEngine;
using static System.IO.File;
using static UnityEngine.Application;
public class UniqueCode : MonoBehaviour
{
    private async void Awake()
    {
        string path = persistentDataPath + "/uniqueCode.txt";
        if (!Exists(path))
        {
            string code = string.Empty;
            for (int i = 0; i < 4; i++) 
                code += (char)Random.Range(65, 91);
            await WriteAllTextAsync(path, code);
        }
        GetComponent<TextMeshProUGUI>().text = await ReadAllTextAsync(path);
    }
}
