package main

import (
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

	_, err = RunCodeIsolated(CSharp, body.Code)

	if err != nil {
		c.AbortWithError(http.StatusBadRequest, err)
	}

}
