/* groovylint-disable DuplicateStringLiteral, NestedBlockDepth */
/* groovylint-disable-next-line CompileStatic */
pipeline {
  agent none
  stages {
    stage('UI Test') {
      agent {
        docker { image 'node:14-alpine' }
      }
      stages {
        stage('Install dependencies') {
          steps {
            dir('app') {
              sh 'npm install'
            }
          }
        }
        stage('Test') {
          steps {
            dir('app') {
              sh 'npm test'
            }
          }
        }
      }
    }
    stage('UI Deploy') {
      agent any
      when {
        beforeAgent true
        branch 'main'
      }
      stages {
        stage('Build Image') {
          steps {
            sh 'docker build -t code-editorv1 app'
          }
        }
      }
    }
  }
}