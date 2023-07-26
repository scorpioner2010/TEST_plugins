using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Patterns.Others;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Patterns
{
    public class GameplayAssistantMember : MonoBehaviour
    {
        public void RunWithDelay(float time, Action logic) => StartCoroutine(StopDelay(time, logic));
        private IEnumerator StopDelay(float time, Action logic)
        {
            yield return new WaitForSeconds(time);
            logic.Invoke();
        }
    }
    
    public static class GameplayAssistant
    {
        private static System.Random _random = new();
        
        public static void RunWithDelay(float time, Action logic)
        {
            Singleton<GameplayAssistantMember>.Instance.RunWithDelay(time, logic);
        }

        public static bool HasLayer(this LayerMask layerMask, int layer)
        {
            if (layerMask == (layerMask | (1 << layer)))
            {
                return true;
            }

            return false;
        }
        
        public static float Remap(float value, float startMin, float startMax, float resultMin, float resultMax, bool clamp = false)
        {
            if (clamp) value = Mathf.Clamp(value, startMin, startMax);
            return (value - startMin) / (startMax - startMin) * (resultMax - resultMin) + resultMin;
        }

        public static string GenerateName(int len)
        {
            System.Random r = new System.Random();
            
            string[] consonants =
            {
                "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v",
                "w", "x"
            };
            
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string name = "";
            name += consonants[r.Next(consonants.Length)].ToUpper();
            name += vowels[r.Next(vowels.Length)];
            int b = 2;
            
            while (b < len)
            {
                name += consonants[r.Next(consonants.Length)];
                b++;
                name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return name;
        }
        
        public static string GetRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        
        #if UNITY_EDITOR
        public static void DrawArrow(Vector3 a, Vector3 b, float arrowheadAngle, float arrowheadDistance, float arrowheadLength, Color color, float widthLine)
        {
            Vector3 dir = b - a;
            Vector3 arrowPos = a + (dir * arrowheadDistance);

            Vector3 up = Quaternion.LookRotation(dir) * new Vector3(0f, Mathf.Sin(arrowheadAngle / 72), -1f) * arrowheadLength;
            Vector3 down = Quaternion.LookRotation(dir) * new Vector3(0f, -Mathf.Sin(arrowheadAngle / 72), -1f) * arrowheadLength;
            Vector3 left = Quaternion.LookRotation(dir) * new Vector3(Mathf.Sin(arrowheadAngle / 72), 0f, -1f) * arrowheadLength;
            Vector3 right = Quaternion.LookRotation(dir) * new Vector3(-Mathf.Sin(arrowheadAngle / 72), 0f, -1f) * arrowheadLength;

            Vector3 upPos = arrowPos + up;
            Vector3 downPos = arrowPos + down;
            Vector3 leftPos = arrowPos + left;
            Vector3 rightPos = arrowPos + right;

            Handles.color = color;
            Handles.DrawLine(a, b, widthLine);
            Handles.DrawLine(arrowPos, upPos, widthLine);
            Handles.DrawLine(arrowPos, leftPos, widthLine);
            Handles.DrawLine(arrowPos, downPos, widthLine);
            Handles.DrawLine(arrowPos, rightPos, widthLine);

            Handles.DrawLine(upPos, leftPos, widthLine);
            Handles.DrawLine(leftPos, downPos, widthLine);
            Handles.DrawLine(downPos, rightPos, widthLine);
            Handles.DrawLine(rightPos, upPos, widthLine);
        }
        #endif

        public static List<T> GetObjectsByDistance<T>(List<T> objects, Vector3 pos, float distance , MonoBehaviour exception = null)
        {
            List<T> all = new List<T>();
            
            foreach (T obj in objects)
            {
                MonoBehaviour behaviour = obj as MonoBehaviour;
                float dis = Vector3.Distance(behaviour.transform.position, pos);
                
                if (exception != null)
                {
                    if (dis < distance && behaviour != exception)
                    {
                        all.Add(obj);
                    }
                }
                else
                {
                    if (dis < distance)
                    {
                        all.Add(obj);
                    }
                }
            }

            return all;
        }

        public static Vector3 ClosestPoint(Vector3 origin, Vector3 end, Vector3 point) 
        { 
            Vector3 heading = (end - origin); 
            float magnitudeMax = heading.magnitude; 
            heading = heading.normalized; 
            Vector3 lhs = point - origin; 
            float dotP = Vector3.Dot(lhs, heading); 
            dotP = Mathf.Clamp(dotP, 0f, magnitudeMax); 
            return origin + heading * dotP; 
        }

        public static bool PresenceObjectOnScreen(Camera camera, Vector3 obj)
        {
            Vector3 viewportPoint = camera.WorldToViewportPoint(obj);
            return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
        }
        
        public static PointInfo CalculatePath(Transform mainObj, float distance, LayerMask ignoredLayers)
        {
            Vector3 position = mainObj.position;
            Vector3 forward = mainObj.forward;
            Vector3 up = mainObj.up;
            Vector3 normal = Vector3.zero;

            for (int i = 0; i < distance; i++)
            {
                bool resultRayCast = Physics.Raycast(position + up, forward, out RaycastHit hitForward, 1);

                if (resultRayCast)
                {
                    bool result = ignoredLayers.HasLayer(hitForward.transform.gameObject.layer);
                    resultRayCast = !result;
                }
                
                if (!resultRayCast)
                {
                    Vector3 startPosition = position + forward + up;
                    Vector3 directionRayCast = up.normalized * -1;

                    bool result = Physics.Raycast(startPosition, directionRayCast, out RaycastHit hit, 5);
            
                    if (result)
                    {
                        normal = hit.normal;
                        position = hit.point;
                        Vector3 C = Vector3.Cross(mainObj.right, hit.normal);
                        forward = C;
                        up = hit.normal;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            PointInfo info = new PointInfo(position, normal);
            return info;
        }
        
        public static void CreateSphereDots(Vector3 pos, float size, int amountDots = 1000, float lifeTime = 6, float sizeDot = 0.5f)
        {
            for (int i = 0; i < amountDots; i++)
            {
                Vector3 c = pos + Random.onUnitSphere * size;
                CreateSphere(Color.blue, sizeDot, c, lifeTime);
            }
        }

        public static void CreateSphere(Color color, float size, Vector3 pos, float lifeTime = 6)
        {
            GameObject f = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            f.transform.position = pos;
            f.transform.localScale = Vector3.one * size;
            f.GetComponent<MeshRenderer>().material.color = color;
            GameObject.Destroy(f.GetComponent<Collider>());
            GameObject.Destroy(f, lifeTime);
        }
        
        public static void DrawLine(Vector3 from, Vector3 to, Color color, float lifeTime = 6)
        {
            Vector3 dir2 = (to - from).normalized;
            float dis = Vector3.Distance(to, from);
            GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gm.GetComponent<MeshRenderer>().material.color = color;
            gm.GetComponent<Collider>().enabled = false;
            gm.transform.position = from;
            gm.transform.position += dir2 * (dis / 2);
            gm.transform.LookAt(from);
            gm.transform.localScale = new Vector3(.2f, .2f, dis);
            GameObject.Destroy(gm, lifeTime);
        }

        public static void DrawCube(Vector3 pos, Color color, float scale, float lifeTime = 6)
        {
            GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gm.GetComponent<MeshRenderer>().material.color = color;
            gm.transform.position = pos;
            gm.GetComponent<Collider>().enabled = false;
            gm.transform.localScale = Vector3.one * scale;
            GameObject.Destroy(gm, lifeTime);
        }
    }
    
    public struct PointInfo
    {
        public Vector3 LastHit;
        public Vector3 Normal;

        public PointInfo(Vector3 lastHit, Vector3 normal)
        {
            LastHit = lastHit;
            Normal = normal;
        }
    }
}