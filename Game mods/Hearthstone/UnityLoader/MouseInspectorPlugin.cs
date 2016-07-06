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
    public class MouseInspectorPlugin : MonoBehaviour
    {
        GUIStyle style = new GUIStyle() { fontStyle = FontStyle.Bold };

        int x = 0;
        int y = 0;

        int page = 0;

        void OnGUI()
        {
            if (page == 0)
                Page0();
            if (page == 1)
                Page1();
        }

        void SpeedHack()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Time.timeScale = 1.0f;
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                Time.timeScale = 5.0f;
            }
        }

        public static string data = "No magic yetss";

        void MovePages()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                y--;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                y++;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                x--;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                x++;
            }
        }

        void Page0()
        {
            MovePages();


            GUI.color = Color.magenta;

            GUI.Label(new Rect(x + 10, y + 10, 200, 200), "Actives", style);
            GUI.Label(new Rect(x + 200 + 10, y + 10, 200, 200), SceneMgr.Get().GetScene().name, style);
            GUI.Label(new Rect(x + 400 + 10, y + 10, 200, 200), data, style);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray, 500.0f);
            int i = 0;
            foreach (RaycastHit hit in hits)
            {
                GUI.Label(new Rect(x + 10 + (200 * i), y + 20, 200, 200), hit.transform.name, style);
                Component[] components = hit.transform.GetComponents(typeof(MonoBehaviour));

                int j = 0;
                foreach (Component component in components)
                {
                    if (component is CollectionCardVisual)
                    {
                        Magic((CollectionCardVisual)component);
                    }

                    GUI.Label(new Rect(x + 20 + (200 * i), y + 30 + (10 * j), 200, 200), component.GetType().Name, style);
                    j++;
                }
                i++;
            }
        }

        void Page1()
        {
            //GameObject[] gameObjects 
        }

        void Magic(CollectionCardVisual card)
        {
            data = card.GetActor().GetEntityDef().GetDebugName();
        }

        int counter = 0;
        void Update()
        {
            EmoteOption[] options = GameObject.FindObjectsOfType<EmoteOption>();
            if (Input.GetKeyDown(KeyCode.F1))
                options[0].DoClick();
            if (Input.GetKeyDown(KeyCode.F2))
                options[2].DoClick();
            if (Input.GetKeyDown(KeyCode.F3))
                options[3].DoClick();
            if (Input.GetKeyDown(KeyCode.F4))
                options[4].DoClick();
            if (Input.GetKeyDown(KeyCode.F5))
                options[5].DoClick();
            if (Input.GetKeyDown(KeyCode.F6))
                options[6].DoClick();
            if (Input.GetKeyDown(KeyCode.F7))
                options[7].DoClick();
        }
    }
}