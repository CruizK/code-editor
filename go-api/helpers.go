package main

import (
	"archive/tar"
	"bytes"
	"context"
	"io"
	"os"

	"github.com/docker/docker/api/types"
	"github.com/docker/docker/api/types/container"
	"github.com/docker/docker/client"
	"github.com/docker/docker/pkg/stdcopy"
)

var cli *client.Client

func init() {
	cli, _ = client.NewClientWithOpts(client.FromEnv, client.WithAPIVersionNegotiation())
}

func RunCodeIsolated(language Language, code string) (string, error) {
	ctx := context.Background()

	out, err := cli.ImagePull(ctx, language.Image, types.ImagePullOptions{})

	if err != nil {
		return "", err
	}

	defer out.Close()

	io.Copy(os.Stdout, out)

	resp, err := cli.ContainerCreate(ctx, &container.Config{
		Image: language.Image,
		Cmd:   []string{"sh", "-c", language.CompileCommand + " && " + language.RunCommand},
		Tty:   false,
	}, nil, nil, nil, "")

	if err != nil {
		return "", err
	}

	var buf bytes.Buffer
	tw := tar.NewWriter(&buf)
	err = tw.WriteHeader(&tar.Header{
		Name: language.SourceFile,
		Mode: 0777,
		Size: int64(len(code)),
	})

	tw.Write([]byte(code))
	tw.Close()

	cli.CopyToContainer(ctx, resp.ID, "/", &buf, types.CopyToContainerOptions{})

	if err != nil {
		return "", err
	}

	if err := cli.ContainerStart(ctx, resp.ID, types.ContainerStartOptions{}); err != nil {
		return "", err
	}

	statusCh, errCh := cli.ContainerWait(ctx, resp.ID, container.WaitConditionNotRunning)
	select {
	case err := <-errCh:
		if err != nil {
			return "", err
		}
	case <-statusCh:
	}

	out, err = cli.ContainerLogs(ctx, resp.ID, types.ContainerLogsOptions{ShowStdout: true})
	if err != nil {
		return "", err
	}

	stdcopy.StdCopy(os.Stdout, os.Stderr, out)

	//_ = cli.ContainerRemove(ctx, resp.ID, types.ContainerRemoveOptions{})

	return "", nil

}
