package server

import "github.com/gin-gonic/gin"

func RunServer() {
	r := gin.Default()

	r.POST("/compile", CompileHandler)

	r.Run("127.0.0.1:8080")
}
