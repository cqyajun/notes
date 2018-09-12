package util

import "github.com/astaxie/beego/logs"

func Init()  {
	log := logs.NewLogger()
	log.SetLogger(logs.AdapterFile,`{"filename":"logs/test.log"}`)

}

func Logs(msg string)  {
	logs.Debug(msg)
}