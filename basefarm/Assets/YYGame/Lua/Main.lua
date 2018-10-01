--主入口函数。从这里开始lua逻辑

require "util/util"
require "hotfix/HotfixMain"
require "config/rescourcesmsg"

Main = {};
local this = Main;
function Main.Start()					
	print("=============== Main.Start()	================>>>") 	
	HotfixMain.Init()	
end


function Main.Start2(args)					
	print("=============Main.Start2==================>>>",args[0],args[1]) 		
end

function Main.CreateUI( ... )
	local loadmgr = CS.YY.LoadManager.GetLoadManagerInstance()
	local obj = CS.UnityEngine.GameObject.Instantiate(loadmgr:Load("DemoUI","UI"))

end