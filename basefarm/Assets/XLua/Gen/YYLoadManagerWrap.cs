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
    public class YYLoadManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YY.LoadManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Load", _m_Load);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadAsync", _m_LoadAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnLoadGroup", _m_UnLoadGroup);
			
			
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
					
					YY.LoadManager gen_ret = new YY.LoadManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to YY.LoadManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YY.LoadManager gen_to_be_invoked = (YY.LoadManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.Init(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Load(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YY.LoadManager gen_to_be_invoked = (YY.LoadManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Type>(L, 4)) 
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    string _group = LuaAPI.lua_tostring(L, 3);
                    System.Type _type = (System.Type)translator.GetObject(L, 4, typeof(System.Type));
                    
                        UnityEngine.Object gen_ret = gen_to_be_invoked.Load( _resName, _group, _type );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    string _group = LuaAPI.lua_tostring(L, 3);
                    
                        UnityEngine.Object gen_ret = gen_to_be_invoked.Load( _resName, _group );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YY.LoadManager.Load!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YY.LoadManager gen_to_be_invoked = (YY.LoadManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Type>(L, 4)&& translator.Assignable<System.Action<UnityEngine.Object>>(L, 5)) 
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    string _group = LuaAPI.lua_tostring(L, 3);
                    System.Type _type = (System.Type)translator.GetObject(L, 4, typeof(System.Type));
                    System.Action<UnityEngine.Object> _callback = translator.GetDelegate<System.Action<UnityEngine.Object>>(L, 5);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.LoadAsync( _resName, _group, _type, _callback );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Type>(L, 4)) 
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    string _group = LuaAPI.lua_tostring(L, 3);
                    System.Type _type = (System.Type)translator.GetObject(L, 4, typeof(System.Type));
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.LoadAsync( _resName, _group, _type );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    string _group = LuaAPI.lua_tostring(L, 3);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.LoadAsync( _resName, _group );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YY.LoadManager.LoadAsync!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnLoadGroup(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YY.LoadManager gen_to_be_invoked = (YY.LoadManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _group = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.UnLoadGroup( _group );
                    
                    
                    
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
			
                YY.LoadManager gen_to_be_invoked = (YY.LoadManager)translator.FastGetCSObj(L, 1);
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
			
                YY.LoadManager gen_to_be_invoked = (YY.LoadManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isReady = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
