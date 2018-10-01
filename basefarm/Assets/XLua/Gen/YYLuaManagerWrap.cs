#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class YYLuaManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YY.LuaManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DoString", _m_DoString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CallFunction", _m_CallFunction);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLuaTable", _m_GetLuaTable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "isReady", _g_get_isReady);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "isReady", _s_set_isReady);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					YY.LuaManager gen_ret = new YY.LuaManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to YY.LuaManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YY.LuaManager gen_to_be_invoked = (YY.LuaManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _luaString = LuaAPI.lua_tostring(L, 2);
                    
                        object[] gen_ret = gen_to_be_invoked.DoString( _luaString );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CallFunction(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YY.LuaManager gen_to_be_invoked = (YY.LuaManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _funcName = LuaAPI.lua_tostring(L, 2);
                    object[] _args = translator.GetParams<object>(L, 3);
                    
                    gen_to_be_invoked.CallFunction( _funcName, _args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLuaTable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YY.LuaManager gen_to_be_invoked = (YY.LuaManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _tableName = LuaAPI.lua_tostring(L, 2);
                    
                        XLua.LuaTable gen_ret = gen_to_be_invoked.GetLuaTable( _tableName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YY.LuaManager gen_to_be_invoked = (YY.LuaManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isReady(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YY.LuaManager gen_to_be_invoked = (YY.LuaManager)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isReady);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isReady(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YY.LuaManager gen_to_be_invoked = (YY.LuaManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isReady = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
