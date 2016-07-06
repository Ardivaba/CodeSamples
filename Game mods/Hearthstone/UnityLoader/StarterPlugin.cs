using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityLoader
{
    [Plugin]
    public class StarterPlugin : MonoBehaviour
    {
        void Start()
        {
        }

        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 200), SceneMgr.Get().GetScene().name);
        }
    }
}
