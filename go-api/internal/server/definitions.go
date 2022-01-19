package server

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
