package server

import (
	"errors"
	"fmt"
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

	fmt.Println(body.Language)

	if err != nil {
		c.AbortWithError(http.StatusBadRequest, err)
		return
	}

	if val, ok := LanguageMap[body.Language]; ok {

		containerId, err := CreateContainer(val)

		if err != nil {
			c.AbortWithError(http.StatusBadRequest, err)
		}

		out, err := RunCodeIsolated(val, containerId, body.Code)

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
	} else {
		c.AbortWithError(http.StatusBadRequest, errors.New("Invalid Language"))
	}
}
