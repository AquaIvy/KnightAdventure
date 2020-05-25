using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightAdventure
{
    public static class Graphic
    {
        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            Debug.DrawLine(start, end, color);
        }


        public static void DrawCircle(Vector3 point, float radius, Color color)
        {

            for (int i = 0; i < 360; i += 6)
            {
                float x1 = point.x + radius * Mathf.Cos(i);
                float y1 = point.y + radius * Mathf.Sin(i);

                float x2 = point.x + radius * Mathf.Cos(i + 6);
                float y2 = point.y + radius * Mathf.Sin(i + 6);
                Debug.DrawLine(new Vector3(x1, y1, 0), new Vector3(x2, y2, 0), color);

            }
        }
    }
}
