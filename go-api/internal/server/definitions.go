package server

type Language struct {
	Name           string
	Image          string
	SourceFile     string
	CompileCommand string
	RunCommand     string
	IsCompiled     bool
}

var CSharp = Language{
	Name:           "CSharp",
	Image:          "mono:6.12.0",
	SourceFile:     "program.cs",
	CompileCommand: "mcs program.cs",
	RunCommand:     "mono program.exe",
	IsCompiled:     true,
}

var Python = Language{
	Name:           "Python 3",
	Image:          "python:3.10-alpine3.15",
	SourceFile:     "main.py",
	CompileCommand: "",
	RunCommand:     "python main.py",
	IsCompiled:     false,
}

var Java = Language{
	Name:           "Java",
	Image:          "openjdk:19-slim-buster",
	SourceFile:     "Main.java",
	CompileCommand: "javac Main.java",
	RunCommand:     "java Main",
	IsCompiled:     true,
}

var LanguageMap = map[string]Language{
	"CSharp": CSharp,
	"Python": Python,
	"Java":   Java,
}
