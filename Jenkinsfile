/* groovylint-disable DuplicateStringLiteral, NestedBlockDepth */
/* groovylint-disable-next-line CompileStatic */
pipeline {
  agent none
  environment {
    DOTNET_CLI_HOME = '/tmp/DOTNET_CLI_HOME'
  }
  stages {
    stage('Run Tests') {
      parallel {
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
        stage('Api Test') {
          agent {
            docker { image 'mcr.microsoft.com/dotnet/sdk:5.0' }
          }
          stages {
            stage('Restore Dependencies') {
              steps {
                dir('api/CodeEditorApi') {
                  sh 'dotnet restore'
                }
              }
            }
            stage('Build & Test') {
              steps {
                dir('api/CodeEditorApi') {
                  sh 'dotnet build'
                  sh 'dotnet test --logger:trx'
                }
              }
            }
          }
        }
      }
    }
    stage('Deployment') {
      agent any
      when {
        beforeAgent true
        branch 'main'
      }
      failFast true
      stages {
        stage('Docker Image Building') {
          parallel {
            stage('Build & Send UI Image') {
              steps {
                dir('app') {
                  sh 'docker build -t code-editor-ui .'
                  sh 'docker save -o code-editor-ui.tar code-editor-ui'
                  sshagent(['ssh-for-staging']) {
                    sh 'scp code-editor-ui.tar cruizk@192.168.0.16:/home/cruizk'
                  }
                }
              }
            }
            stage('Build & Send Api Image') {
              steps {

                dir('api') {
                  sh 'docker build -f Dockerfile.web -t code-editor-api .'
                  sh 'docker save -o code-editor-api.tar code-editor-api'
                  sshagent(['ssh-for-staging']) {
                    sh 'scp code-editor-api.tar cruizk@192.168.0.16:/home/cruizk'
                  }
                }
              }
            }
            stage('Build & Send Db Image') {
              steps {
                dir('api') {
                  sh 'docker build -f Dockerfile.db -t code-editor-db .'
                  sh 'docker save -o code-editor-db.tar code-editor-db'
                  sshagent(['ssh-for-staging']) {
                    sh 'scp code-editor-db.tar cruizk@192.168.0.16:/home/cruizk'
                  }
                }
              }
            }
          }
        }
        stage('Deploy') {
          steps {
            sshagent(['ssh-for-staging']) {
              sh 'cat scripts/deployStaging.sh | ssh cruizk@192.168.0.16 /bin/bash'
            }
          }
        }
      }
    }
  }
}
