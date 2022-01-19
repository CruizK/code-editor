package server

import (
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
)

type CompileBody struct {
	Language string `json:"language"`
	Code     string `json:"code"`
}

func CompileHandler(c *gin.Context) {
	body := CompileBody{}

	err := c.BindJSON(&body)

	if err != nil {
		c.AbortWithError(http.StatusBadRequest, err)
		return
	}

	out, err := RunCodeIsolated(CSharp, "nifty_khayyam", body.Code)

	if err != nil {
		c.AbortWithError(http.StatusBadRequest, err)
	}

	fmt.Print(out)

	c.JSON(200, gin.H{
		"output": out,
	})
}
