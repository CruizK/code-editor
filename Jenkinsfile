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
        stage ('Build Image') {
          steps {
            dir('app') {
              sh 'docker build -t code-editor-ui .'
            }
          }
        }
        stage ('Save Image') {
          steps {
            sh 'docker save -o code-editor-ui.tar code-editor-ui'
          }
        }
        stage('Deploy') {
          steps {
            sshagent(['ssh-for-staging']) {
              sh 'scp code-editor-ui.tar cruizk@192.168.0.16:/home/cruizk'
              sh 'cat scripts/deployStaging.sh | ssh cruizk@192.168.0.16 /bin/bash'
            }
          }
        }
      }
    }
  }
}
