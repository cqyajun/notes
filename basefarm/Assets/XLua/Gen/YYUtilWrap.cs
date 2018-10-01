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
    public class YYUtilWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YY.Util);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 7, 2, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "md5", _m_md5_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearChild", _m_ClearChild_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AppContentPath", _m_AppContentPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogError", _m_LogError_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Decryption", _m_Decryption_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "packXor", _m_packXor_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "NetAvailable", _g_get_NetAvailable);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DataPath", _g_get_DataPath);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					YY.Util gen_ret = new YY.Util();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to YY.Util constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_md5_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _source = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = YY.Util.md5( _source );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearChild_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _go = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    
                    YY.Util.ClearChild( _go );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppContentPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = YY.Util.AppContentPath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogError_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _v = LuaAPI.lua_tostring(L, 1);
                    
                    YY.Util.LogError( _v );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Decryption_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _data = LuaAPI.lua_tobytes(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        byte[] gen_ret = YY.Util.Decryption( _data, _key );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_packXor_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] __data = LuaAPI.lua_tobytes(L, 1);
                    int __len = LuaAPI.xlua_tointeger(L, 2);
                    string __pstr = LuaAPI.lua_tostring(L, 3);
                    
                    YY.Util.packXor( __data, __len, __pstr );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetAvailable(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, YY.Util.NetAvailable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DataPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, YY.Util.DataPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
