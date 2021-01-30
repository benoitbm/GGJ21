using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noesis;

public class DCTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NoesisView panel = GetComponent<NoesisView>();
        Noesis.Grid root = (Noesis.Grid)panel.Content.FindName("ContentRoot");

        Noesis.UserControl xaml = (Noesis.UserControl)Noesis.GUI.LoadXaml("Assets/UI/Test.xaml");
        root.Children.Add(xaml);

        Noesis.UserControl xaml2 = (Noesis.UserControl)Noesis.GUI.LoadXaml("Assets/UI/UserControl1.xaml");
        root.Children.Add(xaml2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
