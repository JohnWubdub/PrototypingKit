using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script by Gabe Cuzzillo
public class Geo : MonoBehaviour 
{
​    //a collection of incredibly useful magic math functions
     //use these for movement and whatever you need
     
    public static float CosLaw(float a, float b, float c) {
        return Mathf.Acos(Mathf.Clamp((Mathf.Pow(a, 2) + Mathf.Pow(b, 2.0f) - Mathf.Pow(c, 2.0f)) /(2.0f * a * b), - 1.0f, 1.0f)) * Mathf.Rad2Deg;
    }
​
    public static Vector2 SnapTo(Vector2 a, float b) {
        return new Vector2(RndTo(a.x, b), RndTo(a.y, b));
    }
​
    public static float RndTo(float a, float b) {
        return Mathf.Round(a / b) * b;
    }
​
    public static float ToAng(Vector2 a) {
        return Geo.Degreed(Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg);
    }
​
    public static float ToAng(Vector2 a, Vector2 b) {
        return ToAng(b - a);
    }
​
    public static Vector2 AddDegrees(Vector2 a, float b) {
        return ToVect(ToAng(a) + b);
    }
​
    public static Vector2 ToVect(float a) {
        return new Vector2(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad));
    }
​
    public static float AngDist(float ang0, float ang1) {
        ang0 = Geo.Degreed(ang0);
        ang1 = Geo.Degreed(ang1);
        float tmp = Mathf.Abs(ang1 - ang0);
        if(tmp > 180.0f) {
            tmp = Mathf.Abs(tmp - 360.0f);
        }
        return tmp;
    }
​
    /*public static float AngDelta(float ang0, float ang1) {
        return Mathf.DeltaAngle(ang0, ang1);
    }*/
​
    public static Vector2 MidPoint(Vector2 a, Vector2 b) {
        return b +((a - b).normalized *((a - b).magnitude / 2.0f));
    }
​
​
    public static Vector3 ToV3(Vector2 v2) {
        return v2;
    }
​
    public static Vector2 ToV2(Vector3 v3) {
        return v3;
    }
​
    public static float Angle(Vector2 v0, Vector2 v1) {
        return Degreed(Mathf.Atan2((v1 - v0).y,(v1 - v0).x) * Mathf.Rad2Deg);
    }
​
    public static float Angle(GameObject a, GameObject b) {
        return Angle(a.transform.position, b.transform.position);
    }
​
    public static bool IsBetween(float a, float b, float c) {
        a = Degreed(a);
        b = Degreed(b);
        c = Degreed(c);
​
​
        if(Mathf.Abs(b - c)>180.0f) {
            return a<= Mathf.Min(b, c) || a>= Mathf.Max(b, c);
        }
        else {
            return a>= Mathf.Min(b, c) && a<= Mathf.Max(b, c);
        }
    }
​
    public static bool IsNumBetween(float a, float b, float c) {
        return(a>b && a< c);
    }
    public static int Mod(int x, int m) {
        return (x % m + m) % m;
    }
    public static float Mod(float a, float b) {
        if(b< 0.0f) {
            Debug.Log("MOD IS BEING GIVEN A NEGATIVE");
        }
        if(b == 0.0f) {
            Debug.Log("Mod given zero");
            return 0.0f;
        }
        while(a<= 0.0f) {
            a += b;
        }
        while(a>= b) {
            a -= b;
        }
        return a;
    }
​
    public static float Degreed(float a) {
        return Geo.Mod(a, 360.0f);
    }
​
    public static bool IsLeft(Vector2 a, Vector2 b, Vector2 c) {
        return((b.x - a.x) *(c.y - a.y) -(b.y - a.y) *(c.x - a.x))>0.0f;
    }
​
    public static Vector2 PerpVectL(Vector2 a, Vector2 b) {
        /*Vector3 v0 = ToV3(a);
        Vector3 v1 = ToV3(b);
        Vector3 v3 = Vector3.back;*/
        return Vector3.Cross(((Vector3)b - (Vector3)a), Vector3.back).normalized;
    }
​
    public static Vector2 PerpVectR(Vector2 a, Vector2 b) {
        return Vector3.Cross(((Vector3)b - (Vector3)a), Vector3.forward).normalized;
    }
    public static Vector3 PerpVect(Vector2 v, bool right) {
        if(right) {
            return Vector3.Cross(v, Vector3.forward).normalized;
        }
        else {
            return Vector3.Cross(v, Vector3.forward).normalized;
        }
    }
​
    public static Vector2 PerpVect(Vector2 a, Vector2 b, bool right) {
        if(right) {
            return(PerpVectR(a, b));
        }
        else {
            return(PerpVectL(a, b));
        }
    }
​
    /*static Vector2 PerpVect(Vector2 v0, Vector2 v1, Vector2 myPos) {
        if(v0.x == v1.x) {
            if(myPos.x>v0.x) {
                return new Vector2(-1, 0);
            }
            else {
                return new Vector2(1, 0);
            }
        }
        else if(v0.y == v1.y) {
            if(myPos.y>v0.y) {
                return new Vector2(0, - 1);
            }
            else {
                return new Vector2(0, 1);
            }
        }
​
        tmp =(v0 - v1).normalized;
        slope = tmp.y / tmp.x;
        nrecip = -(1 / slope);
        y1 = nrecip;
        tmp = new Vector2(1, y1).normalized;
        if(IsLeft(v0, v1, myPos) == IsLeft(v0, v1, v0 + tmp)) {
            return new Vector2(tmp.x * - 1, tmp.y * - 1);
        }
        else {
            return tmp;
        }
    }*/
​
    public static Vector3 SideMost(Vector3[] vts, Vector2 viewPos, Vector2 myPos, bool right) {
        float ang = Geo.ToAng(myPos - viewPos);
        int i = 0;
        int l = 0;
        float tmp = 0;
        Vector3 pt = Vector3.zero;
        if(right) {
            float num = - 999;
            i = 0;
            l = vts.Length;
            while(i< l) {
                Vector3 vt = vts[i];
                tmp = Mathf.DeltaAngle(ang, Geo.ToAng((Vector2)vt - viewPos));
                if(tmp>num) {
                    pt = vt;
                    num = tmp; // Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos))
                }
                i++;
            }
        }
        else {
            float num = 999;
            l = vts.Length;
            i = 0;
            while(i< l) {
                Vector3 vt = vts[i];
                //for vt in vts:
                tmp = Mathf.DeltaAngle(ang, Geo.ToAng((Vector2)vt - viewPos));
                if(tmp< num) {
                    pt = vt;
                    num = tmp; // Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos))
                }
                i++;
            }
        }
        return pt;
    }
​
    public static Vector3 SideMost(MeshFilter msh, Vector2 viewPos, Vector2 myPos, bool right) {
        return SideMost(msh.mesh.vertices, viewPos, myPos, right);
        /*float ang = Geo.ToAng(myPos - viewPos);
        int i = 0;
        int l = 0;
        float tmp = 0;
        vts = msh.mesh.vertices;
        if(right) {
            float num = - 999;
            i = 0;
            l = vts.Length;
            while(i< l) {
                //for vt in vts:
                vt = msh.transform.TransformPoint(vts[i]);
                tmp = Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos));
                if(tmp>num) {
                    pt = vt;
                    num = tmp; // Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos))
                }
                i++;
            }
        }
        else {
            num = 999;
            l = vts.Length;
            i = 0;
            while(i< l) {
                vt = msh.transform.TransformPoint(vts[i]);
                //for vt in vts:
                tmp = Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos));
                if(tmp< num) {
                    pt = vt;
                    num = tmp; // Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos))
                }
                i++;
            }
        }
        return pt;*/
    }
​
    public static Vector2 SideMost(Vector2[] vts, Vector2 viewPos, Vector2 myPos, bool right) {
        float ang = Geo.ToAng(myPos - viewPos);
        Vector2 pt = Vector2.zero;
        if(right) {
            float num = - 999;
            foreach(Vector2 vt in vts) {
                if(Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos))>num) {
                    pt = vt;
                    num = Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos));
                }
            }
        }
        else {
            float num = 999;
            foreach(var vt in vts) {
                if(Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos))< num) {
                    pt = vt;
                    num = Mathf.DeltaAngle(ang, Geo.ToAng(vt - viewPos));
                }
            }
        }
        return pt;
    }
​
    public static Vector2 ClosestPoint(Vector2 v0, Vector2 v1, Vector2 v2) {
        if(v0.x == v1.x) {
            return new Vector2(v0.x, v2.y);
        }
        else if (v0.y == v1.y) {
            return new Vector2(v2.x, v0.y);
        }
        Vector2 v02 = v2 - v0;
        Vector2 v01 = v1 - v0;
        float v01sq =(v01.x * v01.x) +(v01.y * v01.y);
​
        float v02dotv01 = Vector2.Dot(v02, v01);
​
        float t = v02dotv01 / v01sq;
​
        return new Vector2(v0.x +(v01.x * t), v0.y +(v01.y * t));
    }
​
​
​
    public static Vector2 LineLineIntersection(Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2) {
​
        Vector3 intersection = Vector3.zero;
​
        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);
​
        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);
​
        //Lines are not coplanar. Take into account rounding errors.
        if((planarFactor>= 0.00001f) ||(planarFactor<= - 0.00001f)) {
            Debug.Log("NOT COPLANAR");
            return Vector2.zero;
        }
​
​
        //Note: sqrMagnitude does x*x+y*y+z*z on the input vector.
        float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
        return linePoint1 + (lineVec1 * s);
    }
}
