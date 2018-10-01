using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[CSharpCallLua]
public delegate int TestOutDelegate(HotfixCalc calc, int a, out double b, ref string c);
[LuaCallCSharp]
public enum ETest
{
    T1,
    T2,
    T3
}

[Hotfix]
public class HotfixCalc
{

    public int xxx = 100;

    public int Add(int a, int b)
    {
        return a - b;
    }

    public Vector3 Add(Vector3 a, Vector3 b)
    {
        return a - b;
    }

    public int TestOut(int a, out double b, ref string c)
    {
        b = a + 2;
        c = "wrong version";
        return a + 3;
    }

    public int TestOut(int a, out double b, ref string c, GameObject go)
    {
        return TestOut(a, out b, ref c);
    }

    public T Test1<T>()
    {
        return default(T);
    }

    public T1 Test2<T1, T2, T3>(T1 a, out T2 b, ref T3 c)
    {
        b = default(T2);
        return a;
    }

    public static int Test3<T>(T a)
    {
        return 0;
    }

    public static void Test4<T>(T a)
    {
    }

    public void Test5<T>(int a, params T[] arg)
    {

    }
}


public class NoHotfixCalc
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

public class HotFixDemo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LuaEnv luaenv = new LuaEnv();
        HotfixCalc calc = new HotfixCalc();
        NoHotfixCalc ordinaryCalc = new NoHotfixCalc();

        int CALL_TIME = 100 * 1000 * 1000;
        var start = System.DateTime.Now;
        for (int i = 0; i < CALL_TIME; i++)
        {
            calc.Add(2, 1);
        }
        var d1 = (System.DateTime.Now - start).TotalMilliseconds;
        Debug.Log("Hotfix using:" + d1);

        start = System.DateTime.Now;
        for (int i = 0; i < CALL_TIME; i++)
        {
            ordinaryCalc.Add(2, 1);
        }

        var d2 = (System.DateTime.Now - start).TotalMilliseconds;
        Debug.Log("No Hotfix using:" + d2);

        Debug.Log("drop:" + ((d1 - d2) / d1));

        Debug.Log("Before Fix: 2 + 1 = " + calc.Add(2, 1));

        Debug.Log("Before Fix: Vector3(2, 3, 4) + Vector3(1, 2, 3) = " + calc.Add(new Vector3(2, 3, 4), new Vector3(1, 2, 3)));

        luaenv.DoString(@"
            xlua.hotfix(CS.HotfixCalc, 'Add', function(self, a, b)
                ETest.
                return a + b
            end)
        ");

        Debug.Log("After Fix: 2 + 1 = " + calc.Add(2, 1));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void sigin()
    {
        //SignatureLoader sl = new SignatureLoader("BgIAAACkAABSU0ExAAQAAAEAAQAHqG5xim7ZmqCsyXLW8WvwlOZJQsKJOP3Yn7vooFvNMxAX0O5yd4Wrgq0n8A7O5Yqf06Kp8ZhX/O2Ld+AqRcEspc9R9RkJZPcoJAqCQqVzNdCGdya33sxZU0ovuzCVGbll5q2eiiEY9bzFtoR0mhsjhitX5gfJxXjPcsohH94I0Q==", LoadLuaFile);
    }
}
