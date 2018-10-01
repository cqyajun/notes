using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {

    XLua.LuaEnv luaenv;

   [XLua.CSharpCallLua]
    public delegate double LuaMax(double a, double b);
    // Use this for initialization

    private DateTime curTime;
    
    

    void Start () {
        luaenv = new XLua.LuaEnv();
        luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
     //   luaenv.Dispose();


        var max = luaenv.Global.GetInPath<LuaMax>("math.max");

        Debug.Log("max:" + max(32, 12));

        Debug.Log("max:" + max(32, 100));
        Debug.Log("max:" + max(320, 12));

        curTime = DateTime.Now;

        StartCoroutine(SUpdate());
    }


  
    // Update is called once per frame
    IEnumerator  SUpdate () {

        while (true)
        {
            int second = (DateTime.Now - curTime).Seconds;
            Debug.Log(second);
            curTime = DateTime.Now;
            yield return new WaitForSeconds(1);
        }
       
        
    }
}
