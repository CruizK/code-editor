package server

import (
	"archive/tar"
	"bytes"
	"context"
	"io/ioutil"
	"time"

	"github.com/docker/docker/api/types"
	"github.com/docker/docker/api/types/container"
	"github.com/docker/docker/client"
)

var cli *client.Client

func init() {
	cli, _ = client.NewClientWithOpts(client.FromEnv, client.WithAPIVersionNegotiation())
}

func CreateContainer(language Language) (string, error) {
	ctx := context.Background()

	resp, err := cli.ContainerCreate(ctx, &container.Config{
		Image: language.Image,
		Cmd:   []string{"sh", "-c", language.CompileCommand + " && " + language.RunCommand},
		Tty:   false,
	}, nil, nil, nil, "")

	if err != nil {
		return "", err
	}

	return resp.ID, nil
}

func RemoveContainer(containerId string) error {
	ctx := context.Background()

	err := cli.ContainerRemove(ctx, containerId, types.ContainerRemoveOptions{})

	return err
}

func RunCodeIsolated(language Language, containerId string, code string) (string, error) {
	ctx := context.Background()

	now := time.Now()

	var buf bytes.Buffer
	tw := tar.NewWriter(&buf)
	err := tw.WriteHeader(&tar.Header{
		Name: language.SourceFile,
		Mode: 0777,
		Size: int64(len(code)),
	})

	tw.Write([]byte(code))
	tw.Close()

	cli.CopyToContainer(ctx, containerId, "/", &buf, types.CopyToContainerOptions{})

	if err != nil {
		return "", err
	}

	if err := cli.ContainerStart(ctx, containerId, types.ContainerStartOptions{}); err != nil {
		return "", err
	}

	statusCh, errCh := cli.ContainerWait(ctx, containerId, container.WaitConditionNotRunning)
	select {
	case err := <-errCh:
		if err != nil {
			return "", err
		}
	case <-statusCh:
	}

	after := time.Now()

	reader, err := cli.ContainerLogs(ctx, containerId, types.ContainerLogsOptions{
		ShowStdout: true,
		Since:      after.Sub(now).String(),
	})
	if err != nil {
		return "", err
	}

	defer reader.Close()

	// Skip past 8 byte docker log header
	p := make([]byte, 8)
	reader.Read(p)

	// Read Rest
	content, err := ioutil.ReadAll(reader)

	if err != nil {
		return "", err
	}

	return string(content), nil

}
