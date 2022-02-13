package server

import (
	"net/http"

	"github.com/gin-gonic/gin"
)

type CompileBody struct {
	Language string `json:"language"`
	Code     string `json:"code"`
	UserID   int    `json:"userId"`
}

// Replace with redis later
var containerToUserMap map[int]string

func CompileHandler(c *gin.Context) {
	body := CompileBody{}

	err := c.BindJSON(&body)

	if err != nil {
		c.AbortWithError(http.StatusBadRequest, err)
		return
	}

	containerId, err := CreateContainer(CSharp)

	if err != nil {
		c.AbortWithError(http.StatusBadRequest, err)
	}

	out, err := RunCodeIsolated(CSharp, containerId, body.Code)

	if err != nil {
		c.AbortWithError(http.StatusBadRequest, err)
	}

	err = RemoveContainer(containerId)

	if err != nil {
		c.AbortWithError(http.StatusBadRequest, err)
	}

	c.JSON(200, gin.H{
		"output": out,
	})
}
