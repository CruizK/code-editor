package server

import "github.com/gin-gonic/gin"

func RunServer() {
	r := gin.Default()

	r.POST("/compile", CompileHandler)

	for _, v := range LanguageMap {
		err := PullLanguage(v)

		if err != nil {
			panic(err)
		}
	}

	r.Run("0.0.0.0:8081")
}
