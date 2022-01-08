package main

var LangToImage = map[string]string{
	"C#": "something",
}

var LangToCommand = map[string]string{
	"C#": "dotnet run x",
}

type Language struct {
	Name           string
	Image          string
	SourceFile     string
	CompileCommand string
	RunCommand     string
}

var CSharp = Language{
	Name:           "C#",
	Image:          "mono",
	SourceFile:     "program.cs",
	CompileCommand: "mcs program.cs",
	RunCommand:     "mono program.exe",
}
